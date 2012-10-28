using CC.Core.DomainTools;
using KnowYourTurf.Core.Services;
using NHibernate;

namespace KnowYourTurf.Core.Domain.Tools
{
    public class KYTUnitOfWork :UnitOfWork
    {
        private ISession _session;
        private IGetCompanyIdService _getCompanyIdService;

        public KYTUnitOfWork(ISession session, IGetCompanyIdService getCompanyIdService) : base(session)
    {
        _session = session;
        _getCompanyIdService = getCompanyIdService;
        var enableCoFilter = _session.EnableFilter("CompanyConditionFilter");
        var enableDeletdFilter = _session.EnableFilter("IsDeletedConditionFilter");
        var enableStatusFilter = _session.EnableFilter("StatusConditionFilter");
        if (enableCoFilter != null)
            enableCoFilter.SetParameter("CompanyId", _getCompanyIdService.Execute());
        if (enableDeletdFilter != null)
            enableDeletdFilter.SetParameter("IsDeleted", false);
        if (enableStatusFilter != null)
            enableStatusFilter.SetParameter("condition", "Active");
    }
    }
}