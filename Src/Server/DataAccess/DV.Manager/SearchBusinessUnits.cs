using DV.DataProvider;
using DV.Manager.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DV.Manager
{
    internal class SearchBusinessUnits : ISearch<BusinessUnit>
    {
        public IEnumerable<BusinessUnit> Get(string request)
        {
            var data = new dvradiusEntities();
            var businessUnitsResult = data.GetBusinessUnits(request);
            var bizUnits = new List<BusinessUnit>();
            BusinessUnit bizUnit;
            foreach (var getBusinessUnitsResult in businessUnitsResult)
            {
                bizUnit = new BusinessUnit();
                bizUnit.Name = getBusinessUnitsResult.Name;
                bizUnit.NatureofBusiness = getBusinessUnitsResult.NatureofBusiness;
                bizUnit.PrimaryContactNumber = getBusinessUnitsResult.PrimaryContactNumber;
                bizUnit.SecondaryContactNumber = getBusinessUnitsResult.SecondaryContactNumber;
                bizUnit.SMSContactNumber = getBusinessUnitsResult.SMSContactNumber;
                bizUnit.CategoryName = getBusinessUnitsResult.CategoryName;
                bizUnit.SMSLimitCount = getBusinessUnitsResult.SMSLimitCount;
                bizUnit.BusinessUnitID = getBusinessUnitsResult.BusinessUnitID;
                bizUnit.ContactPersonName = getBusinessUnitsResult.ContactPersonName;
                bizUnit.Keywords = getBusinessUnitsResult.Keywords;
                bizUnit.IsSMSEnabled = getBusinessUnitsResult.IsSMSEnabled != null && (bool)getBusinessUnitsResult.IsSMSEnabled;

                var address = new Address();
                address.AddressID = getBusinessUnitsResult.AddressID;
                address.DoorNo = getBusinessUnitsResult.DoorNo;
                address.BuildingName = getBusinessUnitsResult.BuildingName;
                address.Street = getBusinessUnitsResult.Street;
                address.Area = getBusinessUnitsResult.Area;
                address.City = getBusinessUnitsResult.City;
                address.District = getBusinessUnitsResult.District;
                address.Pincode = getBusinessUnitsResult.Pincode;
                address.LandMark = getBusinessUnitsResult.LandMark;
                address.EmailID = getBusinessUnitsResult.EmailID;
                bizUnit.Address = address;

                var businessUnitCategory = new BusinessUnitCategory();
                businessUnitCategory.CategoryID = getBusinessUnitsResult.CategoryID;
                bizUnit.BusinessUnitCategory = businessUnitCategory;

                var businessUnitType = new BusinessUnitType();
                businessUnitType.BusinessUnitTypeID = getBusinessUnitsResult.BusinessUnitTypeID;
                businessUnitType.Name = getBusinessUnitsResult.BusinessUnitTypeName;
                businessUnitType.SMSCountLimit = getBusinessUnitsResult.BusinessUnitSMSCountLimit;
                // TODO
                businessUnitType.SMSLimitFrequency = new SMSLimitFrequency { SMSLimitFrequencyID = getBusinessUnitsResult.BusinessUnitSMSLimitFrequencyID == null ? 0 : (int)getBusinessUnitsResult.BusinessUnitSMSLimitFrequencyID };
                bizUnit.BusinessUnitType = businessUnitType;
                var smsLimitFrequency = new SMSLimitFrequency();
                bizUnit.SMSLimitFrequency = smsLimitFrequency.SMSLimitFrequencyID = getBusinessUnitsResult.SMSLimitFrequency == null
                                                            ? 0
                                                            : (int)getBusinessUnitsResult.SMSLimitFrequency;
                bizUnit.SMSLimitFrequency1 = smsLimitFrequency;

                bizUnits.Add(bizUnit);
            }

            return bizUnits;
        }
    }
}
