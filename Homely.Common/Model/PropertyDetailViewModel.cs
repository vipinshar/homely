using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homely.Common.Model
{
    public class PropertyDetailViewModel
    {
        public PropertyDetailViewModel()
        {
            this.propertyAmenities = new List<PropertyAmenities>();
        }
        public string Locality { get; set; }
        public string SocietyName { get; set; }
        public int Rent { get; set; }
        public string Area { get; set; }
        public string Bathroom { get; set; }
        public string Bedroom { get; set; }
        public string Balcony { get; set; }
        public string FloorInBuilding { get; set; }
        public string Furnished { get; set; }
        public string Availability { get; set; }
        public string Facing { get; set; }
        public string Description { get; set; }
        public string City { get; set; }
        public string SubPropertyType { get; set; }
        public System.DateTime CreateDate { get; set; }
        public string Mobile { get; set; }
        public string EmailID { get; set; }
        public string Ownership { get; set; }
        public string OwnerName { get; set; }
        public string Floor { get; set; }

        public List<PropertyAmenities> propertyAmenities { get; set; }
    }

    public class PropertyAmenities
    {
        public decimal AmenityId { get; set; }
        public string Amenity { get; set; }
        public string BigImagePath { get; set; }
        public string BigImageHoverPath { get; set; }

        public string ImageHoverPath { get; set; }
    }
}
