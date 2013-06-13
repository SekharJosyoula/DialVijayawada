using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DV.TeleCallerHelper.SearchHelpers.Interfaces
{
    interface IBusinessPartner
    {
        bool SendSms
        {
            get;
            set;
        }

        string BusinessUnitName
        {
            get;
            set;
        }

        string Address1
        {
            get;
            set;
        }

        string Area
        {
            get;
            set;
        }
    }
}
