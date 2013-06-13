using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DV.TeleCallerHelper.SearchHelpers.Interfaces
{
    public interface ISearchMainContainer
    {
        /// <summary>
        /// Picks all business partners whose SendSms property is true and submits to server for sending sms
        /// </summary>
        void ExecuteSaveCallerAndSendSms();
    }
}