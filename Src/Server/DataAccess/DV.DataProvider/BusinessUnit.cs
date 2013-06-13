//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DV.DataProvider
{
    using System;
    using System.Collections.Generic;
    
    public partial class BusinessUnit
    {
        public BusinessUnit()
        {
            this.BUContactNumbers = new HashSet<BUContactNumber>();
            this.BusinessUnitPhoneNumbers = new HashSet<BusinessUnitPhoneNumber>();
            this.BusinessUnitPhoneNumbers1 = new HashSet<BusinessUnitPhoneNumber>();
            this.Members = new HashSet<Member>();
            this.SMSHistories = new HashSet<SMSHistory>();
        }
    
        public int BusinessUnitID { get; set; }
        public string Name { get; set; }
        public string NatureofBusiness { get; set; }
        public Nullable<int> CategoryID { get; set; }
        public Nullable<int> BusinessUnitTypeID { get; set; }
        public Nullable<int> AdressID { get; set; }
        public Nullable<int> SMSLimitFrequency { get; set; }
        public Nullable<int> SMSLimitCount { get; set; }
        public string PrimaryContactNumber { get; set; }
        public string SecondaryContactNumber { get; set; }
        public string SMSContactNumber { get; set; }
        public string ContactPersonName { get; set; }
        public string Keywords { get; set; }
        public string AdditionalInfo { get; set; }
        public Nullable<bool> IsSMSEnabled { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
    
        public virtual Address Address { get; set; }
        public virtual ICollection<BUContactNumber> BUContactNumbers { get; set; }
        public virtual BusinessUnitCategory BusinessUnitCategory { get; set; }
        public virtual BusinessUnitType BusinessUnitType { get; set; }
        public virtual SMSLimitFrequency SMSLimitFrequency1 { get; set; }
        public virtual ICollection<BusinessUnitPhoneNumber> BusinessUnitPhoneNumbers { get; set; }
        public virtual ICollection<BusinessUnitPhoneNumber> BusinessUnitPhoneNumbers1 { get; set; }
        public virtual ICollection<Member> Members { get; set; }
        public virtual ICollection<SMSHistory> SMSHistories { get; set; }
    }
}
