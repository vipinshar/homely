using Homely.Common;
using Homely.Common.IRepository;
using Homely.Common.Model;
using Homely.Common.Models;
using Homely.Infrastructure.Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using PagedList;
using System.Text.RegularExpressions;

namespace Homely.Infrastructure.Repository
{
    public class HomePageRepository : IHomePageRepository
    {
        homely_Context db = new homely_Context();
        public HomePageViewModel GetHomePage()
        {
            // HomePageViewModel model = new HomePageViewModel();
            // var count = db.Database.SqlQuery<PropertyCountViewModel>("EXEC PROC_PropCounting").FirstOrDefault();
            //var property = db.Database.SqlQuery<FeaturedListingViewModel>("EXEC PROC_HomePageGzbListing").ToList();
            //var hotProperty = db.Database.SqlQuery<HotPropertyViewModel>("EXEC usp_HotProperties").ToList();
            //var review = db.Database.SqlQuery<ReviewViewModel>("EXEC PROC_Review").ToList();



            // model.propertyCountViewModel = count;
            //  model.featuredListingViewModel = property;
            // model.hotPropertyViewModel = hotProperty;
            // model.reviewViewModel = review;

            return null;
        }
        public List<ReviewViewModel> GetReview()
        {
            var review = db.Database.SqlQuery<ReviewViewModel>("EXEC PROC_Review").ToList();
            return review;
        }

        public List<int> GetPropCount()
        {
            List<int> lst = new List<int>();
            lst.Add(db.tbl_Property.Where(x => x.Enabled == true && x.Rented == false && x.PropertyCity ==(int)city.delhi).Count());
            lst.Add(db.tbl_Property.Where(x => x.Enabled == true && x.Rented == false && x.PropertyCity == (int)city.gurgaon).Count());
            lst.Add(db.tbl_Property.Where(x => x.Enabled == true && x.Rented == false && x.PropertyCity == (int)city.noida).Count());
            lst.Add(db.tbl_Property.Where(x => x.Enabled == true && x.Rented == false && x.PropertyCity == (int)city.ghaziabad).Count());
            lst.Add(db.tbl_Property.Where(x => x.Enabled == true && x.Rented == false && x.PropertyCity == (int)city.faridabad).Count());
            lst.Add(db.tbl_Property.Where(x => x.Enabled == true && x.Rented == false && x.PropertyCity == (int)city.greaternoida).Count());
            lst.Add(db.tbl_Property.Where(x => x.Enabled == true && x.Rented == false && x.LocalityId == (int)city.kundli).Count());
            return lst;

        }

        public IPagedList<FeaturedListingViewModel> GetFeaturedPartial(int city)
        {
            var property = db.Database.SqlQuery<FeaturedListingViewModel>("EXEC PROC_HomePageGzbListing @city", new SqlParameter("@city", city)).ToList();

            if (property != null && property.Count() != 0)
            {
                property.ForEach(x => x.Url = FriendlyURLTitle(x.Url));
                property.ForEach(x =>x.Url =x.Url.Replace(',', '-'));
                property.ForEach(x => x.Url=x.Url.Replace(@"/", "-"));
                property.ForEach(x => x.Url = x.Url.Replace(' ', '-'));
            }


            return property.ToPagedList(1, 9);
        }
        public List<HotPropertyViewModel> GetHotProperties()
        {
            var hotProp = db.Database.SqlQuery<HotPropertyViewModel>("EXEC usp_HotProperties").ToList();
            return hotProp;
        }

        public PropertyCountViewModel GetFreshCount()
        {
            var count = db.Database.SqlQuery<PropertyCountViewModel>("EXEC PROC_PropCounting").FirstOrDefault();
            return count;
        }

        public List<PropertyAmenities> GetAmenities(string code)
        {
            var data = db.PROC_GetAmenForDetailPage(code).Select(x => new PropertyAmenities { Amenity = x.Amenity, AmenityId = x.AmenityId, BigImageHoverPath = x.BigImageHoverPath, BigImagePath = x.BigImagePath }).ToList();
            return data;

        }

