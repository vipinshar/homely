using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Homely.Common.Model
{
    public class MetaDataViewModel
    {
        public int id { get; set; }
        public string name { get; set; }
    }

    public class ImageViewModel
    {
        public bool isSelected { get; set; }
        public int AmenityId { get; set; }

        public string Amenity { get; set; }

        public string ImagePath { get; set; }

        public string ImageHoverPath { get; set; }

        public string BigImageHoverPath { get; set; }

        public string BigImagePath { get; set; }

    }

    public class QuickList
    {
        public QuickList()
        {
            this.loggedUserEmail = "";
        }
        public string email { get; set; }

        public int For { get; set; }

        public string Name { get; set; }

        public string Mobile { get; set; }

        public string loggedUserEmail { get; set; }
    }

    public class Suitable
    {
        public bool isSelected { get; set; }
        public int SuitableId { get; set; }
        public string SuitableName { get; set; }
    }

    public class RefineSearch
    {
        public int minimumContract { get; set; }
        public SelectList minimumContractList { get; set; }

        public int State { get; set; }

        public SelectList stateList { get; set; }

        public int City { get; set; }
        public SelectList cityList { get; set; }

        public int Locality { get; set; }
        public SelectList localityList { get; set; }

        public int propertyType { get; set; }
        public SelectList propertyTypeList { get; set; }

        public IList<ImageViewModel> img { get; set; }

        public string PropertyCode { get; set; }
    }
}
