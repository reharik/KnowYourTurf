/**
 * Created by JetBrains RubyMine.
 * User: RHarik
 * Date: 10/6/11
 * Time: 4:52 PM
 * To change this template use File | Settings | File Templates.
 */
if (typeof kyt == "undefined") {
    var kyt = {};
}

kyt.CrudController = kyt.Controller.extend({
    events:_.extend({
    }, kyt.Controller.prototype.events),

    initializeBase:function(){
        $.extend(this,this.defaults());
        this.options = $.extend({},kyt.crudControllerDefaults, this.options);
        $.clearPubSub();
        this.registerSubscriptions();
    },
    initialize:function(){
        this.initializeBase();
        var displayOptions={
            el:"#masterArea",
            id:this.options.gridName
        };
        var options = $.extend({}, this.options,displayOptions);
        this.views.gridView = new kyt.GridView(options);
    },

    registerSubscriptions: function(){
        // from grid
        $.subscribe('/grid_'+this.options.gridName+'/AddNewItem',$.proxy(this.addEditItem,this), this.cid);
        $.subscribe('/grid_'+this.options.gridName+'/Edit',$.proxy(this.addEditItem,this), this.cid);
        $.subscribe('/grid_'+this.options.gridName+'/Display',$.proxy(this.displayItem,this), this.cid);
        $.subscribe('/grid_'+this.options.gridName+'/Delete',$.proxy(this.deleteItem,this), this.cid);
        $.subscribe('/grid_'+this.options.gridName+'/Redirect',$.proxy(this.redirectItem,this), this.cid);
        // from form
        $.subscribe('/form_editModule/success', $.proxy(this.formSuccess,this), this.cid);
        $.subscribe('/form_editModule/cancel', $.proxy(this.formCancel,this), this.cid);
        // from display
        $.subscribe('/popup_displayModule/cancel', $.proxy(this.displayCancel,this), this.cid);
        $.subscribe('/popup_displayModule/edit', $.proxy(this.displayEdit,this), this.cid);
        this.additionalSubscriptions();
    },
    additionalSubscriptions:function(){},

    //from grid
    addEditItem: function(url, data){
        var _url = url?url:this.options.addEditUrl;
        $("#masterArea").after("<div id='dialogHolder'/>");
        var moduleOptions = {
            id:"editModule",
            el:"#dialogHolder",
            url: _url,
            data:data
        };
        this.modules.popupForm = new kyt.PopupFormModule(moduleOptions);
    },

    displayItem: function(url, data){
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
    deleteItem:function(url,data){

    },
    redirectItem:function(url,data){
        window.location.href = url ? url : this.options.redirectUrl;
    },

    //from form
    formSuccess:function(){
        this.formCancel();
        this.views.gridView.reloadGrid();
    },
    formCancel: function(){
        this.modules.popupForm.destroy();
    },

    //from display
    displayCancel:function(){
        this.modules.popupDisplay.destroy();
    },

    displayEdit:function(data){
        this.modules.popupDisplay.destroy();
        this.addEditItem(data);
    }
});

kyt.crudControllerDefaults={
    gridName:"",
    id:""
};