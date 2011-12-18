/**
 * Created by JetBrains RubyMine.
 * User: Owner
 * Date: 11/5/11
 * Time: 2:56 PM
 * To change this template use File | Settings | File Templates.
 */


if (typeof kyt == "undefined") {
    var kyt = {};
}

kyt.CalculatorController = kyt.CrudController.extend({
    events:_.extend({
    }, kyt.CrudController.prototype.events),
    addEditItem: function(url, data){
        var _url = url?url:this.options.addEditUrl;
        $("#masterArea").after("<div id='dialogHolder'/>");
        var builder = kyt.popupButtonBuilder.builder("editModule");
        builder.addButton("Return",builder.getCancelFunc());
        builder.addButton("Calculate",builder.getSaveFunc());
        builder.addButton("Create Task",$.proxy(this.addTask,this));

        var moduleOptions = {
            id:"editModule",
            el:"#dialogHolder",
            url: _url,
            data:data,
            buttons: builder.getButtons(),
            successHandler:kyt.calculator.successHandler
        };
        this.modules.popupForm= new kyt.PopupFormModule(moduleOptions);
    },

    additionalSubscriptions:function(){
        $.subscribe('/contentLevel/popupFormModule_taskModule/popupLoaded',$.proxy(this.loadTokenizers,this), this.cid);
        $.subscribe('/contentLevel/form_taskModule/success', $.proxy(this.taskSuccess,this), this.cid);
        $.subscribe('/contentLevel/form_taskModule/cancel', $.proxy(this.taskCancel,this), this.cid);
    },

    addTask:function(){
        var calculatorName = $("#CalculatorName").val();
        var data = this[calculatorName]();
        this.modules.popupForm.destroy();
        $("#masterArea").after("<div id='dialogHolder'/>");
        var moduleOptions = {
            id:"taskModule",
            el:"#dialogHolder",
            url: this.options.createATaskUrl,
            data:data
        };
        this.modules.taskForm = new kyt.PopupFormModule(moduleOptions);
    },
    formSuccess:function(result){
        kyt.calculator.successHandlers.success(result)
    },
    FertilizerNeeded: function(){
        return {Field : $("[name=Field]").val(),
            Product:$("[name=Product]").val()+ "_Fertilizers",
            Quantity: $("#BagsNeeded").text()
        };
    },
    Materials: function(){
        return {Field : $("[name=Field]").val(),
            Quantity: $("#TotalMaterials").text()
        };
    },
    Sand: function(){
        return {Field : $("[name=Field]").val(),
            Quantity: $("#TotalSand").text()
        };
    },
    OverseedBagsNeeded:function(){
        return {Field : $("[name=Field]").val(),
            Product:$("[name=Product]").val()+ "_Seeds",
            Quantity: $("#BagsNeeded").text()
        };
    },
    OverseedRateNeeded:function(){
        return {Field : $("[name=Field]").val(),
            Product:$("[name=Product]").val()+ "_Seeds",
            Quantity: $("#BagsUsed").val()
        };
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
    taskSuccess:function(){
        this.taskCancel();
    },
    taskCancel: function(){
        this.modules.taskForm.destroy();
    }



});