/**
 * Created by JetBrains RubyMine.
 * User: Owner
 * Date: 7/15/12
 * Time: 10:41 AM
 * To change this template use File | Settings | File Templates.
 */


CC.errorMessages = {
    "required":"field is Required"
};

CC.validationRules = (function(){
    return {
        required : function(value) {
            return value ?true:false;
        },
        number : function(value) {
            return true;
        }
    };
}());
