using Homely.Common.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Homely.Common.Model;
using Homely.Infrastructure.Database;

namespace Homely.Infrastructure.Repository
{
    public class MasterRepository : IMasterDataRepository
    {
        homely_Context db = new homely_Context();
        public List<ImageViewModel> GetAmenities()
        {
            var data = db.tbl_Master_Amenities.Where(x => x.Enabled == true).ToList().OrderBy(x => x.Order).Select(x => new ImageViewModel
            {
                Amenity = x.Amenity,
                AmenityId = (int)x.AmenityId,
                BigImageHoverPath = x.BigImageHoverPath,
                BigImagePath = x.BigImagePath,
                ImageHoverPath = x.ImageHoverPath,
                ImagePath = x.ImagePath

            }).ToList();
            return data;

        }

        public List<MetaDataViewModel> GetAvailability()
        {
            var data = db.tbl_Master_Availability.Where(x => x.Enabled == true).OrderBy(x => x.Order).Select(x => new MetaDataViewModel { id = (int)x.AvailabilityId, name = x.Availability }).ToList();
            return data;
        }

        public List<MetaDataViewModel> GetCityByStateId(long id)
        {
           var city= db.tblCity.Where(x=>x.fkStateId==id &&x.isActive==true && x.isDelete==false).Select(x=>new MetaDataViewModel {
               id=(int)x.pkCityId,
               name=x.cityName
           }).ToList();
            return city;
        }

        public List<MetaDataViewModel> GetFacing()
        {
            var data = db.tbl_Master_Facing.Where(x => x.Enabled == true).OrderBy(x => x.Order).Select(x => new MetaDataViewModel { id = (int)x.FacingId, name = x.Facing }).ToList();
            return data;
        }

        public List<MetaDataViewModel> GetFurnished()
        {
            var data = db.tbl_Master_Furnished.Where(x => x.Enabled == true).OrderBy(x => x.Order).Select(x => new MetaDataViewModel { id = (int)x.FurnishedId, name = x.Furnished }).ToList();
            return data;
        }

        public List<MetaDataViewModel> GetLocalityByCity(long id)
        {
            var city = db.tblArea.Where(x => x.fkCity == id && x.isActive == true && x.isDelete == false).Select(x => new MetaDataViewModel
            {
                id = (int)x.pkAreaId,
                name = x.areaName
            }).ToList();
            return city;
        }

        public List<MetaDataViewModel> GetOwnership()
        {
            var data = db.tbl_Master_Ownership.Where(x => x.Enabled == true).OrderBy(x => x.Order).Select(x => new MetaDataViewModel { id = (int)x.OwnershipId, name = x.Ownership }).ToList();
            return data;
        }

        public List<MetaDataViewModel> GetProperty()
        {
            var data = (from subPrp in db.tbl_Master_SubPropertyType
                        join propType in db.tbl_Master_PropertyType on subPrp.PropertyTypeID equals propType.PropertyTypeID
                        where subPrp.Enabled == true && propType.Enabled == true
                        orderby subPrp.Order
                        select new MetaDataViewModel
                        {
                            id = (int)subPrp.SubPropertyTypeID,
                            name = subPrp.SubPropertyType
                        }).ToList();
            return data;
        }

        public List<MetaDataViewModel> GetState()
        {
            var data = db.tblState.Where(x => x.isActive == true && x.isDelete == false).Select(x => new MetaDataViewModel { id = (int)x.pkStateId, name = x.stateName }).ToList();
            return data;
        }

        public List<MetaDataViewModel> GetTransaction()
        {
            var data = db.tbl_Master_Transaction.Where(x => x.Enabled == true).OrderBy(x => x.Order).Select(x => new MetaDataViewModel { id = (int)x.TransactionId, name = x.Transaction }).ToList();
            return data;
        }
        public List<MetaDataViewModel> GetBedrooms()
        {
            var data = db.tbl_Master_Bedroom.Where(x => x.isActive == true).OrderBy(x => x.bedroomId).Select(x => new MetaDataViewModel { id = (int)x.bedroomId, name = x.bedroomrestrictionName }).ToList();
            return data;

        }

        /// <summary>
        /// Get master data.
        /// </summary>
        /// <returns></returns>
        public List<Suitable> GetSuitable()
        {
            var data = db.tbl_Master_Suitable.Where(x => x.isActive == true).OrderBy(x => x.suitableId).Select(x => new Suitable { SuitableId = (int)x.suitableId, SuitableName = x.suitableName }).ToList();
            return data;

        }


        /// <summary>
        /// Responsible for QuickList quesry
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int QuickList(QuickList model)
        {
           
            var data = db.PROC_QuickListing(model.loggedUserEmail, model.For.ToString(), model.Name, model.email, model.Mobile);
            return data;
        }

        public List<MetaDataViewModel> GetMinimumContract()
        {
            var data = db.tbl_Master_MinimumContract.Where(x => x.isActive == true).OrderBy(x => x.contractId).Select(x => new MetaDataViewModel { id = (int)x.contractId, name = x.contractName }).ToList();
            return data;
        }

        public List<MetaDataViewModel> GetRestriction()
        {
            var data = db.tbl_Master_Restriction.Where(x => x.isActive == true).OrderBy(x => x.restrictionId).Select(x => new MetaDataViewModel { id = (int)x.restrictionId, name = x.restrictionName }).ToList();
            return data;
        }
    }
}
