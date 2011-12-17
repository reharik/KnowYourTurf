using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Services;
using NHibernate;
using StructureMap;

namespace KnowYourTurf.Core.Domain
{
    public class NullNHibernateUnitOfWork : IUnitOfWork
    {
        public NullNHibernateUnitOfWork(ISession session, ISessionContext sessionContext)
        {
        }

        //No filters
        public NullNHibernateUnitOfWork()
        {
            //_session = ObjectFactory.Container.GetInstance<ISession>();
        }
        //No filters or interceptor
        public NullNHibernateUnitOfWork(bool noFiltersOrInterceptor)
        {
            //_session = ObjectFactory.Container.GetInstance<ISession>("NoFiltersOrInterceptor");
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
        public void DisableFilter(string FilterName)
        {
            throw new System.NotImplementedException();
        }

        public void EnableFilter(string FilterName, string field, object value)
        {
            throw new System.NotImplementedException();
        }

        public void Commit()
        {
            return;
        }

        public void Rollback()
        {
            return;
        }

        public void Dispose()
        {
            return;
        }
    }

}