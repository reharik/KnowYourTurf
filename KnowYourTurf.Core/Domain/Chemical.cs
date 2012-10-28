using Castle.Components.Validator;

namespace KnowYourTurf.Core.Domain
{
    public class Chemical : BaseProduct
    {
        public override string InstantiatingType { get { return "Chemical"; } }
        public virtual string Manufacturer { get; set; }
        public virtual string ActiveIngredient { get; set; }
        [ValidateDecimal]
        public virtual decimal ActiveIngredientPercent { get; set; }
        public virtual string EPARegNumber { get; set; }
        public virtual string EPAEstNumber { get; set; }

    }
}