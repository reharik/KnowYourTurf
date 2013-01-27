/**
 * Created by JetBrains RubyMine.
 * User: Owner
 * Date: 7/14/12
 * Time: 3:06 PM
 * To change this template use File | Settings | File Templates.
 */

CC.ElementCollection = function(){
    this.collection = {};
};

$.extend(CC.ElementCollection.prototype, {
    add:function(element){
        this.collection[element.name] = element;
    },
    destroy:function(){
        for(var el in this.collection){
            this.collection[el].destroy();
        }
        this.collection = {};
    }
});


CC.elementService = (function(){
    var srv = {};
    srv.getElementsViewmodel = function(view){
        var model = new CC.ElementCollection();
        var isPopup = view.$el.find("#popupMessageContainer").size()>0
        view.$el.find("[eltype]").each(function(i,item){
            var element = new CC.Elements[$(item).attr("eltype")]($(item));
            element.init(view,isPopup);
            model.add(element);
        });
        return model;
    };
    return srv;
})();


