/**
 * Created by JetBrains RubyMine.
 * User: RHarik
 * Date: 11/15/11
 * Time: 3:23 PM
 * To change this template use File | Settings | File Templates.
 */

if (typeof kyt == "undefined") {
            var kyt = {};
}

kyt.CreditCardView =  kyt.FormView.extend({
    events:_.extend({
        "click #csvHelp": "csvHelp",
        "change #cc_type":"ccTypeChange"
    }, kyt.FormView.prototype.events),

    config:function(){
        if(extraFormOptions){
            $.extend(true, this.options, extraFormOptions);
        }

        var notification = cc.utilities.messageHandling.notificationResult();
        notification.setSuccessHandler($.proxy(this.successHandler,this));
        notification.setMessageHandler(cc.utilities.messageHandling.messageHandler(true));
        this.options.crudFormOptions.notification = notification;

        $(this.options.crudFormSelector,this.el).crudForm(this.options.crudFormOptions);

        if(this.options.crudFormOptions.additionBeforeSubmitFunc){
            var array = !$.isArray(this.options.crudFormOptions.additionBeforeSubmitFunc) ? [this.options.crudFormOptions.additionBeforeSubmitFunc] : this.options.crudFormOptions.additionBeforeSubmitFunc;
            $(array).each($.proxy(function(i,item){
                $(this.options.crudFormSelector,this.el).data('crudForm').setBeforeSubmitFuncs(item);
            },this));
        }
        $(".rte").cleditor();
    },

    render: function(){
        $("#divContainer").centerInClient();
        $("input.phone").mask("(999) 999-9999");
        $("input.zipcode").mask("99999");
        $("#creditcard").mask("9999 9999 9999 9999");
    },
    csvHelp:function(){
        $.publish("/contentLevel/form_"+this.id+"/csvHelp",[this.options]);
    },
    ccTypeChange:function(e) {
        if($(e.target).val()== 'amex'){
            $("#creditcard").unmask().mask("9999 999999 99999");
        }else{
            $("#creditcard").unmask().mask("9999 9999 9999 9999");
        }
    },
    successHandler:function(result, form, notification){
        var emh = cc.utilities.messageHandling.messageHandler(true);
        emh.resetHandler();
        var message = cc.utilities.messageHandling.mhMessage("success",result.Message,"");
        emh.addMessage(message);
        emh.showAllMessages(notification.getSuccessContainer());
        $.publish("/contentLevel/form_"+this.id+"/success",[result,form]);
    }

});
