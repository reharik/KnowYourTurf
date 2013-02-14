using System.Collections.Generic;
using CC.Core.DomainTools;
using CC.Core.Html.Menu;
using CC.Security;
using KnowYourTurf.Core.Html.Menu;
using KnowYourTurf.Core.Services;
using KnowYourTurf.Web.Config;
using KnowYourTurf.Web.Controllers;

namespace KnowYourTurf.Web.Menus
{
    using KnowYourTurf.Web.Areas.Reporting.Controllers;

    public class MainMenu : IMenuConfig
    {
        private readonly IKYTMenuBuilder _builder;
        private readonly ISessionContext _sessionContext;

        public MainMenu(IKYTMenuBuilder builder, ISessionContext sessionContext)
        {
            _builder = builder;
            _sessionContext = sessionContext;
        }

        public IList<MenuItem> Build(bool withoutPermissions = false)
        {
            IUser user = null;
            if(!withoutPermissions)
            {
                user = _sessionContext.GetCurrentUser();
            }
            return DefaultMenubuilder(user);
        }

        private IList<MenuItem> DefaultMenubuilder(IUser user = null)
        {
            return _builder
                .CreateTagNode<EmployeeDashboardController>(WebLocalizationKeys.HOME)
                .SiteGroupForIteration()
                .CreateTagNode<FieldListController>(WebLocalizationKeys.FIELDS)
//                .CreateTagNode<CalculatorListController>(WebLocalizationKeys.CALCULATORS)

                .CreateNode(WebLocalizationKeys.TASKS)
                    .HasChildren()
                        .CreateNode(WebLocalizationKeys.TASK_LIST)
                            .HasChildren()
                                .CreateTagNode<TaskListController>(WebLocalizationKeys.TASKS)
                                .CreateTagNode<TaskListController>(WebLocalizationKeys.COMPLETED).Route("completedtasks")
                            .EndChildren()
                    .CreateTagNode<TaskCalendarController>(WebLocalizationKeys.TASK_CALENDAR)
                    .EndChildren()
                .CreateTagNode<EventCalendarController>(WebLocalizationKeys.EVENTS)
                .EndCategoryGroup()
                .CreateTagNode<EquipmentListController>(WebLocalizationKeys.EQUIPMENT)
                .CreateNode(WebLocalizationKeys.EQUIPMENT_TASKS)
                    .HasChildren()
                        .CreateNode(WebLocalizationKeys.EQUIPMENT_TASK_LISTS)
                            .HasChildren()
                                .CreateTagNode<EquipmentTaskListController>(WebLocalizationKeys.EQUIPMENT_TASKS)
                                .CreateTagNode<EquipmentTaskListController>(WebLocalizationKeys.COMPLETED_EQUIPMENT_TASKS).Route("completedequipmenttasks")
                            .EndChildren()
                    .CreateTagNode<EquipmentTaskCalendarController>(WebLocalizationKeys.EQUIPMENT_TASK_CALENDAR)
                    .EndChildren()

                .CreateTagNode<WeatherListController>(WebLocalizationKeys.WEATHER)
//                .CreateTagNode<ForumController>(WebLocalizationKeys.FORUM)
                 .CreateNode(WebLocalizationKeys.ADMIN_TOOLS, "tools")
                    .HasChildren()
                        .CreateTagNode<EmailJobListController>(WebLocalizationKeys.EMAIL_JOBS)
                        .CreateTagNode<EmployeeListController>(WebLocalizationKeys.EMPLOYEES)
                        .CreateTagNode<FacilitiesListController>(WebLocalizationKeys.FACILITIES)
                        .CreateTagNode<ListTypeListController>(WebLocalizationKeys.ENUMERATIONS)

                        .CreateNode(WebLocalizationKeys.PRODUCTS)
                        .HasChildren()
                            .CreateTagNode<MaterialListController>(WebLocalizationKeys.MATERIALS)
                            .CreateTagNode<FertilizerListController>(WebLocalizationKeys.FERTILIZERS)
                            .CreateTagNode<ChemicalListController>(WebLocalizationKeys.CHEMICALS)
                        .EndChildren()
                        .CreateTagNode<DocumentListController>(WebLocalizationKeys.DOCUMENTS)
                        .CreateTagNode<PhotoListController>(WebLocalizationKeys.PHOTOS)
                //.CreateNode<EmailJobListController>(c => c.ItemList(), WebLocalizationKeys.EMAIL_JOBS)
                //.CreateNode<EmailTemplateListController>(c => c.EmailTemplateList(null), WebLocalizationKeys.EMAIL_TEMPLATES)
                        .CreateTagNode<EquipmentVendorListController>(WebLocalizationKeys.EQUIPMENT_VENDORS)
                        .CreateTagNode<VendorListController>(WebLocalizationKeys.VENDORS)
                        .CreateNode(WebLocalizationKeys.INVENTORY)
                        .HasChildren()
                            .CreateTagNode<InventoryListController>(WebLocalizationKeys.MATERIALS).Route("inventorylist/0/0/0/Material")//, "ProductType=Material")
                            .CreateTagNode<InventoryListController>(WebLocalizationKeys.FERTILIZERS).Route("inventorylist/0/0/0/Fertilizer")//, "ProductType=Fertilizer")
                            .CreateTagNode<InventoryListController>(WebLocalizationKeys.CHEMICALS).Route("inventorylist/0/0/0/Chemical")//, "ProductType=Chemical")
                        .EndChildren()
                        .CreateNode(WebLocalizationKeys.PURCHASE_ORDERS)
                        .HasChildren()
                            .CreateTagNode<PurchaseOrderListController>(WebLocalizationKeys.CURRENT)
                            .CreateTagNode<CompletedPurchaseOrderListController>(WebLocalizationKeys.COMPLETED)
                        .EndChildren()
                        .CreateNode(WebLocalizationKeys.REPORTS)
                        .HasChildren()
                            .CreateTagNode<TaskReportController>(WebLocalizationKeys.TASK_REPORT)
                            .CreateTagNode<EquipmentTaskReportController>(WebLocalizationKeys.EQUIPMENT_TASK_REPORT)
                            .CreateTagNode<EmployeeDailyTasksController>(WebLocalizationKeys.EMPLOYEE_DAILY_TASKS)
                        .EndChildren()

                    .EndChildren()
                .MenuTree(user);
        }
    }
}
