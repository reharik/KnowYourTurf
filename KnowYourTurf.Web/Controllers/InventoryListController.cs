using System.Web.Mvc;
using KnowYourTurf.Core;
using KnowYourTurf.Core.CoreViewModels;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Html;
using KnowYourTurf.Core.Localization;
using KnowYourTurf.Core.Services;

namespace KnowYourTurf.Web.Controllers
{
    public class InventoryListController : AdminControllerBase
    {
        private readonly IRepository _repository;
        private readonly IDynamicExpressionQuery _dynamicExpressionQuery;
        private readonly IEntityListGrid<InventoryProduct> _inventoryProductListGrid;

        public InventoryListController(IRepository repository, IDynamicExpressionQuery dynamicExpressionQuery,
            IEntityListGrid<InventoryProduct> inventoryProductListGrid )
        {
            _repository = repository;
            _dynamicExpressionQuery = dynamicExpressionQuery;
            _inventoryProductListGrid = inventoryProductListGrid;
        }

        public ActionResult ItemList(InventoryProductListViewModel input)
        {
            StringToken title = null;
            StringToken crudTitle = null;
            switch (input.ProductType)
            {
                case "Chemical":
                    crudTitle = WebLocalizationKeys.INVENTORY_CHEMICAL_INFORMATION;
                    break;
                case "Material":
                    crudTitle = WebLocalizationKeys.INVENTORY_MATERIAL_INFORMATION;
                    break;
                case "Fertilizer":
                    crudTitle = WebLocalizationKeys.INVENTORY_FERTILIZER_INFORMATION;
                    break;
            }
            var url = UrlContext.GetUrlForAction<InventoryListController>(x => x.Products(null)) + "?ProductType=" + input.ProductType;
            ListViewModel model = new ListViewModel()
            {
                //TODO put modifiler here "ProductType=" + productType
                GridDefinition = _inventoryProductListGrid.GetGridDefinition(url),
                Title = crudTitle.ToString()
            };
            return View("Inventory" + input.ProductType + "List", model);
        }

        public JsonResult Products(InventoryProductGridItemsRequestModel input)
        {
            var items = _dynamicExpressionQuery.PerformQuery<InventoryProduct>(input.filters,x=>x.Product.InstantiatingType == input.ProductType);
            var gridItemsViewModel = _inventoryProductListGrid.GetGridItemsViewModel(input.PageSortFilter, items);
            return Json(gridItemsViewModel, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Display(InventoryProductListViewModel input)
        {
            var inventoryProduct = _repository.Find<InventoryProduct>(input.EntityId);
            if (inventoryProduct.Product.InstantiatingType == "Chemical")
            {
                var model = new InventoryChemicalViewModel {Item = inventoryProduct,Title=WebLocalizationKeys.INVENTORY_CHEMICAL_INFORMATION.ToString()};
                return PartialView("InventoryChemicalView", model);
            }
            if (inventoryProduct.Product.InstantiatingType == "Fertilizer")
            {
                var model = new InventoryFertilizerViewModel { Item = inventoryProduct,Title=WebLocalizationKeys.INVENTORY_FERTILIZER_INFORMATION.ToString() };
                return PartialView("InventoryFertilizerView", model);
            }
            if (inventoryProduct.Product.InstantiatingType == "Material")
            {
                var model = new InventoryMaterialViewModel { Item = inventoryProduct, Title = WebLocalizationKeys.INVENTORY_MATERIAL_INFORMATION.ToString()};
                return PartialView("InventoryMaterialView", model);
            }
            return null;
        }

    }
    
    public class InventoryProductListViewModel:ViewModel
    {
        public string ProductType { get; set; }
    }

    public class InventoryProductGridItemsRequestModel : GridItemsRequestModel
    {
        public string ProductType { get; set; }
    }

    public class InventoryFertilizerViewModel : ViewModel
    {
        public InventoryProduct Item { get; set; }
        public Fertilizer Fertilizer { get { return Item.Product as Fertilizer; } }
    }
    public class InventoryChemicalViewModel :ViewModel
    {
        public InventoryProduct Item { get; set; }
        public Chemical Chemical { get { return Item.Product as Chemical; } }
    }
    public class InventoryMaterialViewModel : ViewModel
    {
        public InventoryProduct Item { get; set; }
        public Material Material { get { return Item.Product as Material; } }
    }
}