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
    
    public partial class SMSHistory
    {
        public int SMSHistoryID { get; set; }
        public int CallerRequestHistoryID { get; set; }
        public int BusinessUnitID { get; set; }
        public Nullable<bool> isSMSSenttoCaller { get; set; }
        public Nullable<bool> isSMSSenttoBU { get; set; }
    
        public virtual BusinessUnit BusinessUnit { get; set; }
        public virtual CallerRequestHistory CallerRequestHistory { get; set; }
    }
}
