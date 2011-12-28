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

kyt.BaseFormView = Backbone.View.extend({
    events:{
        'click #save' : 'saveItem',
        'click .cancel' : 'cancel'
    },

    initialize: function(){
       this.options = $.extend({},kyt.formDefaults,this.options);
       this.id=this.options.id;
    },
    config:function(){
        if(extraFormOptions){
            $.extend(true, this.options, extraFormOptions);
        }

        this.options.crudFormOptions.successHandler = $.proxy(this.successHandler,this);
        $(this.options.crudFormSelector,this.el).crudForm(this.options.crudFormOptions);
        if(this.options.crudFormOptions.additionBeforeSubmitFunc){
            var array = !$.isArray(this.options.crudFormOptions.additionBeforeSubmitFunc) ? [this.options.crudFormOptions.additionBeforeSubmitFunc] : this.options.crudFormOptions.additionBeforeSubmitFunc;
            $(array).each($.proxy(function(i,item){
                $(this.options.crudFormSelector,this.el).data('crudForm').setBeforeSubmitFuncs(item);
            },this));
        }
        if(typeof this.options.runAfterRenderFunction == 'function'){
            this.options.runAfterRenderFunction.apply(this,[this.el]);
        }
        $(".rte").cleditor();
    },
    saveItem:function(){
        $(this.options.crudFormSelector,this.el).submit();
    },
    cancel:function(){
        $.publish("/contentLevel/form_"+this.id+"/cancel",[this.id]);
    },
    successHandler:function(result, form, notification){
        var emh = cc.utilities.messageHandling.messageHandler();
        var message = cc.utilities.messageHandling.mhMessage("success",result.Message,"");
        emh.addMessage(message);
        emh.showAllMessages(notification.getSuccessContainer());
        $.publish("/contentLevel/form_"+this.id+"/success",[result,form,this.id]);
    }
});

kyt.FormView = kyt.BaseFormView.extend({
    initialize: function(){
        this.options = $.extend({},kyt.formDefaults,this.options);
        this.id=this.options.id;
        this.config();
        this.render();
    },
    render: function(){
        $.publish("/contentLevel/form_"+this.id+"/pageLoaded",[this.options]);
        return this;
    }
});
kyt.AjaxFormView = kyt.BaseFormView.extend({
    render:function(){
        kyt.repository.ajaxGet(this.options.url, this.options.data, $.proxy(function(result){this.renderCallback(result)},this));
    },
    renderCallback:function(result){
        if(result.LoggedOut){
            window.location.replace(result.RedirectUrl);
            return;
        }
        $(this.el).html(result);
        this.config();
        //callback for render
        this.viewLoaded();

        //general notification of pageloaded
        $.publish("/contentLevel/form_"+this.id+"/pageLoaded",[this.options]);
    }
});


kyt.AssetFormView = kyt.AjaxFormView.extend({
    events:_.extend({
        'click .delete':'deleteItem',
        'click .print':'print'
    }, kyt.AjaxFormView.prototype.events),
    deleteItem:function(){
        if (confirm("Are you sure you would like to delete this Item?")) {
            var entityId = $(this.el).find("#EntityId").val();
            kyt.repository.ajaxGet(this.options.deleteUrl,{"EntityId":entityId}, $.proxy(function(result){
            $.publish("/contentLevel/form_"+this.id+"/success",[result])
            },this));
         }
    },
    print:function(){
        $(this.el).jqprint();
    }
});

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