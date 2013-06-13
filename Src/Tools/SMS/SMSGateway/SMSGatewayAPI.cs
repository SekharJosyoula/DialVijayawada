using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DV.DataProvider;
using System.Net;
using System.IO;
using System.Configuration;

namespace SMSGateway
{
    public class SMSGatewayAPI
    {
        public SMSGatewayResponse SendSMS(SMSGatewayRequest smsgatewayrequest)
        {
            SMSGatewayResponse smsgatewayresponse = new SMSGatewayResponse();
            smsgatewayresponse.SMSQueueItems = new List<SMSQueue>();
            string SMSGatewayAPIURL = "http://www.smsintegra.com/smsweb/desktop_sms/desktopsms.asp?uid={0}&pwd={1}&mobile={2}&msg={3}&sid={4}&dtNow={5}";

            string uid = ConfigurationManager.AppSettings["SMSGatewayAPIUID"];
            string pwd = ConfigurationManager.AppSettings["SMSGatewayAPIPassword"];
            string sid = ConfigurationManager.AppSettings["SMSGatewayAPISendeID"];

            foreach (var QItem in smsgatewayrequest.SMSQueueItems)
            {
                int rescode = -1;
                string resmessage = string.Empty;

                string url = string.Format(SMSGatewayAPIURL, uid, pwd, QItem.RecipientMobileNumber, QItem.Message, sid, DateTime.Now);

                try
                {

                    string returnMessage = SendAPIRequest(url);

                    GetResponsecodeandMessage(returnMessage, out rescode, out resmessage);

                    UpdateQitem(QItem, rescode, resmessage);

                    smsgatewayresponse.SMSQueueItems.Add(QItem);

                }
                catch
                {                   
                    smsgatewayresponse.ReturnCode = -1;
                    smsgatewayresponse.ReturnMessage = "Exception occured";
 
                }
            }

            smsgatewayresponse.ReturnCode = 0;
            smsgatewayresponse.ReturnMessage = "Sucess";

            return smsgatewayresponse;
        }

        private string SendAPIRequest(string url)
        {
            string strSMSResponseString = string.Empty;

            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream responseStream = response.GetResponseStream();
                StreamReader readStream = new StreamReader(responseStream, Encoding.UTF8);
                strSMSResponseString = readStream.ReadToEnd();

            }
            catch 
            {
                throw;
            }

            return strSMSResponseString;
        }
        

        private void GetResponsecodeandMessage(string returnmessage,  out int rescode, out string resmessage)
        {
            string[] responsecodeandmessage = returnmessage.Split('-');

            rescode = Convert.ToInt32( responsecodeandmessage[0] );
            resmessage = responsecodeandmessage[1];

        }

        private void UpdateQitem(SMSQueue QItem, int rescode, string resmessage)
        {
            QItem.ReturnCode = rescode;
            QItem.ReturnMessage = resmessage;
            QItem.isSMSSent = (rescode == 100) ? true : false;
            QItem.ProcessedDateTime = DateTime.Now;
            QItem.isProcessed = true;
        }
    }
}
