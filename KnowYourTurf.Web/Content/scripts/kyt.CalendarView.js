/**
 * Created by .
 * User: Harik
 * Date: 11/22/11
 * Time: 11:51 AM
 * To change this template use File | Settings | File Templates.
 */


kyt.CalendarView = Backbone.View.extend({
    events:{
    },
    initialize: function(){
        this.options = $.extend({},kyt.gridDefaults,this.options);
        this.id = this.options.id;
        this.registerSubscriptions();
        this.render();
    },
    render: function(){
        $(this.options.calendarContainer,this.el).asCalendar(this.options);
        return this;
    },
    registerSubscriptions:function(){

        $.subscribe('/calendar_'+this.id+'/eventDrop', $.proxy(this.eventDrop,this), this.cid);
        $.subscribe('/calendar_'+this.id+'/eventResize', $.proxy(this.eventResize,this), this.cid);
    },
    eventDrop:function(event, dayDelta,minuteDelta,allDay,revertFunc) {
        var data = {"EntityId":event.EntityId,
            "ScheduledDate":$.fullCalendar.formatDate( event.start,"M/d/yyyy hh:mm TT"),
            "StartTime":$.fullCalendar.formatDate( event.start,"M/d/yyyy hh:mm TT"),
            "EndTime":$.fullCalendar.formatDate( event.end,"M/d/yyyy hh:mm TT")};
        kyt.repository.ajaxGet(this.options.EventChangedUrl,data, $.proxy(function(result){this.changeEventCallback(result,revertFunc)},this));
    },
    eventResize:function( event, dayDelta, minuteDelta, revertFunc, jsEvent, ui, view ){
        var data = {"EntityId":event.EntityId,
            "ScheduledDate":$.fullCalendar.formatDate( event.start,"M/d/yyyy hh:mm TT"),
            "StartTime":$.fullCalendar.formatDate( event.start,"M/d/yyyy hh:mm TT"),
            "EndTime":$.fullCalendar.formatDate( event.end,"M/d/yyyy hh:mm TT")
        };
        kyt.repository.ajaxGet(this.options.EventChangedUrl,data,$.proxy(function(result){this.changeEventCallback(result,revertFunc)},this));
    },
    changeEventCallback:function(result,revertFunc){
        if(!result.Success){
            revertFunc();
        }
    },
    reload:function(){
        $(this.options.calendarContainer,this.el).fullCalendar( 'refetchEvents' )
    }
});