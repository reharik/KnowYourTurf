using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using CC.Core;
using CC.Core.CoreViewModelAndDTOs;
using CC.Core.DomainTools;
using CC.Core.Html;
using CC.Core.Services;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Services;
using StructureMap;

namespace KnowYourTurf.Web.Controllers
{
    public class EquipmentTaskListController:KYTController
    {
       private readonly IDynamicExpressionQuery _dynamicExpressionQuery;
        private readonly IRepository _repository;

        public EquipmentTaskListController(IDynamicExpressionQuery dynamicExpressionQuery,
            IRepository repository)
        {
            _dynamicExpressionQuery = dynamicExpressionQuery;
            _repository = repository;
        }

        public ActionResult ItemList(ViewModel input)
        {
            var _pendingEquipmentTaskGrid = ObjectFactory.Container.GetInstance<IEntityListGrid<EquipmentTask>>("PendingEquipmentTasks");
            var url = UrlContext.GetUrlForAction<EquipmentTaskListController>(x => x.EquipmentTasks(null)) + "?RootId=" + input.RootId;
            ListViewModel model = new ListViewModel()
            {
                //AddUpdateUrl = UrlContext.GetUrlForAction<EquipmentTaskController>(x => x.AddUpdate(null)),
                deleteMultipleUrl = UrlContext.GetUrlForAction<EquipmentTaskController>(x => x.DeleteMultiple(null)),
                gridDef = _pendingEquipmentTaskGrid.GetGridDefinition(url, input.User),
                _Title = WebLocalizationKeys.TASKS.ToString(),
                searchField = "EquipmentTaskType.Name"
            };
            model.headerButtons.Add("new");
            model.headerButtons.Add("delete");
            return Json(model,JsonRequestBehavior.AllowGet);
        }

        public JsonResult EquipmentTasks(GridItemsRequestModel input)
        {
            var _pendingEquipmentTaskGrid = ObjectFactory.Container.GetInstance<IEntityListGrid<EquipmentTask>>("PendingEquipmentTasks");
            var equipment = _repository.Query<Equipment>( x => x.Tasks.Any(y =>!y.Complete));
            var equipTasks = new List<EquipmentTask>();
            equipment.ForEachItem(x => equipTasks.AddRange(x.GetAllEquipmentTasks(y => !y.Complete)));

            var items = _dynamicExpressionQuery.PerformQuery(equipTasks, input.filters);
            var gridItemsViewModel = _pendingEquipmentTaskGrid.GetGridItemsViewModel(input.PageSortFilter, items, input.User);
            return Json(gridItemsViewModel, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CompletedEquipmentTasksGrid(ViewModel input)
        {
            var _completedEquipmentTaskGrid = ObjectFactory.Container.GetInstance<IEntityListGrid<EquipmentTask>>("CompletedEquipmentTasks");
            var url = UrlContext.GetUrlForAction<EquipmentTaskListController>(x => x.CompletedEquipmentTasks(null)) + "?RootId=" + input.RootId;
            ListViewModel model = new ListViewModel()
            {
                gridDef = _completedEquipmentTaskGrid.GetGridDefinition(url, input.User),
                ParentId = input.ParentId,
                _Title = WebLocalizationKeys.COMPLETED_TASKS.ToString(),

            };
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        public JsonResult CompletedEquipmentTasks(GridItemsRequestModel input)
        {
            var _completedEquipmentTaskGrid = ObjectFactory.Container.GetInstance<IEntityListGrid<EquipmentTask>>("CompletedEquipmentTasks");
            var equipment = _repository.Query<Equipment>( x => x.Tasks.Any(y =>!y.Complete));
            var equipTasks = new List<EquipmentTask>();
            equipment.ForEachItem(x => equipTasks.AddRange(x.GetAllEquipmentTasks(y => y.Complete)));
            
            var items = _dynamicExpressionQuery.PerformQuery(equipTasks, input.filters);
            var gridItemsViewModel = _completedEquipmentTaskGrid.GetGridItemsViewModel(input.PageSortFilter, items, input.User);
            return Json(gridItemsViewModel, JsonRequestBehavior.AllowGet);
        }
    }
}
