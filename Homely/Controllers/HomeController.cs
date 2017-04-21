using Homely.Common.IRepository;
using Homely.Common.Model;
using Homely.Common.Models;
using Homely.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace Homely.Controllers
{
    public class HomeController : Controller
    {

        private IHomePageRepository _IHomePageRepository = null;
        private IMasterDataRepository _IMasterDataRepository = null;
        public HomeController(IHomePageRepository IHomePageRepository, IMasterDataRepository IMasterDataRepository)
        {
            _IHomePageRepository = IHomePageRepository;
            _IMasterDataRepository = IMasterDataRepository;
        }

        /// <summary>
        /// Home page 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.OwnerShip = _IMasterDataRepository.GetOwnership();

            SelectList ls = new SelectList(_IMasterDataRepository.GetState(), "id", "name");
            ViewBag.State = ls;
            var data = _IHomePageRepository.GetPropCount();
            if (data != null && data.Count != 0)
            {
                ViewBag.countDelhi = data[0];
                ViewBag.countgurg = data[1];
                ViewBag.countnoida = data[2];
                ViewBag.countgzb = data[3];

                ViewBag.countfari = data[4];
                ViewBag.countgreater = data[5];
                ViewBag.countkundli = data[6];
            }
            return View();
        }

        /// <summary>
        /// Responsible for Product detail.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("{id}", Name = "ProductDetails")]
        public ActionResult Details(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                string[] str = id.Split('-');
                if (str != null && str.Count() > 0)
                {
                    var _id = str[0].ToString();
                    var data = _IHomePageRepository.GetPropertyDetail(_id);
                    if (data != null)
                    {
                        ViewBag.Amen = _IHomePageRepository.GetAmenities(_id);
                        List<string> strImg = new List<string>();
                        DirectoryInfo myImageDir = new DirectoryInfo(System.Web.HttpContext.Current.Server.MapPath("~/PropertyImages/" + _id + "/"));
                        if (myImageDir.GetFiles().Length > 0)
                        {
                            try
                            {

                                foreach (var _item in myImageDir.GetFiles().ToList())
                                {
                                    strImg.Add("PropertyImages/" + _id + "/" + _item.Name);
                                }
                                ViewBag.propImage = strImg;
                            }
                            catch (System.IO.DirectoryNotFoundException)
                            {

                            }
                        }
                        else
                        {

                        }



                        return View(data);
                    }
                    return RedirectToAction("Index", "Home");

                }
            }
            return View();
        }

        /// <summary>
        /// Homepage featured lising.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public PartialViewResult FeaturedListing(int id)
        {
            var model = _IHomePageRepository.GetFeaturedPartial(id);
            if (model != null && model.Count > 0)
            {

                foreach (var item in model)
                {
                    DirectoryInfo myImageDir = new DirectoryInfo(System.Web.HttpContext.Current.Server.MapPath("~/PropertyImages/" + item.PropertyCode + "/"));
                    if (myImageDir.GetFiles().Length > 0)
                    {
                        try
                        {
                            item.imageUrl = "~/PropertyImages/" + item.PropertyCode + "/" + myImageDir.GetFiles()[0].Name;
                        }
                        catch (System.IO.DirectoryNotFoundException)
                        {
                            item.imageUrl = "~/PropertyImages/detials-img-big.jpg";
                        }
                    }
                    else
                    {
                        item.imageUrl = "~/PropertyImages/detials-img-big.jpg";
                    }
                }
            }

            ViewBag.Names = model;
            ViewBag.currentCity = 72;
            return PartialView("_featuredPartial");
        }

        /// <summary>
        /// Hot properties.
        /// </summary>
        /// <returns></returns>
        public PartialViewResult HotProperties()
        {
            var model = _IHomePageRepository.GetHotProperties();

            if (model != null && model.Count > 0)
            {
                model.ForEach(x => x.Url = FriendlyURLTitle(x.Url));
                model.ForEach(x => x.Url.Replace(',', '-'));
                foreach (var item in model)
                {
                    DirectoryInfo myImageDir = new DirectoryInfo(System.Web.HttpContext.Current.Server.MapPath("~/PropertyImages/" + item.PropertyCode + "/"));
                    if (myImageDir.GetFiles().Length > 0)
                    {
                        try
                        {
                            item.imageUrl = "~/PropertyImages/" + item.PropertyCode + "/" + myImageDir.GetFiles()[0].Name;
                        }
                        catch (System.IO.DirectoryNotFoundException)
                        {
                            item.imageUrl = "~/PropertyImages/detials-img-big.jpg";
                        }
                    }
                    else
                    {
                        item.imageUrl = "~/PropertyImages/detials-img-big.jpg";
                    }
                }
            }
            return PartialView("_hotProperties", model);
        }

        /// <summary>
        /// Responsible for Propery Views.
        /// </summary>
        /// <returns></returns>
        public PartialViewResult Review()
        {
            var data = _IHomePageRepository.GetReview();


            return PartialView("_hotProperties", data);
        }

        /// <summary>
        /// Responsible for homepage property counting like 100 freshlising and 50 new .
        /// </summary>
        /// <returns></returns>
        public PartialViewResult FreshCount()
        {
            var data = _IHomePageRepository.GetFreshCount();
            return PartialView("_FreshListingPartial", data);
        }

        /// <summary>
        /// Responsible for navigate property.
        /// </summary>
        /// <param name="page"></param>
        /// <param name="For"></param>
        /// <returns></returns>
        public ActionResult PropertyNavigation(int page, string For, int city)
        {
            //if (Session["cityId"] != null)
            //{

            var model = _IHomePageRepository.GetPropertyByCityAndPage(page, city, For);
            if (model != null && model.Count > 0)
            {

                foreach (var item in model)
                {
                    DirectoryInfo myImageDir = new DirectoryInfo(System.Web.HttpContext.Current.Server.MapPath("~/PropertyImages/" + item.PropertyCode + "/"));
                    if (myImageDir.GetFiles().Length > 0)
                    {
                        try
                        {
                            item.imageUrl = "~/PropertyImages/" + item.PropertyCode + "/" + myImageDir.GetFiles()[0].Name;
                        }
                        catch (System.IO.DirectoryNotFoundException)
                        {
                            item.imageUrl = "~/PropertyImages/detials-img-big.jpg";
                        }
                    }
                    else
                    {
                        item.imageUrl = "~/PropertyImages/detials-img-big.jpg";
                    }
                }
            }

            ViewBag.Names = model;
            return PartialView("_featuredPartial");
            //}
            //return RedirectToAction("Index");
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        /// <summary>
        /// responsible for creating url.
        /// </summary>
        /// <param name="pTitle"></param>
        /// <returns></returns>
        string FriendlyURLTitle(string pTitle)
        {
            pTitle = pTitle.Replace(" ", "-");
            pTitle = pTitle.Replace(",", "");
            pTitle = HttpUtility.UrlEncode(pTitle);
            pTitle = pTitle.Replace("%2f", "/");
            return Regex.Replace(pTitle, "%[0-9A-Fa-f]{2}", "");
        }

        /// <summary>
        /// Responsible for quick contact.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult QuickListing(QuickList model)
        {
            if (User.Identity.IsAuthenticated)
            {
                model.loggedUserEmail = User.Identity.Name;
            }

            int result = _IMasterDataRepository.QuickList(model);
            return Json(result, JsonRequestBehavior.AllowGet);
        }



        /// <summary>
        /// Responsible for register and login.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>


    }

    public class PagingCustom
    {
        public int page { get; set; }
        public int city { get; set; }
        public string For { get; set; }
    }
    public class Counting
    {
        public long count { get; set; }
    }
}