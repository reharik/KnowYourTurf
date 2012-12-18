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

kyt.AjaxPopupDisplayModule  = kyt.Module.extend({
    events:_.extend({
    }, kyt.Controller.prototype.events),

    initialize:function(){
        $.extend(this,this.defaults());
        this.registerSubscriptions();
        this.views[this.id + "Form"] = this.options.view ? this.options.view : new kyt.AjaxDisplayView(this.options);
        this.views[this.id + "Form"] .render();
    },

    registerSubscriptions: function(){
        $.subscribe("/contentLevel/display_"+ this.id +"/pageLoaded", $.proxy(this.loadPopupView,this),this.cid);
        $.subscribe("/contentLevel/display_"+ this.id + "/success", $.proxy(this.formSuccess,this), this.cid);
        //
//        $.subscribe("/contentLevel/popup_" + this.id + "/save", $.proxy(this.formSave,this), this.cid);
        $.subscribe("/contentLevel/popup_" + this.id + "/cancel", $.proxy(this.formCancel,this), this.cid);
    },

    loadPopupView:function(formOptions){
        var buttons = this.options.buttons?this.options.buttons:kyt.popupButtonBuilder.builder(this.options.id).standardDisplayButtons();
        var popupOptions = {
            id:this.id,
            el:this.el,
            buttons: buttons,
            title:formOptions.title
        };
        this.views[this.id + "Popup"] = new kyt.PopupView(popupOptions);
    },

    formCancel:function(){
        $.publish("/contentLevel/ajaxPopupDisplayModule_" + this.id + "/displayCancel",[this.id]);
        this.views[this.id + "Form"].remove();
    }
});