using Homely.Common.IRepository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Homely.Controllers
{
    /// <summary>
    /// Compare controller
    /// </summary>
    public class ComparePropertyController : Controller
    {
        private IPropertyListingRepository _IPropertyListingRepository = null;
        public ComparePropertyController(IPropertyListingRepository IPropertyListingRepository)
        {
            _IPropertyListingRepository = IPropertyListingRepository;
        }
        public ActionResult Index(string id)
        {
            string propA = string.Empty;
            string propB = string.Empty;
            string propC = string.Empty;

            if (!string.IsNullOrEmpty(id))
            {
                string request = id.TrimEnd(',');

                string[] arr = request.Split(',');
                if (arr.Length > 1 && arr.Length<4)
                {
                    switch (arr.Length)
                    {
                        case 1:
                            propA = arr[0];
                            break;
                        case 2:
                            propA = arr[0];
                            propB = arr[1];
                            break;
                        case 3:
                            propA = arr[0];
                            propB = arr[1];
                            propC = arr[2];
                            break;
                    }

                    var data = _IPropertyListingRepository.CompareProperty(propA, propB, propC);
                    if (data != null && data.Count != 0)
                    {
                        for (int i = 0; i < data.Count; i++)
                        {
                            var amenList = _IPropertyListingRepository.GetAmenities(data[i].PropertyCode);
                            if (amenList != null && amenList.Count != 0)
                            {
                                data[i].amenLst.AddRange(amenList);
                            }

                            DirectoryInfo myImageDir = new DirectoryInfo(System.Web.HttpContext.Current.Server.MapPath("~/PropertyImages/" + data[i].PropertyCode+ "/"));
                            if (myImageDir.GetFiles().Length > 0)
                            {
                                try
                                {
                                    data[i].image = "~/PropertyImages/" + data[i].PropertyCode + "/" + myImageDir.GetFiles()[0].Name;
                                   
                                }
                                catch (System.IO.DirectoryNotFoundException)
                                {
                                    data[i].image = "~/PropertyImages/default-image.jpg";
                                }
                            }
                            else
                            {
                                data[i].image = "~/PropertyImages/default-image.jpg";
                            }
                        }
                    }
                    return View(data);
                }

            }
            return RedirectToAction("Index", "Home");
        }
    }
}