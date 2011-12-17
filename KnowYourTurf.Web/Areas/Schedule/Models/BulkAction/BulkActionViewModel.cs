using System.Collections.Generic;
using KnowYourTurf.Core;

namespace KnowYourTurf.Web.Areas.Portfolio.Models.BulkAction
{
    public class BulkActionViewModel:ViewModel
    {
        public IEnumerable<int> EntityIds { get; set; }
    }
}