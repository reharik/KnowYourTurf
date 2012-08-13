using System;
using System.Collections.Generic;
using System.Linq;
using FubuMVC.Core;
using KnowYourTurf.Core.CoreViewModels;
using KnowYourTurf.Core.Domain;

namespace KnowYourTurf.Core.Services
{
    public interface IUpdateCollectionService
    {
        void UpdateCollectionDetails<ENTITY>(IEnumerable<ENTITY> origional,
                    IEnumerable<ENTITY> newItems,
                    Action<ENTITY> addEntity,
                    Action<ENTITY> removeEntity) where ENTITY : Entity;

        void Update<ENTITY>(IEnumerable<ENTITY> origional,
                            TokenInputViewModel tokenInputViewModel,
                            Action<ENTITY> addEntity,
                            Action<ENTITY> removeEntity,
            Func<ENTITY, ENTITY, bool> comparer = null) where ENTITY : Entity, IPersistableObject;


    }

    public class UpdateCollectionService : IUpdateCollectionService
    {
        private readonly IRepository _repository;

        public UpdateCollectionService(IRepository repository)
        {
            _repository = repository;
        }

        public void UpdateCollectionDetails<ENTITY>(IEnumerable<ENTITY> origional,
            IEnumerable<ENTITY> newItems,
            Action<ENTITY> addEntity,
            Action<ENTITY> removeEntity) where ENTITY : Entity
        {
            var remove = new List<ENTITY>();
            origional.ForEachItem(x =>
            {
                var newItem = newItems.FirstOrDefault(i => i.EntityId == x.EntityId);
                if (newItem == null)
                {
                    remove.Add(x);
                }
                else
                {
                    x.UpdateSelf(newItem);
                }
            });
            remove.ForEachItem(removeEntity);
            newItems.ForEachItem(x =>
            {
                if (!origional.Contains(x))
                {
                    addEntity(x);
                }
            });
        }

        public void Update<ENTITY>(IEnumerable<ENTITY> origional,
            TokenInputViewModel tokenInputViewModel,
            Action<ENTITY> addEntity,
            Action<ENTITY> removeEntity,
            Func<ENTITY,ENTITY,bool> comparer = null) where ENTITY : Entity, IPersistableObject
        {
            if(comparer == null)
            {
                comparer = (entity, entity1) => entity.EntityId == entity1.EntityId;
            }
            var newItems = new List<ENTITY>();
            if (tokenInputViewModel != null && tokenInputViewModel.selectedItems != null)
            { tokenInputViewModel.selectedItems.Each(x => newItems.Add(_repository.Find<ENTITY>(long.Parse(x.id)))); }
            
            var remove = new List<ENTITY>();
            if (newItems.Any())
            {
                origional.Where(x => comparer(x, newItems.FirstOrDefault())).ForEachItem(x =>
                {
                    if (!newItems.Any(i => i.EntityId == x.EntityId))
                    {
                        remove.Add(x);
                    }
                });
            }
            else
            {
                remove = origional.ToList();
            }
            remove.ForEachItem(removeEntity);
            newItems.ForEachItem(x =>
            {
                if (!origional.Contains(x))
                {
                    addEntity(x);
                }
            });
        }
    }
}