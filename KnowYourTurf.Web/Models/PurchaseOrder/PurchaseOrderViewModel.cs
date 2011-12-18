using System;
using System.Collections.Generic;
using System.Web.Mvc;
using KnowYourTurf.Core;
using KnowYourTurf.Core.Html.Grid;

namespace KnowYourTurf.Web.Models
{
    public class POListViewModel : ListViewModel
    {
        public GridDefinition PoliListDefinition { get; set; }
        public long VendorId { get; set; }
        public IEnumerable<SelectListItem> VendorList { get; set; }
        public Core.Domain.PurchaseOrder PurchaseOrder { get; set; }

        public string ChemUrl { get; set; }
        public string MatUrl { get; set; }
        public string FertUrl { get; set; }
        public string SeedUrl { get; set; }
        public string PoliUrl { get; set; }

        public string ReturnUrl { get; set; }
        public string CommitUrl { get; set; }
    }
}