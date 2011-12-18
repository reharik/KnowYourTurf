/**
 * Created by .
 * User: RHarik
 * Date: 7/28/11
 * Time: 2:52 PM
 * To change this template use File | Settings | File Templates.
 */

if (typeof kyt == "undefined") {
            var kyt = {};
}

if (typeof kyt.userProfile== "undefined") {
            kyt.userProfile= {};
}

kyt.UserProfileController = kyt.Controller.extend({
    events:_.extend({
    }, kyt.Controller.prototype.events),

    initialize:function(){
        $.extend(this,this.defaults());

        kyt.contentLevelControllers["userProfileController"]=this;
        $.unsubscribeByPrefix("/contentLevel");
        this.registerSubscriptions();

        var formOptions = {};
        formOptions.crudFormOptions = {};
        formOptions.crudFormOptions.additionBeforeSubmitFunc = this.options.saveRepeatableItems;
        formOptions.crudFormOptions.successContainer ="#errorMessagesForm";
        formOptions.el = "#masterArea";
        formOptions.id = "userProfile";
        formOptions.url = this.options.userProfileUrl;
        this.views["form"] = new kyt.UserProfileView(formOptions);
        this.views["form"].render();
    },
    registerSubscriptions: function(){
        //$.subscribe("/contentLevel/form_userProfile/pageLoaded", that.delegateEvents ,this.id);
        $.subscribe("/contentLevel/form_userProfile/cancel", this.formCancel,this.cid);
        $.subscribe("/contentLevel/form_userProfile/success",this.formSuccess,this.cid);
        $.subscribe("/contentLevel/userProfileView/pageLoaded", $.proxy(this.loadRepeaters,this),this.cid);
    },

    loadRepeaters:function(data){


    },

    formCancel:function(){
        $.publish("/contentLevel/form_userProfile/cancel",[]);
    },

    formSuccess:function(){
        $.publish("/contentLevel/form_userProfile/success",[]);
    },
    emailConfromation: function(url, data){
        kyt.repository.ajaxGet(url,data, $.proxy(this.confirmEmailCallback,this));
    },
    confirmEmailCallback: function(result){
        var dialog = $("<div></div>").attr("id","dialogHolder");
        $(".kyt_dyn").append(dialog);
        dialog.dialog({
            autoOpen: false,
            modal: true,
            width: 550,
            buttons: {
                Close: function() {
                    window.location.href = returnUrl;
                }
            },
            close: function(event, ui) {
              window.location.href = returnUrl;
            }
        });
        $(dialog).html(result);
        $(dialog).dialog("open");
        $(dialog).dialog("option", "title", titlePopup);
    }
});
