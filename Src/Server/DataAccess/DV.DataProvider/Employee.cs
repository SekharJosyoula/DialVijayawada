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
    
    public partial class Employee
    {
        public Employee()
        {
            this.CallerRequestHistories = new HashSet<CallerRequestHistory>();
        }
    
        public int EmployeeID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int EmployeeTypeID { get; set; }
        public int AddressID { get; set; }
        public string WindowsNTName { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
    
        public virtual Address Address { get; set; }
        public virtual ICollection<CallerRequestHistory> CallerRequestHistories { get; set; }
        public virtual EmployeeType EmployeeType { get; set; }
    }
}