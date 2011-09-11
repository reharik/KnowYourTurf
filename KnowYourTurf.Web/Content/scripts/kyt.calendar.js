if (typeof kyt == "undefined") {
            var kyt = {};
}

if (typeof kyt.calendar == "undefined") {
            kyt.calendar= {};
}
kyt.calendar.controller = (function(){
    return{
        init:function(){
            calendarMetaData.name= "calendarMetaData";
            calendarMetaData.setDisplayButtonBuilder(function(builder){
                                builder.addEditButton();
                                builder.addButton("Copy Task",kyt.popupCrud.controller.copyItem);
                                builder.addCancelButton();
                                return  builder.getButtons();
                            });
            calendarMetaData.addRunAfterSuccess(function(){ $("#calendar").fullCalendar( 'refetchEvents' );});
            calendarMetaData.addRunAfterRender(function(){
                if(!calendarMetaData.getIsDisplay())
                    {$("#EmployeeInput").tokenInput(availableEmployees?availableEmployees:{},{prePopulate: selectedEmployees,
                        internalTokenMarkup:function(item){return "<p><a class='selectedItem'>"+ item.name +"</a></p>";}});
                    $("#EquipmentInput").tokenInput(availableEquipment?availableEquipment:{},{prePopulate: selectedEquipment,
                        internalTokenMarkup:function(item){return "<p><a class='selectedItem'>"+ item.name +"</a></p>";}});
                }
            });
        }
    }
}());

kyt.calendar.events = (function(){
    return{
        dayClick:function(date, allDay, jsEvent, view) {
            var data = {"ScheduledDate" : $.fullCalendar.formatDate( date,"M/d/yyyy"), "ScheduledStartTime": $.fullCalendar.formatDate( date,"hh:mm TT")};
            kyt.popupCrud.repository.itemCall(calendarDef.AddEditUrl,calendarMetaData,data);
        },
        eventClick:function(calEvent, jsEvent, view) {
            var data = {"EntityId": calEvent.EntityId};
            calendarMetaData.setIsDisplay(true);
            kyt.popupCrud.repository.itemCall(calendarDef.DisplayUrl,calendarMetaData,data);
        },
        eventDrop:function(event, dayDelta,minuteDelta,allDay,revertFunc) {
            var data = {"EntityId":event.EntityId,
                "ScheduledDate":$.fullCalendar.formatDate( event.start,"M/d/yyyy hh:mm TT"),
                "StartTime":$.fullCalendar.formatDate( event.start,"M/d/yyyy hh:mm TT"),
                "EndTime":$.fullCalendar.formatDate( event.end,"M/d/yyyy hh:mm TT")};
            kyt.calendar.repository.eventChangedCall(calendarDef.EventChangedUrl,data, revertFunc)
        },
        eventResize:function( event, dayDelta, minuteDelta, revertFunc, jsEvent, ui, view ){
            var data = {"EntityId":event.EntityId,
                "ScheduledDate":$.fullCalendar.formatDate( event.start,"M/d/yyyy hh:mm TT"),
                "StartTime":$.fullCalendar.formatDate( event.start,"M/d/yyyy hh:mm TT"),
                "EndTime":$.fullCalendar.formatDate( event.end,"M/d/yyyy hh:mm TT")};
            kyt.calendar.repository.eventChangedCall(calendarDef.EventChangedUrl,data, revertFunc)
        }
    }
}());

kyt.calendar.repository = (function(){
    var notification = kyt.utilities.messageHandling.notificationResult();
    var eventChangedCallback = function(result, revertFunc){
        if(!result.Success){
            notification.result(result);
            revertFunc();
            return;
        }
        $("#calendar").fullCalendar( 'refetchEvents' )
    };
    return{
        eventChangedCall:function(url,data, revertFunc){
            $.post(url,data,function(result){eventChangedCallback(result,revertFunc)});
        }
    }

}());


