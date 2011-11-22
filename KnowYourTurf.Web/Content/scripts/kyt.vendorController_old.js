/**
 * Created by .
 * User: RHarik
 * Date: 9/1/11
 * Time: 3:03 PM
 * To change this template use File | Settings | File Templates.
 */

if (typeof kyt == "undefined") {
            var kyt = {};
}

kyt.gridDetailController = function(container, options){
    var _container = $(container);
    var myOptions = $.extend({}, options || {});
    var modules = {};
    var that;
    return{
        init: function(){
            that = this;
            // this should be temporary as there will be more global controllers
            // look at namespacing the pubsub
            $.clearPubSub();
            var grid = kyt.gridModule("#masterArea",myOptions);
            modules.gridModule = kyt.gridModule("#masterArea", myOptions);
            $.subscribe('/grid/addItem',function(url){that.addEditItem(url)},"gdCont");
            $.subscribe('/grid/editItem',function(url){that.addEditItem(url)},"gdCont");
            $.subscribe('/grid/viewItem',function(url){that.viewItem(url)},"gdCont");
            $.subscribe('/grid/deleteItem',function(url){that.deleteItem(url)},"gdCont");
            $.subscribe('/form/success',function(container){that.formSuccess(container)},"gdCont");
            $.subscribe('/form/cancel',function(container){that.formCancel(container)},"gdCont");
            $.subscribe("/form/pageLoaded", $.proxy(this.setupTokenInputControllers,that),"gdCont");
            $.each(modules,function(i,item){
               item.init();
            });
        },
        destroy:function(){
            $.unsubscribeByHandle("gdCont");
            for(var mod in modules){
                mod.destroy();
            }
            _container.empty();
        },
        addEditItem: function(url){
            $("#masterArea").after("<div id='detailArea'/>");
            modules.formModule = kyt.formModule("#detailArea");
            modules.formModule.init();
            modules.formModule.loadFormCall(url,assetType);
            $("#masterArea").hide();
        },

        setupTokenInputControllers: function(){
            var chemicalMod = kyt.tokenModule("#ChemicalTokenContainer",extraFormOptions.ChemicalOptons);
            var fertilizerlMod = kyt.tokenModule("#FertilizerTokenContainer",extraFormOptions.FertilizerOptons);
            var materiallMod = kyt.tokenModule("#MaterialTokenContainer",extraFormOptions.MaterialOptons);
            modules["chemicalMod"] =chemicalMod;
            modules["fertilizerlMod"] =fertilizerlMod;
            modules["materiallMod"] =materiallMod;
            modules["chemicalMod"].init();
            modules["fertilizerlMod"].init();
            modules["materiallMod"].init();
        },

        display: function(url, id){},

        formSuccess:function(){
            $("#detailArea").remove();
            $("#masterArea").show();
            this.removeModule("formController");
            this.removeModule("docTokenController");
            this.removeModule("photoTokenController");
            modules.gridModule.reloadGrid();
        },

        formCancel: function(){
            $("#detailArea").remove();
            $("#masterArea").show();
            this.removeModule("formController");
            this.removeModule("docTokenController");
            this.removeModule("photoTokenController");
        },
        removeModule:function(moduleName){
            modules[moduleName] = null;
        }
    }
};


