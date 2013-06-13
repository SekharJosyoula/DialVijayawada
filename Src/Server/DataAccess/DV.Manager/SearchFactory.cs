using DV.DataProvider;
using DV.Manager.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DV.Manager
{
    public static class SearchFactory
    {
        public static ISearch<BusinessUnit> GetSearchFactory()
        {
            //TODO: Need to decide on which factor we will give prodct
            // Need to remove hardcoded BusinessUnits
            return new SearchBusinessUnits();
        }
    }
}
