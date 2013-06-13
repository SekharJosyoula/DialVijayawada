using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DV.DataProvider;
using DV.Manager.Interfaces;

namespace DV.Manager.DBCommitManager
{
    internal class SmsHistoryCommitManager : AbstractCommitManager<SMSHistory>
    {
        public override System.Data.Entity.DbSet<SMSHistory> GetEntites()
        {
            return dvradiusEntities.SMSHistories;
        }

        public override bool IsExists(SMSHistory t)
        {
            //TODO: cant we  encanpsulate this? 
            return (from b in GetEntites()
                    where b.SMSHistoryID.Equals(t.SMSHistoryID)
                    select b).Any();
        }
    }
}
