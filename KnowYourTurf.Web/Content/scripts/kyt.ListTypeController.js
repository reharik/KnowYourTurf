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
    events:_.extend({
    }, kyt.CrudController.prototype.events),
    additionalSubscriptions:function(){
        // from etgrid
        $.subscribe('/grid_EventTypeGrid/AddNewItem',$.proxy(this.addEditItem,this), this.cid);
        $.subscribe('/grid_EventTypeGrid/Edit',$.proxy(this.addEditItem,this), this.cid);
        $.subscribe('/grid_EventTypeGrid/Display',$.proxy(this.displayItem,this), this.cid);
        $.subscribe('/grid_EventTypeGrid/Delete',$.proxy(this.deleteItem,this), this.cid);
        // from ttgrid
        $.subscribe('/grid_TaskTypeGrid/AddNewItem',$.proxy(this.addEditItem,this), this.cid);
        $.subscribe('/grid_TaskTypeGrid/Edit',$.proxy(this.addEditItem,this), this.cid);
        $.subscribe('/grid_TaskTypeGrid/Display',$.proxy(this.displayItem,this), this.cid);
        $.subscribe('/grid_TaskTypeGrid/Delete',$.proxy(this.deleteItem,this), this.cid);
        // from dcgrid
        $.subscribe('/grid_DocumentCategoryGrid/AddNewItem',$.proxy(this.addEditItem,this), this.cid);
        $.subscribe('/grid_DocumentCategoryGrid/Edit',$.proxy(this.addEditItem,this), this.cid);
        $.subscribe('/grid_DocumentCategoryGrid/Display',$.proxy(this.displayItem,this), this.cid);
        $.subscribe('/grid_DocumentCategoryGrid/Delete',$.proxy(this.deleteItem,this), this.cid);
        // from pcgrid
        $.subscribe('/grid_PhotoCategoryGrid/AddNewItem',$.proxy(this.addEditItem,this), this.cid);
        $.subscribe('/grid_PhotoCategoryGrid/Edit',$.proxy(this.addEditItem,this), this.cid);
        $.subscribe('/grid_PhotoCategoryGrid/Display',$.proxy(this.displayItem,this), this.cid);
        $.subscribe('/grid_PhotoCategoryGrid/Delete',$.proxy(this.deleteItem,this), this.cid);
    },
    initialize:function(){
        this.el = ("#masterArea");
        this.initializeBase();
        var etOptions={
            el:"#etGrid",
            id:"eventTypeGrid",
            gridDef:this.options.gridInfo.gridDef,
            addEditUrl:this.options.gridInfo.addEditUrl,
            gridContainer:"#EventTypeGrid"
        };
        this.views.etGridView = new kyt.GridView(etOptions);
        var ttOptions={
            el:"#ttGrid",
            id:"taskTypeGrid",
            gridDef:this.options.ttGridInfo.gridDef,
            addEditUrl:this.options.ttGridInfo.addEditUrl,
            gridContainer:"#TaskTypeGrid"
        };
        this.views.ttGridView = new kyt.GridView(ttOptions);
        var dcOptions={
            el:"#dcGrid",
            id:"documentCategoryGrid",
            gridDef:this.options.dcGridInfo.gridDef,
            addEditUrl:this.options.dcGridInfo.addEditUrl,
            gridContainer:"#DocumentCategoryGrid"
        };
        this.views.dcGridView = new kyt.GridView(dcOptions);
        var pcOptions={
            el:"#pcGrid",
            id:"photoCategoryGrid",
            gridDef:this.options.pcGridInfo.gridDef,
            addEditUrl:this.options.pcGridInfo.addEditUrl,
            gridContainer:"#PhotoCategoryGrid"
        };
        this.views.pcGridView = new kyt.GridView(pcOptions);
    },

    addEditItem: function(url, data){
        var _url = url?url:this.options.addEditUrl;
        $("#masterArea").after("<div id='dialogHolder'/>");
        var moduleOptions = {
            id:"editModule",
            el:"#dialogHolder",
            url: _url,
            data:data
        };
        moduleOptions.runAfterRenderFunction = function () {
            $('#EventColor',this.el).miniColors();
        };
        this.modules.popupForm = new kyt.PopupFormModule(moduleOptions);
    }
});