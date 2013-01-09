/**
 * kyt.forgottenPasswordController
 * Created by .
 * User: PTheron
 * Date: 9/27/11
 * Time: 3:03 PM
 * Handle forgotten-password
 *
 */

if (typeof kyt == "undefined") {
            var kyt = {};
}
kyt.ForgottenPasswordController   = kyt.Controller.extend({
    events:_.extend({
    }, kyt.Controller.prototype.events),

    initialize:function(){
        $.extend(this,this.defaults());

        kyt.contentLevelControllers["forgottenPasswordController"]=this;
        $.unsubscribeByPrefix("/contentLevel");
        this.registerSubscriptions();

        var formOptions = {
            id:"forgottenPassword",
            el: "#popupContentDiv",
            url: this.options.forgottenPasswordUrl,
            crudFormOptions: { errorContainer:"#errorMessagesPU",successContainer:"#errorMessagesForm"}
        };
        $("#outer-wrapper").after("<div id='popupContentDiv'/>");
        this.modules["forgottenPasswordajaxPopup"] = new kyt.AjaxPopupFormModule(formOptions);
    },
    registerSubscriptions: function(){
        $.subscribe("/contentLevel/popup_forgottenPassword/save", $.proxy(this.formCancel,this), this.cid);
        $.subscribe("/contentLevel/form_forgottenPassword/success", $.proxy(this.formSuccess,this), this.cid);
    },
    formCancel:function(){
        this.modules["forgottenPasswordajaxPopup"].destroy();
    }
});

kyt.forgottenPasswordControllerDefaults = {
    crudFormOptions: { errorContainer:"#errorMessagesPU" , successContainer:"#errorMessagesForm"}
};