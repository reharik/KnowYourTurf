/**
 * Created by JetBrains RubyMine.
 * User: Owner
 * Date: 2/26/12
 * Time: 11:14 AM
 * To change this template use File | Settings | File Templates.
 */

KYT.FieldsApp = (function(KYT, Backbone){
    var FieldsApp = {};

    //show user settings and hide the menu
    FieldsApp.show = function(){
        FieldsApp.Menu.show();
        KYT.vent.trigger("route", "employeedashboard",true);
    };

    KYT.State.bind("change:application", function(e,f,g){
        if(KYT.State.get("application")=="fields") FieldsApp.show();
    });

    KYT.addInitializer(function(){
    });

    return FieldsApp;
})(KYT, Backbone);
