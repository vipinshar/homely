using Homely.Common.IRepository;
using Homely.Common.Model;
using Homely.Infrastructure.Database;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.SqlServer;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homely.Infrastructure.Repository
{
    public class PropertyListingRepository : IPropertyListingRepository
    {
        homely_Context db = new homely_Context();

        public string SaveProperty(PropertyViewModel model)
        {
            try
            {
                if (!string.IsNullOrEmpty(model.SocietyName))
                {
                    if (model.SocietyName.Contains("/"))
                    {
                        model.SocietyName = model.SocietyName.Replace('/', '-');
                    }
                }
                string amen = string.Empty;
                string suitable = string.Empty;
                var amenData = model.amenities.Where(x => x.isSelected == true).ToList();
                if (amenData != null && amenData.Count != 0)
                {
                    var data = amenData.Select(x => x.AmenityId.ToString()).ToList();
                    if (data != null)
                    {
                        amen = string.Join(",", data);
                    }
                }
                var suitableData= model.suitableList.Where(x => x.isSelected == true).ToList();
                if (suitableData != null && suitableData.Count != 0)
                {
                    var suit = suitableData.Select(x => x.SuitableId.ToString()).ToList();
                    if (suit != null)
                    {
                        suitable = string.Join(",", suit);
                    }
                }
               
                string result = string.Empty;

                if (model.PropCode == null)
                {
                    result = db.PROC_AddProperty(model.EmailId, model.TransactionType,
                    model.TransactionType, model.SubPropertyType,
                    model.PropertyCity, model.Address,
                    model.Area, model.Measure,
                    model.Rent, model.SocietyName,
                    Convert.ToInt32(model.Bedroom),
                    model.Floor, model.Description == null ? string.Empty : model.Description,
                    Convert.ToInt32(model.Bathroom),
                    Convert.ToInt32(model.Balcony),
                    model.FloorInBuilding, model.Furnished,
                    model.Facing, amen, model.ShoppingMallDistance, model.HospitalDistance,
                    model.SchoolDistance, model.ATMDistance, model.OwnerName, model.OwnerCity,
                    model.OwnerAddress == null ? string.Empty : model.OwnerAddress,
                    model.Mobile, 
                    model.AlternateNumber, 
                    model.OwnerEmailID, 
                    model.Availability,
                    model.PropertyOtherCity,
                    model.OwnerOtherCity,
                    model.OwnerState,
                    model.OwnerAddress2,
                    model.OwnerAddress3,
                    model.OwnerPinCode, 
                    1, 
                    "",
                    model.Locality, 
                    suitable,
                    model.securityDeposit,
                    model.maintenaneCharge, 
                    model.minimumContract,
                    model.restriction,
                    model.pwd).SingleOrDefault();
                }
                else
                {
                    var res = db.PROC_UpdateProperty(model.PropCode, model.TransactionType,
                   model.TransactionType, 
                   model.SubPropertyType,
                   model.PropertyCity,
                   model.Address,
                   model.Area, 
                   model.Measure,
                   model.Rent,
                   model.SocietyName,
                   Convert.ToInt32(model.Bedroom),
                   model.Floor,
                   model.Description == null ? string.Empty : model.Description,
                   Convert.ToInt32(model.Bathroom),
                   Convert.ToInt32(model.Balcony),
                   model.FloorInBuilding, 
                   model.Furnished,
                   model.Facing, 
                   amen,
                   model.ShoppingMallDistance,
                   model.HospitalDistance,
                   model.SchoolDistance,
                   model.ATMDistance, 
                   model.OwnerName, 
                   model.OwnerCity,
                   model.OwnerAddress == null ? string.Empty : model.OwnerAddress,
                   model.Mobile, 
                   model.AlternateNumber, 
                   model.OwnerEmailID, 
                   model.Availability,
                   model.PropertyOtherCity, 
                   model.OwnerOtherCity,
                   model.OwnerState, 
                   model.OwnerAddress2,
                   model.OwnerAddress3, 
                   model.OwnerPinCode, 
                   1,
                   "",
                   model.Locality, 
                   suitable, 
                   model.securityDeposit, 
                   model.maintenaneCharge, 
                   model.minimumContract, 
                   model.restriction).ToString();
                   result = model.PropCode;

                }
                return result;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public IPagedList<RentPropertyViewModel> GetRentProperty(int city, string For)
        {

            var rentLst = db.Database.SqlQuery<RentPropertyViewModel>("EXEC PROC_Property_City_New @City,@For", new SqlParameter("@City", city), new SqlParameter("@For", For)).ToList();
            return rentLst.ToPagedList(1, 5);
        }

        public long GetCityIdByName(string cityName)
        {
            var str = db.tblCity.Where(x => x.cityName == cityName.Trim()).FirstOrDefault();
            if (str != null)
            {
                return str.pkCityId;
            }
            return 0;
        }

        /// <summary>
        /// Get TdI City
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public long GetTDICityId(string name)
        {
            var data = db.tblAreas.Where(x => x.areaName == "TDI City ( Kundli )").FirstOrDefault();
            if (data != null)
            {
                return data.pkAreaId;
            }
            return 0;
        }

        public IPagedList<RentPropertyViewModel> ViewLisingPaging(int page, int city, string For)
        {
            int pageSize = 5;
            int totalCount = 0;
            List<RentPropertyViewModel> objList = new List<RentPropertyViewModel>();
            if (city != 0)
            {
                totalCount = db.tbl_Property.Where(x => x.Enabled == true && x.Rented == false && x.PropertyCity == city).Count();
            }
            else
            {
                totalCount = db.tbl_Property.Where(x => x.Enabled == true && x.Rented == false && x.TransactionType == 4).Count();
            }
            var count = db.tbl_Property.Where(x => x.Enabled == true && x.Rented == false && x.PropertyCity == city).Count();
            var data = db.Database.SqlQuery<RentPropertyViewModel>("EXEC PROC_Property_City_New @City,@For", new SqlParameter("@City", city), new SqlParameter("@For", For)).ToList();
            if (data != null && data.Count != 0)
            {
                int numberOfObjectsPerPage = pageSize;
                objList = data.Skip(numberOfObjectsPerPage * (page - 1)).Take(numberOfObjectsPerPage).ToList();
            }
            StaticPagedList<RentPropertyViewModel> obj = new StaticPagedList<RentPropertyViewModel>(objList, page, pageSize, totalCount);
            return obj;
        }

        public IPagedList<RentPropertyViewModel> SearchProp(RootObjectForSearchPage model)
        {
            int pageSize = 5;
            int totalCount = 0;
            List<RentPropertyViewModel> objList = new List<RentPropertyViewModel>();

            //var datas = db.PROC_SearchProperty(model.City, model.Locality, "", model.propertytype, model.minValue, model.maxValue, model.PropertyCode, "", model.avail, model.OwnershipType, model.Furnished,Convert.ToInt32(model.bedroom), model.minimumContract).ToList().Select(x=>new RentPropertyViewModel {

            //    Area=x.Area,
            //    Bathroom=x.Bathroom,
            //    Bedroom=x.Bedroom,
            //   CreateDate=x.CreateDate,
            //   Url=x.Url,
            //   Locality=x.Locality,
            //   PropertyCode=x.PropertyCode,
            //   Rent=x.Rent,
            //   SocietyName=x.SocietyName 

            //});

            

                var data = db.Database.SqlQuery<RentPropertyViewModel>("EXEC PROC_SearchProperty @CityId,@LocalityId,@Locality,@PropertyType,@MinPrice,@MaxPrice,@PropertyCode,@Amenities,@Avaliability,@Ownership,@Furnishing,@Bedroom,@MinimumContract",
                new SqlParameter("@CityId", model.City),
                new SqlParameter("@LocalityId", model.Locality == null ? "" : model.Locality.ToString()),
                new SqlParameter("@Locality", ""),
                new SqlParameter("@PropertyType", model.propertytype),
                new SqlParameter("@MinPrice", model.minValue),
                new SqlParameter("@MaxPrice", model.maxValue),
                new SqlParameter("@PropertyCode", model.PropertyCode),
                new SqlParameter("@Amenities", ""),
                new SqlParameter("@Avaliability", model.avail ?? 0),
                new SqlParameter("@Ownership", model.OwnershipType),
                new SqlParameter("@Furnishing", model.Furnished),
                new SqlParameter("@Bedroom", string.IsNullOrEmpty(model.bedroom) ? 0: Convert.ToInt32(model.bedroom)),
                new SqlParameter("@MinimumContract", string.IsNullOrEmpty(model.minimumContract) ? "0" : model.minimumContract.ToString())).ToList();

            if (data != null && data.Count() > 0)
            {
                totalCount = data.Count();
            }


            //if (data != null && data.Count != 0)
            //{
            //    int numberOfObjectsPerPage = pageSize;
            //    objList = data.Skip(numberOfObjectsPerPage * (page - 1)).Take(numberOfObjectsPerPage).ToList();
            //}
            StaticPagedList<RentPropertyViewModel> obj = new StaticPagedList<RentPropertyViewModel>(data, 1, pageSize, totalCount);
            return obj;
        }

        public PropertyViewModel GetPropertyById(GetPropertyViewModel model)
        {
            var data = db.tbl_Property.Where(x => x.PropertyCode == model.id && x.editPassword == model.password).Select(x => new PropertyViewModel
            {
                PropCode = x.PropertyCode,
                propId = x.PropertyID,
                PropertyState = 0,
                Address = x.Locality,
                AlternateNumber = x.AlternateNumber,
                Area = x.Area,
                ATMDistance = x.ATMDistance,
                Availability = x.Availability,
                Balcony = x.Balcony,
                Bathroom = x.Bathroom,
                Bedroom = x.Bedroom,
                Description = x.Description,
                Facing = x.Facing,
                Floor = x.Floor,
                FloorInBuilding = x.FloorInBuilding,
                Furnished = x.Furnished,
                Locality = x.LocalityId,
                maintenaneCharge = x.maintenanceCharge,
                Measure = x.Measure,
                strMinContract = x.minimumContract,
                Mobile = x.Mobile,
                OwnerAddress = x.OwnerAddress,
                OwnerAddress2 = x.OwnerAddress2,
                OwnerAddress3 = x.OwnerAddress3,
                OwnerCity = x.OwnerCity,
                OwnerEmailID = x.EmailID,
                OwnerName = x.OwnerName,

                OwnerOtherCity = x.OwnerOtherCity,
                OwnerState = x.OwnerState,
                OwnerPinCode = x.OwnerPinCode,
                PropertyCity = x.PropertyCity,
                OwnershipType = x.OwnershipType,
                PropertyOtherCity = x.PropertyOtherCity,
                Rent = x.Rent,
                rest = x.restriction,
                securityDeposit = x.securityDeposit,
                SocietyName = x.SocietyName,
                SubPropertyType = x.SubPropertyType,
                editSuitableList = x.suitable,
                editAmenitiesList = x.Amenities
            }).FirstOrDefault();
            return data;
        }

        /// <summary>
        /// Use in edit case.
        /// </summary>
        /// <param name="locality"></param>
        /// <returns></returns>
        public PropertCityLocalityModel GetPropCityState(long? locality)
        {
            var data = db.Database.SqlQuery<PropertCityLocalityModel>("EXEC PROC_GetPropertyCityState @LOCALITY", new SqlParameter("@LOCALITY", locality ?? 0)).FirstOrDefault();
            return data;
        }

        public List<PropertyCompareViewModel> CompareProperty(string propA, string propB, string propC)
        {
            var _data = db.Database.SqlQuery<PropertyCompareViewModel>("EXEC PROC_PropertyComparison @PropertyCode1,@PropertyCode2,@PropertyCode3",
                new SqlParameter("@PropertyCode1", propA), new SqlParameter("@PropertyCode2", propB), new SqlParameter("@PropertyCode3", propC)).ToList();

            var data = (from x in _data
                     select new PropertyCompareViewModel
                     {
                         Area = x.Area == null ? "NOT MENTION" : x.Area,
                         Balcony = x.Balcony == null ? "NOT MENTION" : x.Balcony,
                         Bathroom = x.Bathroom == null ? "NOT MENTION" : x.Bathroom,
                         Bedroom = x.Bedroom == null ? "NOT MENTION" : x.Bedroom,
                         CreateDate = x.CreateDate,
                         Description = string.IsNullOrEmpty(x.Description) ? "NOT MENTION" : x.Description,
                         Facing = x.Facing == null ? "NOT MENTION" : x.Facing,
                         Floor = x.Floor == null ? "NOT MENTION" : x.Floor,
                         FloorInBuilding = x.FloorInBuilding == null ? "NOT MENTION" : x.FloorInBuilding,
                         Furnished = x.Furnished == null ? "NOT MENTION" : x.Furnished,
                         Locality = x.Locality == null ? "NOT MENTION" : x.Locality,
                         Ownership = x.Ownership == null ? "NOT MENTION" : x.Ownership,
                         PropertyCode = x.PropertyCode == null ? "NOT MENTION" : x.PropertyCode,
                         Rent = x.Rent,
                         SocietyName = x.SocietyName == null ? "NOT MENTION" : x.SocietyName,
                         SubCity = x.SubCity == null ? "NOT MENTION" : x.SubCity,

                         SubPropertyType = x.SubPropertyType == null ? "NOT MENTION" : x.SubPropertyType,
                         Transaction = x.Transaction == null ? "NOT MENTION" : x.Transaction,
                     }).ToList();

            //var data = db.PROC_PropertyComparison(propA, propB, propC).Select(x => new PropertyCompareViewModel
            //{

            //    Area = x.Area == null ? "NOT MENTION" : x.Area,
            //    Balcony = x.Balcony == null ? "NOT MENTION" : x.Balcony,
            //    Bathroom = x.Bathroom == null ? "NOT MENTION" : x.Bathroom,
            //    Bedroom = x.Bedroom == null ? "NOT MENTION" : x.Bedroom,
            //    CreateDate = x.CreateDate,
            //    Description = string.IsNullOrEmpty(x.Description)?"NOT MENTION":x.Description,
            //    Facing = x.Facing == null ? "NOT MENTION" : x.Facing,
            //    Floor = x.Floor == null ? "NOT MENTION" : x.Floor,
            //    FloorInBuilding = x.FloorInBuilding == null ? "NOT MENTION" : x.FloorInBuilding,
            //    Furnished = x.Furnished == null ? "NOT MENTION" : x.Furnished,
            //    Locality = x.Locality == null ? "NOT MENTION" : x.Locality,
            //    Ownership = x.Ownership == null ? "NOT MENTION" : x.Ownership,
            //    PropertyCode = x.PropertyCode == null ? "NOT MENTION" : x.PropertyCode,
            //    Rent = x.Rent,
            //    SocietyName = x.SocietyName == null ? "NOT MENTION" : x.SocietyName,
            //    SubCity = x.SubCity == null ? "NOT MENTION" : x.cityName,

            //    SubPropertyType = x.SubPropertyType == null ? "NOT MENTION" : x.SubPropertyType,
            //    Transaction = x.Transaction == null ? "NOT MENTION" : x.Transaction,
            //}).ToList();
            return data;
        }

        public List<ImageViewModel> GetAmenities(string code)
        {
            var data = db.PROC_GetAmenForDetailPage(code).Select(x => new ImageViewModel
            {
                Amenity = x.Amenity,
                ImagePath = x.ImageHoverPath
            }).ToList();
            return data;

        }


        /// <summary>
        /// Responsible for create string into list.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>

    }
}
