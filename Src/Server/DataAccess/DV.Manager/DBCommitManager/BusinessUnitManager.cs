using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using DV.DataProvider;
using DV.Manager.Interfaces;
using DV.Manager.Request;

namespace DV.Manager.DBCommitManager
{
    public class BusinessUnitCommitManager : AbstractCommitManager<BusinessUnit>
    {
        public override bool IsExists(BusinessUnit bizUnit)
        {
            //TODO: cant we  encanpsulate this? 
            return (from b in GetEntites()
                    where b.BusinessUnitID.Equals(bizUnit.BusinessUnitID)
                    select b).Any();
        }

        protected override void BeforeInsert(BusinessUnit entity)
        {
            entity.CreatedOn = DateTime.Now.GetIndianTIme();
        }

        protected override void BeforeUpdate(BusinessUnit entity)
        {
            entity.ModifiedOn = DateTime.Now.GetIndianTIme();
        }
        public override DbSet<BusinessUnit> GetEntites()
        {
            return dvradiusEntities.BusinessUnits;
        }
    }
}
