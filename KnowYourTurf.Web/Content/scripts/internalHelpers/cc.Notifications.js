/**
 * Created by JetBrains RubyMine.
 * User: Owner
 * Date: 7/1/12
 * Time: 3:45 PM
 * To change this template use File | Settings | File Templates.
 */
/**
 * Created with JetBrains WebStorm.
 * User: RHarik
 * Date: 4/20/12
 * Time: 8:24 AM
 * To change this template use File | Settings | File Templates.
 */


cc.MessageNotficationService = function(){
    this.areaCache = [];
};

cc.MessageNotficationService.extend = cc.extend;
    $.extend(cc.MessageNotficationService.prototype, {
        processResult:function(result, _area, viewId){
            if(typeof result == "string"){
                result = JSON.parse(result);
            }
            if(result.LoggedOut){//check for something more generic than logged out.
                window.location.replace(result.RedirectUrl);
                return;
            }
            var area = this.retrieveArea(_area);
            return area.processResult(result,viewId);
        },
        addArea: function(area){
            this.areaCache.push(area);
        },
        removeArea: function(areaName){
            DCI.notificationService.areaCache = _.reject(this.areaCache,function(item,idx,list){
                return item.name === areaName;
            },this)
        },
        retrieveArea:function(areaName){
            if(!areaName) {throw Error("No areaName provided");}
            var area = _.find(this.areaCache,function(item){return item.name == areaName;});
            if(!area)throw Error("Notification area not found");
            return area;

        },
        resetArea:function(areaName){
            var area = this.retrieveArea(areaName);
            if(area){
                area.resetArea();
            }
        }
    });

cc.NotificationArea = function(_name,_successContainer,_errorContainer, _ventHandler){
    this.name = _name;
    this.errorContainer = _errorContainer;
    this.successContainer = _successContainer;
    this.ventHandler = _ventHandler;

    this.handleData = function(result,viewId){
        if(result.Success && result.Redirect) {
            if (result.RedirectUrl) {
                window.location.href = result.RedirectUrl;
            } else if(ReturnUrl) {
                window.location.href = ReturnUrl;
            }
            return false;
        }
        var messageType = result.Success?"success":"error";
        if(result.Message){
            this.messageHandler.addMessage(messageType,result.Message,"");
        }
        _.each(result.Errors||[],function(item){
            this.messageHandler.addMessage("error",item.ErrorMessage,item.PropertyName);
        },this);
        if(result.Success){
            this.messageHandler.showAllMessages(this.$successContainer,"valid_field",true);
        }else{
            this.messageHandler.showAllMessages(this.$errorContainer,"invalid_field");
        }
        if(this.ventHandler){
            this.ventHandler.trigger(this.name+":"+viewId+":"+messageType,result);
        }
        return messageType;
    };

};
cc.NotificationArea.extend = cc.extend;

$.extend(cc.NotificationArea.prototype, {

    render:function(formEl)
    {
        this.$errorContainer = $(this.errorContainer,formEl);
        this.$successContainer = $(this.successContainer);
        this.$successContainer.hide().find("ul").removeClass();
        this.$errorContainer.hide().find("ul").removeClass();
        this.messageHandler = new cc.NotificationMessageHandler();
    },

    areaName: function(){return this.name},
    processResult:function(result,viewId){ return this.handleData(result,viewId); },
    getSuccessContainer:function(){ return this.$successContainer; },
    getErrorContainer:function(){ return this.$errorContainer; },
    resetArea:function(){
        this.$successContainer.hide().find("ul").removeClass();
        this.$errorContainer.hide().find("ul").removeClass();
        this.messageHandler.resetHandler();
    }
});
cc.NotificationMessageHandler = function(){
    this.messages = [];
    this.hasMessages = function(){return this.messages.length>0; };
};

cc.NotificationMessageHandler.extend = cc.extend;
    $.extend(cc.NotificationMessageHandler.prototype, {
        addMessage:function(type, message, field){
            if(_.any(this.messages,function(item){
                return item.getMessage()==message;
            })){return false;}
            this.messages.push(new cc.Message(type,message,field));
        },
        createHtmlForAllMessages: function() {
            var returnHtml = "";
            if (!this.hasMessages()) {
                return returnHtml;
            }
            _.each(this.messages,function(item) { returnHtml += this.createHtmlForMessage(item); },this);
            return returnHtml;
        },
        createHtmlForMessage:function(item){
            var cssClass;
            if(item.getMessageType() == "success") cssClass="valid_field";
            if(item.getMessageType() == "error") cssClass="invalid_field";
            var returnHtml = "<li>";
            returnHtml += '<label for="'+item.getField()+'" generated="true" class="'+cssClass+'" style="display: block;">' + item.getMessage() + '</label>';
            returnHtml += "</li>";
            return returnHtml;
        },
        showMessage:function(selector, message, cssClass){ // for ul
            selector.find("ul").removeClass();
            if(!selector.isVisible()){selector.show();}
            selector.find("ul").addClass(cssClass).html(this.createHtmlForMessage(message));
        },
        showAllMessages: function(selector, cssClass, fadeOut) {
            if (this.hasMessages()) {
                var $ul = selector.find("ul");
                $ul.addClass(cssClass).html(this.createHtmlForAllMessages());
                selector.show();
                if(fadeOut){
                    $ul.show().delay(3000).fadeOut(2000,function(){$ul.find("li").remove();});
                }else{
                    $ul.show();
                }
            } else {
                selector.hide().find("ul").removeClass();
            }
//            if(_popup){
//                $(selector).dialog({
//                    autoOpen: false,
//                    modal: true,
//                    width: 500,
//                    buttons: {
//                        "Ok": function() {
//                            $(this).dialog("close");
//                            $("ul", this).html("");
//                            $(".ui-dialog").remove();
//                        }
//                    }
//                });
//                $(selector).dialog('open');
//            }
        },
        resetHandler: function() {
            this.messages = [];
        }
    });

cc.Message = function(type, message, field) {
    this.messageType = type;
    this._message = message;
    this._field = field;
};
cc.Message.extend = cc.extend;
$.extend(cc.Message.prototype, {
    getMessage:function(){return this._message;},
    getMessageType:function(){return this.messageType;},
    getField:function(){return this._field;}
});


