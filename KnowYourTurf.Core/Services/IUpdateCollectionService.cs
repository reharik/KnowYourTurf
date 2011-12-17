using System;
using System.Collections.Generic;
using System.Linq;
using KnowYourTurf.Core.Domain;

namespace KnowYourTurf.Core.Services
{
    public interface IUpdateCollectionService
    {
        void Update<ENTITY>(IEnumerable<ENTITY> origional,
                    IEnumerable<ENTITY> newItems,
                    Action<ENTITY> addEntity,
                    Action<ENTITY> removeEntity) where ENTITY : Entity;

        void UpdateFromCSV<ENTITY>(IEnumerable<ENTITY> origional,
                                                   string newItemsCSV,
                                                   Action<ENTITY> addEntity,
                                                   Action<ENTITY> removeEntity) where ENTITY : Entity;
    }

    public class UpdateCollectionService : IUpdateCollectionService 
    {
        private readonly IRepository _repository;

        public UpdateCollectionService(IRepository repository)
        {
            _repository = repository;
        }

        public void Update<ENTITY>(IEnumerable<ENTITY> origional,
            IEnumerable<ENTITY> newItems,
            Action<ENTITY> addEntity,
            Action<ENTITY> removeEntity) where ENTITY : Entity
        {
            var remove = new List<ENTITY>();
            origional.Each(x =>
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
            remove.Each(removeEntity);
            newItems.Each(x =>
            {
                if (!origional.Contains(x))
                {
                    addEntity(x);
                }
            });
        }

        public void UpdateFromCSV<ENTITY>(IEnumerable<ENTITY> origional,
                    string newItemsCSV,
                    Action<ENTITY> addEntity,
                    Action<ENTITY> removeEntity) where ENTITY : Entity
        {
            var newItems = new List<ENTITY>();
            if (newItemsCSV.IsEmpty())
            {
                origional.Each(removeEntity);
                return;
            }
            newItemsCSV.Split(',').Each(x => newItems.Add(_repository.Find<ENTITY>(Int32.Parse(x))));
            Update(origional, newItems, addEntity, removeEntity);
        }

    }
}