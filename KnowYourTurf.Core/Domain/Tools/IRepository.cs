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
        void Save<ENTITY>(ENTITY entity) where ENTITY : IPersistableObject;

        ENTITY Load<ENTITY>(long id) where ENTITY : IPersistableObject;

        IQueryable<ENTITY> Query<ENTITY>() where ENTITY : IPersistableObject;

        IQueryable<ENTITY> Query<ENTITY>(Expression<Func<ENTITY, bool>> where) where ENTITY : IPersistableObject;

        ENTITY FindBy<ENTITY>(Expression<Func<ENTITY, bool>> where) where ENTITY : IPersistableObject;

        ENTITY Find<ENTITY>(long id) where ENTITY : IPersistableObject;

        IEnumerable<ENTITY> FindAll<ENTITY>() where ENTITY : IPersistableObject;

        IList<ENTITY> ExecuteCriteria<ENTITY>(DetachedCriteria criteria) where ENTITY : IPersistableObject;

        void HardDelete<ENTITY>(ENTITY entity) where ENTITY : IPersistableObject;

        void SoftDelete<ENTITY>(ENTITY entity) where ENTITY : IPersistableObject;
        void Commit();
        void Rollback();
        void Initialize();

        void DisableFilter(string FilterName);
        void EnableFilter(string FilterName, string field, object value);
        IUnitOfWork UnitOfWork { get; set; }
    
    }
}