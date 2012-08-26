/**
 * Created by JetBrains RubyMine.
 * User: Owner
 * Date: 2/26/12
 * Time: 11:22 AM
 * To change this template use File | Settings | File Templates.
 */
KYT.Routing.FieldsApp = (function(KYT, Backbone){
    var FieldsApp = {};

    // Router
    // ------
    FieldsApp.Router = Backbone.Marionette.AppRouter.extend({
          appRoutes: {
              "*path/:entityId/:parentId/:rootId/:string": "showViews",
              "*path/:entityId/:parentId/:rootId": "showViews",
              "*path/:entityId/:parentId": "showViews",
              "*path/:entityId": "showViews",
              "*path": "showViews"
          }
      });

    // Initialization
    // --------------

    // Initialize the router when the application starts
    KYT.addInitializer(function(){
        KYT.FieldsApp.router = new KYT.Routing.FieldsApp.Router({
            controller: KYT.Controller
        });
    });

    return FieldsApp;
})(KYT, Backbone);
