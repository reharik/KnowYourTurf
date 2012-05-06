using System.Collections.Generic;
using KnowYourTurf.Core.Enums;
using KnowYourTurf.Web.Areas.Portfolio.Controllers;
using KnowYourTurf.Web.Controllers;

namespace KnowYourTurf.Web.Services.ViewOptions
{
    public interface IViewOptionConfig
    {
        IList<ViewOption> Build(bool withoutPermissions = false);
    }
    public class ScheduleViewOptionList : IViewOptionConfig
    {
        private readonly IViewOptionBuilder _builder;

        public ScheduleViewOptionList(IViewOptionBuilder viewOptionBuilder)
        {
            _builder = viewOptionBuilder;
        }

        public IList<ViewOption> Build(bool withoutPermissions = false)
        {
            _builder.WithoutPermissions(withoutPermissions);
            _builder.Url<OrthogonalController>(x => x.MainMenu()).ViewId("scheduleMenu").End();
            _builder.UrlForForm<EmployeeDashboardController>(x => x.ViewEmployee(null)).RouteToken("calendar").ViewName("CalendarView").IsChild(false).End();

            _builder.UrlForList<FieldListController>(x => x.ItemList(null)).End();
            _builder.UrlForForm<FieldController>(x => x.AddUpdate(null)).End();

            _builder.UrlForList<TaskListController>(x => x.ItemList(null)).End();
            _builder.UrlForForm<TaskCalendarController>(x => x.TaskCalendar(null)).End();

            _builder.UrlForList<EventCalendarController>(x => x.EventCalendar(null)).End();
            _builder.UrlForList<EquipmentListController>(x => x.ItemList()).End();
            _builder.UrlForForm<EquipmentController>(x => x.AddUpdate(null)).End();

            _builder.UrlForList<CalculatorListController>(x => x.ItemList()).End();
            _builder.UrlForForm<CalculatorController>(x => x.Calculator(null)).End();

            _builder.UrlForList<CalculatorListController>(x => x.ItemList()).End();
            _builder.UrlForForm<CalculatorController>(x => x.Calculator(null)).End();

            _builder.UrlForList<WeatherListController>(x => x.ItemList()).End();
            _builder.UrlForForm<WeatherController>(x => x.AddUpdate(null)).End();

            _builder.Url<ForumController>(x => x.Forum()).End();

            _builder.UrlForList<EmailJobListController>(x => x.ItemList()).End();
            _builder.UrlForForm<EmailJobController>(x => x.AddUpdate(null)).End();

            _builder.UrlForList<EmployeeListController>(x => x.ItemList()).End();
            _builder.UrlForForm<EmployeeController>(x => x.AddUpdate(null)).End();

            _builder.UrlForList<FacilitiesListController>(x => x.ItemList()).End();
            _builder.UrlForForm<FacilitiesController>(x => x.AddUpdate(null)).End();

            _builder.UrlForList<ListTypeListController>(x => x.ItemList()).End();
            _builder.UrlForForm<>(x => x.Calculator(null)).End();
            _builder.UrlForList<CalculatorListController>(x => x.ItemList()).End();
            _builder.UrlForForm<CalculatorController>(x => x.Calculator(null)).End();
            _builder.UrlForList<CalculatorListController>(x => x.ItemList()).End();
            _builder.UrlForForm<CalculatorController>(x => x.Calculator(null)).End();

            //_builder.UrlForForm<PayTrainerController>(x => x.AddUpdate(null), AreaName.Billing).End();

            _builder.UrlForList<TrainerPaymentListController>(x => x.ItemList(null), AreaName.Billing).ViewName("TrainerPaymentListGridView").End();
            _builder.UrlForForm<TrainerPaymentController>(x => x.Display(null), AreaName.Billing).End();

            return _builder.Items;
        }
    }
}
