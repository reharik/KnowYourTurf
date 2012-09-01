//using KnowYourTurf.Core.Domain;

//namespace KnowYourTurf.Core.Services
//{
//    public interface IReceivePurchaseOrderService
//    {
//        Notification ReceivePurchaseOrder(long POId);
//    }

//    public class ReceivePurchaseOrderService : IReceivePurchaseOrderService
//    {
//        private readonly IRepository _repository;
//        private readonly ICrudService _crudService;
//        private readonly IUrlResolver _resolver;

//        public ReceivePurchaseOrderService(IRepository repository,
//            ICrudService crudService,IUrlResolver resolver)
//        {
//            _repository = repository;
//            _crudService = crudService;
//            _resolver = resolver;
//        }

//        public Notification ReceivePurchaseOrder(long POId)
//        {
//            var purchaseOrder = _repository.Find<PurchaseOrder>(POId);
//            var notification = new Notification();
//            var completed = true;
//            purchaseOrder.GetLineItems().ForEachItem(x =>
//            {
//                if (x.TotalReceived <= 0)
//                {
//                    completed = false;
//                    return;
//                }
//                if(x.Product.GetType().Name=="VendorFertilizer")
//                {
//                    var fertilizer = ((VendorFertilizer) x.Product).Fertilizer;
//                    var inventoryFertilizer = _repository.Query<InventoryFertilizer>(y => y.Fertilizer.EntityId == fertilizer.EntityId).FirstOrDefault();
//                    inventoryFertilizer.Quantity += x.TotalReceived;
//                    notification = _crudService.ProcessSave(inventoryFertilizer, notification);
//                }
//                else
//                {
//                    var material = ((VendorMaterial)x.Product).Material;
//                    var inventoryMaterial = _repository.Query<InventoryMaterial>(y => y.Material.EntityId == material.EntityId).FirstOrDefault();
//                    inventoryMaterial.Quantity += x.TotalReceived;
//                    notification = _crudService.ProcessSave(inventoryMaterial, notification);
//                }
//                if(x.QuantityOrdered>x.TotalReceived)
//                {
//                    completed = false;
//                }
//            });
//            purchaseOrder.Completed = completed;
//            if(completed)
//            {
//                notification = _crudService.ProcessSave(purchaseOrder, notification);
//            }
//            if(notification.success)
//            {
//                notification.redirect = true;
//                notification.redirectUrl = _resolver.UrlFor<InventoryListController>(x => x.InventoryList(null));
//            }
//            return notification;
//        }
//    }
//}