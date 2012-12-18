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


kyt.CrudController  = kyt.Controller.extend({
    events:_.extend({
    }, kyt.Controller.prototype.events),

    initialize:function(options){
        $.extend(this,this.defaults());
        kyt.contentLevelControllers["crudController"]=this;
        $.unsubscribeByPrefix("/contentLevel");
        this.id="crudController";
        this.registerSubscriptions();

        var _options = $.extend({},this.options, options);
        _options.el="#masterArea";
        this.views.gridView = new kyt.GridView(_options);
    },

    registerSubscriptions: function(){
        $.subscribe('/contentLevel/grid_/Redirect',$.proxy(this.redirectItem,this), this.cid);
        $.subscribe('/contentLevel/grid_/AddUpdateItem',$.proxy(this.addUpdateItem,this), this.cid);
        $.subscribe('/contentLevel/grid_/Display',$.proxy(this.display,this), this.cid);
        //
        $.subscribe("/contentLevel/form_mainForm/pageLoaded", $.proxy(this.formLoaded,this),this.cid);
        $.subscribe("/contentLevel/display_mainDisplay/pageLoaded", $.proxy(this.formLoaded,this),this.cid);
        //
        $.subscribe('/contentLevel/formModule_mainForm/moduleSuccess',$.proxy(this.moduleSuccess,this),this.cid);
        $.subscribe('/contentLevel/formModule_mainForm/moduleCancel',$.proxy(this.moduleCancel,this), this.cid);
        $.subscribe('/contentLevel/display_mainDisplay/cancel',$.proxy(this.displayCancel,this), this.cid);
        this.registerAdditionalSubscriptions();
    },
    registerAdditionalSubscriptions:function(){},

    //from grid
    addUpdateItem: function(url){
        var parentId = $("#masterArea").find("#ParentId").val();
        var rootId = $("#masterArea").find("#RootId").val();
        var data = {"ParentId":parentId,"RootId":rootId};
        var formOptions = {
            el: "#detailArea",
            id: "mainForm",
            url: url,
            data:data
        };
        $("#masterArea","#contentInner").after("<div id='detailArea'/>");
        this.modules.formModule = new kyt.AjaxFormModule(formOptions);
    },

    display: function(url){
        var formOptions = {
            el: "#detailArea",
            id: "mainDisplay",
            url: url
        };
        $("#masterArea","#contentInner").after("<div id='detailArea'/>");
        this.views.displayView = new kyt.AjaxDisplayView(formOptions);
        this.views.displayView.render();
    },

    redirectItem:function(url){
        var parentId = $("#masterArea").find("#ParentId").val();
        if(parentId && parentId > 0 ){
            url = url +"?ParentId="+parentId;
        }
        $.address.value(url);
    },
    formLoaded:function(){
         $("#masterArea").hide();
    },
    moduleSuccess:function(){
        this.moduleCancel();
        this.views.gridView.reloadGrid();
    },
    moduleCancel: function(){
        this.modules.formModule.destroy();
        $("#masterArea").show();
    },
        displayCancel: function(){
        this.views.displayView.remove();
        $("#masterArea").show();
    }
});
