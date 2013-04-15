using CC.Core.DomainTools;
using StructureMap;

namespace KnowYourTurf.Core.Domain.Tools
{
    public class NoFilterRepository : Repository
    {
        public NoFilterRepository()
        {
            _unitOfWork = ObjectFactory.Container.GetInstance<IUnitOfWork>("NoFilters");
            _unitOfWork.Initialize();
        }
    }
    public class NoInterceptorNoFiltersRepository : Repository
    {
        public NoInterceptorNoFiltersRepository()
        {
            _unitOfWork = ObjectFactory.Container.GetInstance<IUnitOfWork>("NoFilters");
            _unitOfWork.Initialize();
        }
    }
}