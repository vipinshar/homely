using Homely.Common.Model;
using Homely.Common.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homely.Common.IRepository
{
    public interface IHomePageRepository
    {
        List<int> GetPropCount();

        List<PropertyAmenities> GetAmenities(string code);

        HomePageViewModel GetHomePage();

        PropertDetailViewModel GetPropertyDetail(string code);

        void GetDetail(string code);

        //PagingViewModel<FeaturedListingViewModel> GetFeaturedListingPaging(int page, int city, string For);

        long GetCountByCity(int city);

        IPagedList<FeaturedListingViewModel> GetFeaturedPartial(int id);

        List<HotPropertyViewModel> GetHotProperties();
        List<ReviewViewModel> GetReview();

        PropertyCountViewModel GetFreshCount();

        IPagedList<FeaturedListingViewModel> GetPropertyByCityAndPage(int page, int city, string For);
    }
}
