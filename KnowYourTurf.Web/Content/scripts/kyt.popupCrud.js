if (typeof kyt == "undefined") {
            var kyt = {};
}

if (typeof kyt.popupCrud == "undefined") {
            kyt.popupCrud = {};
}

if (typeof kyt.popup == "undefined") {
            kyt.popup = {};
}
runBeforeSubmit = null;
var titlePopup="";
kyt.popupCrud.controller = (function(){
    var addParameters = function(url,params){
        if(url.indexOf('?')>0){
            return url+"&"+params;
        }else{
            return url+"?"+params;
        }
    };
    return{
        itemCRU:function(url,metaData,data){
            kyt.popupCrud.repository.itemCall(url,metaData,data);
        },
        saveItem:function(){
            var crudForm = $(".ui-dialog").data().metaData.getCrudFormName();
            $(crudForm).submit();
        },
        success: function(result, form, notification){
            var metaData = $(".ui-dialog").data().metaData;
            var runAfterSuccess = metaData.getRunAfterSuccess();
            $(runAfterSuccess).each(function(i,func){
                func(result,metaData);
            });
            notification.defaultSuccessHandler(result);
            kyt.popupCrud.controller.cancelDialog();
        },
        cancelDialog: function(event){
            $("#dialogHolder").dialog("close");
            $(".ui-dialog").remove();
            $(".kyt_dyn #dialogHolder").remove();
        },
        editFromDisplay:function(event){
            var metaData = $(".ui-dialog").data().metaData;
            metaData.setIsDisplay(false);
            var addEditUrl = $("#AddEditUrl").val();

            // I dont like it sam I am
            var sibling = $(event.target).parent().parent().siblings()[1];
            var entityId = $(sibling).find("#EntityId").val();
            if(!addEditUrl && metaData){
                addEditUrl = metaData.getAddUrl()+"/"+entityId;
            }
            var dialogName = $(sibling).attr("id");
            $("#"+dialogName+".ui-dialog-content").dialog("close");
            kyt.popupCrud.controller.itemCRU(addEditUrl,metaData,{"EntityId":entityId});
        },
        copyItem:function(){
            var metaData = $(".ui-dialog").data().metaData;
            var entityId = $("[name$='EntityId']").val();
             $(".ui-dialog").remove();
            $(".kyt_dyn #dialogHolder").remove();
            metaData.setIsDisplay(false);
            kyt.popupCrud.controller.itemCRU(addParameters(addUrl, "EntityId="+ entityId +"&Copy=true"),metaData);
        },

        deleteItem:function(){
            var metaData = $(".ui-dialog").data().metaData;
            var data = {"EntityId":$("#EntityId").val()};
            kyt.popupCrud.repository.deleteItemCall(deleteUrl,metaData,data);
        }
    }
}());

kyt.popupCrud.buttonBuilder = function(){
    var buttons = {};
    var _addButton = function(name,func){ buttons[name] = func; };
    var saveFunc = function() {
        kyt.popupCrud.controller.saveItem(this); };
    var editFunc = function(event) {kyt.popupCrud.controller.editFromDisplay(event);};
    var cancelFunc = function(){$(this).dialog("close");};
    return{
        getButtons:function(){return buttons},
        getSaveFunc:function(){return saveFunc;},
        getCancelFunc:function(){return cancelFunc;},
        addSaveButton:function(){_addButton("Save",saveFunc);},
        addEditButton:function(){_addButton("Edit",editFunc);},
        addCancelButton:function(){_addButton("Cancel",cancelFunc);},
        addButton:function(name,func){
            _addButton(name,func);},
        clearButtons:function(){buttons = {};}
    };
};

kyt.popupCrud.popup = (function()  {
    var standardEditButons = function(builder){
        builder.addSaveButton();
        builder.addCancelButton();
        return builder.getButtons();
    };
    var standardDisplayButtons = function(builder){
         builder.addEditButton();
         builder.addButton("Return", builder.getCancelFunc());
        return builder.getButtons();
    };

    var setupDialog = function(dialogInstance, buttons){
        dialogInstance.dialog({
            autoOpen: false,
            modal: true,
            width: 550,
            buttons:buttons,
            close: function(event, ui) {
               $("#dialogHolder").empty();
                $(".ui-dialog").remove();
                $(".kyt_dyn #dialogHolder").remove();
            }
        });
    };
    return{
        displayPopup:function(result, metaData){
            $(".ui-dialog").remove();
            $(".kyt_dyn #dialogHolder").remove();
            var dialog = $("<div></div>").attr("id","dialogHolder");
            $(".kyt_dyn").append(dialog);
            var buttonBuilder = kyt.popupCrud.buttonBuilder();
            var buttons;
            if(metaData.getIsDisplay())
            {
                if(metaData.getDisplayButtonBuilder() && typeof metaData.getDisplayButtonBuilder() == "function" ){
                    buttons = metaData.getDisplayButtonBuilder()(buttonBuilder);
                }else{
                    buttons = standardDisplayButtons(buttonBuilder);
                }
            }else{
                if(metaData.getEditButtonBuilder() && typeof metaData.getEditButtonBuilder() == "function" ){
                    buttons = metaData.getEditButtonBuilder()(buttonBuilder);
                }else{
                    buttons = standardEditButons(buttonBuilder);
                }
            }

            setupDialog(dialog,buttons);
            $(dialog).html(result);
            $(dialog).dialog("open");
            $(dialog).dialog("option", "title",  metaData.getPopupTitle()||titlePopup);

        }
    };
})();

kyt.popupCrud.repository = (function(){

    var itemCallback = function(result,metaData){
        if(result.LoggedOut){
            window.location.replace(result.RedirectUrl);
            return;
        }
        kyt.popupCrud.popup.displayPopup(result, metaData);
        kyt.popupCrud.repository.processPopupMetaData(metaData);
    };
    var deleteCallback = function(result,metaData){
        kyt.popupCrud.controller.cancelDialog();
        var runAfterSuccess = metaData.getRunAfterSuccess();
        $(runAfterSuccess).each(function(i,func){
            func(result,metaData);
        });
    };

    return{
        itemCall: function(url,metaData,data){
            var _url = url?url:metaData.getAddUrl();
            $.get(_url,data, function(result){itemCallback(result,metaData)});
        },
        deleteItemCall:function(url,metaData,data){
            var _url = url?url:metaData.getAddUrl();
            $.get(_url, data,function(result){deleteCallback(result,metaData)});
        },
         processPopupMetaData:function(metaData){
            $(".ui-dialog").data().metaData = metaData;
            processArea(metaData);
            function processArea(item){
                var crudFormName = item.getCrudFormName();
                if($(crudFormName).size()>0){
                    $(crudFormName).data().metaData = item;
                    var options = {metaData:item};
                    if(item.getSuccessHandler()) options.successHandler=item.getSuccessHandler();
                    if(item.getErrorContainer()) options.errorContainer=item.getErrorContainer();
                    if(item.getSuccessContainer()) options.successContainer=item.getSuccessContainer();
                    $(crudFormName).crudForm(options);
                }
                $(item.getRunAfterRender()).each(function(idx,func){
                    if(runBeforeSubmit){
                        $(crudFormName).data().crudForm.setBeforeSubmitFuncs(runBeforeSubmit);
                    }
                    func($(".ui-dialog"));
                });
                $(item.getMetaDatas()).each(function(i,area){processArea(area);})
            }
        }
    }
}());