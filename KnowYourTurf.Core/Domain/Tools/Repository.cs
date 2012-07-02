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

        public void Save<ENTITY>(ENTITY entity) where ENTITY : IPersistableObject
        {
            _unitOfWork.CurrentSession.SaveOrUpdate(entity);
        }

        public IEnumerable<ENTITY> FindAll<ENTITY>() where ENTITY : IPersistableObject
        {
            return _unitOfWork.CurrentSession.Query<ENTITY>();
        }

        public void HardDelete<ENTITY>(ENTITY entity) where ENTITY : IPersistableObject
        {
            _unitOfWork.CurrentSession.Delete(entity);
        }

        public void SoftDelete<ENTITY>(ENTITY entity) where ENTITY : IPersistableObject
        {
            (entity as Entity).IsDeleted = true;
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

        public IList<ENTITY> ExecuteCriteria<ENTITY>(DetachedCriteria criteria) where ENTITY : IPersistableObject
        {
            ICriteria executableCriteria = criteria.GetExecutableCriteria(_unitOfWork.CurrentSession);
            return executableCriteria.List<ENTITY>();
        }

        public ENTITY Load<ENTITY>(long id) where ENTITY : IPersistableObject
        {
            return _unitOfWork.CurrentSession.Load<ENTITY>(id);
        }

        public IQueryable<ENTITY> Query<ENTITY>() where ENTITY : IPersistableObject
        {
            return _unitOfWork.CurrentSession.Query<ENTITY>();
        }

        public IQueryable<ENTITY> Query<ENTITY>(Expression<Func<ENTITY, bool>> where) where ENTITY : IPersistableObject
        {
            return _unitOfWork.CurrentSession.Query<ENTITY>().Where(where);
        }

        public ENTITY FindBy<ENTITY>(Expression<Func<ENTITY, bool>> where) where ENTITY : IPersistableObject
        {
            return _unitOfWork.CurrentSession.Query<ENTITY>().FirstOrDefault(where);
        }

        public ENTITY Find<ENTITY>(long id) where ENTITY : IPersistableObject
        {
            return _unitOfWork.CurrentSession.Get<ENTITY>(id);
        }
    }
}