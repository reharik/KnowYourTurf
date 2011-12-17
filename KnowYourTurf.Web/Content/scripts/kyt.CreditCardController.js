/**
 * Created by JetBrains RubyMine.
 * User: RHarik
 * Date: 11/17/11
 * Time: 8:35 AM
 * To change this template use File | Settings | File Templates.
 */
if (typeof kyt == "undefined") {
            var kyt = {};
}


kyt.CreditCardController = kyt.Controller.extend({
    events:_.extend({
    }, kyt.Controller.prototype.events),

    initialize:function(options){
        $.extend(this,this.defaults());
        $.unsubscribeByPrefix("/contentLevel");
        this.id="creditCardController";
        this.registerSubscriptions();

        var _options = $.extend({},this.options, options);
        _options.el="#masterArea";
        _options.id="creditCardView";
        this.views.creditCardView = new kyt.CreditCardView(_options);

    },
    registerSubscriptions:function(){
        $.subscribe('/contentLevel/form_creditCardView/csvHelp',$.proxy(this.csvHelp,this), this.cid);
    },

    csvHelp:function(formOptions){
        $("#masterArea",".content-outer").after("<div id='popupContentDiv'/>");
        $("#popupContentDiv").html("<img src='/content/images/cvv.jpg' />");

        var builder = kyt.popupButtonBuilder.builder(this.options.id);
        var buttons = builder.addButton("Ok", builder.getCancelFunc()).getButtons();
        var popupOptions = {
            id:this.id,
            el:"#popupContentDiv",
            buttons: buttons,
            width:600,
            title:"Security Code (CSV) Help"
        };
        this.views[this.id + "Popup"] = new kyt.PopupView(popupOptions);

    }
});
