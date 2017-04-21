using System;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using PagedList;

namespace Homely.Common.Model
{
    /// <summary>
    /// Responsible for Property posting.
    /// </summary>
    public class PropertyViewModel
    {
        public PropertyViewModel()
        {
            this.amenities = new List<ImageViewModel>();
            this.OwnerAddress = string.Empty;
            this.Description = string.Empty;
            this.suitableList = new List<Suitable>();
           
        }

        [HiddenInput]
        public Guid propId { get; set; }

        [HiddenInput]
        public string pwd { get; set; }

        [HiddenInput]
        public string PropCode { get; set; }
        /// <summary>
        /// Logged user id.
        /// </summary>
        public string EmailId { get; set; }

        /// <summary>
        /// User registration id.
        /// </summary>
        public Nullable<System.Guid> RegistrationID { get; set; }

        /// <summary>
        /// Property type.
        /// </summary>
        public int TransactionType { get; set; }
        public List<MetaDataViewModel> transactionList { get; set; }

        /// <summary>
        /// Owner ship type.
        /// </summary>
        public int OwnershipType { get; set; }
        public List<MetaDataViewModel> OwnershipTypeList { get; set; }

        /// <summary>
        /// Sub property type.
        /// </summary>
        [Required(ErrorMessage ="*")]
        public int SubPropertyType { get; set; }
        public SelectList SubPropertyTypeList { get; set; }


        [Required(ErrorMessage ="*")]
        public int PropertyCity { get; set; }
        public SelectList PropertyCityList { get; set; }

        public string PropertyOtherCity { get; set; }


        [Required(ErrorMessage ="*")]
        public int? Locality { get; set; }
        public SelectList LocalityList { get; set; }


        [Required(ErrorMessage ="Please enter address")]
        public string Address { get; set; }

        [Required(ErrorMessage ="*")]
        public int? Area { get; set; }
        public string Measure { get; set; }

        [Required(ErrorMessage ="*")]
        public int? Rent { get; set; }
        public string SocietyName { get; set; }

        [Required(ErrorMessage ="*")]
        public string Bedroom { get; set; }
        public SelectList BedroomList { get; set; }
        public int Floor { get; set; }
       
        public string Description { get; set; }
        public Nullable<int> Availability { get; set; }
        public SelectList AvailList { get; set; }
        public string Bathroom { get; set; }
        public SelectList BathroomList { get; set; }
        public string Balcony { get; set; }
        public SelectList BalcontList { get; set; }
        public string FloorInBuilding { get; set; }
        public SelectList FloorBuildingList { get; set; }
        public int Furnished { get; set; }
        public SelectList FurnishedList { get; set; }
        public int Facing { get; set; }
        public SelectList FacingList { get; set; }
        public int[] Amenity { get; set; }

        [Required(ErrorMessage ="*")]
        public bool isChecked { get; set; }

        public List<ImageViewModel> amenities { get; set; }
        public int ShoppingMallDistance { get; set; }
        public SelectList MallList { get; set; }
        public int HospitalDistance { get; set; }
        public SelectList HospitalList { get; set; }
        public int SchoolDistance { get; set; }
        public SelectList ShoppingList { get; set; }
        public int ATMDistance { get; set; }
        public SelectList AtmList { get; set; }

        [Required(ErrorMessage ="*")]
        public string OwnerName { get; set; }
        public int OwnerCity { get; set; }
        public SelectList OwnerCityList { get; set; }
        public string OwnerOtherCity { get; set; }
        public string OwnerAddress { get; set; }

        [Required(ErrorMessage ="*")]
        [RegularExpression(@"^(\d{10})$", ErrorMessage = "Please enter 10 digit mobile no.")]
        public string Mobile { get; set; }

        [RegularExpression(@"^(\d{10})$", ErrorMessage = "Please enter 10 digit mobile no.")]
        public string AlternateNumber { get; set; }

        [Required(ErrorMessage = "*")]
        [EmailAddress(ErrorMessage = "Please enter valid email")]
        public string OwnerEmailID { get; set; }
        public bool Verified { get; set; }
        public bool Rented { get; set; }
        public bool HotProperty { get; set; }
        public bool Enabled { get; set; }
        public System.DateTime CreateDate { get; set; }
        public string RegiesterdFrom { get; set; }
        public Nullable<int> OwnerState { get; set; }
        public SelectList OwnerStateList { get; set; }

