using DV.DataProvider;
using DV.Manager.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DV.Manager.Request;

namespace DV.Manager
{
    public static class ManagerFactory
    {
        //TODO: why genrics cant help here?
        private static Dictionary<String, object> _SearchManagers;

        static ManagerFactory()
        {
            _SearchManagers = new Dictionary<string, object>();

            _SearchManagers.Add(typeof (BusinessUnitSearchRequest).ToString(), new BusinessUnitsSearchManager());
            _SearchManagers.Add(typeof(CallerSearchRequest).ToString(), new CallerSearchManager());
            _SearchManagers.Add(typeof(CallerRequestCommit).ToString(), new CallerRequestCommitManager());
        }

        public static IManager<request, response> Create<request, response>()
        {
            var searchManager = _SearchManagers[typeof(request).ToString()];
            if (searchManager == null)
            {
                throw new NotSupportedException(typeof(request).ToString());
            }
            else
            {
                return searchManager as IManager<request,response>;
            }
        }
    }
}
