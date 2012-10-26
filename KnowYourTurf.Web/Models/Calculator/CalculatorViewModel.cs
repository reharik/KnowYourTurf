using System.Collections.Generic;
using System.Web.Mvc;
using CC.Core.CoreViewModelAndDTOs;
using Castle.Components.Validator;

namespace KnowYourTurf.Web.Models
{
    public abstract class CalculatorViewModel:ViewModel
    {
        public string _calculateUrl { get; set; }
        public string _createATaskUrl { get; set; }
        public string _calculatorDisplayName { get; set; }
        public string _calculatorName { get; set; }
        public string _saveUrl { get; set; }
        public bool Success { get { return true; } }
    }

    public class SuperInputCalcViewModel
    {
        public int EntityId { get; set; }
        public int FieldEntityId { get; set; }
        public int ProductEntityId { get; set; }
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
        public GroupedSelectViewModel _FieldEntityIdList { get; set; }
        public IEnumerable<SelectListItem> _ProductEntityIdList { get; set; }

        [ValidateNonEmpty]
        public int FieldEntityId { get; set; }
        [ValidateNonEmpty]
        public int ProductEntityId { get; set; }
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
        public GroupedSelectViewModel _FieldEntityIdList { get; set; }

        public int FieldEntityId { get; set; }
     
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
        public GroupedSelectViewModel _FieldEntityIdList { get; set; }
        public IEnumerable<SelectListItem> _ProductEntityIdList { get; set; }


        [ValidateNonEmpty]
        public int FieldEntityId { get; set; }
        [ValidateNonEmpty]
        public int ProductEntityId { get; set; }
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
        public GroupedSelectViewModel _FieldEntityIdList { get; set; }
        public IEnumerable<SelectListItem> _ProductEntityIdList { get; set; }


        [ValidateNonEmpty]
        public int FieldEntityId { get; set; }
        [ValidateNonEmpty]
        public int ProductEntityId { get; set; }
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