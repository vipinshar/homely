using Homely.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homely.Common.IRepository
{
    public interface IMasterDataRepository
    {
        List<MetaDataViewModel> GetRestriction();

        List<Suitable> GetSuitable();

        List<MetaDataViewModel> GetMinimumContract();

        List<MetaDataViewModel> GetBedrooms();
        int QuickList(QuickList model);

        List<MetaDataViewModel> GetTransaction();

        List<MetaDataViewModel> GetOwnership();

        List<MetaDataViewModel> GetFurnished();

        List<MetaDataViewModel> GetFacing();

        List<MetaDataViewModel> GetAvailability();

        List<MetaDataViewModel> GetProperty();

        List<MetaDataViewModel> GetState();

        List<ImageViewModel> GetAmenities();

        List<MetaDataViewModel> GetCityByStateId(long id);

        List<MetaDataViewModel> GetLocalityByCity(long id);

      

    }
}
