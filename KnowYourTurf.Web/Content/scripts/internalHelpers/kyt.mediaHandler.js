/**
 * Created by .
 * User: RHarik
 * Date: 4/6/11
 * Time: 11:35 AM
 * To change this template use File | Settings | File Templates.
 */

if (typeof kyt == "undefined") {
            var kyt = {};
}

if (typeof kyt.mediaHandler== "undefined") {
            kyt.mediaHandler = {};
}

kyt.mediaHandler.controller = (function(){
    var setupEvents = function(result,mediaType,availableItems,selectedItems){
        $(result).find("#uploadNew"+mediaType).hide();
        $(result).find("#addNew"+mediaType).click(function(){ $("#CRUDForm"+mediaType).validate().resetForm(); $("#uploadNew"+mediaType).show(); });
        $(result).find("#newUpload"+mediaType).click(function(event) {
           $("#CRUDForm"+mediaType).submit(); });
        $(result).find("#"+mediaType+"Input").tokenInput(availableItems?availableItems:{},{prePopulate: selectedItems,
            internalTokenMarkup:function(item){
                var cssClass = tooltipAjaxUrl ? "class='kyt_tokenTooltip selectedItem' rel='"+tooltipAjaxUrl+"?EntityId="+item.id +"'":"class='selectedItem'";
                return "<p><a "+cssClass+">"+ item.name +"</a></p>";
            },
            afterTokenSelectedFunction:function(){ $(".kyt_tokenTooltip").cluetip({showTitle: false,
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
   };
   
    return{

        init:function(result,mediaType){
            var availableItems = eval("availableItems"+mediaType);
            var selectedItems = eval("selectedItems"+mediaType);
            setupEvents(result,mediaType,availableItems,selectedItems);
        },
        successHandler:function(result,mediaType){
            $("#uploadNew"+mediaType).hide();
            $("#"+mediaType+"Input").tokenInput("add",{id:result.EntityId, name:result.Variable});
            $("#"+mediaType+"Input").tokenInput("addToAvailableList",{id:result.EntityId, name:result.Variable});
            // for some reason this shit;s the bed. I don't know why but I would like to have this hook if possible.
            //$('#CRUDDocForm').data().metaData.getRunAfterSuccess().each(function(i,item){
            //    item(result);
            //});
        }
    }
}());