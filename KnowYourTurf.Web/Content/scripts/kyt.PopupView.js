/**
 * Created by JetBrains RubyMine.
 * User: RHarik
 * Date: 10/7/11
 * Time: 10:13 AM
 * To change this template use File | Settings | File Templates.
 */
if (typeof kyt == "undefined") {
    var kyt = {};
}

kyt.PopupView = Backbone.View.extend({
    
    initialize: function(){
        this.options = $.extend({},kyt.popupDefaults,this.options);
        this.id=this.options.id;

        $(".ui-dialog").remove();
        // since dialog probably has a form in it.  much swich from form em to pu em
        // look at doing this in the form setup
        var errorMessages = $("div[id*='errorMessages']", this.el);
        if(errorMessages){
            var id = errorMessages.attr("id");
            errorMessages.attr("id","errorMessagesPU").removeClass(id).addClass("errorMessagesPU");
        }
        this.render()
    },
    render:function(){
        var that = this;
        $(this.el).dialog({
            modal: true,
            width: this.options.width||550,
            buttons:this.options.buttons,
            title: this.options.title,
            close:function(){
                $.publish("/contentLevel/popup_"+that.id+"/cancel",[]);
                $(".ui-dialog").remove();
            }
        });
        return this;
    }
});

kyt.popupButtonBuilder = (function(){
    return {
        builder: function(id){
        var buttons = {};
        var _addButton = function(name,func){ buttons[name] = func; };
        var saveFunc = function() {
            $.publish("/contentLevel/popup_"+id+"/save",[]);
        };
        var editFunc = function(event) {$.publish("/contentLevel/popup_"+id+"/edit",[event]);};
        var cancelFunc = function(){
                            $(this).dialog("close");
                        };
        return{
            getButtons:function(){return buttons;},
            getSaveFunc:function(){return saveFunc;},
            getCancelFunc:function(){return cancelFunc;},
            addSaveButton:function(){_addButton("Save",saveFunc); return this},
            addEditButton:function(){_addButton("Edit",editFunc);return this},
            addCancelButton:function(){_addButton("Cancel",cancelFunc);return this},
            addButton:function(name,func){_addButton(name,func);return this},
            clearButtons:function(){buttons = {};return this},
            standardEditButons: function(){
                _addButton("Save",saveFunc);
                _addButton("Cancel",cancelFunc);
                return buttons;
            },
            standardDisplayButtons: function(){
                _addButton("Cancel",cancelFunc);
                return buttons;
            }
        };
    }
    }
}());


kyt.popupDefaults = {
    title:""
};
