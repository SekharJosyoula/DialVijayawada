using DV.TeleCallerHelper.Common.Events;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Diagnostics;

namespace DV.TeleCallerHelper.Shell
{
    /// <summary>
    /// Need to improve this class to show a present information in a better way and handle it.
    /// Whatever is done here is too crude..
    /// </summary>
    public static class GlobalExceptionHandler
    {

        public static void AddGlobalHandlers()
        {
            Application.Current.DispatcherUnhandledException += Current_DispatcherUnhandledException;
            Application.Current.Dispatcher.UnhandledException += Dispatcher_UnhandledException;

            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
        }

        static void Dispatcher_UnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            IEventAggregator eventAggregator = ServiceLocator.Current.GetInstance<IEventAggregator>();

            var statusBarEvent = eventAggregator.GetEvent<StatusbarEvent>();
            statusBarEvent.Publish(new StatusbarEventArgs(e.Exception.Message, Common.StatusMessageType.Error));
            Trace.TraceError(string.Format("Dispatcher_UnhandledException>>Error occured at {0} Error Message:{1}", DateTime.Now, e.Exception.ToString()));
            //MessageBox.Show(e.Exception.Message, "Unhandled Exception", MessageBoxButton.OK, MessageBoxImage.Error);
            e.Handled = true;
        }

        static void Current_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            IEventAggregator eventAggregator = ServiceLocator.Current.GetInstance<IEventAggregator>();

            var statusBarEvent = eventAggregator.GetEvent<StatusbarEvent>();
            statusBarEvent.Publish(new StatusbarEventArgs(e.Exception.Message, Common.StatusMessageType.Error));
            Trace.TraceError(string.Format("Current_DispatcherUnhandledException>>Error occured at {0} Error Message:{1}", DateTime.Now, e.Exception.ToString()));
            //MessageBox.Show(e.Exception.Message, "Unhandled Exception", MessageBoxButton.OK, MessageBoxImage.Error);
            e.Handled = true;
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = e.ExceptionObject as Exception;
            IEventAggregator eventAggregator = ServiceLocator.Current.GetInstance<IEventAggregator>();

            var statusBarEvent = eventAggregator.GetEvent<StatusbarEvent>();
            statusBarEvent.Publish(new StatusbarEventArgs(ex.Message, Common.StatusMessageType.Error));
            Trace.TraceError(string.Format("CurrentDomain_UnhandledException>>Error occured at {0} Error Message:{1}", DateTime.Now, ex.ToString()));
            MessageBox.Show(ex.Message, "Unhandled Exception", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}