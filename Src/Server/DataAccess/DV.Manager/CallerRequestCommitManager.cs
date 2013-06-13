using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using DV.DataProvider;
using DV.Manager.DBCommitManager;
using DV.Manager.Interfaces;
using DV.Manager.Request;

namespace DV.Manager
{
    public class CallerRequestCommitManager : IManager<CallerRequestCommit, CallerRequestResponse>
    {
        private readonly ISMSProvider _smsProvider;

        //TODO: will probably remove this and inject through unity later
        public CallerRequestCommitManager()
            : this(new SMSGateWayProvider())
        {

        }
        public CallerRequestCommitManager(ISMSProvider SMSprovider)
        {
            _smsProvider = SMSprovider;
        }

        public CallerRequestResponse Execute(CallerRequestCommit request)
        {
            if (request.Caller.Profession != null)
            {
                using (var x = CommitManagerFactory.Create<Profession>())
                {
                    request.Caller.Profession = x.InsertOrUpdate(request.Caller.Profession);
                    request.Caller.ProfessionID = request.Caller.Profession.ProfessionID;
                }
            }

            if (request.Caller.Address != null)
            {
                using (var x = CommitManagerFactory.Create<Address>())
                {
                    request.Caller.Address = x.InsertOrUpdate(request.Caller.Address);
                    request.Caller.AddressID = request.Caller.Address.AddressID;
                }
            }

            using (var x = CommitManagerFactory.Create<Caller>())
            {
                request.Caller = x.InsertOrUpdate(request.Caller);
            }

            request.CallerRequest.CallerID = request.Caller.CallerID;
            request.CallerRequest.CallDateTime = DateTime.Now.GetIndianTIme();

            using (var x = CommitManagerFactory.Create<CallerRequestHistory>())
            {
                request.CallerRequest = x.Insert(request.CallerRequest);
            }

            // TODO: keep them in transactions think about SMS as well
            //TODO: if caller called from land line and BU he requested for is also having landline then looks like we dont have to insert data into SMSHistory. In that case we will loose track for that user what informationwe updated to him
            CommitCallerSMSHistory(request);

            return new CallerRequestResponse();
        }

        private void CommitCallerSMSHistory(CallerRequestCommit callerRequestCommit)
        {
            string MessageforCaller = @"Hi {0}\\r\\n{1}";
            string MessageforBU = @"Hi {0}\\r\\n{1}\\r\\nis enquired for you, please contact him immediately. ";
            string Message_Callerinfo = @"Client Name: {0}\\r\\nClient Number: {1}";

            bool sentSMSToBizUnit;

            StringBuilder msg_BUinfo = new StringBuilder();
            List<SMSHistory> smsHistories = new List<SMSHistory>();
            List<SMSQueue> smsQueues = new List<SMSQueue>();

            string msg_Callerinfo = string.Format(Message_Callerinfo, callerRequestCommit.Caller.FirstName, callerRequestCommit.Caller.PhoneNumber);

            foreach (var bizUnit in callerRequestCommit.BusinessUnitsIdentifiedForCallerRequest)
            {
                var dbbizUnit = CommitBizUnit(bizUnit);

                //TODO: Need to format this message
                msg_BUinfo.Append(FormatMessageforCaller(dbbizUnit.Name, dbbizUnit.Address.Area, dbbizUnit.ContactNumber ?? dbbizUnit.PrimaryContactNumber));

                string phoneNumebrToSendSMS = GetPhoneNumberToSendSMS(bizUnit);

                if (phoneNumebrToSendSMS != null)
                {
                    string SMSmessagetoBU = string.Format(MessageforBU, bizUnit.Name, msg_Callerinfo);

                    smsQueues.Add(
                                    new SMSQueue
                                    {
                                        RecipientMobileNumber = phoneNumebrToSendSMS,
                                        Message = SMSmessagetoBU,
                                        QueueDateTime = DateTime.Now
                                    }
                                 );

                    sentSMSToBizUnit = true;
                }
                else
                {
                    sentSMSToBizUnit = false;
                }


                smsHistories.Add(GetSMSHistory(bizUnit.BusinessUnitID,
                                                         callerRequestCommit.CallerRequest.CallerRequestHistoryID,
                                                         sentSMSToBizUnit, callerRequestCommit.Caller.CanSendSMS));
            }


            if (callerRequestCommit.Caller.CanSendSMS)
            {
                //TODO: Transactions?
                string SMSmessagetoCaller = string.Format(MessageforCaller, callerRequestCommit.Caller.FirstName, msg_BUinfo.ToString());
                smsQueues.Add(
                                new SMSQueue
                                {
                                    RecipientMobileNumber = callerRequestCommit.Caller.PhoneNumber,
                                    Message = SMSmessagetoCaller,
                                    QueueDateTime = DateTime.Now
                                }
                              );
            }

            using (var SMSQueueCommitManager = CommitManagerFactory.Create<SMSQueue>())
            {
                SMSQueueCommitManager.Insert(smsQueues);
            }

            using (var SMSHistoryCommitManager = CommitManagerFactory.Create<SMSHistory>())
            {
                SMSHistoryCommitManager.Insert(smsHistories);
            }
        }

