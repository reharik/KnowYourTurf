using CC.Core.DomainTools;
using KnowYourTurf.Core.Services;
using NHibernate;

namespace KnowYourTurf.Core.Domain.Tools
{
    public class KYTUnitOfWork : UnitOfWork
    {
        private ISession _session;
        private IGetClientIdService _getClientIdService;

        public KYTUnitOfWork(ISession session, IGetClientIdService getClientIdService)
            : base(session)
        {
            _session = session;
            _getClientIdService = getClientIdService;
            var enableCoFilter = _session.EnableFilter("ClientConditionFilter");
            var enableDeletdFilter = _session.EnableFilter("IsDeletedConditionFilter");
            var enableStatusFilter = _session.EnableFilter("StatusConditionFilter");
            if (enableCoFilter != null)
                enableCoFilter.SetParameter("ClientId", _getClientIdService.Execute());
            if (enableDeletdFilter != null)
                enableDeletdFilter.SetParameter("IsDeleted", false);
            if (enableStatusFilter != null)
                enableStatusFilter.SetParameter("condition", "Active");
        }
    }
}