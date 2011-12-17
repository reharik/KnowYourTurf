using System;
using NHibernate;

namespace KnowYourTurf.Core.Domain
{
    public interface IUnitOfWork : IDisposable
    {
        void Initialize();
        void Commit();
        void Rollback();
        ISession CurrentSession { get; }

        void DisableFilter(string FilterName);
        void EnableFilter(string FilterName, string field, object value);
    }
}