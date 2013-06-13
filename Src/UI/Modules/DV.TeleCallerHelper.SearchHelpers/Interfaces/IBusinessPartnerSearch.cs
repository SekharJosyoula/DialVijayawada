using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DV.TeleCallerHelper.SearchHelpers.Interfaces
{
    public interface IBusinessPartnerSearch
    {
        /// <summary>
        /// Makes a server call to retrieve the business units
        /// </summary>
        void ExecuteSearchBusinessUnits();
    }
}
