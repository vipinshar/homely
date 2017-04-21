using Homely.Common;
using Homely.Common.IRepository;
using Homely.Common.Model;
using Homely.Utility;
using PagedList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Homely.Controllers
{
    public class PropertyController : Controller
    {
        private IPropertyListingRepository _IPropertyListingRepository = null;
        private IMasterDataRepository _IMasterDataRepository = null;
        // GET: Property
        public PropertyController(IPropertyListingRepository IPropertyListingRepository, IMasterDataRepository IMasterDataRepository)
        {
            _IPropertyListingRepository = IPropertyListingRepository;
            _IMasterDataRepository = IMasterDataRepository;
        }

        /// <summary>
        /// Property lising page.
        /// </summary>
        /// <returns></returns>
        [Route("Property/Index")]
        public ActionResult Index()
        {
            string forRent = Request.Form["hdnForChk"];
            if (forRent == "2")
            {
                return RedirectToAction("SearchPropFromHomePage");
            }
            var prevState = Request.Form["State"];
            var prevCity = Request.Form["City"];
            var prevLoca = Request.Form["Locality"];
            var prevBHK = Request.Form["BHK"];
            List<MetaDataViewModel> _Propcity = new List<MetaDataViewModel>();
            List<MetaDataViewModel> _Proplocality = new List<MetaDataViewModel>();
            if (prevState != null)
            {
                _Propcity = _IMasterDataRepository.GetCityByStateId(Convert.ToInt64(prevState));
                _Proplocality = _IMasterDataRepository.GetLocalityByCity(Convert.ToInt64(prevCity));
                if (_Proplocality == null || _Proplocality.Count == 0)
                {
                    _Proplocality.Add(new MetaDataViewModel { id = -1, name = "Other" });
                }
            }
            PropertyViewModel model = new PropertyViewModel();
            var state = _IMasterDataRepository.GetState();
            SetMasterData(model);
            model.PropStateList = new SelectList(state, "id", "name", string.IsNullOrEmpty(prevState) ? 0 : Convert.ToInt32(prevState));

            model.PropertyCityList = new SelectList(_Propcity, "id", "name", string.IsNullOrEmpty(prevCity) ? 0 : Convert.ToInt32(prevCity));
            model.LocalityList = new SelectList(_Proplocality, "id", "name", string.IsNullOrEmpty(prevLoca) ? 0 : Convert.ToInt32(prevLoca));

            return View(model);
        }

        /// <summary>
        /// Post property.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="OwnerFile"></param>
        /// <param name="fileUploadProp"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Post(PropertyViewModel model, HttpPostedFileBase OwnerFile, List<HttpPostedFileBase> fileUploadProp)
        {
            if (ModelState.IsValid)
            {
                //var propCode = Request.Form["PropCode"];
                //model.PropCode = propCode;
                if (!User.Identity.IsAuthenticated)
                {
                    model.EmailId = "";
                }
                else
                {
                    model.EmailId = User.Identity.Name;
                }
                Random rr = new Random();
                model.pwd = rr.Next(10000, 99999).ToString();
                string result = _IPropertyListingRepository.SaveProperty(model);
                if (!string.IsNullOrEmpty(result))
                {
                    CommonUtility.SendMail("Property", model.OwnerName, model.OwnerEmailID, model.pwd, model.OwnerEmailID, "Property Listing");

                    ViewBag.Result = result;
                    DirectoryInfo ObjSearchDir = new DirectoryInfo(Server.MapPath("~/PropertyImages/" + result));
                    if (!ObjSearchDir.Exists)
                    {
                        ObjSearchDir.Create();
                    }

                    if (fileUploadProp != null && fileUploadProp.Count > 0)
                    {
                        string FolderPath = Server.MapPath("~/PropertyImages/" + result + "/");
                        foreach (var item in fileUploadProp)
                        {
                            if (item != null)
                                item.SaveAs(Path.Combine(FolderPath, item.FileName));
                        }

                        if (OwnerFile != null)
                        {
                            //Create folder for Member image
                            DirectoryInfo ObjSearchDirMember = new DirectoryInfo(Server.MapPath("~/PropertyImages/" + result + "/MemberImage"));
                            if (!ObjSearchDirMember.Exists)
                            {
                                ObjSearchDirMember.Create();
                            }

                            string FolderPathMember = Server.MapPath("~/PropertyImages/" + result + "/MemberImage/");
                            OwnerFile.SaveAs(Path.Combine(FolderPathMember, OwnerFile.FileName));
                        }
                    }
                    
                    return View("thankyouIndex",new SuccessMessageViewModel { message= "Your property has been listed successfully. Your unique Property Id is <b>: " + result + ".</b><br/>" } );
                }
            }
            else
            {
                ModelState.AddModelError("Error", "Some error please try again");
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// Responsible for getting city by state id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetCityByState(int id)
        {
            var cities = _IMasterDataRepository.GetCityByStateId(id);
            return Json(cities, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Responsible for getting locality by city id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetLocalityByCity(int id)
        {
            var loca = _IMasterDataRepository.GetLocalityByCity(id);
            return Json(loca, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Responsible for getting propeties by Cities.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("City/{id}", Name = "CityDetails")]
        public ActionResult PropertyByCity(string id)
        {
            RootObjectForSearchPage searchPage = new RootObjectForSearchPage();
            searchPage._bedroom = new SelectList(new List<MetaDataViewModel> { new MetaDataViewModel {id=1,name="1 BHK" },
                                                                               new MetaDataViewModel {id=2,name="2 BHK" },
                                                                               new MetaDataViewModel {id=3,name="3 BHK" },
                                                                               new MetaDataViewModel {id=4,name="4 BHK" },
                                                                               new MetaDataViewModel {id=5,name="5 BHK" },
                                                                               new MetaDataViewModel {id=6,name="PG" }
                                                                                                                }, "id", "name");
            searchPage._propertyType = new SelectList(_IMasterDataRepository.GetProperty(), "id", "name");
            searchPage._state = new SelectList(_IMasterDataRepository.GetState(), "id", "name");

            IPagedList<RentPropertyViewModel> lst = null;
            long cityId = 0;
            string For = "RENT";
            ViewBag.state = new SelectList(_IMasterDataRepository.GetState(), "id", "name");
            ViewBag.banner = "~/images/banner-img.jpg";

            if (!string.IsNullOrEmpty(id))
            {
                if (id != "RENT")
                {
                    if (id == "TDI City ( Kundli )")
                    {
                        cityId = _IPropertyListingRepository.GetTDICityId(id);
                    }
                    else
                    {
                        cityId = _IPropertyListingRepository.GetCityIdByName(id.Trim());
                    }

                    For = "CITY";
                    if (cityId > 0)
                    {
                        setBanner(cityId);
                        lst = _IPropertyListingRepository.GetRentProperty(Convert.ToInt32(cityId), For);
                        foreach (var item in lst)
                        {
                            DirectoryInfo myImageDir = new DirectoryInfo(System.Web.HttpContext.Current.Server.MapPath("~/PropertyImages/" + item.PropertyCode + "/"));
                            if (myImageDir.GetFiles().Length > 0)
                            {
                                try
                                {
                                    item.ImgUrl = "~/PropertyImages/" + item.PropertyCode + "/" + myImageDir.GetFiles()[0].Name;
                                }
                                catch (System.IO.DirectoryNotFoundException)
                                {
                                    item.ImgUrl = "~/PropertyImages/detials-img-big.jpg";
                                }
                            }
                            else
                            {
                                item.ImgUrl = "~/PropertyImages/detials-img-big.jpg";
                            }
                        }

                    }
                    else
                    {

                    }
                }
                else
                {
                    lst = _IPropertyListingRepository.GetRentProperty(0, "RENT");
                    foreach (var item in lst)
                    {
                        DirectoryInfo myImageDir = new DirectoryInfo(System.Web.HttpContext.Current.Server.MapPath("~/PropertyImages/" + item.PropertyCode + "/"));
                        if (myImageDir.GetFiles().Length > 0)
                        {
                            try
                            {
                                item.ImgUrl = "~/PropertyImages/" + item.PropertyCode + "/" + myImageDir.GetFiles()[0].Name;
                            }
                            catch (System.IO.DirectoryNotFoundException)
                            {
                                item.ImgUrl = "~/PropertyImages/detials-img-big.jpg";
                            }
                        }
                        else
                        {
                            item.ImgUrl = "~/PropertyImages/detials-img-big.jpg";
                        }
                    }
                }
            }
            ViewBag.cityTab = cityId;
            ViewBag.Names = lst;
            ViewBag._searchProp = lst;
            return View(searchPage);
        }

        /// <summary>
        /// Refine search section.
        /// </summary>
        /// <returns></returns>
        public ActionResult RefineSearch()
        {
            RootObjectForSearchPage model = new Common.Model.RootObjectForSearchPage();
            model._state = new SelectList(_IMasterDataRepository.GetState(), "id", "name");
            model.img = _IMasterDataRepository.GetAmenities();
            model._propertyType = new SelectList(_IMasterDataRepository.GetProperty(), "id", "name");
            model.minimumContractList = new SelectList(_IMasterDataRepository.GetMinimumContract(), "id", "name");

            var availability = _IMasterDataRepository.GetAvailability();
            var furnished = _IMasterDataRepository.GetFurnished();
            var ownership = _IMasterDataRepository.GetOwnership();
            model.AvailList = new SelectList(availability,"id","name");
            model.FurnishedList = new SelectList(furnished, "id", "name");
            model.OwnershipTypeList = ownership; 
            return PartialView("_RefineSearch", model);
        }

        /// <summary>
        /// Viewlisting pagination.
        /// </summary>
        /// <param name="page"></param>
        /// <param name="For"></param>
        /// <param name="city"></param>
        /// <returns></returns>
        public ActionResult ViewListingNavigation(int page, string For, int city)
        {
            var model = _IPropertyListingRepository.ViewLisingPaging(page, city, For);
            if (model != null && model.Count > 0)
            {
                foreach (var item in model)
                {
                    DirectoryInfo myImageDir = new DirectoryInfo(System.Web.HttpContext.Current.Server.MapPath("~/PropertyImages/" + item.PropertyCode + "/"));
                    if (myImageDir.GetFiles().Length > 0)
                    {
                        try
                        {
                            item.ImgUrl = "~/PropertyImages/" + item.PropertyCode + "/" + myImageDir.GetFiles()[0].Name;
                        }
                        catch (System.IO.DirectoryNotFoundException)
                        {
                            item.ImgUrl = "~/PropertyImages/detials-img-big.jpg";
                        }
                    }
                    else
                    {
                        item.ImgUrl = "~/PropertyImages/detials-img-big.jpg";
                    }
                }
            }

            ViewBag.Names = model;
            return PartialView("_ViewListingPartial", model);
        }

        /// <summary>
        /// Search properties.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SearchProp(RootObjectForSearchPage model)
        {
            RootObjectForSearchPage obj = new RootObjectForSearchPage();
            obj._bedroom = new SelectList(new List<MetaDataViewModel> { new MetaDataViewModel {id=1,name="1 BHK" },
                                                                               new MetaDataViewModel {id=2,name="2 BHK" },
                                                                               new MetaDataViewModel {id=3,name="3 BHK" },
                                                                               new MetaDataViewModel {id=4,name="4 BHK" },
                                                                               new MetaDataViewModel {id=5,name="5 BHK" },
                                                                               new MetaDataViewModel {id=6,name="PG" }}, "id", "name");
            ViewBag.banner = "~/images/banner-img.jpg";
            obj._propertyType = new SelectList(_IMasterDataRepository.GetProperty(), "id", "name");
            obj._state = new SelectList(_IMasterDataRepository.GetState(), "id", "name");
            if (model != null)
            {
                model.PropertyCode = model.PropertyCode == null ? "" : model.PropertyCode;
                if (model.budget != null)
                {
                    string[] ar = model.budget.Split('-');
                    if (ar != null && ar.Count() > 0)
                    {
                        model.minValue = Convert.ToInt32(ar[0]);
                        model.maxValue = Convert.ToInt32(ar[1]);
                    }
                }
                if (model.refine_Budget != null)
                {
                    string[] ar = model.refine_Budget.Split('-');
                    if (ar != null && ar.Count() > 0)
                    {
                        model.minValue = Convert.ToInt32(ar[0]);
                        model.maxValue = Convert.ToInt32(ar[1]);
                    }
                }
                if (model.City > 0)
                {
                    ViewBag.cityTab = model.City;
                    setBanner(model.City);
                }
                else
                {
                    if (model.refine_City > 0)
                    {
                        ViewBag.cityTab = model.refine_City;
                        setBanner(model.refine_City);
                    }
                }
            }
            var data = _IPropertyListingRepository.SearchProp(model);
            foreach (var item in data)
            {

                DirectoryInfo myImageDir = new DirectoryInfo(System.Web.HttpContext.Current.Server.MapPath("~/PropertyImages/" + item.PropertyCode + "/"));
                if (myImageDir.GetFiles().Length > 0)
                {
                    try
                    {
                        item.ImgUrl = "~/PropertyImages/" + item.PropertyCode + "/" + myImageDir.GetFiles()[0].Name;
                    }
                    catch (System.IO.DirectoryNotFoundException)
                    {
                        item.ImgUrl = "~/PropertyImages/detials-img-big.jpg";
                    }
                }
                else
                {
                    item.ImgUrl = "~/PropertyImages/detials-img-big.jpg";
                }
            }
            ViewBag.Names = data;
            if (model.refine_City > 0 | model.refine_Locality != null || model.refine_Locality != null)
            {
                ViewBag.cityTab = model.refine_City;
                ViewBag.Names = data;
                ViewBag._searchProp = data;
                return View("PropertyByCity",obj);
            }
            return PartialView("_ViewListingPartial", data);
        }
        //public ActionResult SearchProp(SearchViewModel model)
        //{
        //    if (model != null)
        //    {
        //        model.propCode = model.propCode == null ? "" : model.propCode;
        //        if (model.budget != null)
        //        {
        //            string[] ar = model.budget.Split('-');
        //            if (ar != null && ar.Count() > 0)
        //            {
        //                model.minValue = Convert.ToInt32(ar[0]);
        //                model.maxValue = Convert.ToInt32(ar[1]);
        //            }
        //        }
        //    }
        //    var data = _IPropertyListingRepository.SearchProp(model);
        //    foreach (var item in data)
        //    {

        //        DirectoryInfo myImageDir = new DirectoryInfo(System.Web.HttpContext.Current.Server.MapPath("~/PropertyImages/" + item.PropertyCode + "/"));
        //        if (myImageDir.GetFiles().Length > 0)
        //        {
        //            try
        //            {
        //                item.ImgUrl = "~/PropertyImages/" + item.PropertyCode + "/" + myImageDir.GetFiles()[0].Name;
        //            }
        //            catch (System.IO.DirectoryNotFoundException)
        //            {
        //                item.ImgUrl = "~/PropertyImages/detials-img-big.jpg";
        //            }
        //        }
        //        else
        //        {
        //            item.ImgUrl = "~/PropertyImages/detials-img-big.jpg";
        //        }
        //    }
        //    ViewBag.Names = data;
        //    return PartialView("_ViewListingPartial", data);
        //}

        /// <summary>
        /// Responsible for deliver data if user search property from homepage.
        /// </summary>
        /// <returns></returns>
        public ActionResult SearchPropFromHomePage()
        {
            long cityId = 0;
            RootObjectForSearchPage model = new RootObjectForSearchPage();
            model.City = Convert.ToInt32(Request.Form["City"]);
            var local = Request.Form["Locality"];
            model.Locality = 0;
            if (!string.IsNullOrEmpty(local))
            {
                model.Locality = Convert.ToInt32(Request.Form["Locality"]);
            }

            model.bedroom = Request.Form["BHK"];
            model.budget = Request.Form["Budget"];
            if (model != null)
            {
                cityId = model.City;
                setBanner(cityId);
                model.PropertyCode = model.PropertyCode == null ? "" : model.PropertyCode;
                if (model.budget != null)
                {
                    string[] ar = model.budget.Split('-');
                    if (ar != null && ar.Count() > 0)
                    {
                        model.minValue = Convert.ToInt32(ar[0]);
                        model.maxValue = Convert.ToInt32(ar[1]);
                    }
                }
            }
            var data = _IPropertyListingRepository.SearchProp(model);
            foreach (var item in data)
            {

                DirectoryInfo myImageDir = new DirectoryInfo(System.Web.HttpContext.Current.Server.MapPath("~/PropertyImages/" + item.PropertyCode + "/"));
                if (myImageDir.GetFiles().Length > 0)
                {
                    try
                    {
                        item.ImgUrl = "~/PropertyImages/" + item.PropertyCode + "/" + myImageDir.GetFiles()[0].Name;
                    }
                    catch (System.IO.DirectoryNotFoundException)
                    {
                        item.ImgUrl = "~/PropertyImages/detials-img-big.jpg";
                    }
                }
                else
                {
                    item.ImgUrl = "~/PropertyImages/detials-img-big.jpg";
                }
            }
            ViewBag.Names = data;
            ViewBag.cityTab = cityId;
            model._bedroom = new SelectList(new List<MetaDataViewModel> { new MetaDataViewModel {id=1,name="1 BHK" },
                                                                               new MetaDataViewModel {id=2,name="2 BHK" },
                                                                               new MetaDataViewModel {id=3,name="3 BHK" },
                                                                               new MetaDataViewModel {id=4,name="4 BHK" },
                                                                               new MetaDataViewModel {id=5,name="5 BHK" },
                                                                               new MetaDataViewModel {id=6,name="PG" }
                                                                                                                }, "id", "name");
            model._propertyType = new SelectList(_IMasterDataRepository.GetProperty(), "id", "name");
            model._state = new SelectList(_IMasterDataRepository.GetState(), "id", "name");
            ViewBag._searchProp = data;
           
            return View("PropertyByCity", model);
        }

        /// <summary>
        /// Responsible for set banner.
        /// </summary>
        /// <param name="cityId"></param>
        public void setBanner(long cityId)
        {
            #region set Banner 
            switch (cityId)
            {
                case 41:
                    ViewBag.banner = "~/images/delhi-banner.jpg";
                    break;
                case 3:
                    ViewBag.banner = "~/images/ghaziabad-banner.jpg";
                    break;
                case 1:
                    ViewBag.banner = "~/images/noida-banner.jpg";
                    break;
                case 14:
                    ViewBag.banner = "~/images/ghaziabad-banner.jpg";
                    break;
                case 6:
                    ViewBag.banner = "~/images/faridabad-banner.jpg";
                    break;
                case 57:
                    ViewBag.banner = "~/images/greater-noida.jpg";
                    break;
                case 213:
                    ViewBag.banner = "~/images/banner-img.jpg";
                    break;


            }
            #endregion
        }

        /// <summary>
        /// Get Detail
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetDetail()
        {
            GetPropertyViewModel model = new GetPropertyViewModel();
            model.id = Request.Form["prop_uniqId"];
            model.password = Request.Form["prop_password"];

            List<int> lstAmen = new List<int>();
            List<int> lstSuitable = new List<int>();
            var data = _IPropertyListingRepository.GetPropertyById(model);
            if (data != null)
            {
                if (!string.IsNullOrEmpty(data.editAmenitiesList))
                {
                    lstAmen = data.editAmenitiesList.Split(',').Select(x => int.Parse(x)).ToList();
                }
                if (!string.IsNullOrEmpty(data.editSuitableList))
                {
                    lstSuitable = data.editSuitableList.Split(',').Select(x => int.Parse(x)).ToList();
                }
                var result = SetMasterWithEdit(data, lstAmen, lstSuitable);
                return View("Index", result);
            }
            TempData["error"] = "PROPERTY NOT FOUND";
            return RedirectToAction("Index","Property");
        }

        #region Private method
        private List<int> ConvertStringToList(string str)
        {
            List<int> lst = str.Split(',').Select(x => int.Parse(x)).ToList();
            return lst;
        }
        private void SetMasterData(PropertyViewModel model)
        {
            var amenities = _IMasterDataRepository.GetAmenities();
            var availability = _IMasterDataRepository.GetAvailability();
            var facing = _IMasterDataRepository.GetFacing();
            var furnished = _IMasterDataRepository.GetFurnished();
            var ownership = _IMasterDataRepository.GetOwnership();
            var property = _IMasterDataRepository.GetProperty();
            var state = _IMasterDataRepository.GetState();
            var transaction = _IMasterDataRepository.GetTransaction();
            var suitable = _IMasterDataRepository.GetSuitable();
            var minimumCont = _IMasterDataRepository.GetMinimumContract();
            var restr = _IMasterDataRepository.GetRestriction();
            var bedro = _IMasterDataRepository.GetBedrooms();


            List<MetaDataViewModel> atm = new List<MetaDataViewModel>();
            atm.Add(new MetaDataViewModel { id = 1, name = "1" });
            atm.Add(new MetaDataViewModel { id = 2, name = "2" });
            atm.Add(new MetaDataViewModel { id = 3, name = "3" });
            atm.Add(new MetaDataViewModel { id = 4, name = "3+" });

            List<MetaDataViewModel> balcony = new List<MetaDataViewModel>();
            balcony.Add(new MetaDataViewModel { id = 1, name = "Not Available" });
            balcony.Add(new MetaDataViewModel { id = 2, name = "1" });
            balcony.Add(new MetaDataViewModel { id = 3, name = "2" });
            balcony.Add(new MetaDataViewModel { id = 4, name = "3" });
            balcony.Add(new MetaDataViewModel { id = 5, name = "4" });

            List<MetaDataViewModel> floor = new List<MetaDataViewModel>();
            floor.Add(new MetaDataViewModel { id = 1, name = "1" });
            floor.Add(new MetaDataViewModel { id = 2, name = "2" });
            floor.Add(new MetaDataViewModel { id = 3, name = "3" });
            floor.Add(new MetaDataViewModel { id = 4, name = "4" });
            floor.Add(new MetaDataViewModel { id = 5, name = "5" });
            floor.Add(new MetaDataViewModel { id = 6, name = "6" });
            floor.Add(new MetaDataViewModel { id = 7, name = "7" });
            floor.Add(new MetaDataViewModel { id = 8, name = "8" });
            floor.Add(new MetaDataViewModel { id = 9, name = "9" });
            floor.Add(new MetaDataViewModel { id = 10, name = "10" });
            floor.Add(new MetaDataViewModel { id = 11, name = "11" });
            floor.Add(new MetaDataViewModel { id = 12, name = "12" });
            floor.Add(new MetaDataViewModel { id = 13, name = "13" });
            floor.Add(new MetaDataViewModel { id = 14, name = "14+" });

            List<MetaDataViewModel> st = new List<MetaDataViewModel>();


            model.FloorBuildingList = new SelectList(floor, "id", "name");
            model.amenities = amenities;
            model.FurnishedList = new SelectList(furnished, "id", "name");
            model.OwnershipTypeList = ownership;
            model.OwnerStateList = new SelectList(state, "id", "name");
            model.SubPropertyTypeList = new SelectList(property, "id", "name");
            model.transactionList = transaction;

            model.BalcontList = new SelectList(balcony, "id", "name");
            model.BathroomList = new SelectList(atm, "id", "name");
            model.BedroomList = new SelectList(bedro, "id", "name");
            model.FacingList = new SelectList(facing, "id", "name");

            model.AvailList = new SelectList(availability, "id", "name");
            model.suitableList = suitable;
            model.minimumContractList = new SelectList(minimumCont, "id", "name");
            model.restrictionList = new SelectList(restr, "id", "name");

        }
        private PropertyViewModel SetMasterWithEdit(PropertyViewModel model, List<int> _amen, List<int> _suita)
        {
            if (model != null)
            {
                List<MetaDataViewModel> _Propcity = new List<MetaDataViewModel>();
                List<MetaDataViewModel> _Proplocality = new List<MetaDataViewModel>();
                var amenities = _IMasterDataRepository.GetAmenities();
                var availability = _IMasterDataRepository.GetAvailability();
                var facing = _IMasterDataRepository.GetFacing();
                var furnished = _IMasterDataRepository.GetFurnished();
                var ownership = _IMasterDataRepository.GetOwnership();
                var property = _IMasterDataRepository.GetProperty();
                var state = _IMasterDataRepository.GetState();
                var transaction = _IMasterDataRepository.GetTransaction();
                var suitable = _IMasterDataRepository.GetSuitable();
                var minimumCont = _IMasterDataRepository.GetMinimumContract();
                var restr = _IMasterDataRepository.GetRestriction();
                var bedro = _IMasterDataRepository.GetBedrooms();

                var oldStateCity = _IPropertyListingRepository.GetPropCityState(model.Locality??0);

                List<MetaDataViewModel> atm = new List<MetaDataViewModel>();
                atm.Add(new MetaDataViewModel { id = 1, name = "1" });
                atm.Add(new MetaDataViewModel { id = 2, name = "2" });
                atm.Add(new MetaDataViewModel { id = 3, name = "3" });
                atm.Add(new MetaDataViewModel { id = 4, name = "3+" });

                List<MetaDataViewModel> balcony = new List<MetaDataViewModel>();
                balcony.Add(new MetaDataViewModel { id = 1, name = "Not Available" });
                balcony.Add(new MetaDataViewModel { id = 2, name = "1" });
                balcony.Add(new MetaDataViewModel { id = 3, name = "2" });
                balcony.Add(new MetaDataViewModel { id = 4, name = "3" });
                balcony.Add(new MetaDataViewModel { id = 5, name = "4" });

                List<MetaDataViewModel> floor = new List<MetaDataViewModel>();
                floor.Add(new MetaDataViewModel { id = 1, name = "1" });
                floor.Add(new MetaDataViewModel { id = 2, name = "2" });
                floor.Add(new MetaDataViewModel { id = 3, name = "3" });
                floor.Add(new MetaDataViewModel { id = 4, name = "4" });
                floor.Add(new MetaDataViewModel { id = 5, name = "5" });
                floor.Add(new MetaDataViewModel { id = 6, name = "6" });
                floor.Add(new MetaDataViewModel { id = 7, name = "7" });
                floor.Add(new MetaDataViewModel { id = 8, name = "8" });
                floor.Add(new MetaDataViewModel { id = 9, name = "9" });
                floor.Add(new MetaDataViewModel { id = 10, name = "10" });
                floor.Add(new MetaDataViewModel { id = 11, name = "11" });
                floor.Add(new MetaDataViewModel { id = 12, name = "12" });
                floor.Add(new MetaDataViewModel { id = 13, name = "13" });
                floor.Add(new MetaDataViewModel { id = 14, name = "14+" });

                List<MetaDataViewModel> st = new List<MetaDataViewModel>();
                model.FloorBuildingList = new SelectList(floor, "id", "name", model.Floor);

                foreach (var item in _amen)
                {
                    amenities.Where(x => x.AmenityId == item).ToList().ForEach(x => x.isSelected = true);
                }
                foreach (var item in _suita)
                {
                    suitable.Where(x => x.SuitableId == item).ToList().ForEach(x => x.isSelected = true);
                }
                model.amenities = amenities;
                model.FurnishedList = new SelectList(furnished, "id", "name", model.Furnished);
                model.OwnershipTypeList = ownership;
                model.OwnerStateList = new SelectList(state, "id", "name", model.PropertyState);
                model.SubPropertyTypeList = new SelectList(property, "id", "name", model.SubPropertyType);
                model.transactionList = transaction;

                model.BalcontList = new SelectList(balcony, "id", "name", model.Balcony);
                model.BathroomList = new SelectList(atm, "id", "name", model.Bathroom);
                model.BedroomList = new SelectList(bedro, "id", "name", model.Bedroom);
                model.FacingList = new SelectList(facing, "id", "name", model.Facing);

                model.AvailList = new SelectList(availability, "id", "name", model.Availability);
                model.suitableList = suitable;
                model.minimumContractList = new SelectList(minimumCont, "id", "name", model.minimumContract);
                model.restrictionList = new SelectList(restr, "id", "name", model.restriction);

                model.PropStateList = new SelectList(state, "id", "name", 0);

                model.PropertyCityList = new SelectList(_Propcity, "id", "name", 0);
                model.LocalityList = new SelectList(_Proplocality, "id", "name", 0);

                var localitySity = _IPropertyListingRepository.GetPropCityState(model.Locality);

                if (localitySity != null)
                {
                    var newCityList = _IMasterDataRepository.GetCityByStateId(localitySity.pkStateId).ToList();
                    var areaList = _IMasterDataRepository.GetLocalityByCity(localitySity.pkCityId).ToList();
                    if (newCityList != null && newCityList.Count != 0)
                    {
                        model.PropertyCityList = new SelectList(newCityList, "id", "name", 0);
                    }
                    if (areaList != null && areaList.Count != 0)
                    {
                        model.LocalityList = new SelectList(areaList, "id", "name", 0);
                    }
                    model.PropertyState = (int)localitySity.pkStateId;
                }
            }
            return model;
        }

        #endregion
    }
}