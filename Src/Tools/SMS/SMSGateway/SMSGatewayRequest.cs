using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DV.DataProvider;

namespace SMSGateway
{
    public class SMSGatewayRequest
    {
        public List<SMSQueue> SMSQueueItems { get; set; }
    }
}
