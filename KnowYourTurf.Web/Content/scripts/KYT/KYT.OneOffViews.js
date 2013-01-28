/**
 * Created by JetBrains RubyMine.
 * User: RHarik
 * Date: 7/2/12
 * Time: 8:30 AM
 * To change this template use File | Settings | File Templates.
 */

KYT.Views.ResetPasswordView = KYT.Views.View.extend({
    initialize: function(){
        KYT.mixin(this, "baseFormView");
    },
    render: function(){
//        KYT.notificationService = new cc.MessageNotficationService();
        this.bindModelAndElements();
        if(this.setBindings){this.setBindings();}
        $("input[name='Password']").focus();
        this.viewLoaded();
        KYT.vent.trigger("form:"+this.id+":pageLoaded",this.options);
        return this;
    }
});

KYT.Views.LoginView = KYT.Views.View.extend({
    registerEvents: function(){ this.events ={
        "click #forgotPasswordLink": "forgotPasswordClick",
        "click .save": "submitClick"
    }},
    initialize: function(){
        var that = this;
        KYT.mixin(this, "formMixin");
        KYT.mixin(this, "modelAndElementsMixin");
        this.registerEvents();
        this.$el.delegate(this.$el,"keypress",function(e){
            if(e.keyCode==13){
                $(".save").focus();
                that.submitClick();
            }
        });
    },
    render: function () {
        this.bindModelAndElements();
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

    submitClick: function (e) {
        var isValid = CC.ValidationRunner.runViewModel(this.cid, this.elementsViewmodel);
        if(!isValid){return;}
        var data = JSON.stringify(ko.mapping.toJS(this.model));
        var promise = KYT.repository.ajaxPostModel(this.model._saveUrl(),data);
        promise.done($.proxy(this.success,this));
    },
    success:function(result){
        if(result.Success){
            window.location.href = result.RedirectUrl;
        }else{
            if(result.Message && !$.noty.getByViewIdAndElementId(this.cid)){
                $(this.errorSelector).noty({type: "error", text: result.Message, viewId:this.cid});
            }
            if(result.Errors && !$.noty.getByViewIdAndElementId(this.cid)){
                _.each(result.Errors,function(item){
                    $(this.errorSelector).noty({type: "error", text:item.ErrorMessage, viewId:this.cid});
                })
            }
        }
    }

});