using DV.DataProvider;
using DV.Manager.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DV.Manager.Request;

namespace DV.Manager
{
    internal class BusinessUnitsSearchManager : IManager<BusinessUnitSearchRequest, IEnumerable<BusinessUnit>>
    {
        public IEnumerable<BusinessUnit> Execute(BusinessUnitSearchRequest request)
        {
            using (var data = new dvradiusEntities())
            {
                var businessUnitsResult = data.GetBusinessUnits(request.SearchString, request.Area);
                var bizUnits = new List<BusinessUnit>();
                BusinessUnit bizUnit;
                foreach (var getBusinessUnitsResult in businessUnitsResult)
                {
                    bizUnit = new BusinessUnit();
                    bizUnit.Name = getBusinessUnitsResult.Name;
                    bizUnit.NatureofBusiness = getBusinessUnitsResult.NatureofBusiness;
                    bizUnit.ContactNumber = getBusinessUnitsResult.PrimaryContactNumber;
                    bizUnit.SMSContactNumber = getBusinessUnitsResult.SMSContactNumber;
                    bizUnit.CategoryName = getBusinessUnitsResult.CategoryName;
                    bizUnit.SMSLimitCount = getBusinessUnitsResult.SMSLimitCount;
                    bizUnit.BusinessUnitID = getBusinessUnitsResult.BusinessUnitID;
                    bizUnit.ContactPersonName = getBusinessUnitsResult.ContactPersonName;
                    bizUnit.Keywords = getBusinessUnitsResult.Keywords;
                    bizUnit.IsSMSEnabled = getBusinessUnitsResult.IsSMSEnabled != null && (bool)getBusinessUnitsResult.IsSMSEnabled;

                    if (getBusinessUnitsResult.AddressID!=null)
                    {
                        var address = new Address();
                        address.AddressID = (int)getBusinessUnitsResult.AddressID;
                        address.DoorNo = getBusinessUnitsResult.DoorNo;
                        address.BuildingName = getBusinessUnitsResult.BuildingName;
                        address.Street = getBusinessUnitsResult.Street;
                        address.Area = getBusinessUnitsResult.Area;
                        address.City = getBusinessUnitsResult.City;
                        address.District = getBusinessUnitsResult.District;
                        address.Pincode = getBusinessUnitsResult.Pincode;
                        address.LandMark = getBusinessUnitsResult.LandMark;
                        address.EmailID = getBusinessUnitsResult.EmailID;
                        address.Address1 = getBusinessUnitsResult.Address1;
                        
                        bizUnit.Address = address;
                    }
                   

                    if (getBusinessUnitsResult.CategoryID != null)
                    {
                        var businessUnitCategory = new BusinessUnitCategory();
                        businessUnitCategory.CategoryID = (int)getBusinessUnitsResult.CategoryID;
                        bizUnit.BusinessUnitCategory = businessUnitCategory;
                    }
                    if (getBusinessUnitsResult.CategoryID != null)
                    {
                        var businessUnitType = new BusinessUnitType();
                        businessUnitType.BusinessUnitTypeID = (int)getBusinessUnitsResult.BusinessUnitTypeID;
                        businessUnitType.Name = getBusinessUnitsResult.BusinessUnitTypeName;
                        businessUnitType.SMSCountLimit = getBusinessUnitsResult.BusinessUnitSMSCountLimit;
                        // TODO
                        businessUnitType.SMSLimitFrequency = new SMSLimitFrequency { SMSLimitFrequencyID = getBusinessUnitsResult.BusinessUnitSMSLimitFrequencyID == null ? 0 : (int)getBusinessUnitsResult.BusinessUnitSMSLimitFrequencyID };
                        bizUnit.BusinessUnitType = businessUnitType;
                    }
                 
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
}
