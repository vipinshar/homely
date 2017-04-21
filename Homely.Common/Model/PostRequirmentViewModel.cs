using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Homely.Common.Model
{
    public class PostRequirmentViewModel
    {
        public string Bedroom { get; set; }
        public SelectList BedroomList { get; set; }


        public string email { get; set; }
        public int minValue { get; set; }
        public int maxValue { get; set; }

        [Required(ErrorMessage = "*")]
        public int SubPropertyType { get; set; }
        public SelectList SubPropertyTypeList { get; set; }

        [Required(ErrorMessage = "*")]
        public int PropertyCity { get; set; }
        public SelectList PropertyCityList { get; set; }

        [Required(ErrorMessage = "*")]
        public int Locality { get; set; }
        public SelectList LocalityList { get; set; }

        public int[] Amenity { get; set; }
        public List<ImageViewModel> amenities { get; set; }
        public int bhk { get; set; }

        public string location { get; set; }

        public int minArea { get; set; }

        [Required(ErrorMessage = "*")]
        public string ownerName { get; set; }

        [Required(ErrorMessage = "*")]
        public int OwnerState { get; set; }
        public SelectList OwnerStateList { get; set; }

        public int OwnerCity { get; set; }
        public SelectList OwnerCityList { get; set; }

        [Required(ErrorMessage ="*")]
        [EmailAddress(ErrorMessage ="Please enter valid email")]
        public string clientEmail { get; set; }

        [Required(ErrorMessage = "*")]
        [RegularExpression(@"^(\d{10})$",ErrorMessage="Please enter 10 digit mobile no.")]
        public string mobile { get; set; }
        public int city { get; set; }

        [Required(ErrorMessage = "*")]
        public string address1 { get; set; }
        public string address2 { get; set; }
        public List<ChkBox> chbox { get; set; }
        public string address3 { get; set; }
        public string pincode { get; set; }
    }

    public class PostFeedbackViewModel
    {
        public string Name { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public string Mobile { get; set; }

        public string Feedback { get; set; }
    }

    public class ChkBox
    {
        public int val { get; set; }
        public string name { get; set; }

        public bool isSelect { get; set; }
    }
}
