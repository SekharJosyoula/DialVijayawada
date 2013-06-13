using DV.DataProvider;
using DV.Manager.Interfaces;

namespace DV.Manager.DBCommitManager
{
    public class BusinessUnitPhoneNumberManager: AbstractCommitManager<BusinessUnitPhoneNumber>
    {
        public override bool IsExists(BusinessUnitPhoneNumber t)
        {
            return t.Id > 0;
        }

        public override System.Data.Entity.DbSet<BusinessUnitPhoneNumber> GetEntites()
        {
            return dvradiusEntities.BusinessUnitPhoneNumbers;
        }
    }
}
