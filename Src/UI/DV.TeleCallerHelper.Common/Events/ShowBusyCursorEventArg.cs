using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DV.TeleCallerHelper.Common.Events
{
    public class ShowBusyCursorEventArg
    {
        private bool _showBusyCursor;
        private string _busyCursorText;

        public bool ShowBusyCursor
        {
            get { return this._showBusyCursor; }
        }

        public string BusyCursorText
        {
            get { return this._busyCursorText; }
        }

        public ShowBusyCursorEventArg(bool showBusyCursor, string busyCursorText)
        {
            this._busyCursorText = busyCursorText;
            this._showBusyCursor = showBusyCursor;
        }
    }
}
