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
            var inventoryProducts = _repository.Query<InventoryProduct>(x=>x.ReadOnlyProduct.EntityId == purchaseOrderLineItem.ReadOnlyProduct.EntityId&&x.UnitType==purchaseOrderLineItem.UnitType);
            InventoryProduct inventoryProduct;
            if (inventoryProducts.FirstOrDefault() != null) {inventoryProduct = inventoryProducts.FirstOrDefault();}
            else
            {
                inventoryProduct = new InventoryProduct
                                       {
                                           UnitType = purchaseOrderLineItem.UnitType,
                                           SizeOfUnit = purchaseOrderLineItem.SizeOfUnit.Value
                                       };
                inventoryProduct.SetProduct(purchaseOrderLineItem.ReadOnlyProduct);
            }

            inventoryProduct.SetLastVendor(purchaseOrderLineItem.PurchaseOrder.ReadOnlyVendor);
            if (inventoryProduct.Quantity.HasValue)
                inventoryProduct.Quantity += purchaseOrderLineItem.TotalReceived.Value;
            else inventoryProduct.Quantity = purchaseOrderLineItem.TotalReceived.Value;

            return _saveEntityService.ProcessSave(inventoryProduct, crudManager);
        }

        public ICrudManager DecrementTaskProduct(Task task, ICrudManager crudManager=null)
        {
            if (crudManager == null) crudManager = new CrudManager(_repository);
            if(task.ReadOnlyInventoryProduct==null)
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
            var inventoryProduct = _repository.Find<InventoryProduct>(task.ReadOnlyInventoryProduct.EntityId);
            inventoryProduct.Quantity -= task.QuantityUsed.Value;
            return _saveEntityService.ProcessSave(inventoryProduct, crudManager);
        }
    }

}