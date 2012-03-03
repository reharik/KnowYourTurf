using System;
using KnowYourTurf.Core.Services;
using NHibernate;
using StructureMap;

namespace KnowYourTurf.Core.Domain
{
    public class UnitOfWork : IUnitOfWork
    {
        private ITransaction _transaction;
        private bool _isDisposed;
        private ISession _session;
        private readonly IGetCompanyIdService _getCompanyIdService;
        private bool _isInitialized;

        public UnitOfWork(ISession session,IGetCompanyIdService getCompanyIdService)
        {
            _session = session;
            var enableCoFilter = _session.EnableFilter("CompanyConditionFilter");
            var enableDeletdFilter = _session.EnableFilter("IsDeletedConditionFilter");
            var enableStatusFilter = _session.EnableFilter("StatusConditionFilter");
            if(enableCoFilter!=null)
                enableCoFilter.SetParameter("CompanyId", ObjectFactory.Container.GetInstance<IGetCompanyIdService>().Execute());
            if (enableDeletdFilter!= null)
                enableDeletdFilter.SetParameter("IsDeleted", false);
            if (enableStatusFilter!= null)
                enableStatusFilter.SetParameter("condition", "Active");
        }
        
        public UnitOfWork()
        {
            _session = ObjectFactory.GetNamedInstance<ISession>("NoCompanyFilter");
        }
        
        public void Initialize()
        {
            should_not_currently_be_disposed();
            if (_isInitialized) return;
          
            CurrentSession = _session;
            begin_new_transaction();

            _isInitialized = true;
        }

        public ISession CurrentSession { get; private set; }

        public void Commit()
        {
            should_not_currently_be_disposed();
            should_be_initialized_first();
            _transaction.Commit();

            begin_new_transaction();
        }

        private void begin_new_transaction()
        {
            if( _transaction != null )
            {
                _transaction.Dispose();
            }

            _transaction = CurrentSession.BeginTransaction();
        }

        public void Rollback()
        {
            should_not_currently_be_disposed();
            should_be_initialized_first();

            _transaction.Rollback();

            begin_new_transaction();
        }

        private void should_not_currently_be_disposed()
        {
            if( _isDisposed ) throw new ObjectDisposedException(GetType().Name);
        }

        private void should_be_initialized_first()
        {
            if( ! _isInitialized ) throw new InvalidOperationException("Must initialize (call Initialize()) on NHibernateUnitOfWork before commiting or rolling back");
        }

        public void Dispose()
        {
            if (_isDisposed || ! _isInitialized) return;
            _transaction.Dispose();
            CurrentSession.Dispose();
            _isDisposed = true;
        }
    }

}