        public void GetDetail(string code)
        {


            var data = db.Database.SqlQuery<PropertyDetailViewModel>("Exec [PROC_PropertyDetails] @PropertyCode", new SqlParameter("@PropertyCode", code)).FirstOrDefault();
            var amen = db.Database.SqlQuery<PropertyAmenities>("Exec [PROC_GetAmenForDetailPage] @PropertyCode", new SqlParameter("@PropertyCode", code)).ToList();
        }



        //public PagingViewModel<FeaturedListingViewModel> GetFeaturedListingPaging(int page, int city, string For)
        //{
        //    PagingViewModel<FeaturedListingViewModel> obj = new PagingViewModel<FeaturedListingViewModel>();
        //    obj.Data = db.Database.SqlQuery<FeaturedListingViewModel>("EXEC PROC_GetPropByPaging @City,@For,@PageNumber,@RowspPage",
        //        new SqlParameter("@City", city), new SqlParameter("@For", city), new SqlParameter("@PageNumber", city), new SqlParameter("@RowspPage", city)).ToList();
        //    obj.NumberOfPages = Convert.ToInt32(Math.Ceiling((double)db.tbl_Property.Where(x => x.Enabled == true && x.Rented == false && x.PropertyCity == city).Count() / 9));
        //    obj.CurrentPage = page;
        //    return obj;

        //}

        public long GetCountByCity(int id)
        {
            var data = Convert.ToInt32(Math.Ceiling((double)db.tbl_Property.Where(x => x.Enabled == true && x.Rented == false && x.PropertyCity == id).Count() / 9));
            return data;
        }

        public IPagedList<FeaturedListingViewModel> GetPropertyByCityAndPage(int page, int city, string For)
        {
            int pageSize = 9;
            var count = db.tbl_Property.Where(x => x.Enabled == true && x.Rented == false && x.PropertyCity == city).Count();
            var data = db.Database.SqlQuery<FeaturedListingViewModel>("EXEC PROC_GetPropByPaging @City,@For,@PageNumber,@RowspPage",
                new SqlParameter("@City", city), new SqlParameter("@For", For), new SqlParameter("@PageNumber", page), new SqlParameter("@RowspPage", pageSize)).ToList();

            //var data = db.Database.SqlQuery<FeaturedListingViewModel>("Exec PROC_Property_City @City,@For", new SqlParameter("@City", city), new SqlParameter("@For", For)).ToList();
            if (data != null && data.Count != 0)
            {
                data.ForEach(x => x.Url = FriendlyURLTitle(x.Url));
                data.ForEach(x => x.Url = x.Url.Replace(',', '-'));
                data.ForEach(x => x.Url = x.Url.Replace(@"/", "-"));
                data.ForEach(x => x.Url = x.Url.Replace(' ', '-'));
            }
            StaticPagedList<FeaturedListingViewModel> obj = new StaticPagedList<FeaturedListingViewModel>(data, page, pageSize, count);
            return obj;
        }

        string FriendlyURLTitle(string pTitle)
        {
            pTitle = pTitle.Replace(" ", "-");
            pTitle = pTitle.Replace(",", "");
            pTitle = HttpUtility.UrlEncode(pTitle);
            pTitle = pTitle.Replace("%2f", "/");
            return Regex.Replace(pTitle, "%[0-9A-Fa-f]{2}", "");
        }

        public PropertDetailViewModel GetPropertyDetail(string code)
        {
            var data = db.PROC_PropertyDetails(code).Select(x => new PropertDetailViewModel
            {
                Area = x.Locality,
                Availability = x.Availability,
                Balcony = x.Balcony,
                Bathroom = x.Bathroom,
                Bedroom = x.Bedroom,
                City = x.City,
                contractName = x.contractName,
                CreateDate = x.CreateDate,
                Description = x.Description,
                EmailID = x.EmailID,
                Facing = x.Facing,
                Floor = x.Floor,
                FloorInBuilding = x.FloorInBuilding,
                Furnished = x.Furnished,
                Locality = x.Locality,
                Maintenance = x.Maintenance,
                Mobile = x.Mobile,
                OwnerName = x.OwnerName,
                Ownership = x.Ownership,
                PropArea = x.Area,
                Rent = x.Rent,
                Security = x.Security,
                SocietyName = x.SocietyName,
                SubPropertyType = x.SubPropertyType,

            }).SingleOrDefault();
            return data;
        }


    }
}
