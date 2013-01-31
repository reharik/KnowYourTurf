using CC.Core.CoreViewModelAndDTOs;
using KnowYourTurf.Core;
using KnowYourTurf.Core.Domain;

namespace KnowYourTurf.Web.Models
{
    public class POCommitViewModel : ListViewModel
    {
        public string _ClosePOUrl { get; set; }
        public string _POLIUrl { get; set; }
        public string VendorCompany { get; set; }
    }
}