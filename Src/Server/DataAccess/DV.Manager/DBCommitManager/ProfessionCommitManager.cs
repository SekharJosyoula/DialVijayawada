using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using DV.DataProvider;
using DV.Manager.Interfaces;

namespace DV.Manager.DBCommitManager
{
    public class ProfessionCommitManager : AbstractCommitManager<Profession>
    {
        public override bool IsExists(Profession profession)
        {
            //TODO: cant we  encanpsulate this? 
            return (from b in GetEntites()
                    where b.ProfessionID.Equals(profession.ProfessionID)
                    select b).Any();
        }

        public override DbSet<Profession> GetEntites()
        {
            return dvradiusEntities.Professions;
        }
    }
}
