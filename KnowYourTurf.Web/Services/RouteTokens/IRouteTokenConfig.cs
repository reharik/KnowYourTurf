using System.Collections.Generic;
using KnowYourTurf.Core.Enums;
using KnowYourTurf.Web.Areas.Portfolio.Controllers;
using KnowYourTurf.Web.Controllers;
using KnowYourTurf.Web.Services.RouteTokens;

namespace KnowYourTurf.Web.Services.ViewOptions
{
    public interface IRouteTokenConfig
    {
        IList<RouteToken> Build(bool withoutPermissions = false);
    }
    public class FieldsRouteTokenList : IRouteTokenConfig
    {
        private readonly IRouteTokenBuilder _builder;

        public FieldsRouteTokenList(IRouteTokenBuilder routeTokenBuilder)
        {
            _builder = routeTokenBuilder;
        }

        public IList<RouteToken> Build(bool withoutPermissions = false)
        {
            _builder.WithoutPermissions(withoutPermissions);
            _builder.Url<OrthogonalController>(x => x.MainMenu(null)).ViewId("fieldsMenu").End();
            _builder.TokenForForm<EmployeeDashboardController>(x => x.ViewEmployee(null)).ViewName("EmployeeDashboardView").IsChild(false).End();

            _builder.TokenForList<FieldListController>(x => x.ItemList(null)).End();
            _builder.TokenForForm<FieldController>(x => x.AddUpdate(null)).End();
            _builder.TokenForForm<FieldDashboardController>(x => x.ViewField(null)).ViewName("FieldDashboardView").End();

            _builder.TokenForList<TaskListController>(x => x.ItemList(null)).End();
            _builder.TokenForForm<TaskCalendarController>(x => x.TaskCalendar(null)).ViewName("CalendarView").SubViewName("TaskFormView").End();
            _builder.TokenForForm<TaskController>(x => x.AddUpdate(null)).End();
            _builder.UrlForDisplay<TaskController>(x => x.Display(null)).End();

            _builder.TokenForList<EventCalendarController>(x => x.EventCalendar(null)).ViewName("CalendarView").End();
            _builder.TokenForList<EquipmentListController>(x => x.ItemList(null)).End();
            _builder.TokenForForm<EquipmentController>(x => x.AddUpdate(null)).End();

            _builder.TokenForList<CalculatorListController>(x => x.ItemList(null)).End();
            _builder.TokenForForm<CalculatorController>(x => x.Calculator(null)).End();

            _builder.TokenForList<CalculatorListController>(x => x.ItemList(null)).End();
            _builder.TokenForForm<CalculatorController>(x => x.Calculator(null)).End();

            _builder.TokenForList<WeatherListController>(x => x.ItemList(null)).End();
            _builder.TokenForForm<WeatherController>(x => x.AddUpdate(null)).End();

            _builder.JustRoute("Forum").End();

            _builder.TokenForList<EmailJobListController>(x => x.ItemList(null)).End();
            _builder.TokenForForm<EmailJobController>(x => x.AddUpdate(null)).End();

            _builder.TokenForList<EmployeeListController>(x => x.ItemList(null)).End();
            _builder.TokenForForm<EmployeeController>(x => x.AddUpdate(null)).End();

            _builder.TokenForList<FacilitiesListController>(x => x.ItemList(null)).End();
            _builder.TokenForForm<FacilitiesController>(x => x.AddUpdate(null)).End();

            _builder.TokenForList<ListTypeListController>(x => x.Display(null)).ViewName("ListTypeListView").End();

            _builder.TokenForList<MaterialListController>(x => x.ItemList(null)).End();
            _builder.TokenForForm<MaterialController>(x => x.AddUpdate(null)).End();
            _builder.TokenForList<FertilizerListController>(x => x.ItemList(null)).End();
            _builder.TokenForForm<FertilizerController>(x => x.AddUpdate(null)).End();
            _builder.TokenForList<ChemicalListController>(x => x.ItemList(null)).End();
            _builder.TokenForForm<ChemicalController>(x => x.AddUpdate(null)).End();

            _builder.TokenForList<DocumentListController>(x => x.ItemList(null)).End();
            _builder.TokenForForm<DocumentController>(x => x.AddUpdate(null)).End();
            _builder.TokenForList<PhotoListController>(x => x.ItemList(null)).End();
            _builder.TokenForForm<PhotoController>(x => x.AddUpdate(null)).End();

            _builder.TokenForList<InventoryListController>(x => x.ItemList(null)).End();

            _builder.TokenForList<PurchaseOrderListController>(x => x.ItemList(null)).End();
            _builder.TokenForForm<PurchaseOrderController>(x => x.AddUpdate(null)).End();

            _builder.TokenForList<PurchaseOrderListController>(x => x.ItemList(null)).End();
            _builder.TokenForForm<PurchaseOrderController>(x => x.AddUpdate(null)).End();

            _builder.TokenForForm<TaskTypeController>(x => x.AddUpdate(null)).End();
            _builder.TokenForForm<EventTypeController>(x => x.AddUpdate(null)).End();
            _builder.TokenForForm<DocumentCategoryController>(x => x.AddUpdate(null)).End();
            _builder.TokenForForm<PhotoCategoryController>(x => x.AddUpdate(null)).End();

            _builder.TokenForList<VendorListController>(x => x.ItemList(null)).End();
            _builder.TokenForForm<VendorController>(x => x.AddUpdate(null)).End();

            _builder.TokenForList<VendorContactListController>(x => x.ItemList(null)).End();
            _builder.TokenForForm<VendorContactController>(x => x.AddUpdate(null)).End();

            _builder.TokenForList<PurchaseOrderListController>(x => x.ItemList(null)).End();
            _builder.TokenForForm<PurchaseOrderController>(x => x.AddUpdate(null)).End();

            _builder.TokenForList<PurchaseOrderLineItemListController>(x => x.ItemList(null)).End();
            _builder.TokenForForm<PurchaseOrderLineItemController>(x => x.AddUpdate(null)).End();

            _builder.TokenForForm<PurchaseOrderCommitController>(x => x.PurchaseOrderCommit(null)).AddUpdateToken("purchaseorderlineitem").End();


            return _builder.Items;
        }
    }
}
