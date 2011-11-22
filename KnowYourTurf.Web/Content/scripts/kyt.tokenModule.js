/**
 * Created by .
 * User: RHarik
 * Date: 9/1/11
 * Time: 2:27 PM
 * To change this template use File | Settings | File Templates.
 */

if (typeof kyt == "undefined") {
            var kyt = {};
}

kyt.tokenModule = function(container, options){
    var _container = container;
    var myOptions = $.extend({}, kyt.tokenDefaults, options || {});
    var that;
    var instantiated;
    return{
        init: function(){
            that = this;
            if(!myOptions.availableItems || myOptions.availableItems.length==0) {
                $("#noAssets",_container).show();
                $("#hasAssets",_container).hide();
            }else{
                $("#noAssets",_container).hide();
                $("#hasAssets",_container).show();
            }
            this.delegateEvents();
        },
        destroy:function(){
            $(_container).empty();
        },
        delegateEvents: function(){
            $("#addNew",_container).click(that.addNew);
            if(!myOptions.availableItems || myOptions.availableItems.length==0) return;
            that.inputSetup();
        },
        inputSetup:function(){
            $(myOptions.inputSelector, _container).tokenInput(myOptions.availableItems, {prePopulate: myOptions.selectedItems,
                internalTokenMarkup:function(item) {
                    var cssClass = myOptions.tooltipAjaxUrl ? "class='kyt_tokenTooltip selectedItem' rel='" + myOptions.tooltipAjaxUrl + "?EntityId=" + item.id + "'" : "class='selectedItem'";
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
            instantiated = true;
        },
        addOptions:function(data){
            $.extend(myOptions, data);
        },
        addNew:function(){
            $.publish("/token"+myOptions.name+"/addEdit",[myOptions.name]);
            return false;
        },
        successHandler: function(result){
            if(!instantiated){
                $("#noAssets",_container).hide();
                $("#hasAssets",_container).show();
                that.inputSetup();
            }
            $(myOptions.inputSelector,_container).tokenInput("add",{id:result.EntityId, name:result.Variable});
            $(myOptions.inputSelector,_container).tokenInput("addToAvailableList",{id:result.EntityId, name:result.Variable});
        }
    }
};

kyt.tokenDefaults = {
        name:"",
        availableItems:[],
        selectedItems: [],
        tooltipAjaxUrl:"",
        inputSelector:"#Input"
};