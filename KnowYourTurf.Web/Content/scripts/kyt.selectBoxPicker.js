

kyt.selectboxPicker = (function(){
    var _this;
    var available;
    var selected;
    return{
        setup:function(that){
            _this = that[0];
            var id = $(_this).attr("id");
            available = $(_this).find("#available"+id);
            selected = $(_this).find("#selected"+id);
            $(_this).find("a.selectboxAddItem").live("click", {"available":available,"selected":selected}, kyt.selectboxPicker.addItem);
            $(_this).find("a.selectboxRemoveItem").live("click",{"available":available,"selected":selected},kyt.selectboxPicker.removeItem);
        },
        addItem:function(e){
            $(e.data.available).find("option:selected").each(function(i, item) {
                var valid=true;
                $(e.data.selected).find("option").each(function(idx, opt){
                    if(opt.value == item.value) valid=false;
                });
                if(valid){
                    $(item).appendTo($(e.data.selected));
                }
            });
            $(e.data.available).find("option:selected").remove();
        },
        removeItem:function(e){
           $(e.data.selected).find("option:selected").each(function(i, item) {
               var valid=true;
                $(e.data.available).find("option").each(function(idx, opt){
                    if(opt.value == item.value) valid=false;
                });
                if(valid){
                    $(item).appendTo($(e.data.available));
                }
            });
            $(e.data.selected).find("option:selected").remove();
        }
    }

}());

(function($) {
    $.fn.asSelectboxPicker = function(userOptions){
        if(this.length<=0)return;
        var pickerDefaultOptions = {};
        var pickerOptions = $.extend(pickerDefaultOptions, userOptions || {});
        kyt.selectboxPicker.setup(this);
    }
})($);