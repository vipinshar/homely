using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homely.Common
{
    using Model;
    using System.Web.Mvc;
    public static class CommonFunction
    {
        public static List<SelectListItem> ConvertListToSelectListItem(List<MetaDataViewModel> model)
        {
            List<SelectListItem> lst = new List<SelectListItem>();
            foreach (var item in model)
            {
                lst.Add(new SelectListItem { Text = item.name, Value = item.id.ToString() });
            }
            return lst;
        }
    }
    /// <summary>
    /// 
    /// </summary>
    public enum city
    {
        delhi=41,
        gurgaon=7,
        noida=1,
        ghaziabad=14,
        faridabad=6,
        greaternoida=57,
        kundli= 43520
    }
}
