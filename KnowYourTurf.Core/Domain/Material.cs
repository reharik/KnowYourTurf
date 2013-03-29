using CC.Core.Domain;

namespace KnowYourTurf.Core.Domain
{
    public class Material : BaseProduct, IPersistableObject
    {
        public override string InstantiatingType { get { return "Material"; } }
    }
}