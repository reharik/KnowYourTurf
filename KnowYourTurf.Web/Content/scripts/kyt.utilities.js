if (typeof kyt == "undefined") {
            var kyt = {};
}

        kyt.namespace = function() {
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

        kyt.object = function(o){
            function F(){}
            F.prototype = o;
            return new F();
        };

kyt.namespace("kyt.utilities");

kyt.popupMetaData = {
    metaData: function(addUrl,errorContainer,successHandler){
        var _crudFormName = "#CRUDForm";
        var _gridName = "#gridContainer";
        var _addUrl = addUrl;
        var _submitUrl;
        var _successHandler = successHandler;
        var _errorContainer= errorContainer?errorContainer: "#errorMessages";
        var _successContainer;
        var _runAfterRender = [];
        var _runAfterSuccess = [];
        var _metaDatas=[];
        var _isDisplay;
        var _editButtonBuilder;
        var _displayButtonBuilder;
        var _submitData={};
        var _loadData={};
        var _popupTitle;
        var _altClickFunction;
        return{
            getAltClickFunction:function(){return _altClickFunction},
            setAltClickFunction:function(func){_altClickFunction=func},
                        
            getSubmitData:function(){return _submitData},
            addSubmitData:function(data){$.extend(_submitData,data||{})},
            getLoadData:function(){return _loadData},
            addLoadData:function(data){$.extend(_loadData,data||{})},

            getPopupTitle: function(){
               return _popupTitle;
            },
            setPopupTitle: function(title){
                _popupTitle = title;
            },
            getData:function(){return _data},
            addData:function(data){$.extend(_data,data||{})},
            getEditButtonBuilder: function(){
               return _editButtonBuilder;
            },
            setEditButtonBuilder: function(func){
                _editButtonBuilder = func;
            },
            getDisplayButtonBuilder: function(){
               return _displayButtonBuilder;
            },
            setDisplayButtonBuilder: function(func){
                _displayButtonBuilder = func;
            },
            getIsDisplay: function(){
               return _isDisplay;
            },
            setIsDisplay: function(bool){
                _isDisplay = bool;
            },
            getCrudFormName: function(){
                return _crudFormName;
            },
            setCrudFormName: function(crudFormName){
                _crudFormName = crudFormName;
            },
            getGridName: function(){
                return _gridName;
            },
            setGridName: function(gridName){
                _gridName = gridName;
            },
            getSubmitUrl: function(){
                return _submitUrl;
            },
            setSubmitUrl: function(submitUrl){
                _submitUrl = submitUrl;
            },
            getAddUrl: function(){
                return _addUrl;
            },
            setAddUrl: function(addUrl){
                _addUrl = addUrl;
            },
            getSuccessHandler: function(){
                return _successHandler == typeof "function" ? _successHandler:eval(_successHandler);
            },
            setSuccessHandler: function(successHandler){
                _successHandler = successHandler;
            },
            getErrorContainer: function(){
                return _errorContainer;
            },
            setErrorContainer: function(errorContainer){
                _errorContainer = errorContainer;
            },
            getSuccessContainer: function(){
                return _successContainer;
            },
            setSuccessContainer: function(successContainer){
                _successContainer = successContainer;
            },
            getRunAfterRender: function(){
                return _runAfterRender;
            },
            addRunAfterRender: function(func){
                if($.inArray(func,_runAfterRender)<=0){
                    _runAfterRender.push(func);
                }
            },
            getRunAfterSuccess: function(){
                return _runAfterSuccess;
            },
            addRunAfterSuccess: function(func){
                if($.inArray(func,_runAfterRender)<=0){
                    _runAfterSuccess.push(func);
                }
            },
            getMetaDatas: function(){
                if(_isDisplay)return null;
                return _metaDatas;
            },
            addMetaData: function(metaData){
                var exists;
                $(_metaDatas).each(function(i,item){
                    if(String(item)==String(func)) {exists=true;}
                });
                if(!exists){
                    _metaDatas.push(metaData);
                }
            },
            name:""
        }
    }
};
kyt.utilities.messageHandling =  {

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
        var errorContainer = '#errorMessages';
        var successContainer = '#errorMessages';
        var successHandler = function(data, callingForm, notificaiton){
             if(data.Redirect) {
                 if (data.RedirectUrl) {
                     window.location.href = data.RedirectUrl;
                 } else if(ReturnUrl) {
                     window.location.href = ReturnUrl;
                 }
             } else if(data.Message){
                     var emh = kyt.utilities.messageHandling.messageHandler();
                    emh.setMessageType = "success";
                     var message = kyt.utilities.messageHandling.mhMessage("success",data.Message,"");
                     emh.addMessage(message);
                     emh.showAllMessages(successContainer,true);
             }
         };

        var errorHandler = function(data){
            $(errorContainer).show();
            var emh = kyt.utilities.messageHandling.messageHandler();
            if(data.MessageType == "Error"){emh.setMessageType = "error";}
            if(data.MessageType == "Warning"){emh.setMessageType = "warning";}
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
                    error = kyt.utilities.messageHandling.mhMessage(type,errorMessage,field);
                    emh.addMessage(error);
                }
            } else if (data.Message) {
                errorMessage = data.Message + " ";
                var type = "warning";
                error = kyt.utilities.messageHandling.mhMessage(type,errorMessage,"warning");
                emh.addMessage(error);
            }
            emh.showAllMessages(errorContainer);
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
            defaultSuccessHandler: successHandler
        }
    },

    messageHandler: function() {
        var messages = [];
        var messageType;
        return{
            setMessageType: function(type){messageType = type;},
            addMessage: function(message) {
                if (!message instanceof kyt.utilities.messageHandling.mhMessage) {
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
            showAllMessages: function(selector,noFadeOut) {
                //$(selector).find("ul").empty();
                if (this.hasMessages()) {
                    $(selector).show();
                    if(noFadeOut){
                        $(selector).find("ul").show();
                    }else{
                        $(selector).find("ul").show().delay(3000).fadeOut(2000);
                    }
                    $(selector).find("ul").html(this.createHtmlForAllMessages());
                } else {
                    $(selector).hide();
                }
            }
        }
    }
};

kyt.utilities.toggleForm = function(type, onlyShowNoHide) {
    if (onlyShowNoHide && $("#" + type + "Collapsible .container:visible").length > 0) return;
    var element = $("#" + type + "Collapsible .container");
    if (!element.is(":hidden")) {
        element.hide();
    } else if (element.is(":hidden")) {
        element.show();
    }
};

kyt.utilities.clearInputs = function(element){
    var formFields = "input, checkbox, select, textarea";
    $(element).find(formFields).each(function(){
        if(this.tagName == "SELECT" ){
            $(this).find("option:selected").removeAttr("selected");
        } else if($(this).text()) {
            $(this).text("");
        } else if ( this.type == "radio") {
            $(this).attr("checked",false);
        } else if($(this).val() && this.type != "radio") {
            $(this).val("");
        }
    })
};

 kyt.utilities.findAndRemoveItem = function(array, item, propertyOnItem){
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

 kyt.utilities.cleanAndHideErrorMessageDiv = function(element){
    $(element).html("<ul></ul>");
     $(element).hide();
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

kyt.utilities.trim = function(stringValue){
    return stringValue.replace(/(^\s*|\s*$)/, "");
};

kyt.utilities.tinyMce = function(){
		$('#tinyMce').tinymce({
			// Location of TinyMCE script
			script_url : '/content/scripts/tiny_mce/tiny_mce.js',

			// General options
			theme : "advanced",
			plugins : "autolink,lists,pagebreak,style,layer,table,save,advhr,advimage,advlink,emotions,iespell,inlinepopups,insertdatetime,preview,media,searchreplace,print,contextmenu,paste,directionality,fullscreen,noneditable,visualchars,nonbreaking,xhtmlxtras,template,advlist",

			// Theme options
			theme_advanced_buttons1 : "save,newdocument,|,bold,italic,underline,strikethrough,|,justifyleft,justifycenter,justifyright,justifyfull,styleselect,formatselect,fontselect,fontsizeselect",
			theme_advanced_buttons2 : "cut,copy,paste,pastetext,pasteword,|,search,replace,|,bullist,numlist,|,outdent,indent,blockquote,|,undo,redo,|,link,unlink,anchor,image,cleanup,help,code,|,insertdate,inserttime,preview,|,forecolor,backcolor",
			theme_advanced_buttons3 : "tablecontrols,|,hr,removeformat,visualaid,|,sub,sup,|,charmap,emotions,iespell,media,advhr,|,print,|,ltr,rtl,|,fullscreen",
			theme_advanced_buttons4 : "insertlayer,moveforward,movebackward,absolute,|,styleprops,|,cite,abbr,acronym,del,ins,attribs,|,visualchars,nonbreaking,template,pagebreak",
			theme_advanced_toolbar_location : "top",
			theme_advanced_toolbar_align : "left",
			theme_advanced_statusbar_location : "bottom",
			theme_advanced_resizing : true,

			// Example content CSS (should be your site CSS)
			content_css : "../css/tinyMce.css",

			// Drop lists for link/image/media/template dialogs
			// template_external_list_url : "lists/template_list.js",
			// external_link_list_url : "lists/link_list.js",
			// external_image_list_url : "lists/image_list.js",
			// media_external_list_url : "lists/media_list.js",

			// Replace values for the template plugin
			template_replace_values : {
				username : "Some User",
				staffid : "991234"
			}
		});
};
