using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DV.DataProvider;
using DV.Manager.Interfaces;

namespace DV.Manager.DBCommitManager
{
    class CallerCommitManager : AbstractCommitManager<Caller>
    {
        public override System.Data.Entity.DbSet<Caller> GetEntites()
        {
            return dvradiusEntities.Callers;
        }

        protected override void BeforeInsert(Caller entity)
        {
            entity.CreatedOn = DateTime.Now.GetIndianTIme();
        }

        protected override void BeforeUpdate(Caller entity)
        {
            entity.ModifiedOn = DateTime.Now.GetIndianTIme();
        }

        public override bool IsExists(Caller t)
        {
            return t.CallerID > 0;
        }
    }
}
