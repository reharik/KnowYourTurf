/**
 * Created by .
 * User: RHarik
 * Date: 8/25/11
 * Time: 10:54 AM
 * To change this template use File | Settings | File Templates.
 */

if (typeof kyt == "undefined") {
            var kyt = {};
}

kyt.HomeController = kyt.Controller.extend({
    events:_.extend({
    }, kyt.Controller.prototype.events),

    initialize:function(){
        $.extend(this,this.defaults());
        this.delegateEvents();
        this.addressChange();
        this.registerSubscriptions();
        kyt.contentLevelControllers = {};
    },
    registerSubscriptions:function(){
        $.subscribe('/form_userProfile/cancel', $.proxy(this.userProfileCancel,this),this.cid);
    },
    delegateEvents: function(){
       //could screw up preformance.  necessary for aggressive ie caching
        $.ajaxSetup({
            cache: false
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
        // this and address should be put in there own header contorller
        $("#main-tabs li" ,"#main-header").click(function(e){
            $("#main-tabs li" ,"#main-header").removeClass("selected");
            $(e.currentTarget).addClass("selected");
            $("#userSettings","#main-header").removeClass("active");
            var url = $("a", e.currentTarget).attr("href");
            if(url){
                if(window.location.href.indexOf(url)>0){
                    window.location.reload();
                }else{
                    window.location.href = url;
                    if($("#main-content").hasClass("no-left-nav")){
                        $("#main-content").removeClass("no-left-nav");
                    }
                    $("#left-navigation").show();
                }
            }
        });
        $("#userSettings","#main-header").click($.proxy(function(){
            this.userProfileSettings();
            return false;
        },this));
    },
    userProfileSettings:function(){
        $("#main-content").addClass("no-left-nav");
        $("#contentInner").empty().append("<div id='masterArea'></div>");
        var profileOptions = {
            el:"#masterArea",
            userProfileUrl:this.options.userProfileUrl,
            id:"userProfileController"
        };
        this.modules["userProfileController"] = new kyt.UserProfileController(profileOptions);
        $("#userSettings","#main-header").addClass("active");
        $("#main-tabs li" ,"#main-header").removeClass("selected");
        $("#left-navigation").hide();
        return false;
    },
    userProfileCancel:function(){
       $("#userProfile").clearForm();
    },
    addressChange: function(){
        $.address.change(function(event) {
            var ccMenu = $(".ccMenu").data().ccMenu;
            var value = $.address.value();
            // hack for ie
            if(value.indexOf("/http:")==0){
                value = value.substring(1);
            }
            if(value=="na")return;
            if(value=="/") {
                ccMenu.resetDrilldownMenu();
                $("#contentArea").show().empty();
            }else{
                kyt.repository.ajaxGet(value,{},function(result){
                        $.each(kyt.contentLevelControllers,function(name,value){
                        if(value)
                        value.destroy();
                    });
                    kyt.contentLevelControllers = {};
                    
                    $("#contentInner").empty().append("<div id='masterArea'></div>");
                    $("#masterArea").html(result);
                    $.publish("/pageLoaded",[]);
                });
            }

            if($("ul.fg-menu-current").children("li").children("a[rel='"+value+"']").size() == 0){
                ccMenu.setMenuByUrl(value);
            }
        });
    }
});
