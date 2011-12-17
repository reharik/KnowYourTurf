using System.Linq;
using System.Web.Mvc;
using KnowYourTurf.Core;
using KnowYourTurf.Core.CoreViewModelAndDTOs;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Enumerations;
using KnowYourTurf.Core.Html;
using KnowYourTurf.Core.Services;
using KnowYourTurf.Web.Areas.Schedule.Grids;
using KnowYourTurf.Web.Controllers;

namespace KnowYourTurf.Web.Areas.Schedule.Controllers
{
    public class ClientListController:KYTController
    {
        private readonly IDynamicExpressionQuery _dynamicExpressionQuery;
        private readonly IEntityListGrid<Client> _clientListGrid;

        public ClientListController(IDynamicExpressionQuery dynamicExpressionQuery,
            IEntityListGrid<Client> clientListGrid)
        {
            _dynamicExpressionQuery = dynamicExpressionQuery;
            _clientListGrid = clientListGrid;
        }

        public ActionResult ItemList(ViewModel input)
        {
            var url = UrlContext.GetUrlForAction<ClientListController>(x => x.Clients(null));
            var model = new ListViewModel()
            {
                AddUpdateUrl = UrlContext.GetUrlForAction<ClientController>(x => x.AddUpdate(null)),
                GridDefinition = _clientListGrid.GetGridDefinition(url)
            };
            return View(model);
        }

        public JsonResult Clients(GridItemsRequestModel input)
        {
            //TODO find way to deal with string here
            var items = _dynamicExpressionQuery.PerformQuery<Client>(input.filters);
            var gridItemsViewModel = _clientListGrid.GetGridItemsViewModel(input.PageSortFilter, items);
            return Json(gridItemsViewModel, JsonRequestBehavior.AllowGet);
        }
    }
}