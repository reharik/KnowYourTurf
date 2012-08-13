using System.Collections.Generic;
using System.Web.Mvc;
using KnowYourTurf.Core;

namespace KnowYourTurf.Web.Models
{
    public class POListViewModel : ViewModel
    {
        public IEnumerable<SelectListItem> _VendorEntityIdList { get; set; }
        public int VendorEntityId { get; set; }
        public string VendorCompany { get; set; }
        public string _ReturnUrl { get; set; }
        public string _CommitUrl { get; set; }
        public string _VendorProductsUrl { get; set; }
        public string _POLIUrl { get; set; }
    }
}