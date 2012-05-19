using System.Collections.Generic;
using KnowYourTurf.Core.Enums;
using KnowYourTurf.Core.Html.Menu;
using KnowYourTurf.Web.Config;
using KnowYourTurf.Web.Controllers;

namespace KnowYourTurf.Web.Menus
{
    public class MainMenu : IMenuConfig
    {
        private readonly IMenuBuilder _builder;

        public MainMenu(IMenuBuilder builder)
        {
            _builder = builder;
        }

        public IList<MenuItem> Build(bool withoutPermissions = false)
        {
            return DefaultMenubuilder(withoutPermissions);
        }

        private IList<MenuItem> DefaultMenubuilder(bool withoutPermissions = false)
        {
            return _builder
                .CreateNode<EmployeeDashboardController>(c => c.ViewEmployee(null), WebLocalizationKeys.HOME).WithOperation(true)
                .CategoryGroupForItteration()
                .CreateNode<FieldListController>(c => c.FieldList(null), WebLocalizationKeys.FIELDS)
                .CreateNode(WebLocalizationKeys.TASKS).WithOperation(true)
                    .HasChildren()
                    .CreateNode<TaskListController>(c => c.TaskList(null), WebLocalizationKeys.TASK_LIST).WithOperation(true)
                    .CreateNode<TaskCalendarController>(c => c.TaskCalendar(null), WebLocalizationKeys.TASK_CALENDAR).WithOperation(true)
                    .EndChildren()
                .CreateNode<EventCalendarController>(c => c.EventCalendar(null), WebLocalizationKeys.EVENTS).WithOperation(true)
                .EndCategoryGroup()
                .CreateNode<EquipmentListController>(c => c.EquipmentList(), WebLocalizationKeys.EQUIPMENT).WithOperation(true)
                .CreateNode<CalculatorListController>(c => c.CalculatorList(), WebLocalizationKeys.CALCULATORS).WithOperation(true)
                .CreateNode<WeatherListController>(c => c.WeatherList(null), WebLocalizationKeys.WEATHER).WithOperation(true)
                .CreateNode<ForumController>(c => c.Forum(), WebLocalizationKeys.FORUM)
                 .CreateNode(WebLocalizationKeys.ADMIN_TOOLS, "tools").WithOperation(true)
                    .HasChildren()
                        .CreateNode<EmailJobListController>(c => c.ItemList(), WebLocalizationKeys.EMAIL_JOBS).WithOperation(true)
                        .CreateNode<EmployeeListController>(c => c.EmployeeList(), WebLocalizationKeys.EMPLOYEES).WithOperation(true)
                        .CreateNode<FacilitiesListController>(c => c.FacilitiesList(), WebLocalizationKeys.FACILITIES).WithOperation(true)
                        .CreateNode<ListTypeListController>(c => c.ListType(), WebLocalizationKeys.ENUMERATIONS).WithOperation(true)

                        .CreateNode(WebLocalizationKeys.PRODUCTS).WithOperation(true)
                        .HasChildren()
                            .CreateNode<MaterialListController>(c => c.MaterialList(), WebLocalizationKeys.MATERIALS).WithOperation(true)
                            .CreateNode<FertilizerListController>(c => c.FertilizerList(), WebLocalizationKeys.FERTILIZERS).WithOperation(true)
                            .CreateNode<ChemicalListController>(c => c.ChemicalList(), WebLocalizationKeys.CHEMICALS).WithOperation(true)
                        .EndChildren()
                        .CreateNode<DocumentListController>(c => c.DocumentList(null), WebLocalizationKeys.DOCUMENTS).WithOperation(true)
                        .CreateNode<PhotoListController>(c => c.PhotoList(null), WebLocalizationKeys.PHOTOS).WithOperation(true)
                //.CreateNode<EmailJobListController>(c => c.ItemList(), WebLocalizationKeys.EMAIL_JOBS)
                //.CreateNode<EmailTemplateListController>(c => c.EmailTemplateList(null), WebLocalizationKeys.EMAIL_TEMPLATES)
                        .CreateNode<VendorListController>(c => c.VendorList(null), WebLocalizationKeys.VENDORS).WithOperation(true)
                        .CreateNode(WebLocalizationKeys.INVENTORY).WithOperation(true)
                        .HasChildren()
                            .CreateNode<InventoryListController>(c => c.InventoryProductList(null), WebLocalizationKeys.MATERIALS, "ProductType=Material").WithOperation(true)
                            .CreateNode<InventoryListController>(c => c.InventoryProductList(null), WebLocalizationKeys.FERTILIZERS, "ProductType=Fertilizer").WithOperation(true)
                            .CreateNode<InventoryListController>(c => c.InventoryProductList(null), WebLocalizationKeys.CHEMICALS, "ProductType=Chemical").WithOperation(true)
                        .EndChildren()
                        .CreateNode(WebLocalizationKeys.PURCHASE_ORDERS).WithOperation(true)
                        .HasChildren()
                            .CreateNode<PurchaseOrderListController>(c => c.PurchaseOrderList(null), WebLocalizationKeys.CURRENT).WithOperation(true)
                            .CreateNode<PurchaseOrderListController>(c => c.PurchaseOrderList(null), WebLocalizationKeys.COMPLETED).addUrlParameter("Completed", "true").WithOperation(true)
                        .EndChildren()
                    .EndChildren()
                .MenuTree(withoutPermissions);
        }
    }
}