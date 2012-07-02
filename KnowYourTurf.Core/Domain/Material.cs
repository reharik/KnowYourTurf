using KnowYourTurf.Core;
using KnowYourTurf.Core.Localization;

namespace KnowYourTurf.Core.Domain
{
    public class Material : BaseProduct, IPersistableObject
    {
        public override string InstantiatingType { get { return "Material"; } }
    }
}