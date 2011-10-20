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
kyt.employeeDashboardController = function(options){
    var controllerOptions = $.extend({}, options || {});
    var views = {};
    var modules = {};
    var that;
    return{
        init: function(){
            that = this;
            $.clearPubSub();
            controllerOptions.el="#masterArea";
            views.displayView = new kyt.DisplayView(controllerOptions);

            var ptgOptions = {
                el:"#pendingTaskGridContainer",
                id:"pendingTaskGrid",
                gridContainer:"#gridContainer_pt",
                gridDef:controllerOptions.pendingGridDef,
                addEditUrl:controllerOptions.pendingTaskaddEditUrl
            };
            views.pendingTaskGridView = new kyt.GridView(ptgOptions);
            var ctgOptions = {
                el:"#completeTaskGridContainer",
                id:"completeTaskGrid",
                gridContainer:"#gridContainer_ct",
                gridDef:controllerOptions.completeGridDef,
                // this is not used except for copy task which is why it's for the pendingGrid
                addEditUrl:controllerOptions.pendingTaskaddEditUrl
            };
            views.completeTaskGridView = new kyt.GridView(ctgOptions);
            this.registerSubscriptions();
        },

        registerSubscriptions: function(){
            // from grid
            $.subscribe('/grid_pendingTaskGrid/AddNewItem',function(url,data){that.addEditItem(url,data,"pendingTaskForm")}, "fieldDashboardController");
            $.subscribe('/grid_pendingTaskGrid/Edit',function(url,data){that.addEditItem(url,data,"pendingTaskForm")}, "fieldDashboardController");
            $.subscribe('/grid_pendingTaskGrid/Display',function(url,data){that.displayItem(url,data,"pendingTaskDisplay")}, "fieldDashboardController");

            $.subscribe('/grid_completeTaskGrid/Display',function(url,data){that.displayItem(url,data,"completeTaskDisplay")}, "fieldDashboardController");

            // from form
            $.subscribe('/form_pendingTaskForm/success', that.formSuccess, "fieldDashboardController");
            $.subscribe('/form_pendingTaskForm/cancel', that.popupCancel, "fieldDashboardController");

            // from display
            $.subscribe('/popup_pendingTaskDisplay/cancel', that.popupCancel, "fieldDashboardController");
            $.subscribe('/popup_pendingTaskDisplay/edit', that.displayEdit, "fieldDashboardController");
            $.subscribe('/popup_pendingTaskDisplay/copyTask', that.copyTask, "fieldDashboardController");

            $.subscribe('/popup_completeTaskDisplay/cancel', that.popupCancel, "fieldDashboardController");
            $.subscribe('/popup_completeTaskDisplay/copyTask', that.copyTask, "fieldDashboardController");
        },

        addEditItem: function(url, data,name){
            var crudFormOptions={};
            crudFormOptions.additionalSubmitData =  {"From":"Field","ParentId":entityId};
            var _url = url?url:controllerOptions[name+"addEditUrl"];
            var moduleOptions = {
                id:name,
                url: _url,
                data:data,
                crudFormOptions:crudFormOptions,
                buttons: kyt.popupButtonBuilder.builder(name).standardEditButons()
            };
            modules[name] = kyt.popupFormModule(moduleOptions);
            modules[name].init();
        },

        displayItem: function(url, data,name){
            var _url = url?url:controllerOptions.displayUrl;
            var builder = kyt.popupButtonBuilder.builder(name);
            var buttons = builder.standardDisplayButtons();
            builder.clearButtons();
            builder.addButton("Copy Task", function(){$.publish("/popup_"+name+"/copyTask",[$("#AddEditUrl",this).val(),name])});
            builder.addCancelButton();
            if(name == "pendingTaskDisplay" ){
                builder.addEditButton();
            }
            buttons = builder.getButtons();
            var moduleOptions = {
                id:name,
                url: _url,
                buttons: buttons
            };
            modules[name] = kyt.popupDisplayModule(moduleOptions);
            modules[name].init();
        },
        //from form
        formSuccess:function(result,form,id){
            that.popupCancel(id);
            views[that.getRootOfName(id) +"GridView"].reloadGrid();
            views["completeTaskGridView"].reloadGrid();

        },
        popupCancel: function(id){
            modules[id].destroy();
        },

        //from display
        displayEdit:function(url, name){
            modules[name].destroy();
            that.addEditItem(url, null,that.getRootOfName(name)+"Form");
        },

        copyTask:function(url, name){
            modules[name].destroy();
            that.addEditItem(url, {"Copy":"true"}, "pendingTaskForm");
        },

        getRootOfName:function(name){
            if(name.indexOf("Display")>0){
                return name.substring(0,name.indexOf("Display"));
            }else if(name.indexOf("Form")>0){
                return name.substring(0,name.indexOf("Form"));
            }
        }
    }
};