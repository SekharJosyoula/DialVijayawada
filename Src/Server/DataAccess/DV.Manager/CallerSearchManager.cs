using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DV.DataProvider;
using DV.Manager.Interfaces;
using DV.Manager.Request;

namespace DV.Manager
{
    public class CallerSearchManager : IManager<CallerSearchRequest,Caller>
    {
        //TODO: Make return type as IEnumerable<Caller>
        public Caller Execute(CallerSearchRequest request)
        {
            using (var data = new dvradiusEntities())
            {
                return (from c in data.Callers.Include("Address").Include("Profession")
                        where c.PhoneNumber.Contains(request.PhoneNumber)
                        select c).FirstOrDefault();
            }
        }
    }
}
