/**
 * Created by JetBrains RubyMine.
 * User: RHarik
 * Date: 10/10/11
 * Time: 10:08 AM
 * To change this template use File | Settings | File Templates.
 */


if (typeof kyt == "undefined") {
            var kyt = {};
}

kyt.AjaxPopupFormModule  = kyt.Module.extend({
    events:_.extend({
    }, kyt.Controller.prototype.events),

    initialize:function(){
        $.extend(this,this.defaults());
        this.registerSubscriptions();
        if(!this.options.crudFormOptions)this.options.crudFormOptions={};
        this.options.crudFormOptions.errorContainer = "#errorMessagesPU";
        this.views[this.id + "Form"] = new kyt.AjaxFormView(this.options);
        this.views[this.id + "Form"].render();
    },

    registerSubscriptions: function(){
        $.subscribe("/contentLevel/form_"+ this.id +"/pageLoaded", $.proxy(this.loadPopupView,this),this.cid);
        $.subscribe("/contentLevel/form_"+ this.id + "/success", $.proxy(this.formSuccess,this), this.cid);
        //
        $.subscribe("/contentLevel/popup_" + this.id + "/save", $.proxy(this.formSave,this), this.cid);
        $.subscribe("/contentLevel/popup_" + this.id + "/cancel", $.proxy(this.formCancel,this), this.cid);
    },

    loadPopupView:function(formOptions){
        var buttons = this.options.buttons?this.options.buttons:kyt.popupButtonBuilder.builder(this.id).standardEditButons();
        var popupOptions = {
            id:this.id,
            el:this.el,
            buttons: buttons,
            title:formOptions.title
        };
        this.views[this.id + "Popup"] = new kyt.PopupView(popupOptions);
    },
    formSuccess:function(result){
        $.publish("/contentLevel/ajaxPopupFormModule_" + this.id + "/formSuccess",[result,this.id]);
    },

    formSave:function(){
        this.views[this.id + "Form"].saveItem();
    },

    formCancel:function(){
        $.publish("/contentLevel/ajaxPopupFormModule_" + this.id + "/formCancel",[this.id]);
    }
});