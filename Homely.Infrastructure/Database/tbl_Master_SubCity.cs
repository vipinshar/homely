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
    
    public partial class tbl_Master_SubCity
    {
        public string SubCity { get; set; }
        public int SubCityId { get; set; }
        public int CityId { get; set; }
        public bool Enabled { get; set; }
        public System.DateTime CreateDate { get; set; }
    }
}