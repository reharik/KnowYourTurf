using CC.Core.DomainTools;
using CC.Security.Interfaces;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Services;

namespace KnowYourTurf.Core.Html.Menu
{
    public class KYTGenMenuBuilder : KYTMenuBuilder
    {
        public KYTGenMenuBuilder(IRepository repository, IAuthorizationService authorizationService, ISessionContext sessionContext) : base(repository, authorizationService, sessionContext)
        {
        }

        public override IKYTMenuBuilder CategoryGroupForItteration()
        {
            _categories = new[] { new Site { Name = "default", EntityId = 1 } };
            count = _categories.Count;
            return this;
        }     
    }
}