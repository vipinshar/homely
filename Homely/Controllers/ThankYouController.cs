using Homely.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Homely.Controllers
{
    public class ThankYouController : Controller
    {
      [Route("ThankYou/Index")]
        public ActionResult Index()
        {
            SuccessMessageViewModel model = new SuccessMessageViewModel();
            return View(model);
        }
    }
}