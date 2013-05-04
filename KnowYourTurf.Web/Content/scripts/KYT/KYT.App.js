/**
 * Created by JetBrains RubyMine.
 * User: Owner
 * Date: 2/26/12
 * Time: 10:53 AM
 * To change this template use File | Settings | File Templates.
 */

var KYT = new Backbone.Marionette.Application();


KYT.addRegions({
    header: "#main-header",
    content:"#contentInner",
    menu:"#left-navigation"
});

  // an initializer to run this functional area
  // when the app starts up
KYT.addInitializer(function(){

    $("#ajaxLoading").hide();

    KYT.vent.bind("route",function(route,triggerRoute){
        KYT.Routing.showRoute(route,triggerRoute);
    });


//    CC.notification = new CC.NotificationService();
//    CC.notification.render($("#messageContainer").get(0));
    Backbone.Marionette.TemplateCache.prototype.loadTemplate = function(templateId){
        return KYT.repository.ajaxGet(this.url, this.data);
    },

    // overriding compileTemplate with passthrough function because we are not compiling
    Backbone.Marionette.TemplateCache.prototype.compileTemplate = function(rawTemplate){ return rawTemplate;};

});

KYT.bind("initialize:after", function(){
    KYT.State.set({"application":"fields"});
    if (Backbone.history){
        Backbone.history.start();
    }
});

KYT.generateRoute = function(route,_entityId,_parentId,_rootId,_var){
    var rel = KYT.State.get("Relationships");
    var entityId = _entityId?_entityId:0;
    var parentId = _parentId && _parentId>0 ?_parentId:rel.parentId;
    var rootId = _rootId && _rootId>0?_rootId:rel.rootId;
    var variable = _var?"/"+_var:"";
    return route+"/"+ entityId+ "/"+ parentId+ "/"+rootId+variable;
};

// calling start will run all of the initializers
// this can be done from your JS file directly, or
// from a script block in your HTML
$(function(){
  KYT.start();
});