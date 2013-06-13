using DV.TeleCallerHelper.Common;
using DV.TeleCallerHelper.Common.Events;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DV.TeleCallerHelper.Shell.ViewModels
{
    public class ShellViewModel : ViewModelBase
    {
        private StatusbarViewModel _statBarVm;
        private TopInfoViewModel _topInfoVm;

        public StatusbarViewModel StatusbarViewModel
        {
            get 
            { 
                return this._statBarVm; 
            }
            set
            {
                this._statBarVm = value;
                this.RaisePropertyChanged("StatusbarViewModel");
            }
        }

        public TopInfoViewModel TopInfobarViewModel
        {
            get
            {
                return this._topInfoVm;
            }
            set
            {
                this._topInfoVm = value;
                this.RaisePropertyChanged("TopInfobarViewModel");
            }
        }

        private bool _showBusyIndicator;

        public bool ShowBusyIndicator
        {
            get { return _showBusyIndicator; }
            set
            {
                _showBusyIndicator = value;
                this.RaisePropertyChanged("ShowBusyIndicator");
            }
        }

        private string _busyIndicatorText;

        public string BusyIndicatorText
        {
            get { return _busyIndicatorText; }
            set
            {
                _busyIndicatorText = value;
                this.RaisePropertyChanged("BusyIndicatorText");
            }
        }
        

        public ShellViewModel()
        {
            this.StatusbarViewModel = new StatusbarViewModel();
            this.TopInfobarViewModel = new TopInfoViewModel();
            var eventAggr = ServiceLocator.Current.GetInstance<IEventAggregator>();
            eventAggr.GetEvent<ShowBusyCursorEvent>().Subscribe(ShowHideBusyCursor);
        }

        private void ShowHideBusyCursor(ShowBusyCursorEventArg obj)
        {
            this.ShowBusyIndicator = obj.ShowBusyCursor;
            this.BusyIndicatorText = obj.BusyCursorText;
        }
    }
}
