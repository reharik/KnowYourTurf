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
        $.subscribe('/contentLevel/grid/AddUpdateItem',$.proxy(this.addEditItem,this), this.cid);
        //
        $.subscribe("/contentLevel/form_mainForm/pageLoaded", $.proxy(this.formLoaded,this),this.cid);
        //
        $.subscribe('/contentLevel/formModule_mainForm/moduleSuccess',$.proxy(this.moduleSuccess,this),this.cid);
        $.subscribe('/contentLevel/formModule_mainForm/moduleCancel',$.proxy(this.moduleCancel,this), this.cid);
        this.registerAdditionalSubscriptions();
    },
    registerAdditionalSubscriptions:function(){},

    //from grid
    addEditItem: function(url){
        var formOptions = {
            el: "#detailArea",
            id: "mainForm",
            url: url
        };
        $("#masterArea","#contentInner").after("<div id='detailArea'/>");
        this.modules.formModule = new kyt.FormModule(formOptions);
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
    }
});
