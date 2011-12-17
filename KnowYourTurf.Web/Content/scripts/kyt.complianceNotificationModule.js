/**
 * Created by .
 * User: RHarik
 * Date: 8/3/11
 * Time: 1:39 PM
 * To change this template use File | Settings | File Templates.
 */

if (typeof kyt == "undefined") {
            var kyt = {};
}

if (typeof kyt.complianceNotification == "undefined") {
            kyt.complianceNotification= {};
}

kyt.complianceNotificationModule = function(container, options){
    var _container = container;
    var myOptions = $.extend({}, kyt.complianceNotificationModuleDefaults, options || {});
    var modules = {};

    var prePopulate = function(){
        $(_container).find("#daysBefore" ).slider( "option", "value", $("[name='ComplianceNotificationSchedule.DaysBeforeExpiration']").val());
        $(_container).find( "#repeatDaysUntil" ).slider( "option", "value", $("[name='ComplianceNotificationSchedule.RepeatDaysUntilExpiration']").val());
        $(_container).find( "#repeatDaysAfter" ).slider( "option", "value", $("[name='ComplianceNotificationSchedule.RepeatDaysAfterExpiration']").val());
        $(_container).find( "#endPeriod" ).slider( "option", "value", $("[name='ComplianceNotificationSchedule.EndDaysAfterExpiration']").val());
    };

    return{
        init:function(){
            this.delegateEvents();
            prePopulate();
        },
        slideEvent:function(event,ui){$("#").val(ui.value);},

        destroy:function(){
            $.unsubscribeByHandle("fCont");
            $(_container).empty();
        },
        delegateEvents: function(){
            var that = this;
            $("#daysBefore").slider({min:0,max:60,slide:function(event,ui){$("[name='ComplianceNotificationSchedule.DaysBeforeExpiration']").val(ui.value);}});
            $("#repeatDaysUntil").slider({min:0,max:60,slide:function(event,ui){$("[name='ComplianceNotificationSchedule.RepeatDaysUntilExpiration']").val(ui.value);}});
            $("#repeatDaysAfter").slider({min:0,max:60,slide:function(event,ui){$("[name='ComplianceNotificationSchedule.RepeatDaysAfterExpiration']").val(ui.value);}});
            $("#endPeriod").slider({min:0,max:60,slide:function(event,ui){$("[name='ComplianceNotificationSchedule.EndDaysAfterExpiration']").val(ui.value);}});
            $("[name='ComplianceNotificationSchedule.DaysBeforeExpiration']").change(function(){
                $("#daysBefore").slider( "option", "value", $(this).val() );
            });
            $("[name='ComplianceNotificationSchedule.RepeatDaysUntilExpiration']").change(function(){
                 $("#repeatDaysUntil").slider( "option", "value", $(this).val() );
            });
            $("[name='ComplianceNotificationSchedule.RepeatDaysAfterExpiration']").change(function(){
                 $("#repeatDaysAfter").slider( "option", "value", $(this).val() );
            });
            $("[name='ComplianceNotificationSchedule.EndDaysAfterExpiration']").change(function(){
                 $("#endPeriod").slider( "option", "value", $(this).val() );
            });
        }
    }
 };

kyt.complianceNotificationModuleDefaults = { };
