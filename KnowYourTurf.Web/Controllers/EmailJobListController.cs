using System.Linq;
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
    public class EmailJobListController : KYTController
    {
        private readonly IDynamicExpressionQuery _dynamicExpressionQuery;
        private readonly IEmailJobListGrid _emailJobListGrid;

        public EmailJobListController(IDynamicExpressionQuery dynamicExpressionQuery,
            IEmailJobListGrid emailJobListGrid)
        {
            _dynamicExpressionQuery = dynamicExpressionQuery;
            _emailJobListGrid = emailJobListGrid;
        }

        public ActionResult EmailJobList()
        {
            var url = UrlContext.GetUrlForAction<EmailJobListController>(x => x.EmailJobs(null));
            ListViewModel model = new ListViewModel()
            {
                AddEditUrl = UrlContext.GetUrlForAction<EmailJobController>(x => x.EmailJob(null)),
                ListDefinition = _emailJobListGrid.GetGridDefinition(url, WebLocalizationKeys.EMAIL_TEMPLATES),
                CrudTitle = WebLocalizationKeys.EMAIL_JOB_INFORMATION.ToString()
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