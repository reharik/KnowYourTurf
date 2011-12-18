using System.Web.Mvc;
using KnowYourTurf.Core;
using KnowYourTurf.Core.CoreViewModels;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Html;
using KnowYourTurf.Core.Services;

namespace KnowYourTurf.Web.Controllers
{
    public class EmailTemplateListController:KYTController
    {
       private readonly IDynamicExpressionQuery _dynamicExpressionQuery;
       private readonly IEntityListGrid<EmailTemplate> _emailTemplateListGrid;

        public EmailTemplateListController(IDynamicExpressionQuery dynamicExpressionQuery,
            IEntityListGrid<EmailTemplate> emailTemplateListGrid)
        {
            _dynamicExpressionQuery = dynamicExpressionQuery;
            _emailTemplateListGrid = emailTemplateListGrid;
        }

        public ActionResult EmailTemplateList(ListViewModel input)
        {
            var url = UrlContext.GetUrlForAction<EmailTemplateListController>(x => x.EmailTemplates(null));
            ListViewModel model = new ListViewModel()
            {
                AddUpdateUrl = UrlContext.GetUrlForAction<EmailTemplateController>(x => x.AddUpdate(null)),
                GridDefinition = _emailTemplateListGrid.GetGridDefinition(url)
            };
            return View(model);
        }

        public JsonResult EmailTemplates(GridItemsRequestModel input)
        {
            var items = _dynamicExpressionQuery.PerformQuery<EmailTemplate>();
            var gridItemsViewModel = _emailTemplateListGrid.GetGridItemsViewModel(input.PageSortFilter, items);
            return Json(gridItemsViewModel, JsonRequestBehavior.AllowGet);
        }
    }
}