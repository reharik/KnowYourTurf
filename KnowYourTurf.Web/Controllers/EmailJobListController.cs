using System.Web.Mvc;
using KnowYourTurf.Core;
using KnowYourTurf.Core.CoreViewModels;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Html;
using KnowYourTurf.Core.Services;

namespace KnowYourTurf.Web.Controllers
{
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

        public ActionResult ItemList()
        {
            var url = UrlContext.GetUrlForAction<EmailJobListController>(x => x.EmailJobs(null));
            ListViewModel model = new ListViewModel()
            {
                AddUpdateUrl = UrlContext.GetUrlForAction<EmailJobController>(x => x.EmailJob(null)),
                GridDefinition = _emailJobListGrid.GetGridDefinition(url)
            };
            return View(model);
        }

        public JsonResult EmailJobs(GridItemsRequestModel input)
        {
            var items = _dynamicExpressionQuery.PerformQuery<EmailJob>(input.filters);
            var gridItemsViewModel = _emailJobListGrid.GetGridItemsViewModel(input.PageSortFilter, items);
            return Json(gridItemsViewModel, JsonRequestBehavior.AllowGet);
        }
    }
}