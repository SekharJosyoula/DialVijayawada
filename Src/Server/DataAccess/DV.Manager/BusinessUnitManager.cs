using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DV.DataProvider;
using DV.Manager.Interfaces;
using DV.Manager.Request;

namespace DV.Manager
{
    //TODO: dont like this name. why this is different from others?
    public static class BusinessUnitManager
    {
        public static IEnumerable<string> GetBusinessUnitNames(string searchstring)
        {
            using (var dvrdbContext = new dvradiusEntities())
            {
                return dvrdbContext.GetBusinessUnitsAutoPopulate(searchstring).ToList();
            }
        }
    }
}
