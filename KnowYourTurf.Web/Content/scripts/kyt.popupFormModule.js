/**
 * Created by JetBrains RubyMine.
 * User: Owner
 * Date: 10/8/11
 * Time: 7:26 PM
 * To change this template use File | Settings | File Templates.
 */
if (typeof kyt == "undefined") {
            var kyt = {};
}


kyt.PopupFormModule = kyt.Module.extend({
    events:_.extend({
    }, kyt.Module.prototype.events),

    initialize:function(){
        $.extend(this,this.defaults());
        this.registerSubscriptions();
        this.views.formView = new kyt.AjaxFormView(this.options);
        this.views.formView.render();
    },

    registerSubscriptions: function(){
        // doesn't need cancel cuz popup cancel will remove both
        $.subscribe("/form_"+ this.id +"/pageLoaded", $.proxy(this.loadPopupView,this),this.cid);
        //
        $.subscribe("/popup_" + this.id + "/save", $.proxy(this.formSave,this), this.cid);
        $.subscribe("/popup_" + this.id + "/cancel", $.proxy(this.formCancel,this), this.cid);

    },
    loadPopupView:function(formOptions){
        var buttons = this.options.buttons? this.options.buttons: kyt.popupButtonBuilder.builder(this.id).standardEditButons();
        var popupOptions = {
            id:this.id,
            el:"#dialogHolder",
            buttons: buttons,
            title:formOptions.title
        };
        this.views["popupView"] = new kyt.PopupView(popupOptions);
        $.publish("/popupFormModule_"+this.id+"/popupLoaded",[formOptions]);
    },

    formSave:function(){
        this.views["formView"].saveItem();
    },
    formCancel:function(){
        $.publish("/form_"+ this.id +"/cancel",[this.id]);
        this.views["popupView"].close();
    }
});