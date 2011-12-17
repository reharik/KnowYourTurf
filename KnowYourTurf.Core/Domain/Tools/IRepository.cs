using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using NHibernate;
using NHibernate.Criterion;

namespace KnowYourTurf.Core.Domain
{
    public interface IRepository
    {
        ISession CurrentSession();

        void Save<ENTITY>(ENTITY entity)
            where ENTITY : Entity;

        ENTITY Load<ENTITY>(int id)
            where ENTITY : Entity;

        IQueryable<ENTITY> Query<ENTITY>()
            where ENTITY : Entity;

        IQueryable<T> Query<T>(Expression<Func<T, bool>> where);

        T FindBy<T>(Expression<Func<T, bool>> where);

        T Find<T>(int id) where T : Entity;

        IEnumerable<T> FindAll<T>() where T : Entity;

        void Delete<ENTITY>(ENTITY entity) where ENTITY : Entity;

        void HardDelete(object target);

        IQueryOver<ENTITY> QueryOver<ENTITY>() where ENTITY : Entity;

        void Commit();
        void Rollback();
        void Initialize();
        IList<ENTITY> ExecuteCriteria<ENTITY>(DetachedCriteria criteria) where ENTITY : Entity;

        IList<T> GetNamedQuery<T>(string sprocName);
        void DisableFilter(string FilterName);
        void EnableFilter(string FilterName, string field, object value);
        IUnitOfWork UnitOfWork { get; set; }
    }
}
