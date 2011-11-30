/**
 * Created by .
 * User: Harik
 * Date: 11/22/11
 * Time: 8:16 PM
 * To change this template use File | Settings | File Templates.
 */

kyt.CalendarController = kyt.Controller.extend({
    events:_.extend({
    }, kyt.Controller.prototype.events),
    initialize:function(){
        $.extend(this,this.defaults());
        this.options = $.extend({},kyt.crudControllerDefaults, this.options);
        $.clearPubSub();
        this.registerSubscriptions();
        var displayOptions={
            el:"#masterArea",
            id:"event"
        };
        var options = $.extend({}, this.options,displayOptions);
        this.views.calendarView = new kyt.CalendarView(options);
    },
    registerSubscriptions:function(){
        $.subscribe('/calendar_event/dayClick', $.proxy(this.dayClick,this), this.cid);
        $.subscribe('/calendar_event/eventClick', $.proxy(this.eventClick,this), this.cid);
        // from form
        $.subscribe('/form_editModule/success', $.proxy(this.formSuccess,this), this.cid);
        $.subscribe('/form_editModule/cancel', $.proxy(this.formCancel,this), this.cid);
        // from display
        $.subscribe('/popup_displayModule/cancel', $.proxy(this.displayCancel,this), this.cid);
        $.subscribe('/popup_displayModule/edit', $.proxy(this.displayEdit,this), this.cid);
    },
    dayClick:function(date, allDay, jsEvent, view) {
        var data = {"ScheduledDate" : $.fullCalendar.formatDate( date,"M/d/yyyy"), "ScheduledStartTime": $.fullCalendar.formatDate( date,"hh:mm TT")};
        this.editEvent(this.options.AddEditUrl,data);
    },

    editEvent:function(url, data){
        $("#masterArea").after("<div id='dialogHolder'/>");
        var moduleOptions = {
            id:"editModule",
            el:"#dialogHolder",
            url: url,
            data:data,
            buttons: kyt.popupButtonBuilder.builder("editModule").standardEditButons()
        };
        this.modules.popupForm = new kyt.PopupFormModule(moduleOptions);
    },

    eventClick:function(calEvent, jsEvent, view) {
        var data = {"EntityId": calEvent.EntityId};
        var builder = kyt.popupButtonBuilder.builder("displayModule");
        builder.addButton("Delete", $.proxy(this.deleteItem,this));
        builder.addEditButton();
        builder.addButton("Copy Event",$.proxy(this.copyItem,this));
        builder.addCancelButton();
       $("#masterArea").after("<div id='dialogHolder'/>");
        var moduleOptions = {
            id:"displayModule",
            el:"#dialogHolder",
            url: this.options.DisplayUrl,
            data:data,
            buttons:builder.getButtons()
        };
        this.modules.popupDisplay = new kyt.PopupDisplayModule(moduleOptions);
    },

    copyItem:function(){
        var entityId = $("[name$='EntityId']").val();
        var data = {"EntityId":entityId,"Copy":true};
        this.editEvent(this.options.AddEditUrl,data);
    },

    deleteItem: function(){
        if (confirm("Are you sure you would like to delete this Item?")) {
            ///// do delete here!
        var entityId = $("#EntityId").val();
        kyt.repository.ajaxGet(this.options.deleteUrl,{"EntityId":entityId}, $.proxy(function(){
            this.modules.popupDisplay.destroy();
            this.views.calendarView.reload();},this));
        }
    },
    //from form
    formSuccess:function(){
        this.formCancel();
        this.views.calendarView.reload();

    },
    formCancel: function(){
        this.modules.popupForm.destroy();
    },

    //from display
    displayCancel:function(){
        this.modules.popupDisplay.destroy();
    },

    displayEdit:function(url){
        this.modules.popupDisplay.destroy();
        this.editEvent(url);
    }

});