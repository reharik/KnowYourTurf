using System.Web.Mvc;
using AutoMapper;
using CC.Core.CoreViewModelAndDTOs;
using CC.Core.CustomAttributes;
using CC.Core.DomainTools;
using CC.Core.Html;
using CC.Core.Localization;
using CC.Core.Services;
using Castle.Components.Validator;
using KnowYourTurf.Core.Domain;
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
            switch (input.Var)
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
            var url = UrlContext.GetUrlForAction<InventoryListController>(x => x.Products(null)) + "?ProductType=" + input.Var;
            ListViewModel model = new ListViewModel()
            {
                //TODO put modifiler here "ProductType=" + productType
                gridDef = _inventoryProductListGrid.GetGridDefinition(url, input.User),
                _Title = crudTitle.ToString(),
                searchField = "Product.Name"
            };
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Products(InventoryProductGridItemsRequestModel input)
        {
            var items = _dynamicExpressionQuery.PerformQuery<InventoryProduct>(input.filters, x => x.Product.InstantiatingType == input.ProductType);
            var gridItemsViewModel = _inventoryProductListGrid.GetGridItemsViewModel(input.PageSortFilter, items, input.User);
            return Json(gridItemsViewModel, JsonRequestBehavior.AllowGet);
        }


        public ActionResult Display_Template(InventoryProductListViewModel input)
        {
            var inventoryProduct = _repository.Find<InventoryProduct>(input.EntityId);
            if (inventoryProduct.Product.InstantiatingType == "Chemical")
            {
                return View("InventoryChemicalView", new InventoryChemicalViewModel());
            }
            if (inventoryProduct.Product.InstantiatingType == "Fertilizer")
            {
                return View("InventoryFertilizerView", new InventoryFertilizerViewModel());
            }
            if (inventoryProduct.Product.InstantiatingType == "Material")
            {
                return View("InventoryMaterialView", new InventoryMaterialViewModel());
            }
            return null;
        }

        public ActionResult Display(InventoryProductListViewModel input)
        {
            InventoryProductViewModel model = new InventoryProductViewModel();
            var inventoryProduct = _repository.Find<InventoryProduct>(input.EntityId);
            if (inventoryProduct.Product.InstantiatingType == "Chemical")
            {
                model = Mapper.Map<InventoryProduct, InventoryChemicalViewModel>(inventoryProduct);
                model._Title = WebLocalizationKeys.INVENTORY_CHEMICAL_INFORMATION.ToString();
            }
            if (inventoryProduct.Product.InstantiatingType == "Fertilizer")
            {
                model = Mapper.Map<InventoryProduct, InventoryFertilizerViewModel>(inventoryProduct);
                model._Title = WebLocalizationKeys.INVENTORY_FERTILIZER_INFORMATION.ToString();
            }
            if (inventoryProduct.Product.InstantiatingType == "Material")
            {
                model = Mapper.Map<InventoryProduct, InventoryMaterialViewModel>(inventoryProduct);
                model._Title = WebLocalizationKeys.INVENTORY_MATERIAL_INFORMATION.ToString();
            }
            return Json(model, JsonRequestBehavior.AllowGet);
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

    public class InventoryProductViewModel : ViewModel
    {
        [ValidateNonEmpty]
        public string Name { get; set; }
        [TextArea]
        public string Description { get; set; }
        public string Notes { get; set; }
        public double? Quantity { get; set; }
        public int SizeOfUnit { get; set; }
        public string UnitType { get; set; }
        public string LastVendorCompany { get; set; }
}

    public class InventoryFertilizerViewModel : InventoryProductViewModel
    {
        [ValidateNonEmpty, ValidateDouble]
        public double N { get; set; }
        [ValidateNonEmpty, ValidateDouble]
        public double P { get; set; }
        [ValidateNonEmpty, ValidateDouble]
        public double K { get; set; }
    }
    public class InventoryChemicalViewModel : InventoryProductViewModel
    {
        public string Manufacturer { get; set; }
        public string ActiveIngredient { get; set; }
        [ValidateDecimal]
        public decimal ActiveIngredientPercent { get; set; }
        public string EPARegNumber { get; set; }
        public string EPAEstNumber { get; set; }
    }
    public class InventoryMaterialViewModel : InventoryProductViewModel
    {
    }
}