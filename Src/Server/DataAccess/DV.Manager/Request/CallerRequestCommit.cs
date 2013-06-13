using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DV.DataProvider;

namespace DV.Manager.Request
{
    public class CallerRequestCommit
    {
        public Caller Caller { get; set; }
        public CallerRequestHistory CallerRequest{ get; set; }
        public IEnumerable<BusinessUnit> BusinessUnitsIdentifiedForCallerRequest { get; set; }

        public bool SendSmsToCaller { get; set; }
    }

    //TODO: Not sure why we shoould create this. But for now dont delete this. 
    // Names related to this flow are too bad. please come up with propernames
    public class CallerRequestResponse
    {

    }
}
