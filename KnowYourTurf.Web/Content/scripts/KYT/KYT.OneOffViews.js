/**
 * Created by JetBrains RubyMine.
 * User: RHarik
 * Date: 7/2/12
 * Time: 8:30 AM
 * To change this template use File | Settings | File Templates.
 */

KYT.Views.ResetPasswordView = KYT.Views.BaseFormView.extend({
    render: function(){
        KYT.notificationService = new cc.MessageNotficationService();
        this.config();
        if(this.setBindings){this.setBindings();}
        $("input[name='Password']").focus();
        this.viewLoaded();
        KYT.vent.trigger("form:"+this.id+":pageLoaded",this.options);
        return this;
    }
});

KYT.Views.LoginView = KYT.Views.BaseFormView.extend({

    events: _.extend({
        "click #forgotPasswordLink": "forgotPasswordClick",
        "click #registrationLink": "registrationLinkClick",
        "click .save": "submitClick"
    }, KYT.Views.BaseFormView.prototype.events),

    render: function () {
        this.config();
        if (this.setBindings) { this.setBindings(); }
        $("input[name='UserName']").focus();
        this.viewLoaded();
        KYT.vent.trigger("form:" + this.id + ":pageLoaded", this.options);
        return this;
    },

    forgotPasswordClick: function (e) {
        e.preventDefault();
        var popupOptions = {
            url: this.options.forgotPasswordUrl,
            popupTitle: this.options.popupTitle
        };
        var forgottenPasswordView = new KYT.Views.AjaxPopupFormModule(popupOptions);
        forgottenPasswordView.render();
    },

    registrationLinkClick: function (e) {
        window.location.href = "/Registration/#newregistration";
    },

    submitClick: function (e) {
        return false;
    }

});