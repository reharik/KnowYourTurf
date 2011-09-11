using System;
using NHibernate;

namespace KnowYourTurf.Core.Domain
{
    public interface IUnitOfWork : IDisposable
    {
        ISession CurrentSession { get; }
        void Initialize();
        void Commit();
        void Rollback();
    }
}