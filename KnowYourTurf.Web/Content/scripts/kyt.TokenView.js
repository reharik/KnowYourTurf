/**
 * Created by JetBrains RubyMine.
 * User: RHarik
 * Date: 10/7/11
 * Time: 9:45 AM
 * To change this template use File | Settings | File Templates.
 */
if (typeof kyt == "undefined") {
    var kyt = {};
}

kyt.TokenView = Backbone.View.extend({
    initialize: function(){
        this.options = $.extend({},kyt.tokenDefaults,this.options);
        this.id=this.options.id;
        this.inputSetup();
    },
    inputSetup:function(){
        $(this.options.inputSelector).tokenInput(this.options.availableItems, {prePopulate: this.options.selectedItems,
            internalTokenMarkup:function(item) {
//                var cssClass = that.options.tooltipAjaxUrl ? "class='kyt_tokenTooltip selectedItem' rel='" + this.options.tooltipAjaxUrl + "?EntityId=" + item.id + "'" : "class='selectedItem'";
                var cssClass = "class='selectedItem'";
                return "<p><a " + cssClass + ">" + item.name + "</a></p>";
            },
            afterTokenSelectedFunction:function() {
                $(".kyt_tokenTooltip").cluetip({showTitle: false,
                    cluetipClass: 'rounded',
                    arrows: true,
                    hoverIntent: {
                        sensitivity:  3,
                        interval:     50,
                        timeout:      500
                    },
                    mouseOutClose:true
                    //delayedClose:5000
                });
            }
        });
        this.options.instantiated = true;
    }
});

kyt.tokenDefaults = {

    availableItems:[],
    selectedItems: [],
    tooltipAjaxUrl:"",
    inputSelector:"#Input"
};