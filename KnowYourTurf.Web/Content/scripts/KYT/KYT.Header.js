/**
 * Created by JetBrains RubyMine.
 * User: Owner
 * Date: 2/26/12
 * Time: 11:04 AM
 * To change this template use File | Settings | File Templates.
 */

KYT.Header = (function(KYT, Backbone){
    var Header = {};

    Header.HeaderView = Backbone.View.extend({
        events:{
            'click #userSettings' : 'userSettings'
        },
        initialize: function(){
            this.setupAppSelectionEvents();
            this.setupGlobalSettings();
        },
        setupGlobalSettings:function(){
          $("a[rel^='prettyPhoto']").live('mouseover', function()
            {
                if (!$(this).data('init'))
                {
                    $(this).data('init', true);
                    $(this).prettyPhoto({theme: 'light_rounded', show_title:false,deeplinking:false});
                    $(this).trigger('mouseover');
                }
            });  
        },
        userSettings:function(e){
            e.preventDefault();
            KYT.State.set({"application":"userSettings"});
            KYT.vent.trigger("route","usersettings",true);
        },
        setupAppSelectionEvents: function(){
            var self = this;

            //when the application is hit, either through the url
            //or through the application somehow, make sure the
            // header is showing the right state
            KYT.State.bind("change:application", function(){
                self.setSelection(KYT.State.get("application"));
            });
        },
        // makes the header show the correct menu item as being selected
        setSelection: function(app){
            this.$("#main-tabs li").removeClass("selected");
            this.$("#userSettings").removeClass("active");

            if(app == "userSettings"){
                this.$("#userSettings").addClass("active");
            }
        }

    });

    // Initialize the header functionality when the
    // application starts.
    KYT.addInitializer(function(){
        Header.view = new Header.HeaderView({
            el: $("#main-header")
        });
        // dont' really need to add this to the KYT.header.show() here
        // cuz there will only ever be one header but if we do you need a
        // surrounding div for main-header and set the KYT.header to that

        // probably trigger the router for portfolio dashboard here
        // but maybe not cuz portapp may not be loaded yet
    });

    return Header;
})(KYT, Backbone);