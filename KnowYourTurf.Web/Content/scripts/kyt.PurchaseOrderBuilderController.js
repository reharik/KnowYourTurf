/**
 * Created by JetBrains RubyMine.
 * User: Owner
 * Date: 11/7/11
 * Time: 7:05 PM
 * To change this template use File | Settings | File Templates.
 */

kyt.PurchaseOrderBuilderController = kyt.Controller.extend({
    events:_.extend({
    }, kyt.Controller.prototype.events),

    initialize:function(){
        $.extend(this,this.defaults());
        $.clearPubSub();
        this.registerSubscriptions();
        var viewOptions={
            el:"#masterArea",
            id:"POBuilder"
        };
        this.views.gridView = new kyt.FormView(viewOptions);
        var pExtraOptions = {
            pager:"productPager",
            url:$("#vendorId").val()>0?this.options.productGridDef.Url+$("#vendorId").val():"",
            grouping:true,
            groupingView : {
                groupField : ['InstantiatingType'],
                groupColumnShow : [false]
            }
        };
        var productGridOptions={
            el:"#productGridArea",
            id:"productGrid",
            gridContainer:"#productGrid",
            gridDef:this.options.productGridDef,
            gridOptions: pExtraOptions
        };
        this.views.productGridView = new kyt.GridView(productGridOptions);

        var poliExtraOptions = {
            pager:"poliPager",
            caption:"Purchase Order Line Items"
        };
        var poliGridOptions={
            el:"#poliGridArea",
            id:"poliGrid",
            gridContainer:"#poliGrid",
            gridDef:this.options.poliGridDef,
            gridOptions: poliExtraOptions
        };
        this.views.poliGridView = new kyt.GridView(poliGridOptions);
        $("#productGrid").jqGrid('setGridState', $("#vendorId").val()<=0?'hidden':'visible');
    },
    registerSubscriptions:function(){
        $.subscribe("/contentLevel/form_POBuilder/pageLoaded", $.proxy(this.pageLoaded,this));
        $.subscribe("/contentLevel/grid_productGrid/Other", $.proxy(this.addToOrder,this));
        $.subscribe("/contentLevel/grid_productGrid/Display", $.proxy(this.displayProduct,this));
        $.subscribe("/contentLevel/grid_poliGrid/Delete", $.proxy(this.deleteItem,this));
        $.subscribe("/contentLevel/grid_poliGrid/AddUpdateItem", $.proxy(this.editItem,this));
        $.subscribe("/contentLevel/grid_poliGrid/Display", $.proxy(this.displayItem,this));
        $.subscribe('/contentLevel/popup_displayModule/cancel', $.proxy(this.displayCancel,this), this.cid);
        $.subscribe('/contentLevel/popup_displayModule/edit', $.proxy(this.displayEdit,this), this.cid);

        $.subscribe('/form_editModule/success', $.proxy(this.itemSuccess,this), this.cid);

    },
    pageLoaded:function(){
        this.showPOData();
        this.setupEvents()
    },
    showPOData: function() {
        var poId = $("#PurchaseOrder_EntityId").val();
        if (poId > 0) {
            $("#viewPOID").show();
            $("#viewVendor").show();
            $("#editVendor").hide();
            $("#PurchaseOrder_EntityId").val(poId);
            $("#POID").val(poId);
            $("#viewPOID").find("span").text(poId);
        } else {
            $("#viewPOID").hide();
            $("#viewVendor").hide();
            $("#editVendor").show();
        }
    },
    setupEvents: function(){
         $("#vendor").change($.proxy(function(){
             var vendorId = $("#vendor").val();
             $("#vendorId").val(vendorId);
            if(vendorId>0){
                $("#productGrid").setGridParam({url:this.options.productGridDef.Url.substring(0,this.options.productGridDef.Url.lastIndexOf("=")+1) + vendorId});
                $("#productGrid").jqGrid('setGridState','visible');
                $("#productGrid").trigger("reloadGrid");
            }
        },this));

        $("#commit").click($.proxy(this.commitPO, this));
        $("#return").click($.proxy(function(){window.location.href = this.options.returnUrl;},this));
    },
    addToOrder:function(url){
        var poId = $("#poId").val();
        var vendorId = $("#vendorId").val();
        kyt.repository.ajaxGet(url,{"PurchaseOrderId":poId,"VendorId":vendorId}, $.proxy(this.addToOrderCallback,this))
    },
    addToOrderCallback:function(result){
        $("#poId").val(result.EntityId);
        var url = this.views.poliGridView.getUrl();
        url = url.substr(0,url.indexOf("?EntityId"));
        url = url+"?EntityId="+result.EntityId;
        this.views.poliGridView.setUrl(url);
        this.views.poliGridView.reloadGrid();
    },
    displayProduct:function(url, data){
        var builder = kyt.popupButtonBuilder.builder("displayModule");
        var button = builder.addButton("Return",builder.addCancelButton()).getButtons();
        var _url = url?url:this.options.displayUrl;
        $("#masterArea").after("<div id='dialogHolder'/>");
        var moduleOptions = {
            id:"displayModule",
            el:"#dialogHolder",
            url: _url,
            buttons: button
        };
        this.modules.popupDisplay= new kyt.AjaxPopupDisplayModule(moduleOptions);
    },

    displayItem:function(url, data){
        var _url = url?url:this.options.displayUrl;
        $("#masterArea").after("<div id='dialogHolder'/>");
        var moduleOptions = {
            id:"displayModule",
            el:"#dialogHolder",
            url: _url,
            buttons: kyt.popupButtonBuilder.builder("displayModule").standardDisplayButtons()
        };
        this.modules.popupDisplay= new kyt.PopupDisplayModule(moduleOptions);
    },
        //from display
    displayCancel:function(){
        this.modules.popupDisplay.destroy();
    },

    displayEdit:function(data){
        this.modules.popupDisplay.destroy();
        this.editItem(data);
    },

    deleteItem:function(url){
        if (confirm("Are you sure you would like to delete this Item?")) {
        kyt.repository.ajaxGet(url,{}, $.proxy(function(){
            this.views.poliGridView.reloadGrid();},this));
        }
    },
    editItem:function(url, data){
        var poId = $("#poId").val();
         var _url = url?url:this.options.addUpdateUrl;
        _url = _url+"?ParentId="+poId;
        $("#masterArea").after("<div id='dialogHolder'/>");
        var moduleOptions = {
            id:"editModule",
            el:"#dialogHolder",
            url: _url,
            data:data
        };
        this.modules.popupForm = new kyt.AjaxPopupFormModule(moduleOptions);
    },
    itemSuccess:function(){
       this.itemCancel();
       this.views.poliGridView.reloadGrid();
    },
    itemCancel: function(){
       this.modules.popupForm.destroy();
    },
    commitPO:function(){
        var purchaseOrderId = $("#PurchaseOrder_EntityId").val();
        window.location.href = this.options.commitUrl+"?ParentId="+purchaseOrderId;
    }
});