/**
 * Created by JetBrains RubyMine.
 * User: Owner
 * Date: 3/11/12
 * Time: 6:48 PM
 * To change this template use File | Settings | File Templates.
 */

KYT.Controller = (function(KYT, Backbone){
    var Controller = {};

       Controller.showViews=function(splat,entityId, parentId,rootId,param1){
           var routeToken = _.find(KYT.routeTokens,function(item){
               return item.route == splat;
           });
           if(!routeToken)return;
           // this is so you don't set the id to the routetoken which stays in scope
           var viewOptions = $.extend({},routeToken);
           if(entityId) viewOptions.url +="/"+entityId;
           if(parentId) viewOptions.url +="?ParentId="+parentId;
           if(rootId) viewOptions.url +="&RootId="+rootId;
           if(param1) viewOptions.url +="&Param1="+param1;

            var item = new KYT.Views[routeToken.viewName](viewOptions);

           if(routeToken.isChild){
               var hasParent = KYT.WorkflowManager.addChildView(item);
               if(!hasParent){
                   KYT.WorkflowManager.cleanAllViews();
                   KYT.State.set({"currentView":item});
                   KYT.content.show(item);
               }
           }else{
               KYT.WorkflowManager.cleanAllViews();
               KYT.State.set({"currentView":item});
               KYT.content.show(item);
           }
       };

       KYT.addInitializer(function(){
       });

       return Controller;
   })(KYT, Backbone);
