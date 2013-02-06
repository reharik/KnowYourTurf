/**
 * Created by JetBrains RubyMine.
 * User: Owner
 * Date: 7/15/12
 * Time: 10:41 AM
 * To change this template use File | Settings | File Templates.
 */

CC.ValidationRunner = (function(){
    var runner = {};
    var validator = CC.validationRules;
    function classList(elem){
        if(!elem.attr('class')){return[];}
        return elem.attr('class').split(/\s+/);
    }

    runner.runElement = function(CCElement, errorSelector){
        var elementIsValid = true;
        var classes = classList(CCElement.$input);
        var val = CCElement.getValue();
        if((!val || val=="") && !_.any(classes,function(item){return item == "required" || item == "fileRequired"})){
            CCElement.isValid = true;
            return;
        }
        $.each(classes, function(idx,rule){
            if(rule && validator[rule]){
                var isValid = validator[rule](CCElement);
                if(!isValid){
                    elementIsValid = false;
                    if(!$.noty.getByViewIdAndElementId(CCElement.viewId,CCElement.cid)){
                        $(errorSelector).noty({type: "error", text: CCElement.friendlyName+" "+CC.errorMessages[rule], elementId: CCElement.cid, viewId:CCElement.viewId});
                    }
                }else{
                    $.noty.closeByElementId(CCElement.cid);
                }
            }
        });
        if(classes.length){
            CCElement.setValidState(elementIsValid);
        }else{
            CCElement.isValid = elementIsValid;
        }
    };
    runner.runViewModel = function(cid, viewModel, errorSelector){
        var isValid = true;
        var collection = viewModel.collection;
//        $.noty.closeByViewId(cid);
        for (var el in collection){
            if(collection.hasOwnProperty(el)){
                collection[el].errorSelector = errorSelector?errorSelector:"#messageContainer";
                collection[el].validate();
                if(!collection[el].isValid) {
                    isValid = false;
                }
            }
        }
        return isValid;
    };
    return runner;
})();
