using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DV.TeleCallerHelper.Common.Events
{
    public class StatusbarEventArgs
    {
        private string _statusMsg;

        public string StatusMessage
        {
            get { return _statusMsg; }
        }

        private StatusMessageType _statMsgType;

        public StatusMessageType StatusType
        {
            get { return _statMsgType; }
        }

        public StatusbarEventArgs(string statMsg, StatusMessageType statMsgType)
        {
            this._statusMsg = statMsg;
            this._statMsgType = statMsgType;
        }
    }
}

namespace DV.TeleCallerHelper.Common
{
    public enum StatusMessageType
    { 
        None = 0,
        Info = 1,
        Warning = 2,
        Error = 3
    }
}