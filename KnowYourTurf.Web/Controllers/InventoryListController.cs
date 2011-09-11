using System.Linq;
using System.Web.Mvc;
using KnowYourTurf.Core;
using KnowYourTurf.Core.CoreViewModels;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Html;
using KnowYourTurf.Core.Html.Grid;
using KnowYourTurf.Core.Localization;
using KnowYourTurf.Core.Services;
using KnowYourTurf.Web.Grids;
using KnowYourTurf.Web.Models;

namespace KnowYourTurf.Web.Controllers
{
    public class InventoryListController:KYTController
    {
        private readonly IRepository _repository;
        private readonly IDynamicExpressionQuery _dynamicExpressionQuery;
        private readonly IInventoryProductListGrid _inventoryProductListGrid;

        public InventoryListController(IRepository repository, IDynamicExpressionQuery dynamicExpressionQuery,
            IInventoryProductListGrid inventoryProductListGrid )
        {
            _repository = repository;
            _dynamicExpressionQuery = dynamicExpressionQuery;
            _inventoryProductListGrid = inventoryProductListGrid;
        }

        public ActionResult InventoryProductList(string productType)
        {
            StringToken title = null;
            StringToken crudTitle = null;
            switch(productType)
            {
                case "Chemical":
                    title = WebLocalizationKeys.CHEMICALS;
                    crudTitle = WebLocalizationKeys.INVENTORY_CHEMICAL_INFORMATION;
                    break;
                case "Material":
                    title = WebLocalizationKeys.MATERIALS;
                    crudTitle = WebLocalizationKeys.INVENTORY_MATERIAL_INFORMATION;
                    break;
                case "Fertilizer":
                    title = WebLocalizationKeys.FERTILIZERS;
                    crudTitle = WebLocalizationKeys.INVENTORY_FERTILIZER_INFORMATION;
                    break;
                case "Seed":
                    title = WebLocalizationKeys.SEEDS;
                    crudTitle = WebLocalizationKeys.INVENTORY_SEED_INFORMATION;
                    break;
            }
            var url = UrlContext.GetUrlForAction<InventoryListController>(x => x.Products(null)) + "?ProductType=" + productType;
            ListViewModel model = new ListViewModel()
            {
                //TODO put modifiler here "ProductType=" + productType
                ListDefinition = _inventoryProductListGrid.GetGridDefinition(url, title),
                CrudTitle = crudTitle.ToString()
            };
            return View("Inventory" + productType + "List", model);
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
                var model = new InventoryChemicalViewModel {InventoryChemical = inventoryProduct};
                return PartialView("InventoryChemicalView", model);
            }
            if (inventoryProduct.Product.InstantiatingType == "Fertilizer")
            {
                var model = new InventoryFertilizerViewModel { InventoryFertilizer = inventoryProduct };
                return PartialView("InventoryFertilizerView", model);
            }
            if (inventoryProduct.Product.InstantiatingType == "Material")
            {
                var model = new InventoryMaterialViewModel { InventoryMaterial = inventoryProduct };
                return PartialView("InventoryMaterialView", model);
            }
            if (inventoryProduct.Product.InstantiatingType == "Seed")
            {
                var model = new InventorySeedViewModel { InventorySeed = inventoryProduct };
                return PartialView("InventorySeedView", model);
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

    public class InventoryFertilizerViewModel
    {
        public InventoryProduct InventoryFertilizer { get; set; }
        public Fertilizer Fertilizer { get { return InventoryFertilizer.Product as Fertilizer; } }
    }
    public class InventoryChemicalViewModel
    {
        public InventoryProduct InventoryChemical { get; set; }
        public Chemical Chemical { get { return InventoryChemical.Product as Chemical; } }
    }
    public class InventoryMaterialViewModel
    {
        public InventoryProduct InventoryMaterial { get; set; }
        public Material Material { get { return InventoryMaterial.Product as Material; } }
    }
    public class InventorySeedViewModel
    {
        public InventoryProduct InventorySeed { get; set; }
        public Seed Seed { get { return InventorySeed.Product as Seed; } }
    }
}