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


kyt.PopupDisplayModule = kyt.Module.extend({
    events:_.extend({
    }, kyt.Module.prototype.events),

    initialize:function(){
        $.extend(this,this.defaults());
        this.registerSubscriptions();
        this.views.displayView = new kyt.AjaxDisplayView(this.options);
        this.views.displayView.render();
    },

    registerSubscriptions: function(){
        // doesn't need cancel cuz popup cancel will remove both
        $.subscribe("/display_"+ this.id +"/pageLoaded", $.proxy(this.loadPopupView,this),this.cid);
        //
//        $.subscribe("/popup_" + this.id + "/cancel", $.proxy(this.formCancel,this), this.cid);

    },
    loadPopupView:function(formOptions){
        var buttons = this.options.buttons? this.options.buttons: kyt.popupButtonBuilder.builder(this.id).standardDisplayButtons();

        var popupOptions = {
            id:this.id,
            el:"#dialogHolder",
            buttons: buttons,
            title:formOptions.title
        };
        this.views["popupView"] = new kyt.PopupView(popupOptions);
    },

//    //from documentPopup
//    formCancel:function(){
//        $.publish("/DisplayModule_"+this.id+"/cancel",[id]);
//        this.views["popupView"].close();
//        this.views["displayView"].remove();
//    }
});