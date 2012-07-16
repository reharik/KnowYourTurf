/**
 * Created by JetBrains RubyMine.
 * User: Owner
 * Date: 7/14/12
 * Time: 3:06 PM
 * To change this template use File | Settings | File Templates.
 */
if(!CC){CC={};}
CC.notification = new CC.NotificationService();

CC.ViewModel = function(){
    this.collection = [];
};

$.extend(CC.ViewModel.prototype, {
    add:function(element){
        this.collection.push(element);
    }
});


CC.modelService = (function(){
    var srv = {};
    srv.getModels = function(json, $el){
        var model = new CC.ViewModel();
        for(var key in json){
            var modelType = key.substring(0,key.indexOf('_'));
            var element = new CC.Elements[modelType](json[key],$el);
            model.add(element);
        }
        return model;
    };
    return srv;
})();


