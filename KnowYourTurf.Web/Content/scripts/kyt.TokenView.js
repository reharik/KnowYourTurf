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
    events:{
        'click #addNew' : 'addNew'
    },
    initialize: function(){
        this.options = $.extend({},kyt.tokenDefaults,this.options);
        this.id=this.options.id;

        if(!this.options.availableItems || this.options.availableItems.length==0) {
            $("#noAssets",this.el).show();
            $("#hasAssets",this.el).hide();
        }else{
            $("#noAssets",this.el).hide();
            $("#hasAssets",this.el).show();
            this.inputSetup();
        }
    },
    inputSetup:function(){
        $(this.options.inputSelector, this.el).tokenInput(this.options.availableItems, {prePopulate: this.options.selectedItems,
            internalTokenMarkup:function(item) {
//                var cssClass = that.options.tooltipAjaxUrl ? "class='kyt_tokenTooltip selectedItem' rel='" + this.options.tooltipAjaxUrl + "?EntityId=" + item.id + "'" : "class='selectedItem'";
                var cssClass = "class='selectedItem'";
                // added () to value
                return "<p><a " + cssClass + ">" + item.name() + "</a></p>";
            },
            afterTokenSelectedFunction:function() {
//                $(".kyt_tokenTooltip").cluetip({showTitle: false,
//                    cluetipClass: 'rounded',
//                    arrows: true,
//                    hoverIntent: {
//                        sensitivity:  3,
//                        interval:     50,
//                        timeout:      500
//                    },
//                    mouseOutClose:true
//                    //delayedClose:5000
//                });
            }
        });
        this.options.instantiated = true;
    },
    addNew:function(){
        $.publish("/contentLevel/token_"+this.id+"/addUpdate",[this.id]);
        return false;
    },
    successHandler: function(result){
        if(!this.options.instantiated){
            $("#noAssets",this.el).hide();
            $("#hasAssets",this.el).show();
            this.inputSetup();
        }
        $(this.options.inputSelector,this.el).tokenInput("add",{id:result.EntityId, name:result.Variable});
        $(this.options.inputSelector,this.el).tokenInput("addToAvailableList",{id:result.EntityId, name:result.Variable});
    }
});

kyt.tokenDefaults = {

    availableItems:[],
    selectedItems: [],
    tooltipAjaxUrl:"",
    inputSelector:"#Input"
};