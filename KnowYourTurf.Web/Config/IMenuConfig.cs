using System.Collections.Generic;
using KnowYourTurf.Core.Html.Menu;
using KnowYourTurf.Web.Controllers;

namespace KnowYourTurf.Web.Config
{
    public interface IMenuConfig
    {
        IList<MenuItem> Build();
    }

    public class MenuConfig : IMenuConfig
    {
        private readonly IMenuBuilder _builder;

        public MenuConfig(IMenuBuilder builder)
        {
            _builder = builder;
        }

        public IList<MenuItem> Build()
        {
            return DefaultMenubuilder();
        }

        private IList<MenuItem> DefaultMenubuilder()
        {
            return _builder
                .CreateNode<HomeController>(c => c.Home(), WebLocalizationKeys.HOME)
                .CreateNode<VendorListController>(c => c.VendorList(null), WebLocalizationKeys.VENDORS)
                .CreateNode(WebLocalizationKeys.INVENTORY)
                    .HasChildren()
                    .CreateNode<InventoryListController>(c => c.InventoryProductList(null), WebLocalizationKeys.MATERIALS,"ProductType=Material")
                    .CreateNode<InventoryListController>(c => c.InventoryProductList(null), WebLocalizationKeys.FERTILIZERS, "ProductType=Fertilizer")
                    .CreateNode<InventoryListController>(c => c.InventoryProductList(null), WebLocalizationKeys.CHEMICALS, "ProductType=Chemical")
                    .CreateNode<InventoryListController>(c => c.InventoryProductList(null), WebLocalizationKeys.SEEDS, "ProductType=Seed")
                    .EndChildren()
                .CreateNode<FieldListController>(c => c.FieldList(), WebLocalizationKeys.FIELDS)
                .CreateNode<EquipmentListController>(c => c.EquipmentList(), WebLocalizationKeys.EQUIPMENT)
                .CreateNode(WebLocalizationKeys.TASKS)
                    .HasChildren()
                    .CreateNode<TaskListController>(c => c.TaskList(), WebLocalizationKeys.TASK_LIST)
                    .CreateNode<TaskCalendarController>(c => c.TaskCalendar(), WebLocalizationKeys.TASK_CALENDAR)
                    .EndChildren()
                .CreateNode<PurchaseOrderListController>(c => c.PurchaseOrderList(), WebLocalizationKeys.PURCHASE_ORDERS)
                .CreateNode<EventCalendarController>(c => c.EventCalendar(), WebLocalizationKeys.EVENTS)
                .CreateNode<CalculatorListController>(c => c.CalculatorList(), WebLocalizationKeys.CALCULATORS)
                .CreateNode<WeatherListController>(c => c.WeatherList(null), WebLocalizationKeys.WEATHER)
                .CreateNode<ForumController>(c => c.Forum(), WebLocalizationKeys.FORUM)

                .MenuTree();
        }
    }
}