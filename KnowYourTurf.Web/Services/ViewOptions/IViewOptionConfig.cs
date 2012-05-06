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
    public class FieldsViewOptionList : IViewOptionConfig
    {
        private readonly IViewOptionBuilder _builder;

        public FieldsViewOptionList(IViewOptionBuilder viewOptionBuilder)
        {
            _builder = viewOptionBuilder;
        }

        public IList<ViewOption> Build(bool withoutPermissions = false)
        {
            _builder.WithoutPermissions(withoutPermissions);
            _builder.Url<OrthogonalController>(x => x.MainMenu(null)).ViewId("scheduleMenu").End();
            _builder.UrlForForm<EmployeeDashboardController>(x => x.ViewEmployee(null)).IsChild(false).End();

            _builder.UrlForList<FieldListController>(x => x.ItemList(null)).End();
            _builder.UrlForForm<FieldController>(x => x.AddUpdate(null)).End();

            _builder.UrlForList<TaskListController>(x => x.ItemList(null)).End();
            _builder.UrlForForm<TaskCalendarController>(x => x.TaskCalendar(null)).End();

            _builder.UrlForList<EventCalendarController>(x => x.EventCalendar(null)).End();
            _builder.UrlForList<EquipmentListController>(x => x.ItemList(null)).End();
            _builder.UrlForForm<EquipmentController>(x => x.AddUpdate(null)).End();

            _builder.UrlForList<CalculatorListController>(x => x.ItemList(null)).End();
            _builder.UrlForForm<CalculatorController>(x => x.Calculator(null)).End();

            _builder.UrlForList<CalculatorListController>(x => x.ItemList(null)).End();
            _builder.UrlForForm<CalculatorController>(x => x.Calculator(null)).End();

            _builder.UrlForList<WeatherListController>(x => x.ItemList(null)).End();
            _builder.UrlForForm<WeatherController>(x => x.AddUpdate(null)).End();

            _builder.Url<ForumController>(x => x.Forum(null)).End();

            _builder.UrlForList<EmailJobListController>(x => x.ItemList(null)).End();
            _builder.UrlForForm<EmailJobController>(x => x.AddUpdate(null)).End();

            _builder.UrlForList<EmployeeListController>(x => x.ItemList(null)).End();
            _builder.UrlForForm<EmployeeController>(x => x.AddUpdate(null)).End();

            _builder.UrlForList<FacilitiesListController>(x => x.ItemList(null)).End();
            _builder.UrlForForm<FacilitiesController>(x => x.AddUpdate(null)).End();

            _builder.UrlForList<ListTypeListController>(x => x.ItemList(null)).End();

            _builder.UrlForList<MaterialListController>(x => x.ItemList(null)).End();
            _builder.UrlForForm<MaterialController>(x => x.AddUpdate(null)).End();
            _builder.UrlForList<FertilizerListController>(x => x.ItemList(null)).End();
            _builder.UrlForForm<FertilizerController>(x => x.AddUpdate(null)).End();
            _builder.UrlForList<ChemicalListController>(x => x.ItemList(null)).End();
            _builder.UrlForForm<ChemicalController>(x => x.AddUpdate(null)).End();

            _builder.UrlForList<DocumentListController>(x => x.ItemList(null)).End();
            _builder.UrlForForm<DocumentController>(x => x.AddUpdate(null)).End();
            _builder.UrlForList<PhotoListController>(x => x.ItemList(null)).End();
            _builder.UrlForForm<PhotoController>(x => x.AddUpdate(null)).End();

            _builder.UrlForList<InventoryListController>(x => x.ItemList(null)).End();

            _builder.UrlForList<PurchaseOrderListController>(x => x.ItemList(null)).End();
            _builder.UrlForForm<PurchaseOrderController>(x => x.AddUpdate(null)).End();

            _builder.UrlForList<PurchaseOrderListController>(x => x.ItemList(null)).End();
            _builder.UrlForForm<PurchaseOrderController>(x => x.AddUpdate(null)).End();

            //_builder.UrlForForm<PayTrainerController>(x => x.AddUpdate(null), AreaName.Billing).End();

            return _builder.Items;
        }
    }
}
