using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DV.DataProvider;
using DV.Manager.Interfaces;

namespace DV.Manager
{
    public class SearchCaller
    {
        public Caller GetCaller(string phoneNumber)
        {
            var data = new dvradiusEntities();
            return data.Callers.FirstOrDefault(caller => caller.PhoneNumber.Contains(phoneNumber));
        }
    }
}
