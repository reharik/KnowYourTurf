/**
 * Created by .
 * User: RHarik
 * Date: 9/13/11
 * Time: 5:30 PM
 * To change this template use File | Settings | File Templates.
 */


if (typeof kyt == "undefined") {
            var kyt = {};
}

kyt.multiItemRepeater = function(container, options){
    var _container = container;
    var myOptions = $.extend({}, kyt.multiItemRepeaterDefaults, options || {});
    var modules = {};
    var $firstUnit;
    var that;
    return{
        init: function(){
            that = this;
            $firstUnit = $(".unit",myOptions.template).clone();
        	$firstUnit.find('input').val('');
            that.delegateEvents();
            if(!$.isEmptyObject(myOptions.existingItems)){
                $.each(myOptions.existingItems,function(i, item){
                    var clone = that.addRow();
                    myOptions.existingItemFunc(clone, item);
                });
            }else{
                that.addRow();
            }
        },
        destroy:function(){
            for(var mod in modules){
                mod.destroy();
            }
            $(_container).empty();
        },
        delegateEvents: function(){
            $('span.bottom_add button', _container).click(function() {
                that.addRow(this);
                return false;
            });
            $(_container).delegate('a.close',"click",function() {
                that.deleteRow(this);
                return false;
            });
            $(_container).delegate('.unit','hover',function() {
                that.clearCurrents();
                $("a.close", this).css("visibility","visible");
                $(".move-up", this).css("visibility","visible");
                $(".move-down", this).css("visibility","visible");
                $(this).addClass('current');
            });
            $(_container).hover(function() {},function() { that.clearCurrents(); });

            $(_container).delegate('.move-up','click',function() {
                that.moveUp($(this));
            });
            $(_container).delegate('.move-down','click',function() {
                that.moveDown($(this));
            });
        },
        clearCurrents: function() {
            $('.unit',_container).each(function() {
                $("a.close").css("visibility","hidden");
                $(".move-up").css("visibility","hidden");
                $(".move-down").css("visibility","hidden");
                $(this).removeClass('current');  // clear all currents;
            });
        },
        moveUp : function(item) {
                var unit = $(item).closest('.unit');
                var prev = unit.prev('.unit');
                if (!prev || prev.find('th').length>0) {  return;  } // don't move before header
                var newUnit = unit.clone();
                prev.before(newUnit);
                unit.remove();
        },

        moveDown: function(item) {
            var unit = $(item).closest('.unit');
            var next = unit.next('.unit');
            if (next.length==0) {  return;  } // don't move before header
                var newUnit = unit.clone();
                next.after(newUnit);
                unit.remove();
        },

        deleteRow: function(item) {
            $(item).closest('.unit').remove();
        },

        addRow: function() {
            var clone = $firstUnit.clone();
            $(".items",_container).last('.unit').append(clone);
            return clone;
        }
    }
};

kyt.multiItemRepeaterDefaults = {
    existingItems:{},
    existingItemFunc:function(){}
};