using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DV.DataProvider;
using DV.Manager.Interfaces;

namespace DV.Manager.DBCommitManager
{
    class SMSQueueCommitManager : AbstractCommitManager<SMSQueue>
    {
        public override System.Data.Entity.DbSet<SMSQueue> GetEntites()
        {
            return dvradiusEntities.SMSQueues;
        }

        public override bool IsExists(SMSQueue t)
        {
            //TODO: cant we  encanpsulate this? 
            return (from b in GetEntites()
                    where b.SMSQueueID.Equals(t.SMSQueueID)
                    select b).Any();
        }
    }
}
