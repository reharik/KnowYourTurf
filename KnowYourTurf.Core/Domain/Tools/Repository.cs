using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using KnowYourTurf.Core.Enums;
using KnowYourTurf.Core.Services;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Linq;
using StructureMap;

namespace KnowYourTurf.Core.Domain
{
    public class Repository : IRepository
    {
        private  IUnitOfWork _unitOfWork;
        private ISystemClock _clock;

        public IUnitOfWork UnitOfWork
        {
            get { return _unitOfWork; }
            set { _unitOfWork = value; }
        }

        //No Filters
        public Repository(RepoConfig config)
        {
            switch(config.Key)
            {
                case "NoFilters":
                    _unitOfWork = ObjectFactory.Container.GetInstance<IUnitOfWork>("NoFilters");
                    break;
                case "NoFiltersOrInterceptor":
                    _unitOfWork = ObjectFactory.Container.GetInstance<IUnitOfWork>("NoFiltersOrInterceptor");
                    break;
                case "NoFiltersSpecialInterceptor":
                    _unitOfWork = ObjectFactory.Container.GetInstance<IUnitOfWork>("NoFiltersSpecialInterceptor");
                    break;
            }
            _unitOfWork.Initialize();
        }

        public Repository(IUnitOfWork unitOfWork, ISystemClock clock)
        {
            _unitOfWork = unitOfWork;
            _unitOfWork.Initialize();
            _clock = clock;
        }

        public void DisableFilter(string FilterName)
        {
            _unitOfWork.DisableFilter(FilterName);
        }

        public void EnableFilter(string FilterName, string field, object value)
        {
            _unitOfWork.EnableFilter(FilterName, field, value);
        }
        public ISession CurrentSession()
        {
            return _unitOfWork.CurrentSession;
        }

        public void Save<ENTITY>(ENTITY entity) where ENTITY : Entity
        {
            _unitOfWork.CurrentSession.SaveOrUpdate(entity);
        }

        public IEnumerable<T> FindAll<T>() where T : Entity
        {
            return _unitOfWork.CurrentSession.Query<T>();
        }

        public void HardDelete(object target)
        {
            _unitOfWork.CurrentSession.Delete(target);
        }

        public void SoftDelete<ENTITY>(ENTITY entity) where ENTITY : Entity
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

        public IList<ENTITY> ExecuteCriteria<ENTITY>(DetachedCriteria criteria) where ENTITY : Entity
        {
            ICriteria executableCriteria = criteria.GetExecutableCriteria(_unitOfWork.CurrentSession);
            return executableCriteria.List<ENTITY>();
        }

        public ENTITY Load<ENTITY>(long id) where ENTITY : Entity
        {
            return _unitOfWork.CurrentSession.Load<ENTITY>(id);
        }

        public IQueryable<ENTITY> Query<ENTITY>() where ENTITY : Entity
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

        public T Find<T>(long id) where T : Entity
        {
            return _unitOfWork.CurrentSession.Get<T>(id);
        }
    }
}