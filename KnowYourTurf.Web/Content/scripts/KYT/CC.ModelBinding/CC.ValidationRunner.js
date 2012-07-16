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
    runner.run = function(CCElement){
        for(var rule in CCElement.validation){
            if(rule){
                var isValid = validator[rule](CCElement.$input.val());
                if(!isValid){
                    CC.notification.add({key:CCElement.fieldName, message:CC.errorMessages[rule]});
                }else{
                    CC.notification.remove({key:CCElement.fieldName, message:CC.errorMessages[rule]});
                }
            }
        }
    };
    return runner;
})();
