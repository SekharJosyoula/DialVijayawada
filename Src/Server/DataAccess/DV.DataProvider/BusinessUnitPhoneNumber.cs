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
    
    public partial class BusinessUnitPhoneNumber
    {
        public int Id { get; set; }
        public int BusinessUnitId { get; set; }
        public string PhoneNumber { get; set; }
        public int PhoneNumberTypeId { get; set; }
        public bool isActive { get; set; }
    
        public virtual BusinessUnit BusinessUnit { get; set; }
        public virtual BusinessUnit BusinessUnit1 { get; set; }
        public virtual PhoneNumberType PhoneNumberType { get; set; }
    }
}
