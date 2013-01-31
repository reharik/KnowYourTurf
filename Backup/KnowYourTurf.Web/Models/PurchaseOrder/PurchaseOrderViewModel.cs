using System.Collections.Generic;
using System.Web.Mvc;
using CC.Core.CoreViewModelAndDTOs;
using Castle.Components.Validator;
using KnowYourTurf.Core;

namespace KnowYourTurf.Web.Models
{
    public class POListViewModel : ViewModel
    {
        public IEnumerable<SelectListItem> _VendorEntityIdList { get; set; }
        [ValidateNonEmpty]
        public int VendorEntityId { get; set; }
        public string VendorCompany { get; set; }
        public string _commitPOUrl { get; set; }
        public string _vendorProductsUrl { get; set; }
        public string _POLIUrl { get; set; }

        public string _addToOrderUrl { get; set; }

        public string _removePOLItemUrl { get; set; }

        public string _editPOLItemUrl { get; set; }

        public string _editPOLItemTitle { get; set; }
    }
}
