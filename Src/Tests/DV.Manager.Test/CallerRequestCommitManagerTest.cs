using System;
using System.Linq;
using System.Collections.Generic;
using DV.DataProvider;
using DV.Manager.Request;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DV.Manager.Test
{
    [TestClass]
    public class CallerRequestCommitManagerTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            var x = new CallerRequestCommitManager();
            CallerRequestCommit callerRequestCommit = new CallerRequestCommit();
            callerRequestCommit.Caller = new Caller();
            callerRequestCommit.Caller.PhoneNumber = "4254359889";
            callerRequestCommit.Caller.FirstName = "Vital";
            callerRequestCommit.Caller.LastName = "P";

            callerRequestCommit.CallerRequest = new CallerRequestHistory();
            callerRequestCommit.CallerRequest.CallDurationinSecs = 60;
            callerRequestCommit.CallerRequest.RequestedDetails = "KFC New York";
            //TODO: Assume always get something
            callerRequestCommit.CallerRequest.TeleCallerID = GetFirstTeleCaller().EmployeeID;

            callerRequestCommit.BusinessUnitsIdentifiedForCallerRequest = GetTop5BU();

            var ret  =x.Execute(callerRequestCommit);
            Assert.IsNotNull(ret);
        }

        private Employee GetFirstTeleCaller()
        {
            using (var data = new dvradiusEntities())
            {
                return (from emp in data.Employees select emp).Take(1).FirstOrDefault();
            }
        }

        private IEnumerable<BusinessUnit> GetTop5BU()
        {
            using (var data = new dvradiusEntities())
            {
                return (from bu in data.BusinessUnits.Include("Address") 
                        where bu.Address != null && bu.SecondaryContactNumber != null &&
                        bu.Name != null && bu.Address.Area != null
                        select bu).Take(5).ToList();
           }
        }

        [TestMethod]
        public void TestMethid()
        {
            SMSGateWayProvider x = new SMSGateWayProvider();
            string msg = "Hi Vital\r\n(DR RAMESH)CITI CARDIAC \r\nRING ROAD\r\n2470283\r\n 2470881\r\n 2484800\r\nA K OPTICALS\r\n1 TOWN\r\n9703841786\r\n 9290449529\r\n ";
            x.SendSMS(msg, "919849420435");

        }

    }
}