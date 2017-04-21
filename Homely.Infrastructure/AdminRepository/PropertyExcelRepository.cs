using Homely.Common.IAdminRepository;
using Homely.Common.Model;
using Homely.Infrastructure.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homely.Infrastructure.AdminRepository
{
    public class PropertyExcelRepository: IPropertyExcelRepository
    {
        homely_Context db = new homely_Context();

        /// <summary>
        /// Get property code.
        /// </summary>
        /// <param name="city"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public List<string> GetPropertyCode(int city, DateTime? from, DateTime to)
        {
            var codes = db.tbl_Property.Where(x => x.PropertyCity == city && x.CreateDate <= from && x.CreateDate >= to && x.Enabled==true).Select(x=>x.PropertyCode).ToList();
            return codes;
        }


        public List<PropertyInfo> GetInfo(List<string> lst)
        {
            List<PropertyInfo> _lst = new List<PropertyInfo>();
            foreach (var item in lst)
            {
                var data = db.PROC_PropertyDetails(item).FirstOrDefault();
              

                if (data != null)
                    _lst.Add(new PropertyInfo {
                        City=data.City,
                        Locality=data.Locality,
                        OwnerContact=data.Mobile,
                        PropertyId=item,
                        Society=data.SocietyName,
                        OwnerName=data.OwnerName
                    });

            }
            return _lst;
        }
    }
}
