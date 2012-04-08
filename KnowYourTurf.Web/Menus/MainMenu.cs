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
                .CreateNode<EmployeeDashboardController>(c => c.ViewEmployee(null), WebLocalizationKeys.HOME)
                .CategoryGroupForItteration()
                .CreateNode<FieldListController>(c => c.FieldList(), WebLocalizationKeys.FIELDS)
                .CreateNode(WebLocalizationKeys.TASKS)
                    .HasChildren()
                    .CreateNode<TaskListController>(c => c.TaskList(), WebLocalizationKeys.TASK_LIST)
                    .CreateNode<TaskCalendarController>(c => c.TaskCalendar(), WebLocalizationKeys.TASK_CALENDAR)
                    .EndChildren()
                .CreateNode<EventCalendarController>(c => c.EventCalendar(), WebLocalizationKeys.EVENTS)
                .EndCategoryGroup()
                .CreateNode<EquipmentListController>(c => c.EquipmentList(), WebLocalizationKeys.EQUIPMENT)
                .CreateNode<CalculatorListController>(c => c.CalculatorList(), WebLocalizationKeys.CALCULATORS)
                .CreateNode<WeatherListController>(c => c.WeatherList(null), WebLocalizationKeys.WEATHER)
                .CreateNode<ForumController>(c => c.Forum(), WebLocalizationKeys.FORUM)
                 .CreateNode(WebLocalizationKeys.ADMIN_TOOLS, "tools")
                    .HasChildren()
                        .CreateNode<EmailJobListController>(c => c.ItemList(), WebLocalizationKeys.EMAIL_JOBS)
                        .CreateNode<EmployeeListController>(c => c.EmployeeList(), WebLocalizationKeys.EMPLOYEES)
                        .CreateNode<FacilitiesListController>(c => c.FacilitiesList(), WebLocalizationKeys.FACILITIES)
                        .CreateNode<ListTypeListController>(c => c.ListType(), WebLocalizationKeys.ENUMERATIONS)

                        .CreateNode(WebLocalizationKeys.PRODUCTS)
                        .HasChildren()
                            .CreateNode<MaterialListController>(c => c.MaterialList(), WebLocalizationKeys.MATERIALS)
                            .CreateNode<FertilizerListController>(c => c.FertilizerList(), WebLocalizationKeys.FERTILIZERS)
                            .CreateNode<ChemicalListController>(c => c.ChemicalList(), WebLocalizationKeys.CHEMICALS)
                        .EndChildren()
                        .CreateNode<DocumentListController>(c => c.DocumentList(null), WebLocalizationKeys.DOCUMENTS)
                        .CreateNode<PhotoListController>(c => c.PhotoList(null), WebLocalizationKeys.PHOTOS)
                //.CreateNode<EmailJobListController>(c => c.ItemList(), WebLocalizationKeys.EMAIL_JOBS)
                //.CreateNode<EmailTemplateListController>(c => c.EmailTemplateList(null), WebLocalizationKeys.EMAIL_TEMPLATES)
                        .CreateNode<VendorListController>(c => c.VendorList(null), WebLocalizationKeys.VENDORS)
                        .CreateNode(WebLocalizationKeys.INVENTORY)
                        .HasChildren()
                            .CreateNode<InventoryListController>(c => c.InventoryProductList(null), WebLocalizationKeys.MATERIALS, "ProductType=Material")
                            .CreateNode<InventoryListController>(c => c.InventoryProductList(null), WebLocalizationKeys.FERTILIZERS, "ProductType=Fertilizer")
                            .CreateNode<InventoryListController>(c => c.InventoryProductList(null), WebLocalizationKeys.CHEMICALS, "ProductType=Chemical")
                        .EndChildren()
                        .CreateNode(WebLocalizationKeys.PURCHASE_ORDERS)
                        .HasChildren()
                            .CreateNode<PurchaseOrderListController>(c => c.PurchaseOrderList(null), WebLocalizationKeys.CURRENT)
                            .CreateNode<PurchaseOrderListController>(c => c.PurchaseOrderList(null), WebLocalizationKeys.COMPLETED).addUrlParameter("Completed", "true")
                        .EndChildren()
                    .EndChildren()
                .MenuTree(withoutPermissions);
        }
    }
}