using KnowYourTurf.Core;
using KnowYourTurf.Core.Domain;

namespace KnowYourTurf.Web.Models
{
    public class POCommitViewModel : ListViewModel
    {
        public string ClosePOUrl { get; set; }
        public PurchaseOrder PurchaseOrder { get; set; }
    }
}