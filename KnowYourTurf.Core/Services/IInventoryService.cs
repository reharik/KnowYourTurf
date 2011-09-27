using System;
using KnowYourTurf.Core.Domain;
using System.Linq;
using xVal.ServerSide;

namespace KnowYourTurf.Core.Services
{
    public interface IInventoryService
    {
        ICrudManager ReceivePurchaseOrderLineItem(PurchaseOrderLineItem purchaseOrderLineItem, ICrudManager crudManager=null);
        ICrudManager DecrementTaskProduct(Task task, ICrudManager crudManager = null);
    }

    public class InventoryService : IInventoryService
    {
        private readonly IRepository _repository;
        private readonly ISaveEntityService _saveEntityService;

        public InventoryService(IRepository repository, ISaveEntityService saveEntityService)
        {
            _repository = repository;
            _saveEntityService = saveEntityService;
        }

        public ICrudManager ReceivePurchaseOrderLineItem(PurchaseOrderLineItem purchaseOrderLineItem, ICrudManager crudManager = null)
        {
            var inventoryProducts = _repository.Query<InventoryProduct>(x=>x.Product.EntityId == purchaseOrderLineItem.Product.EntityId&&x.UnitType==purchaseOrderLineItem.UnitType);
            var inventoryProduct = inventoryProducts.FirstOrDefault() ?? new InventoryProduct
                                                                             {
                                                                                 Product = purchaseOrderLineItem.Product,
                                                                                 UnitType = purchaseOrderLineItem.UnitType,
                                                                                 SizeOfUnit = purchaseOrderLineItem.SizeOfUnit.Value
                                                                             };

            inventoryProduct.LastVendor = purchaseOrderLineItem.PurchaseOrder.Vendor;
            if (inventoryProduct.Quantity.HasValue)
                inventoryProduct.Quantity += purchaseOrderLineItem.TotalReceived.Value;
            else inventoryProduct.Quantity = purchaseOrderLineItem.TotalReceived.Value;

            return _saveEntityService.ProcessSave(inventoryProduct, crudManager);
        }

        public ICrudManager DecrementTaskProduct(Task task, ICrudManager crudManager=null)
        {
            if (crudManager == null) crudManager = new CrudManager(_repository);
            if(task.InventoryProduct==null)
            {
                return crudManager;
            }
            if(!task.QuantityUsed.HasValue)
            {
                var crudReport = new CrudReport{Success = false};
                crudReport.AddErrorInfo(new ErrorInfo("QuantityUsed", CoreLocalizationKeys.QUANTITY_USED_REQUIRED.ToString()));
                crudManager.AddCrudReport(crudReport);
                return crudManager;
            }
            
            task.InventoryProduct.Quantity -= task.QuantityUsed.Value;
            return _saveEntityService.ProcessSave(task.InventoryProduct, crudManager);
        }
    }

}