/**
 * Created by JetBrains RubyMine.
 * User: Owner
 * Date: 11/8/11
 * Time: 7:22 PM
 * To change this template use File | Settings | File Templates.
 */


kyt.PurchaseOrderController = kyt.Controller.extend({
    events:_.extend({
    }, kyt.Controller.prototype.events),

    initialize:function(){
        $.extend(this,this.defaults());
        this.options = $.extend({},kyt.crudControllerDefaults, this.options);
        $.clearPubSub();
        this.registerSubscriptions();
        var displayOptions={
            el:"#masterArea",
            id:this.options.gridName
        };
        var options = $.extend({}, this.options,displayOptions);
        this.views.gridView = new kyt.GridView(options);
    },
    registerSubscriptions: function(){
    // from grid
        $.subscribe('/contentLevel/grid_'+this.options.gridName+'/AddNewItem',$.proxy(this.addEditItem,this), this.cid);
        $.subscribe('/contentLevel/grid_'+this.options.gridName+'/Edit',$.proxy(this.addEditItem,this), this.cid);
        $.subscribe('/contentLevel/grid_'+this.options.gridName+'/Display',$.proxy(this.displayItem,this), this.cid);
        $.subscribe('/contentLevel/grid_'+this.options.gridName+'/Delete',$.proxy(this.deleteItem,this), this.cid);
        $.subscribe('/contentLevel/grid_'+this.options.gridName+'/Redirect',$.proxy(this.addEditItem,this), this.cid);
        // from form
        // from display
        $.subscribe('/contentLevel/popup_displayModule/cancel', $.proxy(this.displayCancel,this), this.cid);
        $.subscribe('/contentLevel/popup_displayModule/edit', $.proxy(this.displayEdit,this), this.cid);
    },
    addEditItem: function(url, data){
        var parentId = data ? data.ParentId : 0;
        window.location.href = url + "?ParentId=" + parentId;
    },
    displayItem: function(url, data){
        var builder = kyt.popupButtonBuilder.builder("displayModule");
        builder.addButton("Edit",function(e) {
                            var poId = $("#PurchaseOrder_EntityId").val();
                            var editUrl = $("#AddEditUrl",this).val();
                            $(this).dialog("close");
                            $(this).remove();
                            $(".ui-dialog").remove();
            $.publish("/contentLevel/popup_displayModule/edit",[editUrl,{"ParentId":poId}]);
        });
        builder.addCancelButton();
        var buttons = builder.getButtons();
        var _url = url?url:this.options.displayUrl;
        $("#masterArea").after("<div id='dialogHolder'/>");
        var moduleOptions = {
            id:"displayModule",
            el:"#dialogHolder",
            url: _url,
            buttons: buttons
        };
        this.modules.popupDisplay= new kyt.PopupDisplayModule(moduleOptions);
    },

    deleteItem:function(url,data){
        if (confirm("Are you sure you would like to delete this Item?")) {
        kyt.repository.ajaxGet(url,{}, $.proxy(function(){
            this.views.gridView.reloadGrid();},this));
        }
    },
        //from display
    displayCancel:function(){
        this.modules.popupDisplay.destroy();
    },

    displayEdit:function(url, data){
        this.modules.popupDisplay.destroy();
        this.addEditItem(url, data);
    }
});