﻿using System.Linq;
using System.Web.Mvc;
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
    public class FertilizerListController:KYTController
    {
        private readonly IDynamicExpressionQuery _dynamicExpressionQuery;
        private readonly IFertilizerListGrid _fertilizerListGrid;

        public FertilizerListController(IDynamicExpressionQuery dynamicExpressionQuery,
            IFertilizerListGrid fertilizerListGrid)
        {
            _dynamicExpressionQuery = dynamicExpressionQuery;
            _fertilizerListGrid = fertilizerListGrid;
        }

        public ActionResult FertilizerList()
        {
            var url = UrlContext.GetUrlForAction<FertilizerListController>(x => x.Fertilizers(null));
            ListViewModel model = new ListViewModel()
            {
                AddEditUrl = UrlContext.GetUrlForAction<FertilizerController>(x => x.AddEdit(null)),
                ListDefinition = _fertilizerListGrid.GetGridDefinition(url, WebLocalizationKeys.FERTILIZERS),
                CrudTitle = WebLocalizationKeys.FERTILIZER_INFORMATION.ToString()
            };
            return View(model);
        }

        public JsonResult Fertilizers(GridItemsRequestModel input)
        {
            var items = _dynamicExpressionQuery.PerformQuery<Fertilizer>(input.filters);
            var gridItemsViewModel = _fertilizerListGrid.GetGridItemsViewModel(input.PageSortFilter, items);
            return Json(gridItemsViewModel, JsonRequestBehavior.AllowGet);
        }
    }
}