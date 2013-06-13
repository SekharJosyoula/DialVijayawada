using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using DV.DataProvider;
using DV.Manager.Interfaces;

namespace DV.Manager.DBCommitManager
{
    class AddressCommitManager : AbstractCommitManager<Address>
    {
        public override bool IsExists(Address address)
        {
            //TODO: cant we  encanpsulate this? 
            return (from b in GetEntites()
                    where b.AddressID.Equals(address.AddressID)
                    select b).Any();
        }

        protected override void BeforeInsert(Address entity)
        {
            entity.CreatedOn = DateTime.Now.GetIndianTIme();
        }

        protected override void BeforeUpdate(Address entity)
        {
            entity.ModifiedOn = DateTime.Now.GetIndianTIme();
        }

        public override DbSet<Address> GetEntites()
        {
            return dvradiusEntities.Addresses;
        }
    }
}
