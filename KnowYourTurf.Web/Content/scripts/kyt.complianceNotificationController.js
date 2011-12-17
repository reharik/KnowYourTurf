/**
 * Created by .
 * User: RHarik
 * Date: 9/1/11
 * Time: 3:03 PM
 * To change this template use File | Settings | File Templates.
 */

if (typeof kyt == "undefined") {
            var kyt = {};
}

kyt.ComplianceNotificationController  = kyt.Controller.extend({
    events:_.extend({
    }, kyt.Controller.prototype.events),

    initialize:function(){
        $.extend(this,this.defaults());

        kyt.contentLevelControllers["complianceNotificationController"]=this;
        $.unsubscribeByPrefix("/contentLevel");
        this.registerSubscriptions();

        var formOptions = {
            el: "#masterArea",
            id: "complianceSettings"
        };
        this.views.formView = new kyt.ComplianceItemsFormView(formOptions);
    },
    registerSubscriptions:function(){
        $.subscribe("/contentLevel/form_complianceSettings/pageLoaded", $.proxy(this.formLoaded,this));
    },
    formLoaded:function(){
        var emailTokenOptions = {
            el: "#emailTokenContainer",
            id: "emailToken",
            inputSelector:$(this.options.emailOptions.inputSelector),
            availableItems:this.options.emailOptions.availableItems,
            selectedItems:this.options.emailOptions.selectedItems
         };
        this.views.emailTokenView = new kyt.TokenView(emailTokenOptions);


        var complianceItemTokenOptions = {
            el: "#complianceItemTokenContainer",
            id: "complianceItem",
            inputSelector:$(this.options.ciOptions.inputSelector),
            availableItems:this.options.ciOptions.availableItems,
            selectedItems:this.options.ciOptions.selectedItems
         };
        this.views.complianceItemTokenView = new kyt.TokenView(complianceItemTokenOptions);
    }
});


//
//kyt.complianceNotificationController = function(container, options){
//    var _container = $(container);
//    var myOptions = $.extend({}, kyt.gridDetailDefaults, options || {});
//    var modules = {};
//
//    return{
//        init: function(){
//            var that = this;
//            kyt.contentLevelControllers["complianceNotificationController"]=that;
//            var form = kyt.formModule("#masterArea",{notAjax:true});
//            var cnm = kyt.complianceNotificationModule("#masterArea");
//            var email = kyt.tokenModule("#emailTokenContainer",emailOptions);
//            var complianceItems = kyt.tokenModule("#complianceItemsTokenContainer",ciOptions);
//            modules.formModule= form;
//            modules.complianceModule= cnm;
//            modules.emailTM= email;
//            modules.complianceItemsTM= complianceItems;
//            $.each(modules,function(i,item){
//               item.init();
//            });
//        },
//        destroy:function(){
//            $.each(modules,function(item,value){
//               if(value){
//                   that.removeModule(item);
//               }
//            });
//            _container.empty();
//        },
//        addFormModule: function(module){
//            that.modules.formModule=module;
//        }
//    }
//};
//
//kyt.gridDetailDefaults = {
//};
