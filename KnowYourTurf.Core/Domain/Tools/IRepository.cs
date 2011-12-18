using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using NHibernate.Criterion;

namespace KnowYourTurf.Core.Domain
{
    public interface IRepository
    {
        void Save<ENTITY>(ENTITY entity)
            where ENTITY : DomainEntity;

        ENTITY Load<ENTITY>(long id)
            where ENTITY : DomainEntity;

        IQueryable<ENTITY> Query<ENTITY>()
            where ENTITY : DomainEntity;

        IQueryable<T> Query<T>(Expression<Func<T, bool>> where);

        T FindBy<T>(Expression<Func<T, bool>> where);

        T Find<T>(long id) where T : DomainEntity;

        IEnumerable<T> FindAll<T>() where T : DomainEntity;

        void HardDelete(object target);

        IUnitOfWork UnitOfWork { get; }
        void SoftDelete<ENTITY>(ENTITY entity) where ENTITY : DomainEntity;
        void Commit();
        void Rollback();
        void Initialize();
        IList<ENTITY> ExecuteCriteria<ENTITY>(DetachedCriteria criteria) where ENTITY : DomainEntity;
    
    }
}