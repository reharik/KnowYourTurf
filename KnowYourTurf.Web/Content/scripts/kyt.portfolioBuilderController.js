/**
 * Created by JetBrains RubyMine.
 * User: RHarik
 * Date: 10/7/11
 * Time: 2:08 PM
 * To change this template use File | Settings | File Templates.
 */

if (typeof kyt == "undefined") {
            var kyt = {};
}

kyt.PortfolioBuilderController  = kyt.Controller.extend({
    events:_.extend({
    }, kyt.Controller.prototype.events),

    initialize:function(){
        $.extend(this,this.defaults());

        kyt.contentLevelControllers["portfolioBuilderController"]=this;
        $.unsubscribeByPrefix("/contentLevel");
        this.registerSubscriptions();
        if(this.options.portfolioCount <= 0){
           this.loadLandingPage();
        }else{
            this.loadGrid();
        }
    },

    registerSubscriptions: function(){
        $.subscribe('/contentLevel/grid/AddUpdateItem',$.proxy(this.addEditItem,this),this.cid);
        $.subscribe('/contentLevel/grid/Preview',$.proxy(this.loadPreviewPortfolioDisplay,this),this.cid);
        $.subscribe("/contentLevel/gridLoadComplete",$.proxy(this.resetLandingPageIfEmpty,this) ,this.cid);
        // from form
        $.subscribe('/contentLevel/form_Portfolio/success',$.proxy(this.formSuccess,this),this.cid);
        $.subscribe('/contentLevel/form_Portfolio/cancel',$.proxy(this.formCancel,this),this.cid);
        $.subscribe("/contentLevel/form_Portfolio/pageLoaded", $.proxy(this.setupMainForm,this),this.cid);
        $.subscribe("/contentLevel/form_Portfolio/preview", $.proxy(this.loadPreviewPortfolioDisplay,this),this.cid);
        $.subscribe("/contentLevel/form_Portfolio/print", $.proxy(this.printSetup,this),this.cid);
        $.subscribe("/contentLevel/form_Portfolio/addAllItems", $.proxy(function(){ this.addEditItem(this.options.formUrl) },this) ,this.cid);
        $.subscribe("/contentLevel/form_Portfolio/removeAllItems",  $.proxy(function(){ this.addEditItem(this.options.formUrl) },this) ,this.cid);
        $.subscribe("/contentLevel/form_Portfolio/emailPortfolio",  $.proxy(this.emailPortfolio,this),this.cid);
        $.subscribe("/contentLevel/form_Portfolio/setupNewHeadshot",$.proxy(this.setupNewHeadShot,this),this.cid);
        //
        $.subscribe("/contentLevel/ajaxPopupDisplayModule_previewPortfolio/displayCancel", $.proxy(this.closePopupPreview,this),this.cid);
        //
        $.subscribe("/contentLevel/form_headShotAddUpdate/pageLoaded", $.proxy(this.loadHeadShotPopupModule,this) ,this.cid);
        $.subscribe('/contentLevel/form_headShotAddUpdate/success',$.proxy(this.headShotFormSuccess,this),this.cid);
        //
        $.subscribe('/contentLevel/ajaxPopupFormModule_emailPortfolio/formSuccess',$.proxy(this.emailFormCancel,this),this.cid);
        $.subscribe('/contentLevel/ajaxPopupFormModule_emailPortfolio/formCancel',$.proxy(this.emailFormCancel,this),this.cid);
        //
        $.subscribe('/contentLevel/popup_headShot/save',$.proxy(this.headShotFormSave,this),this.cid);
        $.subscribe('/contentLevel/popup_headShot/close',$.proxy(this.headShotPopupClose,this),this.cid);

    },

    loadGrid:function(){
        $("#gridArea").show();
        $("#landingPage").hide();
        this.el="#masterArea";
        this.views.gridView = new kyt.AssetGridView(this.options);
    },

    loadLandingPage:function(){
        $("#gridArea").hide();
        var landingPageOptions = $.extend({},this.options,{el:"#landingPage"});
        this.views.portfolioLandingPage = new kyt.PortfolioLandingPageView(landingPageOptions);
    },

    //from grid
    addEditItem: function(url,assetType){
        this.options.formUrl = url;
        if(this.views.formView) this.views.formView.remove();

        var formOptions = {
            el: "#detailArea",
            id: "Portfolio",
            url: url,
            data:{"AssetType":assetType}
        };
        $("#masterArea","#contentInner").after("<div id='detailArea'/>");
        this.views.formView = new kyt.PortfolioFormView(formOptions);
        this.views.formView.render();
    },

    //from form
    setupMainForm: function(formOptions){
        $("#masterArea").hide();
        var that = this;
        $.each(formOptions.portfolioBuilderData.assetTypes, function(i,asset){

            var tokenOptions = {
                el:"#"+asset,
                id:asset
            };
            $.extend(true, tokenOptions, formOptions.portfolioBuilderData[asset]);
            that.views[asset+"TokenView"] = new kyt.TokenView(tokenOptions);

            // set up the ordinal input names for submision purposes
            var assetTypeName = $("#"+asset).find("#AssetType").attr("name");
            var newAssName = assetTypeName.replace("~",formOptions.portfolioBuilderData[asset].index);
            $("#"+asset).find("#AssetType").attr("name",newAssName);

            var inputName = $("#"+asset).find("#Input").attr("name");
            var newInputName = inputName.replace("~",formOptions.portfolioBuilderData[asset].index);
            $("#"+asset).find("#Input").attr("name",newInputName);
            $.subscribe("/contentLevel/token_"+ asset +"/addEdit",$.proxy(that.addEditAsset,that),"addEditAsset");
        });
         $("input[name='Item.Name']").focus();
//        $(window).scrollTop($('#headerArea').position().top)
    },
    formSuccess:function(){

        this.formCancel();
        if(!this.views.gridView){
            this.loadGrid();
        }else{
            $("#gridArea").show();
            $("#landingPage").hide();
            this.views.gridView.reloadGrid();
        }
    },

    formCancel: function(){
       this.views.formView.remove();
       $("#masterArea").show();
    },

    resetLandingPageIfEmpty:function(count){
        if(count==0){
            if(!this.views.portfolioLandingPage){
                this.loadLandingPage();
            }else{
                $("#gridArea").hide();
                $("#landingPage").show();
            }
            this.options.portfolioCount=0;
        }
    },

    //from Asset tokneizers
    addEditAsset:function(name){
        $("#masterArea","#contentInner").after("<div id='detailAssetArea'/>");
         var formOptions = {
            el: "#detailAssetArea",
            id: name,
            url: this.options.addEditAssetUrl,
            data:{"AssetType":name}
        };
        this.modules[name+"FormModule"] = new kyt.AssetFormModule(formOptions);

        $.subscribe("/contentLevel/form_"+name+"/pageLoaded", $.proxy(this.hideMasterAndDetail,this), this.cid);
        $.subscribe('/contentLevel/formModule_'+ name +'/moduleSuccess',$.proxy(this.moduleAssetSuccess,this), "");
        $.subscribe('/contentLevel/formModule_'+ name +'/moduleCancel',$.proxy(this.moduleAssetCancel,this), "");
    },

    hideMasterAndDetail:function(){
        $("#masterArea").hide();
        $("#detailArea").hide();
    },

    moduleAssetSuccess:function(result, id){
        this.moduleAssetCancel(id);
        this.views[id+"TokenView"].successHandler(result);
    },

    moduleAssetCancel: function(id){
        $("#detailArea").show();
        this.removeModule(id+"FormModule");
    },

     //display preview stuff
    loadPreviewPortfolioDisplay:function(url){
         $("#masterArea").after("<div id='popupContentDiv'/>");
        var _url;
        if(url){
            _url = url;
        }else{
            var id = $("#EntityId","#detailArea").val();
            _url = this.options.previewPortfolioUrl+"/"+id;
        }
        var displayOptions = {
            el: "#popupContentDiv",
            id: "previewPortfolio",
            url: _url
        };
        this.modules["previewDisplay"] = new kyt.AjaxPopupDisplayModule(displayOptions);
    },

    closePopupPreview:function(){
        this.removeModule("previewDisplay");
    },

    printSetup:function(){
        $.subscribe("/contentLevel/display_printPortfolio/pageLoaded", $.proxy(this.print,this),this.cid);

        $("#masterArea").after("<div id='printContentDiv'/>");

        var id = $("#EntityId","#detailArea").val();
        _url = this.options.previewPortfolioUrl+"/"+id;

        var displayOptions = {
            el: "#printContentDiv",
            id: "printPortfolio",
            url: _url
        };


        this.views[this.id + "print"] = new kyt.AjaxDisplayView(displayOptions);
        this.views[this.id + "print"].render();
    },

    print:function(){
        $("#printContentDiv").jqprint();
        $.unsubscribeByPrefix("/contentLevel/display_printPortfolio");
        this.views[this.id + "print"].remove();
    },

    //email stuff
    emailPortfolio:function(){
        $("#popupContentDiv").remove();
        $("#detailArea").after("<div id='popupContentDiv'/>");
        var id = $("#EntityId","#detailArea").val();

        var formOptions = {
            id: "emailPortfolio",
            el: "#popupContentDiv",
            url: this.options.sendPortfolioFormUrl,
            data:{"EntityId":id},
            crudFormOptions: { errorContainer:"#errorMessagesPU",successContainer:"#errorMessagesForm"}
        };
        this.modules["emailPortfolio"] = new kyt.AjaxPopupFormModule(formOptions);
    },

    emailFormCancel:function(){
        this.modules["emailPortfolio"].destroy();
    },

    emailPortfolioSave:function(){
        this.views[emailPortfolio].saveItem();
    },
    emailPortfolioClose:function(){
        this.views["popupEmailPortfolio"].close();
        this.views["emailPortfolio"].remove();
        this.removeView("popupEmailPortfolio");
        this.removeView("emailPortfolio");
    },

    //Headshot Form Stuff
    setupNewHeadShot:function(){
        $("#detailArea").after("<div id='addNewHeadShotDiv'/>");
        var id = $("#EntityId","#detailArea").val();

        var formOptions = {
            id: "headShotAddUpdate",
            el: "#addNewHeadShotDiv",
            url: this.options.headShotAddUpdateUrl,
            crudFormOptions: { errorContainer:"#errorMessagesPU",successContainer:"#errorMessagesForm"}
        };
        this.views["headShotAddUpdate"] = new kyt.AjaxFormView(formOptions);
        this.views["headShotAddUpdate"].render();
    },

    loadHeadShotPopupModule: function(formOptions){
        var popupOptions = {
            id:"headShot",
            el:"#addNewHeadShotDiv",
            buttons: kyt.popupButtonBuilder.builder("headShot").standardEditButons(),
            title:formOptions.title
        };
        this.views.headShotPopupView = new kyt.PopupView(popupOptions);
    },

    headShotFormSuccess:function(result){
        $("[name='Item.HeadShot.EntityId']").append($("<option></option>").attr("value", result.EntityId).text(result.Name));
        $("[name='Item.HeadShot.EntityId']").val(result.EntityId);
        $("a#FileUrl img").attr("src",result.DocumentUrl);
        this.views.formView.options.headShotDtos.push({Url:result.DocumentUrl,EntityId:result.EntityId});
        this.views.headShotPopupView.remove();
        delete this.views.headShotPopupView;
    },

    // Headshot Popup Stuff
    headShotFormSave:function(dialog){
        this.views["headShotAddUpdate"].saveItem();
    },

    headShotPopupClose:function(){
        this.views["headShotPopupModule"].close();
        this.views["headShotAddUpdate"].remove();
        this.removeView("headShotAddUpdate");
       this.removeView("headShotPopupModule");
    },

    removeView:function(view){
        this.views[view].remove();
        delete this.views[view];
    },

    removeModule:function(module){
        this.modules[module].destroy();
        delete this.modules[module];
    }

});