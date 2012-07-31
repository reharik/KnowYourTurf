/**
 * Created by JetBrains RubyMine.
 * User: Owner
 * Date: 7/14/12
 * Time: 3:06 PM
 * To change this template use File | Settings | File Templates.
 */

CC.ElementCollection = function(){
    this.collection = [];
};

$.extend(CC.ElementCollection.prototype, {
    add:function(element){
        this.collection.push(element);
    },
    destroy:function(){
        _.each(this.collection, function(item){
            item.destroy();
        });
        this.collection = [];
    }
});


CC.elementService = (function(){
    var srv = {};
    srv.getElementsViewmodel = function($el){
        var model = new CC.ElementCollection();
        $el.find("[eltype]").each(function(i,item){
            var element = new CC.Elements[$(item).attr("eltype")]($(item));
            model.add(element);
        });
        return model;
    };
    srv.initAllElements = function(viewModel){
        $.each(viewModel.collection, function(i,item){
            item.init();
        });
    };
    return srv;
})();


