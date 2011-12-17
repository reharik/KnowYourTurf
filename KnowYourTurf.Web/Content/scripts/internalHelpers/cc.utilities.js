if (typeof cc == "undefined") {
            var cc = {};
}

cc.namespace = function() {
    var a = arguments, o = null, i, j, d;
    for (i = 0; i < a.length; i = i + 1) {
        d = a[i].split(".");
        o = window;
        for (j = 0; j < d.length; j = j + 1) {
            o[d[j]] = o[d[j]] || {};
            o = o[d[j]];
        }
    }
    return o;
};

cc.object = function(o){
    function F(){}
    F.prototype = o;
    return new F();
};

cc.namespace("cc.utilities");

cc.utilities.messageHandling =  {

    mhMessage: function(type, message, field) {
        var messageType = type;
        var _message = message;
        var _field = field;
        return {
            getField: function(){
                return _field;
            },
            setField: function(newField){
                _field = newField;
            },
            getMessageType: function() {
                return messageType;
            },
            setMessageType: function(newMessageType) {
                messageType = newMessageType;
            },
            getMessage: function() {
                return _message;
            },
            setMessage: function(newMessage) {
                _message = newMessage;
            }
        }
    },

    notificationResult: function(){
        var messageHandler = cc.utilities.messageHandling.messageHandler(false);
        messageHandler.resetHandler();
        
        var errorContainer = '#errorMessagesForm';
        var successContainer = '#errorMessagesForm';
        var successHandler = function(data, callingForm, notificaiton){
             if(data.Redirect) {
                 if (data.RedirectUrl) {
                     window.location.href = data.RedirectUrl;
                 } else if(ReturnUrl) {
                     window.location.href = ReturnUrl;
                 }
             } else if(data.Message){

                    messageHandler.setMessageType = "success";
                     var message = cc.utilities.messageHandling.mhMessage("success",data.Message,"");
                     messageHandler.addMessage(message);
                     messageHandler.showAllMessages(successContainer,true);
             }
        };
        var errorHandler = function(data){
            $(errorContainer).show();
            if(data.MessageType == "Error"){messageHandler.setMessageType = "error";}
            if(data.MessageType == "Warning"){messageHandler.setMessageType = "warning";}
            var errorMessage;
            var field;
            var message;
            var error;
            if (data.Errors && data.Errors.length > 0) {
                for (var i = 0; i < data.Errors.length; i++) {
                    field = data.Errors[i].PropertyName;
                    message = data.Errors[i].ErrorMessage;
                    errorMessage = field + ": " + message;
                    type = "error";
                    error = cc.utilities.messageHandling.mhMessage(type,errorMessage,field);
                    messageHandler.addMessage(error);
                }
            } else if (data.Message) {
                errorMessage = data.Message + " ";
                var type = "warning";
                error = cc.utilities.messageHandling.mhMessage(type,errorMessage,"warning");
                messageHandler.addMessage(error);
            }
            messageHandler.showAllMessages(errorContainer);
        };
        return {
            result: function(resultData, callingForm){
                if(typeof resultData == "string"){
                    resultData = JSON.parse(resultData);
                }
                if(resultData.LoggedOut){
                    window.location.replace(resultData.RedirectUrl);
                }
                if (resultData.Success){
                    successHandler(resultData, callingForm, this);
                } else {
                    errorHandler(resultData);
                }
            },
            setSuccessHandler: function(newHandler){
                successHandler = newHandler;
            },
            setErrorHandler: function(newHandler){
                errorHandler = newHandler;
            },
            setErrorContainer: function(newContainer){
                errorContainer = newContainer;
            },
            setSuccessContainer: function(newContainer){
                successContainer = newContainer;
            },
            getSuccessHandler: function(){
                return successHandler ;
            },
            getErrorHandler: function(){
                return errorHandler ;
            },
            getErrorContainer: function(){
                return errorContainer;
            },
            getSuccessContainer: function(){
                return successContainer;
            },
            setMessageHandler: function(newMessageHandler){
                messageHandler = newMessageHandler;
            },
            getMessageHandler:function(){
                return messageHandler;
            }
        }
    },

    // this is poop and you know it.
    messageHandler: function(popup) {
        var messages = [];
        var messageType;
        var _popup = popup;
        return{
            setMessageType: function(type){messageType = type;},
            addMessage: function(message) {
                if (!message instanceof cc.utilities.messageHandling.mhMessage) {
                    return false;
                }
                var isInCollection;
                $(messages).each(function(i, item) {
                    if (item.getMessage() == message.getMessage()) {
                        isInCollection = true;
                        return false;
                    }
                });
                if (!isInCollection) {
                    messages.push(message);
                }
            },

            removeMessageType: function(messageType) {
                $(messages).each(function(i, item) {
                    if (item.getMessageType() == messageType) {
                        messages.splice(i, 1);
                    }
                });
            },
            hasMessages: function() {
                return (messages.length > 0);
            },
            resetHandler: function() {
                messages = null;
                messages = [];
            },

            createHtmlForAllMessages: function() {
                 var returnHtml = "";
                 if (messages.length <= 0) {
                     return returnHtml;
                 }
                 $(messages).each(function(i, item) {
                     var cssClass;
                     if(item.getMessageType() == "success") cssClass="valid_field";
                     if(item.getMessageType() == "warning") cssClass="invalid_field";
                     if(item.getMessageType() == "error") cssClass="invalid_field";
                         returnHtml += "<li>";
                         returnHtml += '<label for="'+item.getField()+'" generated="true" class="'+cssClass+'" style="display: block;">' + item.getMessage() + '</label>';
                         returnHtml += "</li>";
                 });
                 return returnHtml;
             },
            showAllMessages: function(selector, fadeOut) {
                //$(selector).find("ul").empty();
                if (this.hasMessages()) {
                    $(selector).show();
                    if(fadeOut){
                        $(selector).find("ul").show().delay(3000).fadeOut(2000);
                    }else{
                        $(selector).find("ul").show();
                    }
                    $(selector).find("ul").html(this.createHtmlForAllMessages());
                } else {
                    $(selector).hide();
                }
                if(_popup){
                     $(selector).dialog({
                        autoOpen: false,
                        modal: true,
                        width: 500,
                        buttons: {
                            "Ok": function() {
                                $(this).dialog("close");
                                $("ul", this).html("");
                                $(".ui-dialog").remove();
                            }
                        }
                    });
                    $(selector).dialog('open');
                }
            },
            getPopup:function(){
            return _popup;
        }

        }
    }
};

