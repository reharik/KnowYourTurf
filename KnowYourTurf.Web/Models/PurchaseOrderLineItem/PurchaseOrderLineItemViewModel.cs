using System.Collections.Generic;
using System.Web.Mvc;
using Castle.Components.Validator;
using KnowYourTurf.Core;
using KnowYourTurf.Core.Enums;
using KnowYourTurf.Core.Localization;

namespace KnowYourTurf.Web.Models
{
    public class PurchaseOrderLineItemViewModel:ViewModel
    {
        public IEnumerable<SelectListItem> _UnitTypeList { get; set; }
        public string _saveUrl { get; set; }

        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        [ValidateNonEmpty, ValidateInteger]
        public int? QuantityOrdered { get; set; }
        [ValidateInteger]
        public int? SizeOfUnit { get; set; }
        [ValidateDecimal]
        public double? Price { get; set; }
        public bool Taxable { get; set; }
        [ValidateNonEmpty, ValueOf(typeof(UnitType))]
        public string UnitType { get; set; }
        [ValidateDecimal]
        public double? SubTotal { get; set; }
        [ValidateDecimal]
        public double? Tax { get; set; }
        public int? TotalReceived { get; set; }
        public bool Completed { get; set; }
    }
}