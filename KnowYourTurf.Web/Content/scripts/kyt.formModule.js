/**
 * Created by .
 * User: RHarik
 * Date: 9/1/11
 * Time: 1:33 PM
 * To change this template use File | Settings | File Templates.
 */


if (typeof kyt == "undefined") {
            var kyt = {};
}

kyt.formModule = function(container, options){
    var _container = container;
    var myOptions = $.extend({}, kyt.formDefaults, options || {});
    var modules = {};
    return{
        init: function(){
            var that = this;
            if(myOptions.notAjax){
                if(extraFormOptions){
                    $.extend(myOptions, extraFormOptions);
                }
                if(typeof myOptions.runAfterRenderFunction == 'function'){
                    myOptions.runAfterRenderFunction.apply(this,[_container]);
                }
                this.delegateEvents();
            }else{
                $.subscribe("/form"+myOptions.name+"/loadCallback",$.proxy(this.loadFormCallback,that),"fCont");
                $.subscribe("/form"+myOptions.name+"/loadCallback",$.proxy(this.delegateEvents,that),"fCont");
            }
        },
        destroy:function(){
            $.unsubscribeByHandle("fCont");
            for(var mod in modules){
                mod.destroy();
            }
            $(_container).empty();
        },
        delegateEvents: function(){
            var that = this;
            myOptions.crudFormOptions.successHandler = that.successHandler;
            $(myOptions.crudFormSelector,_container).crudForm(myOptions.crudFormOptions);
            if(myOptions.crudFormOptions.additionBeforeSubmitFunc){
                var array = !$.isArray(myOptions.crudFormOptions.additionBeforeSubmitFunc) ? [myOptions.crudFormOptions.additionBeforeSubmitFunc] : myOptions.crudFormOptions.additionBeforeSubmitFunc;
                $(array).each(function(i,item){
                    $(myOptions.crudFormSelector,_container).data('crudForm').setBeforeSubmitFuncs(item);
                });
            }
            $("#save",_container).click(that.saveItem);
            $(".cancel",_container).click(that.cancel);
            $(".delete",_container).click(that.deleteItem);
            $(".print",_container).click(that.print);
            $(myOptions.crudFormSelector,_container).crudForm(myOptions.crudFormOptions);
        },
        loadFormCall:function(url){
            $.get(url, function(result){
                $.publish("/form"+myOptions.name+"/loadCallback",[result])});
        },
        loadFormCallback:function(result){
            if(result.LoggedOut){
                window.location.replace(result.RedirectUrl);
                return;
            }
            $(_container).html(result);
            if(extraFormOptions){
                 $.extend(myOptions, extraFormOptions);
            }
            if(typeof myOptions.runAfterRenderFunction == 'function'){
                myOptions.runAfterRenderFunction.apply(this,[_container]);
            }
            $.publish("/form"+myOptions.name+"/pageLoaded",[])
        },
        saveItem:function(){
            $(_container).find(myOptions.crudFormSelector).submit();
        },
        cancel:function(){
            $.publish("/form"+myOptions.name+"/cancel",[myOptions.name]);
        },
        deleteItem:function(){
            var entityId = $(_container).find("#EntityId");
            $.publish("/form"+myOptions.name+"/delete",[$(_container),entityId]);
        },
        print:function(){
            var entityId = $(_container).find("#EntityId");
            $.publish("/form"+myOptions.name+"/print",[$(_container,entityId)]);
        },
        successHandler:function(result, form, notification){
            var emh = cc.utilities.messageHandling.messageHandler();
            var message = cc.utilities.messageHandling.mhMessage("success",result.Message,"");
            emh.addMessage(message);
            emh.showAllMessages(notification.getSuccessContainer());
            $.publish("/form"+myOptions.name+"/success",[result,form]);
        },
        addModule:function(name, module){
            modules[name] =module;
        }

    }
};

kyt.formDefaults = {
    name:"",
    crudFormSelector:"#CRUDForm",
    events:{
    },
    crudFormOptions:{
        errorContainer:"#puErrorMessages",
        successContainer:"#puErrorMessages",
        additionBeforeSubmitFunc:null
    },
    runAfterRenderFunction: null
};