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
                .CreateTagNode<EmployeeDashboardController>(WebLocalizationKeys.HOME)
                .CategoryGroupForItteration()
                .CreateTagNode<FieldListController>(WebLocalizationKeys.FIELDS)
                .CreateNode(WebLocalizationKeys.TASKS)
                    .HasChildren()
                    .CreateTagNode<TaskListController>(WebLocalizationKeys.TASK_LIST)
                    .CreateTagNode<TaskCalendarController>(WebLocalizationKeys.TASK_CALENDAR)
                    .EndChildren()
                .CreateTagNode<EventCalendarController>(WebLocalizationKeys.EVENTS)
                .EndCategoryGroup()
                .CreateTagNode<EquipmentListController>(WebLocalizationKeys.EQUIPMENT)
                .CreateTagNode<CalculatorListController>(WebLocalizationKeys.CALCULATORS)
                .CreateTagNode<WeatherListController>(WebLocalizationKeys.WEATHER)
                .CreateTagNode<ForumController>(WebLocalizationKeys.FORUM)
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
                        .CreateTagNode<VendorListController>(WebLocalizationKeys.VENDORS)
                        .CreateNode(WebLocalizationKeys.INVENTORY)
                        .HasChildren()
                            .CreateTagNode<InventoryListController>(WebLocalizationKeys.MATERIALS, "ProductType=Material")
                            .CreateTagNode<InventoryListController>(WebLocalizationKeys.FERTILIZERS, "ProductType=Fertilizer")
                            .CreateTagNode<InventoryListController>(WebLocalizationKeys.CHEMICALS, "ProductType=Chemical")
                        .EndChildren()
                        .CreateNode(WebLocalizationKeys.PURCHASE_ORDERS)
                        .HasChildren()
                            .CreateTagNode<PurchaseOrderListController>(WebLocalizationKeys.CURRENT)
                            .CreateTagNode<PurchaseOrderListController>(WebLocalizationKeys.COMPLETED).addUrlParameter("Completed", "true")
                        .EndChildren()
                    .EndChildren()
                .MenuTree(withoutPermissions);
        }
    }
}