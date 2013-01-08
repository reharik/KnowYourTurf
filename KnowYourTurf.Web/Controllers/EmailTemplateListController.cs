using System.Web.Mvc;
using CC.Core.CoreViewModelAndDTOs;
using CC.Core.Html;
using CC.Core.Services;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Services;

namespace KnowYourTurf.Web.Controllers
{
    using KnowYourTurf.Web.Config;

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
                gridDef = _emailTemplateListGrid.GetGridDefinition(url, input.User)
            };
            return View(model);
        }

        public JsonResult EmailTemplates(GridItemsRequestModel input)
        {
            var items = _dynamicExpressionQuery.PerformQuery<EmailTemplate>();
            var gridItemsViewModel = _emailTemplateListGrid.GetGridItemsViewModel(input.PageSortFilter, items, input.User);
            return new CustomJsonResult(gridItemsViewModel);
        }
    }
}