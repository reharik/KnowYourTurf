/**
 * Created by JetBrains RubyMine.
 * User: Owner
 * Date: 9/18/11
 * Time: 6:58 PM
 * To change this template use File | Settings | File Templates.
 */

if (typeof kyt == "undefined") {
            var kyt = {};
}

kyt.formModule = function(container, options){
    var _container = container;
    var myOptions = $.extend({}, kyt.formDefaults, options || {});
    var modules = {};
    var setupDialog = function(dialogInstance, buttons){
        dialogInstance.dialog({
            autoOpen: false,
            modal: true,
            width: 550,
            buttons:buttons,
            close: function(event, ui) {
                $(".ui-dialog").remove();
            }
        });
    };
    return{
        displayPopup:function(buttons){
            $(".ui-dialog").remove();
            setupDialog(_container,buttons);
            $(_container).dialog("open");
            $(_container).dialog("option", "title",  titlePopup);
        },
        init: function(){
            var that = this;
        },
        closePopup:function(){
            $(_container).dialog("close");
             $(".ui-dialog").remove();
        }
    }
};

kyt.popupButtonBuilder = function(){
    var buttons = {};
    var _addButton = function(name,func){ buttons[name] = func; };
    var saveFunc = function() { $.publish("/popup/saveItem",[])};
    var editFunc = function(event) { $.publish("/popup/saveItem",[event])};
    var cancelFunc = function() { $.publish("/popup/closeItem",[])};
    return{
        getButtons:function(){return buttons},
        getSaveFunc:function(){return saveFunc;},
        getCancelFunc:function(){return cancelFunc;},
        addSaveButton:function(){_addButton("Save",saveFunc);},
        addEditButton:function(){_addButton("Edit",editFunc);},
        addCancelButton:function(){_addButton("Cancel",cancelFunc);},
        addButton:function(name,func){
            _addButton(name,func);},
        clearButtons:function(){buttons = {};},

        standardEditButons: function(){
            _addButton("Save",saveFunc);
            _addButton("Cancel",cancelFunc);
            return buttons;
        },
        standardDisplayButtons: function(){
             _addButton("Edit",editFunc);
            _addButton("Return",_addButton("Cancel",cancelFunc));
            return buttons();
        }

    };
};
