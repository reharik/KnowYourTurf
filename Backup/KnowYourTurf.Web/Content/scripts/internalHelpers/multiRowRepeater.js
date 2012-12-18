/**
 * Created by .
 * User: RHarik
 * Date: 9/13/11
 * Time: 5:30 PM
 * To change this template use File | Settings | File Templates.
 */

$.fn.multiRows = function(templateSelector, existingItems, existingItemFunc){

    return this.each(function() {
        var context = this;
        var $template = $(templateSelector);
        var $context = $(this);
        var $firstUnit;

        this.render = function() {
        	$firstUnit = $template.find(".unit").clone();
        	$firstUnit.find('input').val('');
        };

        this.registerEventHandlers = function() {

                function clearCurrents() {
                     $('.unit',$context).each(function() {
                         $("a.close").css("visibility","hidden");
                         $(".move-up").css("visibility","hidden");
                         $(".move-down").css("visibility","hidden");
                       $(this).removeClass('current');  // clear all currents;
                    });
                }
                $('span.bottom_add button', $context.parent()).click(function() {
                    addRow(this);
                    return false;
         	    });
                $('a.close',$context).live("click",function() {
                    deleteRow(this);
                    return false;
        	    });
                $('.unit',$context).live('hover',function() {
                    clearCurrents();
                    $("a.close", this).css("visibility","visible");
                    $(".move-up", this).css("visibility","visible");
                    $(".move-down", this).css("visibility","visible");
                    $(this).addClass('current');
                });
                $context.hover(function() {},function() { clearCurrents(); });

                $('.move-up',$context).live('click',function() {
                    moveUp($(this));
                });
                $('.move-down',$context).live('click',function() {
                    moveDown($(this));
                });

        };

        moveUp = function(item) {
                var unit = $(item).closest('.unit');
                var prev = unit.prev('.unit');
                if (!prev || prev.find('th').length>0) {  return;  } // don't move before header
                var newUnit = unit.clone();
                prev.before(newUnit);
                unit.remove();
        };

        moveDown = function(item) {
            var unit = $(item).closest('.unit');
            var next = unit.next('.unit');
            if (next.length==0) {  return;  } // don't move before header
                var newUnit = unit.clone();
                next.after(newUnit);
                unit.remove();

        };

        deleteRow = function(item) {
            $(item).closest('.unit').remove();
        };

        addRow = function() {
            var clone = $firstUnit.clone();
            $context.last('.unit').append(clone);
            return clone;
        };

        function init() {
             context.render();
             context.registerEventHandlers();
            if(!$.isEmptyObject(existingItems) && existingItemFunc){
                $.each(existingItems,function(i, item){
                    var clone = addRow();
                    existingItemFunc(clone, item);
                });
            }else{
                addRow();
            }
        }

       init();

    });
};