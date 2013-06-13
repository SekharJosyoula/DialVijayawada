using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DV.DataProvider;
using DV.Manager.DBCommitManager;
using DV.Manager.Interfaces;

namespace DV.Manager
{
    public static class CommitManagerFactory
    {
        //TODO: why genrics cant help here?
        private static Dictionary<String, Type> _CommitManagers;

        static CommitManagerFactory()
        {
            _CommitManagers = new Dictionary<string, Type>();

            _CommitManagers.Add(typeof(BusinessUnit).ToString(), typeof(BusinessUnitCommitManager));
            _CommitManagers.Add(typeof(Caller).ToString(), typeof(CallerCommitManager));
            _CommitManagers.Add(typeof(SMSHistory).ToString(), typeof(SmsHistoryCommitManager));
            _CommitManagers.Add(typeof(CallerRequestHistory).ToString(), typeof(CallerRequestHistoryCommitManager));
            _CommitManagers.Add(typeof(SMSQueue).ToString(), typeof(SMSQueueCommitManager));
            _CommitManagers.Add(typeof(Address).ToString(), typeof(AddressCommitManager));
            _CommitManagers.Add(typeof(Profession).ToString(), typeof(ProfessionCommitManager));
            _CommitManagers.Add(typeof(BusinessUnitPhoneNumber).ToString(), typeof(BusinessUnitPhoneNumberManager));
            
        }


        public static AbstractCommitManager<TEntity> Create<TEntity>() where TEntity : class
        {
            var searchManager = _CommitManagers[typeof(TEntity).ToString()];
            if (searchManager == null)
            {
                throw new NotSupportedException(typeof(TEntity).ToString());
            }
            else
            {
                return Activator.CreateInstance(searchManager) as AbstractCommitManager<TEntity>;
            }
        }
    }

}
