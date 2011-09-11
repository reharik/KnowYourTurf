if (typeof kyt == 'undefined') {
    kyt = function() {
    }
}

if (typeof kyt.calendar == 'undefined') {
    kyt.calendar = function() {
    }
}

(function($) {
    $.fn.asCalendar = function(calendarDefinition, userOptions) {
        var calendarDefaultOptions = {
            header: {
				left: 'prev,next today',
				center: calendarDefinition.Title,
				right: 'month,agendaWeek,agendaDay'
			},
            editable:true,
            theme:true,
            firstDay:1,
            allDaySlot:false,
            allDayDefault:false,
            slotMinutes:15,
            events: calendarDefinition.Url,
            dayClick: kyt.calendar.events.dayClick,
            eventClick: kyt.calendar.events.eventClick,
            eventDrop: kyt.calendar.events.eventDrop,
            eventResize: kyt.calendar.events.eventResize
        };

        var calendarOptions = $.extend(calendarDefaultOptions, userOptions || {});
        var calendar = this;
        calendar.fullCalendar(calendarOptions);
        return calendar;
    }
})(jQuery);