cc.utilities.toggleForm = function(type, onlyShowNoHide) {
    if (onlyShowNoHide && $("#" + type + "Collapsible .kyt_container:visible").length > 0) return;
    var element = $("#" + type + "Collapsible .kyt_container");
    if (!element.is(":hidden")) {
        element.hide();
    } else if (element.is(":hidden")) {
        element.show();
    }
};

cc.utilities.clearInputs = function(element){
    var formFields = "input, checkbox, select, textarea";
    $(element).find(formFields).each(function(){
        if(this.tagName == "SELECT" ){
            $(this).find("option:selected").removeAttr("selected");
        } else if($(this).text()) {
            $(this).text("");
        } else if ( this.type == "radio") {
            $(this).attr("checked",false);
        } else if ( this.type == "checkbox") {
            $(this).attr("checked",false);
        } else if($(this).val() && this.type != "radio") {
            $(this).val("");
        }
    })
};

 cc.utilities.findAndRemoveItem = function(array, item, propertyOnItem){
    if(!$.isArray(array)) return false;
    var indexOfItem;
    $(array).each(function(idx,x){
        if(propertyOnItem){
            if(x[propertyOnItem] == item){
                 indexOfItem=idx;
            }
        }else{
            if(x == item){
                indexOfItem=idx;
            }
        }
    });
    if(indexOfItem >= 0){
        array.splice(indexOfItem,1);
    }
 };

 cc.utilities.cleanAndHideErrorMessageDiv = function(element){
    $(element).html("");
     $(element).hide();
};

 cc.utilities.openDocInNewWindow = function(url){
     window.open(url,'_blank','');
 };

 cc.straightNotification = function(result,form,notification){
    notification.result(result);
};

(function($) {
    $.fn.convertToListItems = function(value, display) {
        var listItems = [];
        this.each(function(i, item) {
            var selectedItem = "";
            if (item.IsDefault) {
                selectedItem = "selected=\"selected\"";
            }
            var option = '<option value="' + item[value] + '" ' + selectedItem + '> ' + item[display] + ' </option>';
            listItems.push(option);
        });
        return listItems;
    };


})(jQuery);


cc.utilities.trim = function(stringValue){
    return stringValue.replace(/(^\s*|\s*$)/, "");
};

cc.utilities.fixedWidthDropdown = function(){
    if($.browser.msie){
        $('select.kyt_fixedWidthDropdown').css("width","auto");
    }else{
        $('select.kyt_fixedWidthDropdown').ieSelectStyle({ applyStyle : false });
    }
};

(function($) {
    $.fn.extend({
        exclusiveCheck: function() {
            var checkboxes = $(this).find("input:checkbox");
            checkboxes.each(function(i,item){
                $(item).click(function(){
                    if(this.checked){
                        checkboxes.each(function() {
                            if ($(this)[0]!==$(item)[0]) this.checked = false;
                        });
                    }
                })
            });
        }});
})(jQuery);


$.fn.clearForm = function() {
  return this.each(function() {
    var type = this.type, tag = this.tagName.toLowerCase();
    if (tag != 'input')
      return $(':input',this).clearForm();
    if (type == 'text' || type == 'password' || tag == 'textarea')
      this.value = '';
    else if (type == 'checkbox' || type == 'radio')
      this.checked = false;
    else if (tag == 'select')
      this.selectedIndex = -1;
  });
};
