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
  $.ajaxSetup({
        cache: false,
        complete:function(){KYT.showThrob = false; $("#ajaxLoading").hide();},
        beforeSend:function(){setTimeout(function() {if(KYT.showThrob) $("#ajaxLoading").show(); }, 500)}
    });
    $("#ajaxLoading").hide();

    jQuery.validator.addMethod("number", function(value, element) {
        return this.optional(element) || /^-?(?:\d+|\d{1,3}(?:,\d{3})+)?(?:\.\d+)?$/.test(value);
    }, "Please enter a valid number.");

    // the events delegate doesn't seem to be picking up focusin
    $(".datePicker").live('focusin', function() {
        var $this = $(this);
        $this.datepicker({ changeYear: true, changeMonth: true });
    });

     $(".timePicker").live('focusin', function() {
        var $this = $(this);
        $this.timepicker({
            showPeriod: true,
            showLeadingZero: false
        });
    });

    KYT.vent.bind("route",function(route,triggerRoute){
        KYT.Routing.showRoute(route,triggerRoute);
    });
});

KYT.bind("initialize:after", function(){
    KYT.State.set({"application":"fields"});
    if (Backbone.history){
        Backbone.history.start();
    }
});

// calling start will run all of the initializers
// this can be done from your JS file directly, or
// from a script block in your HTML
$(function(){
  KYT.start();
});