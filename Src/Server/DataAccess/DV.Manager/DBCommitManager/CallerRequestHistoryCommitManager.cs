using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using DV.DataProvider;
using DV.Manager.Interfaces;

namespace DV.Manager.DBCommitManager
{
    public class CallerRequestHistoryCommitManager : AbstractCommitManager<CallerRequestHistory>
    {
        public override bool IsExists(CallerRequestHistory bizUnit)
        {
            //TODO: cant we  encanpsulate this? 
            return (from b in GetEntites()
                    where b.CallerRequestHistoryID.Equals(bizUnit.CallerRequestHistoryID)
                    select b).Any();
        }

        public override DbSet<CallerRequestHistory> GetEntites()
        {
            return dvradiusEntities.CallerRequestHistories;
        }
    }
}
