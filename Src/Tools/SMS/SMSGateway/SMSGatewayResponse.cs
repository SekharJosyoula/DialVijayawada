using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DV.DataProvider;

namespace SMSGateway
{
    public class SMSGatewayResponse
    {
        public List<SMSQueue> SMSQueueItems { get; set; }
        public int ReturnCode { get; set; }
        public string ReturnMessage { get; set; }
    }
}
