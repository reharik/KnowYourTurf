using System.Collections.Generic;
using KnowYourTurf.Core.Html.Menu;
using KnowYourTurf.Web.Controllers;

namespace KnowYourTurf.Web.Config
{
    public interface IMenuConfig
    {
        IList<MenuItem> Build(bool withoutPermissions = false);
    }

    public class MenuConfig : IMenuConfig
    {
        private readonly IMenuBuilder _builder;

        public MenuConfig(IMenuBuilder builder)
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
                .CreateNode<HomeController>(c => c.Home(), WebLocalizationKeys.HOME)
                
                .CreateNode<FieldListController>(c => c.FieldList(), WebLocalizationKeys.FIELDS)
                .CreateNode<EquipmentListController>(c => c.EquipmentList(), WebLocalizationKeys.EQUIPMENT)
                .CreateNode(WebLocalizationKeys.TASKS)
                    .HasChildren()
                    .CreateNode<TaskListController>(c => c.TaskList(), WebLocalizationKeys.TASK_LIST)
                    .CreateNode<TaskCalendarController>(c => c.TaskCalendar(), WebLocalizationKeys.TASK_CALENDAR)
                    .EndChildren()
                .CreateNode<EventCalendarController>(c => c.EventCalendar(), WebLocalizationKeys.EVENTS)
                .CreateNode<CalculatorListController>(c => c.CalculatorList(), WebLocalizationKeys.CALCULATORS)
                .CreateNode<WeatherListController>(c => c.WeatherList(null), WebLocalizationKeys.WEATHER)
                .CreateNode<ForumController>(c => c.Forum(), WebLocalizationKeys.FORUM)
                .MenuTree(withoutPermissions);
        }
    }
}