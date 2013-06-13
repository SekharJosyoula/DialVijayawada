using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DV.DataProvider
{
    public class SMSGateWayProvider : ISMSProvider
    {

        public bool SendSMS(string message, string phoneNumber)
        {
            var url = string.Format(@"http://www.smsintegra.com/smsweb/desktop_sms/desktopsms.asp?uid={0}&pwd={1}&mobile={2}&msg={3}&sid={4}&dtNow={5}", "udaykvv", "pwd", phoneNumber, message, "SmsIntegra", DateTime.Now.ToShortDateString());
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream responseStream = response.GetResponseStream();
            StreamReader readStream = new StreamReader(responseStream, Encoding.UTF8);
            string strSMSResponseString = readStream.ReadToEnd();
            //TODO: write code to send SMS
            return true;
        }
    }
}
