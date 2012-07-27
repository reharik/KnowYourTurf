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
        $.each(classList(CCElement.$input), function(idx,rule){
            if(rule && validator[rule]){
                var isValid = validator[rule](CCElement.getValue());
                //TODO possibly replace this with CCElement.cid
                if(!isValid){
                    CC.notification.add({key:CCElement.fieldName, message:CC.errorMessages[rule]});
                }else{
                    CC.notification.remove({key:CCElement.fieldName, message:CC.errorMessages[rule]});
                }
                CCElement.setValidState(isValid);
            }
        });
    };
    runner.runViewModel = function(viewModel){
        var isValid = true;
        $.each(viewModel.collection,function(i,item){
            item.validate();
            if(!item.isValid) {isValid = false;}
        });
        return isValid;
    };
    return runner;
})();
