using Castle.Components.Validator;

namespace KnowYourTurf.Core.Domain
{
    public class Fertilizer : BaseProduct
    {
        public override string InstantiatingType{get{return "Fertilizer";}}
        [ValidateNonEmpty, ValidateDoubleAttribute]
        public virtual double N { get; set; }
        [ValidateNonEmpty, ValidateDoubleAttribute]
        public virtual double P { get; set; }
        [ValidateNonEmpty, ValidateDoubleAttribute]
        public virtual double K { get; set; }
    }
}