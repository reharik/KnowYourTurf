/**
 * Created by JetBrains RubyMine.
 * User: Owner
 * Date: 11/6/11
 * Time: 9:29 AM
 * To change this template use File | Settings | File Templates.
 */

if (typeof kyt == "undefined") {
    var kyt = {};
}

kyt.ListTypeController = kyt.CrudController.extend({
    registerAdditionalSubscriptions:function(){
        // from etgrid
        $.subscribe('/contentLevel/grid_EventTypeGrid/AddUpdateItem',$.proxy(this.addUpdateItem,this), this.cid);
        $.subscribe('/contentLevel/grid_EventTypeGrid/Display',$.proxy(this.displayItem,this), this.cid);
        $.subscribe('/contentLevel/grid_EventTypeGrid/Delete',$.proxy(this.deleteItem,this), this.cid);
        // from ttgrid
        $.subscribe('/contentLevel/grid_TaskTypeGrid/AddUpdateItem',$.proxy(this.addUpdateItem,this), this.cid);
        $.subscribe('/contentLevel/grid_TaskTypeGrid/Display',$.proxy(this.displayItem,this), this.cid);
        $.subscribe('/contentLevel/grid_TaskTypeGrid/Delete',$.proxy(this.deleteItem,this), this.cid);
        // from dcgrid
        $.subscribe('/contentLevel/grid_DocumentCategoryGrid/AddUpdateItem',$.proxy(this.addUpdateItem,this), this.cid);
        $.subscribe('/contentLevel/grid_DocumentCategoryGrid/Display',$.proxy(this.displayItem,this), this.cid);
        $.subscribe('/contentLevel/grid_DocumentCategoryGrid/Delete',$.proxy(this.deleteItem,this), this.cid);
        // from pcgrid
        $.subscribe('/contentLevel/grid_PhotoCategoryGrid/AddUpdateItem',$.proxy(this.addUpdateItem,this), this.cid);
        $.subscribe('/contentLevel/grid_PhotoCategoryGrid/Edit',$.proxy(this.addEditItem,this), this.cid);
        $.subscribe('/contentLevel/grid_PhotoCategoryGrid/Display',$.proxy(this.displayItem,this), this.cid);
        $.subscribe('/contentLevel/grid_PhotoCategoryGrid/Delete',$.proxy(this.deleteItem,this), this.cid);

        // from form
        $.subscribe("/contentLevel/form_eventTypeGrid/pageLoaded", $.proxy(this.formLoaded,this),this.cid);
        $.subscribe("/contentLevel/form_taskTypeGrid/pageLoaded", $.proxy(this.formLoaded,this),this.cid);
        $.subscribe("/contentLevel/form_documentCategoryGrid/pageLoaded", $.proxy(this.formLoaded,this),this.cid);
        $.subscribe("/contentLevel/form_editModule/pageLoaded", $.proxy(this.formLoaded,this),this.cid);
        
    },
    initialize:function(options){
        $.extend(this,this.defaults());
        kyt.contentLevelControllers["ListTypeController"]=this;
        $.unsubscribeByPrefix("/contentLevel");
        this.id="ListTypeController";
        this.registerSubscriptions();

        $.extend(this.options, options);

        this.el = ("#masterArea");
        var etOptions={
            el:"#etGrid",
            id:"eventTypeGrid",
            gridName:"EventTypeGrid",
            gridDef:this.options.gridInfo.gridDef,
            addUpdateUrl:this.options.gridInfo.addUpdateUrl,
            gridContainer:"#EventTypeGrid",
            deleteMultipleUrl:this.options.gridInfo.deleteMultiple,
            gridOptions:{height:"400px"}
        };
        this.views.etGridView = new kyt.GridView(etOptions);
        var ttOptions={
            el:"#ttGrid",
            id:"taskTypeGrid",
            gridName:"TaskTypeGrid",
            gridDef:this.options.ttGridInfo.gridDef,
            addUpdateUrl:this.options.ttGridInfo.addUpdateUrl,
            gridContainer:"#TaskTypeGrid",
            deleteMultipleUrl:this.options.ttGridInfo.deleteMultiple,
            gridOptions:{height:"400px"}
        };
        this.views.ttGridView = new kyt.GridView(ttOptions);
        var dcOptions={
            el:"#dcGrid",
            id:"documentCategoryGrid",
            gridName:"DocumentCategoryGrid",
            gridDef:this.options.dcGridInfo.gridDef,
            addUpdateUrl:this.options.dcGridInfo.addUpdateUrl,
            gridContainer:"#DocumentCategoryGrid",
            deleteMultipleUrl:this.options.dcGridInfo.deleteMultiple,
            gridOptions:{height:"400px"}
        };
        this.views.dcGridView = new kyt.GridView(dcOptions);
        var pcOptions={
            el:"#pcGrid",
            id:"photoCategoryGrid",
            gridName:"PhotoCategoryGrid",
            gridDef:this.options.pcGridInfo.gridDef,
            addUpdateUrl:this.options.pcGridInfo.addUpdateUrl,
            gridContainer:"#PhotoCategoryGrid",
            deleteMultipleUrl:this.options.pcGridInfo.deleteMultiple,
            gridOptions:{height:"400px"}
        };
        this.views.pcGridView = new kyt.GridView(pcOptions);
    },
    addUpdateItem: function(url, data){
        var _url = url?url:this.options.addUpdateUrl;
        $("#masterArea","#contentInner").after("<div id='detailArea'/>");
        var moduleOptions = {
            id:"mainForm",
            el:"#detailArea",
            url: _url,
            data:data
        };
        moduleOptions.runAfterRenderFunction = function () {
            $('#EventColor',this.el).miniColors();
        };
        this.modules.formModule = new kyt.AjaxFormModule(moduleOptions);
    },
     //from form
    moduleSuccess:function(){
        this.moduleCancel();
         this.views.etGridView.reloadGrid();
        this.views.ttGridView.reloadGrid();
        this.views.dcGridView.reloadGrid();
        this.views.pcGridView.reloadGrid();
    },
    moduleCancel: function(){
        this.modules.formModule.destroy();
        $("#masterArea").show();
    }

});