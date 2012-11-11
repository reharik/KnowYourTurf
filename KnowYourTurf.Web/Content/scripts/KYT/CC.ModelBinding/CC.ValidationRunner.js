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

    runner.runElement = function(CCElement,notification){
        var elementIsValid = true;
        var classes = classList(CCElement.$input);
        var val = CCElement.getValue();
        if((!val || val=="") && !_.any(classes,function(item){return item == "required"})){
            CCElement.isValid = true;
            return;
        }
        $.each(classes, function(idx,rule){
            if(rule && validator[rule]){
                var isValid = validator[rule](CCElement);
                var possibleErrorMsg = new CC.NotificationMessage(CCElement.cid, CCElement.viewId, CCElement.friendlyName+" "+CC.errorMessages[rule],"error");
                if(!isValid){
                    elementIsValid = false;
                    notification.add(possibleErrorMsg);
                }else{
                    notification.remove(possibleErrorMsg);
                }
            }
        });
        if(classes.length){
            CCElement.setValidState(elementIsValid);
        }else{
            CCElement.isValid = elementIsValid;
        }
    };
    runner.runViewModel = function(viewModel){
        var isValid = true;
        var collection = viewModel.collection;
        for (var el in collection){
            if(collection.hasOwnProperty(el)){
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
