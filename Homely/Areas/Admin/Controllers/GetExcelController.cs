using Homely.Areas.Admin.Models;
using Homely.Common.IAdminRepository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Homely.Areas.Admin.Controllers
{
    public class GetExcelController : Controller
    {
        private IPropertyExcelRepository _IPropertyExcelRepository = null;
        public GetExcelController(IPropertyExcelRepository IPropertyExcelRepository)
        {
            _IPropertyExcelRepository = IPropertyExcelRepository;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View(new GetExcelViewModel());
        }
        [HttpPost]
        public ActionResult Index(GetExcelViewModel model)
        {
            DateTime fromDate = new DateTime();
            DateTime toDate = new DateTime();

            if (!string.IsNullOrEmpty(model.FromDate))
            {
                fromDate = Convert.ToDateTime(model.FromDate);
            }
            else { fromDate = DateTime.UtcNow.AddDays(-30); }
            if (!string.IsNullOrEmpty(model.ToDate))
            {
                fromDate = Convert.ToDateTime(model.ToDate);
            }
            else { toDate = DateTime.UtcNow; }

            var data = _IPropertyExcelRepository.GetPropertyCode(model.CityId, fromDate, toDate);
            List<string> propImage = new List<string>();

            if (data != null && data.Count != 0)
            {
                foreach (var item in data)
                {
                    DirectoryInfo myImageDir = new DirectoryInfo(System.Web.HttpContext.Current.Server.MapPath("~/PropertyImages/" + item + "/"));
                    if (myImageDir.GetFiles().Length > 0)
                    {
                       
                    }
                    else
                    {
                        propImage.Add(item);
                    }
                }
            }
            if (propImage != null && propImage.Count != 0)
            {
                var lst = _IPropertyExcelRepository.GetInfo(propImage);
                if (lst != null && lst.Count != 0)
                {
                    string result = RenderRazorViewToString("/Areas/Admin/Views/GetExcel/Excel.cshtml", lst);
                    if (!string.IsNullOrEmpty(result))
                    {

                        return File(Encoding.ASCII.GetBytes(result), "application/vnd.ms-excel", "PropertyDetails.xls");
                    }
                }
            }

            return View();
        }

        public string RenderRazorViewToString(string viewName, object model)
        {
            ViewData.Model = model;
            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext,
                                                                         viewName);
                var viewContext = new ViewContext(ControllerContext, viewResult.View,
                                             ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);
                viewResult.ViewEngine.ReleaseView(ControllerContext, viewResult.View);
                return sw.GetStringBuilder().ToString();
            }
        }
    }


}