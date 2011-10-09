using System.Web.Mvc;
using KnowYourTurf.Core;
using KnowYourTurf.Core.CoreViewModels;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Html;
using KnowYourTurf.Core.Services;
using KnowYourTurf.Web.Grids;
using KnowYourTurf.Web.Models;

namespace KnowYourTurf.Web.Controllers
{
    public class EmailTemplateListController:KYTController
    {
       private readonly IDynamicExpressionQuery _dynamicExpressionQuery;
        private readonly IEmailTemplateListGrid _emailTemplateListGrid;

        public EmailTemplateListController(IDynamicExpressionQuery dynamicExpressionQuery,
            IEmailTemplateListGrid emailTemplateListGrid)
        {
            _dynamicExpressionQuery = dynamicExpressionQuery;
            _emailTemplateListGrid = emailTemplateListGrid;
        }

        public ActionResult EmailTemplateList(ListViewModel input)
        {
            var url = UrlContext.GetUrlForAction<EmailTemplateListController>(x => x.EmailTemplates(null));
            ListViewModel model = new ListViewModel()
            {
                AddEditUrl = UrlContext.GetUrlForAction<EmailTemplateController>(x => x.AddEdit(null)),
                ListDefinition = _emailTemplateListGrid.GetGridDefinition(url, WebLocalizationKeys.WEATHER)
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