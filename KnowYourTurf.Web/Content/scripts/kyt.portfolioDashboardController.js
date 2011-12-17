/**
 * Created by JetBrains RubyMine.
 * User: RHarik
 * Date: 9/22/11
 * Time: 2:09 PM
 * To change this template use File | Settings | File Templates.
 */

if (typeof kyt == "undefined") {
            var kyt = {};
}

kyt.PortfolioDashboardController  = kyt.Controller.extend({
    events:_.extend({
        "click #buildNewPortfolio":"buildNewPortfolio",
        "click #addCoverText":"addCoverText",
        "click #addReflections":"addReflections",
        "click #updatePortfolio":"updatePortfolio",
        "click #addItems":"addItems",
        "click #addFiles":"addFiles"
    }, kyt.Controller.prototype.events),

    initialize:function(){
        $.extend(this,this.defaults());

        kyt.contentLevelControllers["portfolioDashboardController"]=this;
        $.unsubscribeByPrefix("/contentLevel");
        $.unsubscribeByPrefix("/pageLoaded");
    },
    buildNewPortfolio:function(){
        $.subscribe("/pageLoaded", $.proxy(function(){
            $.publish('/contentLevel/grid/AddUpdateItem', [this.options.portfolioAddUpdatetUrl]);
            $.unsubscribe("/pageLoaded")
        },this));
        $.address.value(this.options.portfolioListUrl);
    },
    addCoverText:function () {
        $.subscribe("/pageLoaded", function () {
            $.publish('/contentLevel/grid/AddUpdateItem', [this.options.coverTextAddUpdateUrl]);
            $.unsubscribe("/pageLoaded")
        });
        $.address.value(this.options.coverTextListUrl);
    },
    addReflections:function () {
        $.subscribe("/pageLoaded", function () {
            $.publish('/contentLevel/grid/AddUpdateItem', [this.options.reflectionAddUpdateUrl]);
            $.unsubscribe("/pageLoaded")
        });
        $.address.value(this.options.reflectionListUrl);
    },
    updatePortfolio: function(){
        $.address.value(this.options.portfolioListUrl);
    },
    addItems:function(){
         var ccMenu = $(".ccMenu").data().ccMenu;
         ccMenu.goToItem(this.options.addItemsMenuName);
    },
    addFiles:function(){$.address.value(this.options.filesListUrl);}
});