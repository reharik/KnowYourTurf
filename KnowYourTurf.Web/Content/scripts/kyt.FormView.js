/**
 * Created by JetBrains RubyMine.
 * User: RHarik
 * Date: 10/7/11
 * Time: 7:44 AM
 * To change this template use File | Settings | File Templates.
 */

if (typeof kyt == "undefined") {
    var kyt = {};
}

kyt.FormView = Backbone.View.extend({
    events:{
        'click #save' : 'saveItem',
        'click .cancel' : 'cancel'
    },
    initialize: function(){
        this.options = $.extend({},kyt.formDefaults,this.options);
        this.id=this.options.id;

        if(extraFormOptions){
            $.extend(true, this.options, extraFormOptions);
        }

        this.options.crudFormOptions.successHandler = this.successHandler;
        $(this.options.crudFormSelector,this.el).crudForm(this.options.crudFormOptions);
        if(this.options.crudFormOptions.additionBeforeSubmitFunc){
            var array = !$.isArray(this.options.crudFormOptions.additionBeforeSubmitFunc) ? [this.options.crudFormOptions.additionBeforeSubmitFunc] : this.options.crudFormOptions.additionBeforeSubmitFunc;
            $(array).each(function(i,item){
                $(this.options.crudFormSelector,this.el).data('crudForm').setBeforeSubmitFuncs(item);
            });
        }
        this.render();

    },
    render: function(){
        $.publish("/form_"+this.id+"/pageLoaded",[this.options]);
        return this;
    },
    saveItem:function(){
        $(this.options.crudFormSelector,this.el).submit();
    },
    cancel:function(){
        $.publish("/form_"+this.id+"/cancel",[this.id]);
    },
    successHandler:function(result, form, notification){
        var emh = cc.utilities.messageHandling.messageHandler();
        var message = cc.utilities.messageHandling.mhMessage("success",result.Message,"");
        emh.addMessage(message);
        emh.showAllMessages(notification.getSuccessContainer());
        $.publish("/form_"+this.id+"/success",[result,form,this.id]);
    }
});

kyt.AjaxFormView = Backbone.View.extend({
    events:{
        'click #save' : 'saveItem',
        'click .cancel' : 'cancel'
    },
    initialize: function(){
        this.options = $.extend({},kyt.formDefaults,this.options);
        this.id=this.options.id;
    },
    render:function(){
        kyt.repository.ajaxGet(this.options.url, this.options.data, $.proxy(this.renderCallback,this));
    },
    renderCallback:function(result){
        if(result.LoggedOut){
            window.location.replace(result.RedirectUrl);
            return;
        }
        $(this.el).html(result);
        if(extraFormOptions){
            this.options = $.extend(true, this.options, extraFormOptions);
        }

        if(typeof this.options.runAfterRenderFunction == 'function'){
            this.options.runAfterRenderFunction(this.el);
        }

        this.options.crudFormOptions.successHandler = $.proxy(this.successHandler,this);
        $(this.options.crudFormSelector,this.el).crudForm(this.options.crudFormOptions);
        if(this.options.crudFormOptions.additionBeforeSubmitFunc){
            var array = !$.isArray(this.options.crudFormOptions.additionBeforeSubmitFunc) ? [this.options.crudFormOptions.additionBeforeSubmitFunc] : this.options.crudFormOptions.additionBeforeSubmitFunc;
            $(array).each(function(i,item){
                $(this.options.crudFormSelector,this.el).data('crudForm').setBeforeSubmitFuncs(item);
            });
        }
        $.publish("/form_"+this.id+"/pageLoaded",[this.options]);
        this.viewLoaded();
    },
    viewLoaded:function(){},
    saveItem:function(){
        $(this.options.crudFormSelector,this.el).submit();
    },
    cancel:function(){
        $.publish("/form_"+this.id+"/cancel",[this.id]);
    },
    successHandler:function(result, form, notification){
        var emh = kyt.utilities.messageHandling.messageHandler();
        var message = kyt.utilities.messageHandling.mhMessage("success",result.Message,"");
        emh.addMessage(message);
        emh.showAllMessages(notification.getSuccessContainer());
        $.publish("/form_"+this.id+"/success",[result,form,this.id]);
    }
});
//
//kyt.AjaxFormWithColorPicker = kyt.AjaxFormView.extend({
//    events:_.extend({
//    }, kyt.AjaxFormView.prototype.events),
//    viewLoaded:function(){
//        $('#colorPicker').farbtastic('#FieldColor');
//    }
//});

kyt.formDefaults = {
    id:"",
    data:{},
    crudFormSelector:"#CRUDForm",
    crudFormOptions:{
        errorContainer:"#errorMessagesForm",
        successContainer:"#errorMessagesGrid",
        additionBeforeSubmitFunc:null
    },
    runAfterRenderFunction: null
};