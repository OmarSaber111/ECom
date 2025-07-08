using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Core.Sharing
{
    public class ProductParams
    {
        //string sort,int? categoryid, int pagesize, int pagenumber
        public string? sort { get; set; }
        public int? categoryid { get; set; }
        public int pagesize { get; set; }
        public int pagenumber { get; set; }
        public string? Search { get; set; }
    }
}
