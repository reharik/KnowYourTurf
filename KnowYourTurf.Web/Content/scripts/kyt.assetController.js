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


kyt.AssetController  = kyt.CrudController.extend({
    events:_.extend({
    }, kyt.CrudController.prototype.events),

    initialize:function(options){
        $.extend(this,this.defaults());
        kyt.contentLevelControllers["assetController"]=this;
        $.unsubscribeByPrefix("/contentLevel");
        this.id="assController";
        this.registerSubscriptions();

        var _options = $.extend({},this.options, options);
        _options.el="#masterArea";
        this.views.gridView = new kyt.AssetGridView(_options);
    },

    registerAdditionalSubscriptions: function(){
        $.subscribe('/contentLevel/grid/addToPortfolio',$.proxy(this.addToPortfolio,this),this.cid);
        //
        $.subscribe("/contentLevel/display_addToPortfolio/portfolioChosen", $.proxy(this.portfolioChosen,this),this.cid);
        //
        $.subscribe('/contentLevel/formModule_mainForm/moduleSuccess',$.proxy(this.moduleSuccess,this),this.cid);
        $.subscribe('/contentLevel/formModule_mainForm/moduleCancel',$.proxy(this.moduleCancel,this), this.cid);
    },

    //from grid
    addEditItem: function(url,assetType){
        var formOptions = {
            el: "#detailArea",
            id: "mainForm",
            url: url,
            data:{"AssetType":assetType}
        };
        $("#masterArea","#contentInner").after("<div id='detailArea'/>");
        this.modules.formModule = new kyt.AssetFormModule(formOptions);
    },
    addToPortfolio: function(result){
        $("#masterArea",".content-outer").after("<div id='popupContentDiv'/>");
        $("#popupContentDiv").html(result);
        var displayOptions = {
            el:"#popupContentDiv",
            id:"addToPortfolio",
            url:this.options.addItemsToPortfolioUrl
        };
        displayOptions.view = new kyt.AddToPortfolioView(displayOptions);
        this.modules.addToPortfolio = new kyt.AjaxPopupDisplayModule(displayOptions);
    },
    portfolioChosen:function(id){
        this.views.gridView.handleAddToPortfolio(id);
    }
});
