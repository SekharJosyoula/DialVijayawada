using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DV.TeleCallerHelper.Common
{
    public class TabbedViewModelBase : ViewModelBase
    {
        private string _viewTitle;

        public string ViewTitle
        {
            get
            {
                return _viewTitle;
            }
            set
            {
                this._viewTitle = value;
                this.RaisePropertyChanged("ViewTitle");
            }
        }
    }
}
