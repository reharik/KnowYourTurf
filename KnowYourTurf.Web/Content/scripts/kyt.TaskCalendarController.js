/**
 * Created by JetBrains RubyMine.
 * User: Owner
 * Date: 12/4/11
 * Time: 5:25 AM
 * To change this template use File | Settings | File Templates.
 */

kyt.TaskCalendarController = kyt.CalendarController.extend({
    events:_.extend({
    }, kyt.CalendarController.prototype.events),

    additionalSubscriptions:function(){
        $.subscribe('/contentLevel/form_editModule/pageLoaded',$.proxy(this.loadTokenizers,this), this.cid);
    },
    loadTokenizers:function(formOptions){
        var employeeTokenOptions = {
            id:this.id+"employee",
            el:"#employeeTokenizer",
            availableItems:formOptions.employeeOptions.availableItems,
            selectedItems:formOptions.employeeOptions.selectedItems,
            inputSelector:formOptions.employeeOptions.inputSelector
        };

        var equipmentTokenOptions = {
            id:this.id+"equipment",
            el:"#equipmentTokenizer",
            availableItems:formOptions.equipmentOptions.availableItems,
            selectedItems:formOptions.equipmentOptions.selectedItems,
            inputSelector:formOptions.equipmentOptions.inputSelector
        };
        this.views.employeeToken= new kyt.TokenView(employeeTokenOptions);
        this.views.equipmentToken = new kyt.TokenView(equipmentTokenOptions);

    },
    eventClick:function(calEvent, jsEvent, view) {
        $("#dialogHolder").remove();
        var data = {"EntityId": calEvent.EntityId, popup:true};
        var builder = kyt.popupButtonBuilder.builder("displayModule");
        builder.addButton("Delete", $.proxy(this.deleteItem,this));
        builder.addUpdateButton();
        builder.addButton("Copy Task",$.proxy(this.copyItem,this));
        builder.addCancelButton();

       $("#masterArea").after("<div id='dialogHolder'/>");
        var moduleOptions = {
            id:"displayModule",
            el:"#dialogHolder",
            url: this.options.DisplayUrl,
            data:data,
            buttons:builder.getButtons()
        };
        this.modules.popupDisplay = new kyt.AjaxPopupDisplayModule(moduleOptions);
    }
});