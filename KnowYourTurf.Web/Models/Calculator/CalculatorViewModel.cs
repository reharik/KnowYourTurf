using System.Collections.Generic;
using System.Web.Mvc;
using Castle.Components.Validator;
using KnowYourTurf.Core;
using KnowYourTurf.Core.Domain;

namespace KnowYourTurf.Web.Models
{
    public abstract class CalculatorViewModel:ViewModel
    {
        public string CalculateUrl { get; set; }
        public string CreateATaskUrl { get; set; }
        public string CalculatorDisplayName { get; set; }
        public string CalculatorName { get; set; }
        public bool Success { get { return true; } }
        public Calculator Item { get; set; }
    }

    public class SuperInputCalcViewModel
    {
        public long EntityId { get; set; }
        public string Field { get; set; }
        public string Product { get; set; }
        public double? FertilizerRate { get; set; }
        public double Drainageline { get; set; }
        public double DitchlineWidth { get; set; }
        public double DitchDepth { get; set; }
        public double Depth { get; set; }
        public double PipeRadius { get; set; }
        public double Area { get; set; }
        public double Height { get; set; }
        public double Diameter { get; set; }
        public double SeedRate { get; set; }
        public double OverSeedPercent { get; set; }
        public double BagsUsed { get; set; }
    }

    public class FertilzierNeededCalcViewModel: CalculatorViewModel
    {
        [ValidateNonEmpty]
        public string Field { get; set; }
        public IDictionary<string, IEnumerable<SelectListItem>> FieldList { get; set; }
        [ValidateNonEmpty]
        public string Product { get; set; }
        public IEnumerable<SelectListItem> ProductList { get; set; }
        [ValidateDouble]
        public double? N { get; set; }
        [ValidateDouble]
        public double? P { get; set; }
        [ValidateDouble]
        public double? K { get; set; }
        public string BagSize { get; set; }
        public string FieldArea { get; set; }
        [ValidateNonEmpty, ValidateDouble]
        public double? FertilizerRate { get; set; }
        [ValidateDouble]
        public double? BagsNeeded { get; set; }
    }

    public class MaterialsCalcViewModel : CalculatorViewModel
    {
        [ValidateNonEmpty]
        public string Field { get; set; }
        public IDictionary<string, IEnumerable<SelectListItem>> FieldList { get; set; }
        [ValidateNonEmpty, ValidateDouble]
        public double? Drainageline { get; set; }
        [ValidateNonEmpty, ValidateDouble]
        public double? DitchlineWidth { get; set; }
        [ValidateNonEmpty, ValidateDouble]
        public double? DitchDepth { get; set; }
        [ValidateNonEmpty, ValidateDouble]
        public double? Depth { get; set; }
        [ValidateNonEmpty, ValidateDouble]
        public double? PipeRadius { get; set; }
        [ValidateDouble]
        public double? FieldArea { get; set; }
        [ValidateDouble]
        public double? TotalMaterials { get; set; }

    }

    public class SandCalcViewModel: CalculatorViewModel
    {
        [ValidateNonEmpty, ValidateDouble]
        public double? Area { get; set; }
        [ValidateNonEmpty, ValidateDouble]
        public double? Height { get; set; }
        [ValidateNonEmpty, ValidateDouble]
        public double? Diameter { get; set; }
        [ValidateDouble]
        public double? TotalSand { get; set; }
    }

    public class OverseedBagsNeededCalcViewModel:CalculatorViewModel
    {
        [ValidateNonEmpty]
        public string Field { get; set; }
        public IDictionary<string, IEnumerable<SelectListItem>> FieldList { get; set; }
        [ValidateNonEmpty]
        public string Product { get; set; }
        public IEnumerable<SelectListItem> ProductList { get; set; }
        public string BagSize { get; set; }
        public string FieldArea { get; set; }
        public double? BagsNeeded { get; set; }
        [ValidateNonEmpty, ValidateDouble]
        public double? SeedRate { get; set; }
        [ValidateNonEmpty, ValidateDouble]
        public double? OverSeedPercent { get; set; }
    }

    public class OverseedRateNeededCalcViewModel : CalculatorViewModel
    {
        [ValidateNonEmpty]
        public string Field { get; set; }
        public IDictionary<string, IEnumerable<SelectListItem>> FieldList { get; set; }
        [ValidateNonEmpty]
        public string Product { get; set; }
        public IEnumerable<SelectListItem> ProductList { get; set; }
        public string BagSize { get; set; }
        public string FieldArea { get; set; }
        [ValidateNonEmpty, ValidateDouble]
        public double? BagsUsed { get; set; }
        [ValidateDouble]
        public double? SeedRate { get; set; }
        [ValidateNonEmpty, ValidateDouble]
        public double? OverSeedPercent { get; set; }
    }
}