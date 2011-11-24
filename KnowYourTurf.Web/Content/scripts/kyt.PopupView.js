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
        //this shit pisses me off figure it out damn it!
        this.id=this.options.id;
        this.el=this.options.el;

        $(".ui-dialog").remove();
        this.render()
    },
    render:function(){
        $(this.el).dialog({
            modal: true,
            width: 550,
            buttons:this.options.buttons,
            title: this.options.title
        });
        return this;
    },
    close:function(){
        $(this.el).dialog("close");
        $(this.el).remove();
        $(".ui-dialog").remove();
    }
});

kyt.popupButtonBuilder = (function(){
    return {
        builder: function(_id){
        var id = _id;
        var buttons = {};
        var _addButton = function(name,func){ buttons[name] = func; };
        var saveFunc = function() {
            $.publish("/popup_"+id+"/save",[id]);
        };
        var editFunc = function(e) {
                            var url = $("#AddEditUrl",this).val();
                            $(this).dialog("close");
                            $(this).remove();
                            $(".ui-dialog").remove();
            $.publish("/popup_"+id+"/edit",[url,id]);
        };
        var cancelFunc = function(){
                            $.publish("/popup_"+id+"/cancel",[id]);
                            $(this).dialog("close");
                            $(this).remove();
                            $(".ui-dialog").remove();
                        };
        return{
            getButtons:function(){return buttons;},
            getSaveFunc:function(){return saveFunc;},
            getCancelFunc:function(){return cancelFunc;},
            addSaveButton:function(){_addButton("Save",saveFunc); return this;},
            addEditButton:function(){_addButton("Edit",editFunc);return this;},
            addCancelButton:function(){_addButton("Cancel",cancelFunc);return this;},
            addButton:function(name,func){_addButton(name,func);return this;},
            clearButtons:function(){buttons = {};return this;},
            standardEditButons: function(){
                _addButton("Save",saveFunc);
                _addButton("Cancel",cancelFunc);
                return buttons;
            },
            standardDisplayButtons: function(){
                _addButton("Edit",editFunc);
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
