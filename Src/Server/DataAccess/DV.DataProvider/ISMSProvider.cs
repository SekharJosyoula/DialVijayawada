using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DV.DataProvider
{
    public interface ISMSProvider
    {
        bool SendSMS(string message, string phoneNumber);
    }
}
