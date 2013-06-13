using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Timers;

using DV.Manager;
using DV.DataProvider;
using DV.Manager.Request;
using SMSGateway;

namespace SMSService
{
    class SMSService
    {
        Timer timer = new Timer();

        public void Start()
        {
            TraceService("start service");
            timer.Elapsed += new ElapsedEventHandler(OnElapsedTime);

            //This statement is used to set interval to 1 minute (= 60,000 milliseconds)

            timer.Interval = 60000;

            //enabling the timer
            timer.Enabled = true;
        }

        public void Stop()
        {
            timer.Enabled = false;
            TraceService("stopping service");
        }

        private void OnElapsedTime(object source, ElapsedEventArgs e)
        {
            TraceService("Procee request started at " + DateTime.Now);
            ProcessRequest();
            TraceService("Procee request ended at " + DateTime.Now);
        }

        private void TraceService(string content)
        {
            //need t remove this method once we add log4net
            //set up a filestream
            FileStream fs = new FileStream(@"d:\dvsmsService.txt", FileMode.OpenOrCreate, FileAccess.Write);

            //set up a streamwriter for adding text
            StreamWriter sw = new StreamWriter(fs);

            //find the end of the underlying filestream
            sw.BaseStream.Seek(0, SeekOrigin.End);

            //add the text
            sw.WriteLine(content);
            //add the text to the underlying filestream

            sw.Flush();
            //close the writer
            sw.Close();
        }
        private void ProcessRequest()
        {
            var SMSQueueItems = GetSMSQuesueItemsfromDB();

            if (SMSQueueItems != null && SMSQueueItems.Count > 0)
            {
                var smsgatewayresponse = SendSMS(SMSQueueItems);
                UpdateProcessedQItems(smsgatewayresponse.SMSQueueItems);
            }
        }

        private List<SMSQueue> GetSMSQuesueItemsfromDB()
        {
            using (var SMSQueueCommitManager = CommitManagerFactory.Create<SMSQueue>())
            {
                return SMSQueueCommitManager.GetEntites().Where(item => item.isProcessed == (bool?)false).ToList();
                
            }

        }

        private void UpdateProcessedQItems(List<SMSQueue> smsQueues)
        {
            using (var SMSQueueCommitManager = CommitManagerFactory.Create<SMSQueue>())
            {
                foreach (var item in smsQueues)
                {
                    SMSQueueCommitManager.Update(item);
                }
            }

        }

        private SMSGatewayResponse SendSMS(List<SMSQueue> Qitems)
        {
            SMSGatewayRequest request = new SMSGatewayRequest();
            SMSGatewayAPI smsgatewayAPI = new SMSGatewayAPI();

            request.SMSQueueItems = Qitems;
            var result = smsgatewayAPI.SendSMS(request);

            return result;
        }
    }
}
