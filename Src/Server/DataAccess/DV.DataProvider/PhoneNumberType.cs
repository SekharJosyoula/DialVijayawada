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
    
    public partial class PhoneNumberType
    {
        public PhoneNumberType()
        {
            this.BusinessUnitPhoneNumbers = new HashSet<BusinessUnitPhoneNumber>();
        }
    
        public int Id { get; set; }
        public string Type { get; set; }
    
        public virtual ICollection<BusinessUnitPhoneNumber> BusinessUnitPhoneNumbers { get; set; }
    }
}
