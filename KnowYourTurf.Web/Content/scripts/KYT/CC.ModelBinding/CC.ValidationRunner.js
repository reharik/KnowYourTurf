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
    runner.runElement = function(CCElement){
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
                var possibleErrorMsg = new CC.NotificationMessage(CCElement.cid, CCElement.fieldName+" "+CC.errorMessages[rule],"error");
                if(!isValid){
                    elementIsValid = false;
                    CC.notification.add(possibleErrorMsg);
                }else{
                    CC.notification.remove(possibleErrorMsg);
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
        $.each(viewModel.collection,function(i,item){
            item.validate();
            if(!item.isValid) {
                isValid = false;}
        });
        return isValid;
    };
    return runner;
})();
