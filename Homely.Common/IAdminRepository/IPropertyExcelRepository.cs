using Homely.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homely.Common.IAdminRepository
{
    public interface IPropertyExcelRepository
    {
        List<string> GetPropertyCode(int city, DateTime? from, DateTime to);

        List<PropertyInfo> GetInfo(List<string> lst);
    }
}