        private static BusinessUnit CommitBizUnit(BusinessUnit bizUnit)
        {
            BusinessUnit dbbizUnit;
            using (var bizUnitCommitManager = CommitManagerFactory.Create<BusinessUnit>())
            {
                if (!bizUnitCommitManager.IsExists(bizUnit))
                {
                    if (bizUnit.Address != null)
                    {
                        using (var x = CommitManagerFactory.Create<Address>())
                        {
                            bizUnit.Address = x.InsertOrUpdate(bizUnit.Address);
                            bizUnit.AdressID = bizUnit.Address.AddressID;
                        }
                    }

                    dbbizUnit = bizUnitCommitManager.Insert(bizUnit);
                }
                else
                {
                    return bizUnit;
                }
            }

            if (!string.IsNullOrWhiteSpace(bizUnit.ContactNumber))
            {
                using (var businessUnitPhoneNumberManager = CommitManagerFactory.Create<BusinessUnitPhoneNumber>())
                {
                    string[] phoneNUmbers = bizUnit.ContactNumber.Split(',');
                    var bizUnitPhonenUmbers = phoneNUmbers.Select(ph => new BusinessUnitPhoneNumber
                        {
                            BusinessUnitId = dbbizUnit.BusinessUnitID,
                            PhoneNumber = ph,
                            isActive = true,
                            PhoneNumberTypeId = 1 //TODO: hardcoded. 1=>Default
                        });

                    businessUnitPhoneNumberManager.Insert(bizUnitPhonenUmbers);
                }
            }

            return dbbizUnit;

        }

        private SMSHistory GetSMSHistory(int bizUnitid, int callerRequestHistoryid, bool smsSentToCaller, bool isSMSSenttoBU)
        {
            var smsHistory = new SMSHistory();
            smsHistory.CallerRequestHistoryID = callerRequestHistoryid;
            smsHistory.BusinessUnitID = bizUnitid;
            smsHistory.isSMSSenttoCaller = smsSentToCaller;
            smsHistory.isSMSSenttoBU = isSMSSenttoBU;
            return smsHistory;
        }

        private string GetPhoneNumberToSendSMS(BusinessUnit bizUnit)
        {
            //if (CanSendSMS(bizUnit.SecondaryContactNumber))
            //{
            //    return bizUnit.SecondaryContactNumber;
            //}
            //else if (CanSendSMS(bizUnit.PrimaryContactNumber))
            //{
            //    return bizUnit.PrimaryContactNumber;
            //}
            //else
            //{
            return bizUnit.SMSContactNumber;
            //}
        }

        private bool SendSMS(string message, string phoneNumber)
        {
            return _smsProvider.SendSMS(message, phoneNumber);
        }

        private bool CanSendSMS(string phoneNumber)
        {
            //TODO: write code to identify if we can send SMS or not. ie whether it is mobile or landline
            return true;
        }

        private string FormatMessageforCaller(string BUName, string area, string mobileNumber)
        {
            StringBuilder msgCaller = new StringBuilder();

            msgCaller.Append(BUName + "\\r\\n");
            msgCaller.Append(area + "\\r\\n");

            var mobileNumbers = mobileNumber.Split(',');
            foreach (var number in mobileNumbers)
            {
                msgCaller.Append(number + "\\r\\n");
            }

            return msgCaller.ToString();

        }

        private SMSQueue GetSMSQueue(string recipientMobileNumber, string message, bool smsSentToCaller, bool isSMSSenttoBU)
        {
            var smsqueue = new SMSQueue();
            smsqueue.RecipientMobileNumber = recipientMobileNumber;
            smsqueue.Message = message;

            return smsqueue;
        }
    }
}
