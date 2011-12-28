/**
 * Created by JetBrains RubyMine.
 * User: Owner
 * Date: 10/9/11
 * Time: 4:05 PM
 * To change this template use File | Settings | File Templates.
 */
if (typeof kyt == "undefined") {
            var kyt = {};
}


kyt.EmployeeDashboardController  = kyt.Controller.extend({
    events:_.extend({
    }, kyt.Controller.prototype.events),

    initialize:function(){
        $.extend(this,this.defaults());
        $.clearPubSub();
        this.registerSubscriptions();
        this.id = "employeeDashboardController";
        var displayOptions={
            el:"#masterArea",
            id:"mainForm"
        };
        var _options = $.extend({},this.options,displayOptions);
        this.modules.mainForm = new kyt.FormModule(_options);

        var ptgOptions = {
            el:"#pendingTaskGridContainer",
            id:"pendingTaskGrid",
            gridName:"pendingTaskGrid",
            gridContainer:"#gridContainer_pt",
            gridDef:this.options.pendingGridDef,
            addUpdateUrl:this.options.pendingTaskaddUpdateUrl,
            deleteMultipleUrl:this.options.deleteMultipleUrl
        };
        this.views.pendingTaskGridView = new kyt.GridView(ptgOptions);
        var ctgOptions = {
            el:"#completeTaskGridContainer",
            id:"completeTaskGrid",
            gridName:"completeTaskGrid",
            gridContainer:"#gridContainer_ct",
            gridDef:this.options.completeGridDef,
            // this is not used except for copy task which is why it's for the pendingGrid
            addUpdateUrl:this.options.pendingTaskaddUpdateUrl
        };
        this.views.completeTaskGridView = new kyt.GridView(ctgOptions);
    },

    registerSubscriptions: function(){
        // from main form
        $.subscribe('/contentLevel/form_mainForm/pageLoaded',$.proxy(this.loadRolesTokenizers,this), this.cid, "empDash");
        $.subscribe('/contentLevel/formModule_mainForm/moduleSuccess',$.proxy(this.mainFormSuccess,this), this.cid, "empDash");
        $.subscribe('/contentLevel/formModule_mainForm/moduleCancel',$.proxy(this.mainFormSuccess,this), this.cid, "empDash");

        // from grid
        $.subscribe('/contentLevel/grid_pendingTaskGrid/AddUpdateItem',$.proxy(function(url,data){this.addUpdateItem(url,data,"pendingTaskForm")},this), this.cid, "empDash");
        $.subscribe('/contentLevel/grid_pendingTaskGrid/Edit',$.proxy(function(url,data){this.addUpdateItem(url,data,"pendingTaskForm")},this), this.cid, "empDash");
        $.subscribe('/contentLevel/grid_pendingTaskGrid/Display',$.proxy(function(url,data){this.displayItem(url,data,"pendingTaskDisplay")},this), this.cid, "empDash");

        $.subscribe('/contentLevel/grid_completeTaskGrid/Display',$.proxy(function(url,data){this.displayItem(url,data,"completeTaskDisplay")},this), this.cid, "empDash");

        $.subscribe('/contentLevel/form_pendingTaskForm/pageLoaded',$.proxy(this.loadTokenizers,this), this.cid, "empDash");
        // from form
        $.subscribe('/contentLevel/form_pendingTaskForm/success', $.proxy(this.formSuccess,this), this.cid, "empDash");
        $.subscribe('/contentLevel/popup_pendingTaskForm/cancel', $.proxy(this.popupCancel,this), this.cid, "empDash");

        // from display
        $.subscribe('/contentLevel/popup_pendingTaskDisplay/cancel', $.proxy(this.popupCancel,this), this.cid, "empDash");
        $.subscribe('/contentLevel/popup_pendingTaskDisplay/edit', $.proxy(this.displayEdit,this), this.cid, "empDash");
        $.subscribe('/contentLevel/popup_pendingTaskDisplay/copyTask', $.proxy(this.copyTask,this), this.cid, "empDash");

        $.subscribe('/contentLevel/popup_completeTaskDisplay/cancel', $.proxy(this.popupCancel,this), this.cid, "empDash");
        $.subscribe('/contentLevel/popup_completeTaskDisplay/copyTask', $.proxy(this.copyTask,this), this.cid, "empDash");
    },

    mainFormSuccess:function(result){
        if(result.success) return;
        $.address.value(this.options.employeeListUrl);
    },

    loadRolesTokenizers: function(formOptions){
        var options = $.extend({},formOptions.rolesOptions,{el:"#rolesInputRoot"});
        this.views.roles = new kyt.TokenView(options);
    },
    addUpdateItem: function(url, data,name){
        if(this.options.popupIsActive){return;}
        this.options.popupIsActive = true;
        var crudFormOptions={};
        crudFormOptions.additionalSubmitData =  {"From":"Employee","ParentId":entityId};
        var _url = url?url:this.options[name+"addUpdateUrl"];
        if(!data)data={};
        data.Popup=true;
        $("#masterArea").after("<div id='dialogHolder'/>");
        var moduleOptions = {
            id:name,
            el:"#dialogHolder",
            url: _url,
            data:data,
            crudFormOptions:crudFormOptions,
            buttons: kyt.popupButtonBuilder.builder(name).standardEditButons()
        };
        this.modules[name] = new kyt.AjaxPopupFormModule(moduleOptions);
    },

    displayItem: function(url, data,name){
        var _url = url?url:this.options.displayUrl;
        var builder = kyt.popupButtonBuilder.builder(name);
        var buttons = builder.standardDisplayButtons();
        if(name == "pendingTaskDisplay" || name== "completeTaskDisplay"){
            builder.clearButtons();
            builder.addButton("Copy Task", function(){$.publish("/contentLevel/popup_"+name+"/copyTask",[$("#AddUpdateUrl",this).val(),name])});
            builder.addCancelButton();
            if(name == "pendingTaskDisplay" ){
                builder.addEditButton();
            }
        buttons = builder.getButtons();
        }
        $("#masterArea").after("<div id='dialogHolder'/>");
            var moduleOptions = {
            id:name,
            el:"#dialogHolder",
            url: _url,
            buttons: buttons
        };
        this.modules[name] = new kyt.AjaxPopupDisplayModule(moduleOptions);
    },
    //from popupformmodule
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
    //from form
    formSuccess:function(result,form,id){
        this.popupCancel(id);
        this.views[this.getRootOfName(id) +"GridView"].reloadGrid();
        if(id=="pendingTaskForm"){
            this.views["completeTaskGridView"].reloadGrid();
        }

    },
    popupCancel: function(id){
        this.options.popupIsActive=false;
        this.modules[id].destroy();
    },

    //from display
    displayEdit:function(url, name){
        this.modules[name].destroy();
        this.addUpdateItem(url, null,this.getRootOfName(name)+"Form");
    },

    copyTask:function(url, name){
        this.modules[name].destroy();
        this.addUpdateItem(url, {"Copy":"true"}, "pendingTaskForm");
    },

    getRootOfName:function(name){
        if(name.indexOf("Display")>0){
            return name.substring(0,name.indexOf("Display"));
        }else if(name.indexOf("Form")>0){
            return name.substring(0,name.indexOf("Form"));
        }
    }
});