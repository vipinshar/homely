using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Homely.Areas.Admin.Models
{
    public class GetExcelViewModel
    {
        public int CityId { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
    }

    public class PropertyInfoViewModel
    {
        public string City { get; set; }

        public string Locality { get; set; }

        public string Society { get; set; }
        public string PropertyId { get; set; }

        public string Address { get; set; }

        public string OwnerContact { get; set; }

        
    }
}