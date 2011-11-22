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
        $.subscribe('/grid_eventTypeGrid/AddNewItem',$.proxy(this.addEditItem,this), this.cid);
        $.subscribe('/grid_eventTypeGrid/Edit',$.proxy(this.addEditItem,this), this.cid);
        $.subscribe('/grid_eventTypeGrid/Display',$.proxy(this.displayItem,this), this.cid);
        $.subscribe('/grid_eventTypeGrid/Delete',$.proxy(this.deleteItem,this), this.cid);
        // from ttgrid
        $.subscribe('/grid_taskTypeGrid/AddNewItem',$.proxy(this.addEditItem,this), this.cid);
        $.subscribe('/grid_taskTypeGrid/Edit',$.proxy(this.addEditItem,this), this.cid);
        $.subscribe('/grid_taskTypeGrid/Display',$.proxy(this.displayItem,this), this.cid);
        $.subscribe('/grid_taskTypeGrid/Delete',$.proxy(this.deleteItem,this), this.cid);
        // from dcgrid
        $.subscribe('/grid_documentCategoryGrid/AddNewItem',$.proxy(this.addEditItem,this), this.cid);
        $.subscribe('/grid_documentCategoryGrid/Edit',$.proxy(this.addEditItem,this), this.cid);
        $.subscribe('/grid_documentCategoryGrid/Display',$.proxy(this.displayItem,this), this.cid);
        $.subscribe('/grid_documentCategoryGrid/Delete',$.proxy(this.deleteItem,this), this.cid);
        // from pcgrid
        $.subscribe('/grid_photoCategoryGrid/AddNewItem',$.proxy(this.addEditItem,this), this.cid);
        $.subscribe('/grid_photoCategoryGrid/Edit',$.proxy(this.addEditItem,this), this.cid);
        $.subscribe('/grid_photoCategoryGrid/Display',$.proxy(this.displayItem,this), this.cid);
        $.subscribe('/grid_photoCategoryGrid/Delete',$.proxy(this.deleteItem,this), this.cid);
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