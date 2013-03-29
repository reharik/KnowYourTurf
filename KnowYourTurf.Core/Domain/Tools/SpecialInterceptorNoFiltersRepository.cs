using CC.Core.DomainTools;
using StructureMap;

namespace KnowYourTurf.Core.Domain.Tools
{
    public class SpecialInterceptorNoFiltersRepository : Repository
    {
        public SpecialInterceptorNoFiltersRepository()
        {
            _unitOfWork = ObjectFactory.Container.GetInstance<IUnitOfWork>("SpecialInterceptorNoFilters");
            _unitOfWork.Initialize();
        }
    }
}