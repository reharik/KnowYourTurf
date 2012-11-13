using System;
using CC.Core.DomainTools;
using CC.Core.Services;
using KnowYourTurf.Core.Domain;
using System.Linq;
using xVal.ServerSide;

namespace KnowYourTurf.Core.Services
{
    public interface IInventoryService
    {
        IValidationManager ReceivePurchaseOrderLineItem(PurchaseOrderLineItem purchaseOrderLineItem, IValidationManager crudManager = null);
        IValidationManager DecrementTaskProduct(Task task, IValidationManager crudManager = null);
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

        public IValidationManager ReceivePurchaseOrderLineItem(PurchaseOrderLineItem purchaseOrderLineItem, IValidationManager crudManager = null)
        {
            var inventoryProducts = _repository.Query<InventoryProduct>(x=>x.Product.EntityId == purchaseOrderLineItem.Product.EntityId&&x.UnitType==purchaseOrderLineItem.UnitType);
            InventoryProduct inventoryProduct;
            if (inventoryProducts.FirstOrDefault() != null) {inventoryProduct = inventoryProducts.FirstOrDefault();}
            else
            {
                inventoryProduct = new InventoryProduct
                                       {
                                           UnitType = purchaseOrderLineItem.UnitType,
                                           SizeOfUnit = purchaseOrderLineItem.SizeOfUnit.Value
                                       };
                inventoryProduct.Product =purchaseOrderLineItem.Product;
            }

            inventoryProduct.LastFieldVendor = purchaseOrderLineItem.PurchaseOrder.FieldVendor;
            if (inventoryProduct.Quantity.HasValue)
                inventoryProduct.Quantity += purchaseOrderLineItem.TotalReceived.Value;
            else inventoryProduct.Quantity = purchaseOrderLineItem.TotalReceived.Value;

            return _saveEntityService.ProcessSave(inventoryProduct);
        }

        public IValidationManager DecrementTaskProduct(Task task, IValidationManager crudManager = null)
        {
            if (crudManager == null) crudManager = new ValidationManager(_repository);
            if(task.InventoryProduct==null)
            {
                return crudManager;
            }
            if(!task.QuantityUsed.HasValue)
            {
                var crudReport = new ValidationReport { Success = false };
                crudReport.AddErrorInfo(new ErrorInfo("QuantityUsed", CoreLocalizationKeys.QUANTITY_USED_REQUIRED.ToString()));
                crudManager.AddValidationReport(crudReport);
                return crudManager;
            }
            var inventoryProduct = _repository.Find<InventoryProduct>(task.InventoryProduct.EntityId);
            inventoryProduct.Quantity -= task.QuantityUsed.Value;
            return _saveEntityService.ProcessSave(inventoryProduct, crudManager);
        }
    }

}