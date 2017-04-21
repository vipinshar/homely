using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homely.Common.Model
{
   public class SearchViewModel
    {
        public int city { get; set; }

        public int localityId { get; set; }

        public int bedroom { get; set; }

        public string budget { get; set; }

        public int propType { get; set; }

        public string propCode { get; set; }

        public int minValue { get; set; }

        public int maxValue { get; set; }

        public int avail { get; set; }

        public int furnish { get; set; }

        public int ownership { get; set; }
    }

   
}
