using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web.Mvc;
using HtmlTags;
using KnowYourTurf.Core;
using KnowYourTurf.Core.CoreViewModels;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Html;
using KnowYourTurf.Core.Html.Grid;
using KnowYourTurf.Core.Services;
using KnowYourTurf.Web.Grids;
using KnowYourTurf.Web.Models;

namespace KnowYourTurf.Web.Controllers
{
    public class VendorListController:KYTController
    {
       private readonly IDynamicExpressionQuery _dynamicExpressionQuery;
        private readonly IVendorListGrid _vendorListGrid;

        public VendorListController(IDynamicExpressionQuery dynamicExpressionQuery,
            IVendorListGrid vendorListGrid)
        {
            _dynamicExpressionQuery = dynamicExpressionQuery;
            _vendorListGrid = vendorListGrid;
        }

        public ActionResult VendorList(ListViewModel input)
        {
            var url = UrlContext.GetUrlForAction<VendorListController>(x => x.Vendors(null));
            ListViewModel model = new ListViewModel()
            {
                AddEditUrl = UrlContext.GetUrlForAction<VendorController>(x => x.AddEdit(null)),
                ListDefinition = _vendorListGrid.GetGridDefinition(url, WebLocalizationKeys.VENDORS)
            };
            return View(model);
        }
        
        public JsonResult Vendors(GridItemsRequestModel input)
        {
            var items = _dynamicExpressionQuery.PerformQuery<Vendor>();
            Action<IGridColumn, Vendor> mod = (c, v) =>
                                          {
                                              if (c.GetType() == typeof(ImageButtonColumn<Vendor>) && c.ColumnIndex == 10)
                                              {
                                                  var col = (ImageButtonColumn<Vendor>)c;
                                                  col.AddDataToEvent("{ 'ParentId' : " + v.EntityId + "}");
                                              }
                                          };

            _vendorListGrid.AddColumnModifications(mod);
            var gridItemsViewModel = _vendorListGrid.GetGridItemsViewModel(input.PageSortFilter, items);
            return Json(gridItemsViewModel, JsonRequestBehavior.AllowGet);
        }
    }
}