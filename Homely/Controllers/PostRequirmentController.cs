using Homely.Common.IRepository;
using Homely.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Homely.Controllers
{
    public class PostRequirmentController : Controller
    {
        private IPropertyListingRepository _IPropertyListingRepository = null;
        private IMasterDataRepository _IMasterDataRepository = null;
        private IPostRequirmentRepository _IPostRequirmentRepository = null;

        public PostRequirmentController(IPropertyListingRepository IPropertyListingRepository, IMasterDataRepository IMasterDataRepository, IPostRequirmentRepository IPostRequirmentRepository)
        {
            _IPropertyListingRepository = IPropertyListingRepository;
            _IMasterDataRepository = IMasterDataRepository;
            _IPostRequirmentRepository = IPostRequirmentRepository;
        }

        // GET: PostRequirment
        [Route("PostRequirment/Index")]
        public ActionResult Index()
        {
            PostRequirmentViewModel model = new PostRequirmentViewModel();
            var amenities = _IMasterDataRepository.GetAmenities();
            var property = _IMasterDataRepository.GetProperty();
            var transaction = _IMasterDataRepository.GetTransaction();
            var statelist = _IMasterDataRepository.GetState();
           

            List<ChkBox> lst = new List<ChkBox>();
            lst.Add(new ChkBox { name = "1", val = 1, isSelect = true });
            lst.Add(new ChkBox { name = "2", val = 2, isSelect = false });
            lst.Add(new ChkBox { name = "3", val = 3, isSelect = false });
            lst.Add(new ChkBox { name = "4", val = 4, isSelect = false });
            lst.Add(new ChkBox { name = "5", val = 5, isSelect = false });

            model.amenities = amenities;
            model.chbox = lst;
            model.SubPropertyTypeList = new SelectList(property, "id", "name");
            model.OwnerStateList = new SelectList(statelist, "id", "name");
            model.minValue = 5000;
            model.maxValue = 20000;
            model.BedroomList = new SelectList(new List<MetaDataViewModel> { new MetaDataViewModel {id=1,name="1 BHK" },
                                                                               new MetaDataViewModel {id=2,name="2 BHK" },
                                                                               new MetaDataViewModel {id=3,name="3 BHK" },
                                                                               new MetaDataViewModel {id=4,name="4 BHK" },
                                                                               new MetaDataViewModel {id=5,name="5 BHK" },
                                                                               new MetaDataViewModel {id=6,name="PG" }
                                                                                                                }, "id", "name");
            return View(model);
        }

        [HttpPost]
        [Route("PostRequirment/Index")]
        public ActionResult Index(PostRequirmentViewModel model)
        {
           string s= HttpContext.User.Identity.Name;

            string loggedUser = string.Empty;
            if (User.Identity.IsAuthenticated)
            {
                loggedUser = User.Identity.Name;
            }
            model.email = loggedUser;
            int result = _IPostRequirmentRepository.PostRequirment(model);
            if (result > 0)
            {
                return View("thankyouIndex", new SuccessMessageViewModel { message = "Your requirment has been registered. We will contact you as soon as possible with solutions."});
            }
            return RedirectToAction("Index");
        }
    }
}