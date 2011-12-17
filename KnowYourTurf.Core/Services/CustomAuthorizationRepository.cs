using System.Collections.Generic;
using NHibernate;
using Rhino.Security.Model;
using Rhino.Security.Services;

namespace KnowYourTurf.Core.Services
{
    public class CustomAuthorizationRepository : AuthorizationRepository
    {
        private readonly ISession _session;

        public CustomAuthorizationRepository(ISession session)
            : base(session)
        {
            _session = session;
        }

        public virtual IEnumerable<Operation> GetAllOperations()
        {
            return _session.CreateCriteria<Operation>()
                .SetCacheable(true).List<Operation>();
        }

    }
}