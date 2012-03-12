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

        void DisableFilter(string FilterName);
        void EnableFilter(string FilterName, string field, object value);
    }
}