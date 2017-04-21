using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homely.Common.Model
{
    public class PropertyCompareViewModel
    {
        public PropertyCompareViewModel()
        {
            this.Area = "NOT MENTION";
            this.amenLst = new List<ImageViewModel>();
        }
        public string image { get; set; }
        public string PropertyCode { get; set; }
        public string Transaction { get; set; }= "NOT MENTION";
        public string Ownership { get; set; }= "NOT MENTION";
        public string SubPropertyType { get; set; }= "NOT MENTION";
        public string SubCity { get; set; }= "NOT MENTION";
        public string Locality { get; set; }= "NOT MENTION";
        public string Area { get; set; }= "NOT MENTION";
        public string SocietyName { get; set; }= "NOT MENTION";
        public int Rent { get; set; }= 0;
        public string Bedroom { get; set; }= "NOT MENTION";
        public string Bathroom { get; set; }= "NOT MENTION";
        public string Balcony { get; set; }= "NOT MENTION";
        public string Floor { get; set; } = "NOT MENTION";
        public string Description { get; set; } = "NOT MENTION";
        public System.DateTime CreateDate { get; set; }
        public string FloorInBuilding { get; set; } = "NOT MENTION";
        public string Furnished { get; set; } = "NOT MENTION";
        public string Facing { get; set; } = "NOT MENTION";

        public List<ImageViewModel> amenLst { get; set; }

    }
}
