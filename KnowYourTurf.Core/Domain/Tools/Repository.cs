using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Linq;

namespace KnowYourTurf.Core.Domain
{
    public class Repository : IRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        public IUnitOfWork UnitOfWork
        {
            get { return _unitOfWork; }
        }

        public Repository()
        {
            _unitOfWork = new UnitOfWork();
            _unitOfWork.Initialize();
        }

        public Repository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _unitOfWork.Initialize();
        }

        public void Save<ENTITY>(ENTITY entity) where ENTITY : DomainEntity
        {
            _unitOfWork.CurrentSession.SaveOrUpdate(entity);
        }

        public IEnumerable<T> FindAll<T>() where T : DomainEntity
        {
            return _unitOfWork.CurrentSession.Query<T>();
        }

        public void HardDelete(object target)
        {
            _unitOfWork.CurrentSession.Delete(target);
        }

        public void SoftDelete<ENTITY>(ENTITY entity) where ENTITY : DomainEntity
        {
            entity.IsDeleted = true;
            _unitOfWork.CurrentSession.SaveOrUpdate(entity);
        }

        public void Initialize()
        {
            _unitOfWork.Initialize();
        }

        public void Commit()
        {
            _unitOfWork.Commit();
        }

        public void Rollback()
        {
            _unitOfWork.Rollback();
        }

        public IList<ENTITY> ExecuteCriteria<ENTITY>(DetachedCriteria criteria) where ENTITY : DomainEntity
        {
            ICriteria executableCriteria = criteria.GetExecutableCriteria(_unitOfWork.CurrentSession);
            return executableCriteria.List<ENTITY>();
        }

        public ENTITY Load<ENTITY>(long id) where ENTITY : DomainEntity
        {
            return _unitOfWork.CurrentSession.Load<ENTITY>(id);
        }

        public IQueryable<ENTITY> Query<ENTITY>() where ENTITY : DomainEntity
        {
            return _unitOfWork.CurrentSession.Query<ENTITY>();
        }

        public IQueryable<ENTITY> Query<ENTITY>(Expression<Func<ENTITY, bool>> where)
        {
            return _unitOfWork.CurrentSession.Query<ENTITY>().Where(where);
        }

        public T FindBy<T>(Expression<Func<T, bool>> where)
        {
            return _unitOfWork.CurrentSession.Query<T>().FirstOrDefault(where);
        }

        public T Find<T>(long id) where T : DomainEntity
        {
            return _unitOfWork.CurrentSession.Get<T>(id);
        }
    }
}