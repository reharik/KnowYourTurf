using CC.Core.DomainTools;
using StructureMap;

namespace KnowYourTurf.Core.Domain.Tools
{
    public class NoInterceptorNoFiltersRepository : Repository
    {
        public NoInterceptorNoFiltersRepository()
        {
            _unitOfWork = ObjectFactory.Container.GetInstance<IUnitOfWork>("NoInterceptorNoFilters");
            _unitOfWork.Initialize();
        }
    }
}