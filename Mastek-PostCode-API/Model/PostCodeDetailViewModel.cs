using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mastek_PostCode_API.Model
{
    public class PostCodeDetailViewModel
    {
        public string PostCode { get; set; }
        public string Country { get; set; }
        public string Region { get; set; }
        public string AdminDistrict { get; set; }
        public string ParliamentaryConstituency { get; set; }
        public string Area { get; set; }
    }
}
