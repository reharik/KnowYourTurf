/**
 * Created by JetBrains RubyMine.
 * User: Owner
 * Date: 3/11/12
 * Time: 6:48 PM
 * To change this template use File | Settings | File Templates.
 */

KYT.Controller = (function(KYT, Backbone){
    var Controller = {};

       Controller.showViews=function(splat,entityId, parentId,rootId,_var) {
           var routeToken = _.find(KYT.routeTokens, function(item) {
               return item.route == splat;
           });
           if (!routeToken)return;
           // this is so you don't set the id to the routetoken which stays in scope
           var viewOptions = $.extend({}, routeToken);
           if (entityId) {
               viewOptions.jsonUrl = viewOptions.url + "_json/" + entityId;
               viewOptions.url += "/" + entityId;
               viewOptions.route += "/" + entityId;
           }
           if (parentId) {
               viewOptions.url += "?ParentId=" + parentId;
               viewOptions.jsonUrl += "?ParentId=" + parentId;
               viewOptions.route += "/" + parentId;
           }
           if (rootId) {
               viewOptions.url += "&RootId=" + rootId;
               viewOptions.jsonUrl += "&RootId=" + rootId;
               viewOptions.route += "/" + rootId;
           }
           if (_var) {
               viewOptions.url += "&Var=" + _var;
               viewOptions.jsonUrl += "&Var=" + _var;
               viewOptions.route += "/" + _var;
           }
           KYT.State.set({"Relationships":
           {
               "entityId":entityId ? entityId : 0,
               "parentId":parentId ? parentId : 0,
               "rootId":rootId ? rootId : 0,
               "extraVar":_var ? _var : ""
           }
           });
           var item;
           if(routeToken.itemName && KYT.Views[routeToken.itemName]){
               item = new KYT.Views[routeToken.itemName](viewOptions);
           }else{
               item = new KYT.Views[routeToken.viewName](viewOptions);
           }

           if (routeToken.isChild) {
               var hasParent = KYT.WorkflowManager.addChildView(item);
               if (!hasParent) {
                   KYT.WorkflowManager.cleanAllViews();
                   KYT.State.set({"currentView":item});
                   KYT.content.show(item);
               }
           } else {
               KYT.WorkflowManager.cleanAllViews();
               KYT.State.set({"currentView":item});
               KYT.content.show(item);
           }
       };

       KYT.addInitializer(function(){
       });

       return Controller;
   })(KYT, Backbone);
