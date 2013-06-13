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
    public class StatusbarViewModel : ViewModelBase
    {
        private string _statusMessage;

        public string StatusMessage
        {
            get {
                return this._statusMessage;
            }
            set
            {
                this._statusMessage = value;
                this.RaisePropertyChanged("StatusMessage");
            }
        }

        private StatusMessageType _statMsgType;

        public StatusMessageType StatusMessageType
        {
            get { return _statMsgType; }
            set { _statMsgType = value;
            this.RaisePropertyChanged("StatusMessageType");
            }
        }

        public StatusbarViewModel()
        {
            var eventAggr = ServiceLocator.Current.GetInstance<IEventAggregator>();
            eventAggr.GetEvent<StatusbarEvent>().Subscribe(UpdateStatusbar);

            this.StatusMessageType = Common.StatusMessageType.Info;
            this.StatusMessage = "Ready...";
        }

        private void UpdateStatusbar(StatusbarEventArgs obj)
        {
            this.StatusMessage = obj.StatusMessage;
            this.StatusMessageType = obj.StatusType;
        }
    }
}