        [Required(ErrorMessage ="*")]
        public Nullable<int> PropertyState { get; set; }
        public SelectList PropStateList { get; set; }
      

        public string OwnerAddress2 { get; set; }
        public string OwnerAddress3 { get; set; }
        public string OwnerPinCode { get; set; }
        public Nullable<int> TimeSlot { get; set; }
        public string AnyOtherTime { get; set; }
        public string slugUrl { get; set; }



        public List<Suitable> suitableList { get; set; }
        /// <summary>
        /// Use only in edit mode
        /// </summary>
        public string editSuitableList { get; set; }

        /// <summary>
        /// use only in edit.
        /// </summary>
        public string editAmenitiesList { get; set; }

        
        public string securityDeposit { get; set; }

        public string maintenaneCharge { get; set; }

        public int? minimumContract { get; set; }

        /// <summary>
        /// use only edit.
        /// </summary>
        public string strMinContract { get; set; }
        public SelectList minimumContractList { get; set; }

        public int? restriction { get; set; }
        public SelectList restrictionList { get; set; }

        public string rest { get; set; }

    }

    /// <summary>
    /// Responsible for getting property detail                                                 `               
    /// </summary>
    public class PropertDetailViewModel
    {
        public string Locality { get; set; }
        public string SocietyName { get; set; }
        public int Rent { get; set; }
        public string PropArea { get; set; }
        public string Bathroom { get; set; }
        public string Bedroom { get; set; }
        public string Balcony { get; set; }
        public string FloorInBuilding { get; set; }
        public string Furnished { get; set; }
        public string Availability { get; set; }
        public string Facing { get; set; }
        public string Description { get; set; }
        public string SubPropertyType { get; set; }
        public System.DateTime CreateDate { get; set; }
        public string Mobile { get; set; }
        public string EmailID { get; set; }
        public string Ownership { get; set; }
        public string OwnerName { get; set; }
        public string Security { get; set; }
        public string Maintenance { get; set; }
        public string contractName { get; set; }
        public string Restriction { get; set; }
        public string Floor { get; set; }
        public string City { get; set; }
        public string Area { get; set; }
    }

    /// <summary>
    /// Responsible for getting data after search
    /// </summary>
    public class RentPropertyViewModel
    {
       // public int State { get; set; }

        public string Locality { get; set; }
        public string SocietyName { get; set; }
        public string Area { get; set; }
        public string Url { get; set; }
        public int Rent { get; set; }
        public string Bathroom { get; set; }

        public string Bedroom { get; set; }

        public string PropertyCode { get; set; }
        public DateTime CreateDate { get; set; }
        public string ImgUrl { get; set; }
    }

    public class RootObjectForSearchPage
    {
        public RootObjectForSearchPage()
        {
            this.minimumContract = "";
        }

        public int OwnershipType { get; set; }
        public List<MetaDataViewModel> OwnershipTypeList { get; set; }

        public int Furnished { get; set; }
        public SelectList FurnishedList { get; set; }

        public string minimumContract { get; set; }
       
        public SelectList minimumContractList { get; set; }

        //public IPagedList<RentPropertyViewModel> _searchProp { get; set; }

        public int State { get; set; }
        public SelectList _state { get; set; }

        public int City { get; set; }
        public SelectList _city { get; set; }

        public int? Locality { get; set; }
        public SelectList _locality { get; set; }

        public string bedroom { get; set; }
        public SelectList _bedroom { get; set; }

        public int propertytype { get; set; }
        public SelectList _propertyType { get; set; }


        public string PropertyCode { get; set; }

        public int minValue { get; set; }

        public int maxValue { get; set; }


        public int? avail { get; set; }

        public SelectList AvailList { get; set; }
      

        public string budget { get; set; }

        public List<ImageViewModel> img { get; set; }

        #region Refine search section

        public int refine_State { get; set; }
     

        public int refine_City { get; set; }
     

        public int? refine_Locality { get; set; }

        public string refine_Budget { get; set; }


        #endregion
    }

    public class GetPropertyViewModel
    {
        public string id { get; set; }

        public string password { get; set; }
    }

    public class PropertCityLocalityModel
    {
        public long pkAreaid { get; set; }

        public long pkCityId { get; set; }

        public long pkStateId { get; set; }
    }

    
}
