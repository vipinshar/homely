using Homely.Common.Model;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homely.Common.IRepository
{
    public interface IPropertyListingRepository
    {
        /// <summary>
        /// Use in edit property
        /// </summary>
        /// <param name="locality"></param>
        /// <returns></returns>
        PropertCityLocalityModel GetPropCityState(long? locality);

        /// <summary>
        /// Edit property.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        PropertyViewModel GetPropertyById(GetPropertyViewModel model);

        string SaveProperty(PropertyViewModel model);

        IPagedList<RentPropertyViewModel> GetRentProperty(int city, string For);

        long GetCityIdByName(string cityName);

        long GetTDICityId(string name);

        IPagedList<RentPropertyViewModel> ViewLisingPaging(int page, int city, string For);

        IPagedList<RentPropertyViewModel> SearchProp(RootObjectForSearchPage model);

        List<PropertyCompareViewModel> CompareProperty(string propA, string propB, string propC);

        List<ImageViewModel> GetAmenities(string code);

    }
}
