using System;
using System.Web.Mvc;
using CC.Core.CoreViewModelAndDTOs;
using CC.Core.Html;
using CC.Core.Html.Grid;
using CC.Core.Services;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Services;

namespace KnowYourTurf.Web.Controllers
{
    using KnowYourTurf.Web.Config;

    public class VendorListController:KYTController
    {
       private readonly IDynamicExpressionQuery _dynamicExpressionQuery;
       private readonly IEntityListGrid<FieldVendor> _vendorListGrid;

        public VendorListController(IDynamicExpressionQuery dynamicExpressionQuery,
            IEntityListGrid<FieldVendor> vendorListGrid)
        {
            _dynamicExpressionQuery = dynamicExpressionQuery;
            _vendorListGrid = vendorListGrid;
        }

        public ActionResult ItemList(ListViewModel input)
        {
            var url = UrlContext.GetUrlForAction<VendorListController>(x => x.Vendors(null));
            ListViewModel model = new ListViewModel()
            {
                deleteMultipleUrl = UrlContext.GetUrlForAction<VendorController>(x => x.DeleteMultiple(null)),
                gridDef = _vendorListGrid.GetGridDefinition(url, input.User),
                _Title = WebLocalizationKeys.VENDORS.ToString()
            };
            model.headerButtons.Add("new");
            model.headerButtons.Add("delete");
            return new CustomJsonResult(model);
        }
        
        public JsonResult Vendors(GridItemsRequestModel input)
        {
            var items = _dynamicExpressionQuery.PerformQuery<FieldVendor>(input.filters);
            Action<IGridColumn, FieldVendor> mod = (c, v) =>
                                          {
                                              if (c.GetType() == typeof(ImageButtonColumn<FieldVendor>) && c.ColumnIndex == 10)
                                              {
                                                  var col = (ImageButtonColumn<FieldVendor>)c;
                                                  col.AddDataToEvent("{ 'ParentId' : " + v.EntityId + "}");
                                              }
                                          };

            _vendorListGrid.AddColumnModifications(mod);
            var gridItemsViewModel = _vendorListGrid.GetGridItemsViewModel(input.PageSortFilter, items, input.User);
            return new CustomJsonResult(gridItemsViewModel);
        }
    }
}