//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Homely.Infrastructure.Database
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbl_Master_Restriction
    {
        public int restrictionId { get; set; }
        public string restrictionName { get; set; }
        public Nullable<bool> isActive { get; set; }
        public Nullable<System.DateTime> createdDate { get; set; }
        public Nullable<System.DateTime> updatedDate { get; set; }
    }
}
