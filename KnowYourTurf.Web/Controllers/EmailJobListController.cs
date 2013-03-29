using System.Web.Mvc;
using CC.Core.CoreViewModelAndDTOs;
using CC.Core.Html;
using CC.Core.Services;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Services;

namespace KnowYourTurf.Web.Controllers
{
    using KnowYourTurf.Web.Config;

    public class EmailJobListController : KYTController
    {
        private readonly IDynamicExpressionQuery _dynamicExpressionQuery;
        private readonly IEntityListGrid<EmailJob> _emailJobListGrid;

        public EmailJobListController(IDynamicExpressionQuery dynamicExpressionQuery,
            IEntityListGrid<EmailJob> emailJobListGrid)
        {
            _dynamicExpressionQuery = dynamicExpressionQuery;
            _emailJobListGrid = emailJobListGrid;
        }

        public ActionResult ItemList(ViewModel input)
        {
            var url = UrlContext.GetUrlForAction<EmailJobListController>(x => x.EmailJobs(null));
            ListViewModel model = new ListViewModel()
            {
                gridDef = _emailJobListGrid.GetGridDefinition(url, input.User)
            };
            model.headerButtons.Add("new");
            return new CustomJsonResult(model);
        }

        public JsonResult EmailJobs(GridItemsRequestModel input)
        {
            var items = _dynamicExpressionQuery.PerformQuery<EmailJob>(input.filters);
            var gridItemsViewModel = _emailJobListGrid.GetGridItemsViewModel(input.PageSortFilter, items, input.User);
            return new CustomJsonResult(gridItemsViewModel);
        }
    }
}