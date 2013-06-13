using DV.TeleCallerHelper.Shell.ViewModels;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Windows.Controls.Ribbon;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;
using System.Windows.Media;
using Xceed.Wpf.Toolkit;

namespace DV.TeleCallerHelper.Shell.Views
{
    /// <summary>
    /// Interaction logic for ShellWindow.xaml
    /// </summary>
    public partial class ShellWindow : RibbonWindow
    {
        public ShellWindow()
        {
            InitializeComponent();
            // TODO: to be moved into XAML
            this.DataContext = new ShellViewModel();
        }
    }

    public class CloseTabbedViewAction : TriggerAction<FrameworkElement>
    {
        protected override void Invoke(object parameter)
        {
            RoutedEventArgs args = parameter as RoutedEventArgs;
            if (args == null) return;

            // Find the parent tab item that contains the view to remove.
            TabItem tabItem = FindVisualParent<TabItem>(args.OriginalSource as DependencyObject);

            // Find the parent tab control that represents the region.
            TabControl tabControl = FindVisualParent<TabControl>(tabItem);

            if (tabControl != null && tabItem != null)
            {
                // Get the view.
                object view = tabItem.Content;

                // Get the region associated with the tab control.
                IRegion region = RegionManager.GetObservableRegion(tabControl).Value;
                if (region != null)
                {
                    region.Remove(view);
                }
            }
        }

        private T FindVisualParent<T>(DependencyObject node) where T : DependencyObject
        {
            DependencyObject parent = VisualTreeHelper.GetParent(node);
            if (parent == null || parent is T) return (T)parent;

            // Recurse up the visual tree.
            return FindVisualParent<T>(parent);
        }

        private bool NotifyIfImplements<T>(object content, Action<T> action) where T : class
        {
            bool notified = false;

            // Get the implementor of the specified interface -
            // either the view or the view model.
            //T target = Implementor<T>(content);
            //if (target != null)
            //{
            //    action(target);
            //    notified = true;
            //}
            return notified;
        }
    }
}