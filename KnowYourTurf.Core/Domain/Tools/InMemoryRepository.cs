using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using NHibernate;
using NHibernate.Criterion;

namespace DecisionCritical.Core.Domain
{
    public class InMemoryRepository : IRepository 
    {
        private int _nextId = 0;
        private InMemoryUnitOfWork _uow;

        public InMemoryRepository()
        {
            _uow = new InMemoryUnitOfWork();
            _uow.Initialize();
        }


        public int LastIdAssigned { get { return _nextId; } }

        public ISession CurrentSession()
        {
            throw new NotImplementedException();
        }

        public void Save<ENTITY>(ENTITY entity) where ENTITY : Entity
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            var Entity = entity as Entity;
            if (Entity != null && Entity.IsNew())
            {
                Entity.EntityId = Interlocked.Increment(ref _nextId);
            }

            if (! _uow.Items.Contains(entity))
            {
                _uow.Items.Add(entity);
            }

            _uow.SavedItems.Add(entity);
        }

        public void Save(object entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            var Entity = entity as Entity;
            if (Entity != null && Entity.IsNew())
            {
                Entity.EntityId = Interlocked.Increment(ref _nextId);
            }

            if (!_uow.Items.Contains(entity))
            {
                _uow.Items.Add(entity);
            }

            _uow.SavedItems.Add(entity);
        }

        public void SaveAll(IEnumerable entities)
        {
            entities.Each(Save);
        }

        public IEnumerable<T> FindAll<T>() where T : Entity
        {
            return findItems<T>();
        }

        public void Delete<ENTITY>(ENTITY entity) where ENTITY : Entity
        {
            _uow.Items.Remove(entity);
            _uow.DeletedItems.Add(entity);
        }

        public void HardDelete(object target)
        {
            _uow.Items.Remove(target);
            _uow.DeletedItems.Add(target);
        }

        private IQueryable<ENTITY> findItems<ENTITY>()
        {
            return _uow.Items.Where(e => e is ENTITY).Cast<ENTITY>().AsQueryable();
        }

        public ENTITY Load<ENTITY>(int id) where ENTITY : Entity
        {
            return FindBy<ENTITY>(t => t.EntityId == id);
        }

        public IQueryable<ENTITY> Query<ENTITY>() where ENTITY : Entity
        {
            return findItems<ENTITY>();
        }

        public IQueryable<T> Query<T>(Expression<Func<T, bool>> where)
        {
            return findItems<T>().Where(where);
        }

        public T FindBy<T>(Expression<Func<T, bool>> where)
        {
            return findItems<T>().Where(where).FirstOrDefault();
        }

        public T Find<T>(int id) where T : Entity
        {
            return FindBy<T>(t => t.EntityId == id);
        }

        public IUnitOfWork UnitOfWork
        {
            get { return _uow; }
        }

        public IQueryOver<ENTITY> QueryOver<ENTITY>() where ENTITY : Entity
        {
            throw new NotImplementedException();
        }

        public void Commit()
        {
            throw new NotImplementedException();
        }

        public void Rollback()
        {
            throw new NotImplementedException();
        }

        public void Initialize()
        {
            throw new NotImplementedException();
        }

        public IList<ENTITY> ExecuteCriteria<ENTITY>(DetachedCriteria criteria) where ENTITY : Entity
        {
            throw new NotImplementedException();
        }

        public IList<T> GetNamedQuery<T>(string sprocName)
        {
            throw new NotImplementedException();
        }
    }

    public class InMemoryUnitOfWork : IUnitOfWork
    {
        private readonly IList<object> _items = new List<object>();
        private readonly ArrayList savedItems = new ArrayList();
        private readonly ArrayList deletedItems = new ArrayList();
        private bool _initialized;
        private bool _wasCommitted;
        public IList<object> Items { get { return _items; } }
        public IList SavedItems { get { return savedItems; } }
        public IList DeletedItems { get { return deletedItems; } }
        public bool WasCommitted { get { return _wasCommitted; } }
        public bool Initialized { get { return _initialized; } }
       
        public void Initialize()
        {
            beginNewTransaction();
            _initialized = true;
            _wasCommitted = false;
        }

        private void beginNewTransaction()
        {
            ClearTransactionHistory();
        }

        private void ClearTransactionHistory()
        {
            _items.Clear();
            savedItems.Clear();
            deletedItems.Clear();
        }

        private void shouldBeInitializedFirst()
        {
            if(!_initialized)
            {
                throw new ObjectDisposedException(GetType().Name);
            }
        }

        public void Commit()
        {
            shouldBeInitializedFirst();
            Dispose();
            _wasCommitted = true;
        }

        public void Rollback()
        {
            shouldBeInitializedFirst();
            Dispose();
        }

        public ISession CurrentSession
        {
            get { throw new System.NotImplementedException(); }
        }

        public void Dispose()
        {
            ClearTransactionHistory();
            _initialized = false;
            _wasCommitted = false;
 
        }
    }
}