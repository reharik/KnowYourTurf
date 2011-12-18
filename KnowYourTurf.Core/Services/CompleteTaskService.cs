//using FubuMVC.Core;
//using KnowYourTurf.Core.Domain;

//namespace KnowYourTurf.Core.Services
//{
//    public interface ICompleteTaskService
//    {
//        void CompleteTask(Task task);
//    }

//    public class CompleteTaskService : ICompleteTaskService
//    {
//        private readonly IRepository _repository;

//        public CompleteTaskService(IRepository repository)
//        {
//            _repository = repository;
//        }

//        public void CompleteTask(Task task)
//        {
//            task.GetMaterialsRequired().Each(x =>
//            {
//                if (x.QuantityUsed <= 0) return;
//                var material = _repository.Find<InventoryMaterial>(x.Material.EntityId);
//                material.Quantity -= x.QuantityUsed;
//                _repository.Save(material);
//            });

//            task.GetFertilizersRequired().Each(x =>
//            {
//                if (x.QuantityUsed <= 0) return;
//                var fertilizer = _repository.Find<InventoryFertilizer>(x.Fertilizer.EntityId);
//                fertilizer.Quantity -= x.QuantityUsed;
//                _repository.Save(fertilizer);
//            });

//            task.Status = TaskStatus.Complete.Key;
//            _repository.Save(task);
//        }
//    }
//}