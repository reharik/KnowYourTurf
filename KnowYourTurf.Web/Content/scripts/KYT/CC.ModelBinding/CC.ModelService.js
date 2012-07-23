/**
 * Created by JetBrains RubyMine.
 * User: Owner
 * Date: 7/14/12
 * Time: 3:06 PM
 * To change this template use File | Settings | File Templates.
 */
if(!CC){CC={};}
CC.notification = new CC.NotificationService();

CC.ElementColection = function(){
    this.collection = [];
};

$.extend(CC.ElementColection.prototype, {
    add:function(element){
        this.collection.push(element);
    }
});


CC.elementService = (function(){
    var srv = {};
    srv.getElements = function($el){
        var model = new CC.ElementColection();
        $el.find("[eltype]").each(function(i,item){
            var element = new CC.Elements[$(item).attr("eltype")]($(item));
            model.add(element);
        });
        return model;
    };
    return srv;
})();


