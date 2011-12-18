/**
 * Created by JetBrains RubyMine.
 * User: Owner
 * Date: 11/5/11
 * Time: 2:47 PM
 * To change this template use File | Settings | File Templates.
 */

if (typeof kyt == "undefined") {
    var kyt = {};
}

kyt.TaskListController = kyt.CrudController.extend({
    events:_.extend({
    }, kyt.CrudController.prototype.events),
    additionalSubscriptions:function(){
        $.subscribe('/contentLevel/popupFormModule_/popupLoaded',$.proxy(this.loadTokenizers,this), this.cid);
    },
    loadTokenizers:function(formOptions){
        var employeeTokenOptions = {
            id:"employee",
            el:"#employeeTokenizer",
            availableItems:formOptions.employeeOptions.availableItems,
            selectedItems:formOptions.employeeOptions.selectedItems,
            inputSelector:formOptions.employeeOptions.inputSelector
        };

        var equipmentTokenOptions = {
            id:"equipment",
            el:"#equipmentTokenizer",
            availableItems:formOptions.equipmentOptions.availableItems,
            selectedItems:formOptions.equipmentOptions.selectedItems,
            inputSelector:formOptions.equipmentOptions.inputSelector
        };
        this.views.employeeToken= new kyt.TokenView(employeeTokenOptions);
        this.views.equipmentToken = new kyt.TokenView(equipmentTokenOptions);

    }
});

