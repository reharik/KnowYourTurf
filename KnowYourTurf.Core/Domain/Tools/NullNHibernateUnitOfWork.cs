using CC.Core.DomainTools;
using NHibernate;

namespace KnowYourTurf.Core.Domain
{
    public class NullNHibernateUnitOfWork : IUnitOfWork
    {
        public NullNHibernateUnitOfWork()
        {
        }

        public void DISABLE_TENANT_FILTER()
        {
            throw new System.NotImplementedException();
        }

        public void Initialize()
        {
            return;
        }

        public ISession CurrentSession { get; private set; }

        public void Commit()
        {
            return;
        }

        public void Rollback()
        {
            return;
        }

        public void DisableFilter(string FilterName)
        {
            throw new System.NotImplementedException();
        }

        public void EnableFilter(string FilterName, string field, object value)
        {
            throw new System.NotImplementedException();
        }

        public void Dispose()
        {
            return;
        }
    }

}