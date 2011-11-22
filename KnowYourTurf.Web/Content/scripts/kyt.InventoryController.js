/**
 * Created by JetBrains RubyMine.
 * User: Owner
 * Date: 11/6/11
 * Time: 6:04 PM
 * To change this template use File | Settings | File Templates.
 */
if (typeof kyt == "undefined") {
    var kyt = {};
}

kyt.InventoryController = kyt.CrudController.extend({
    events:_.extend({
    }, kyt.CrudController.prototype.events),
    displayItem: function(url, data){
        var _url = url?url:this.options.displayUrl;
        $("#masterArea").after("<div id='dialogHolder'/>");

        var builder = kyt.popupButtonBuilder.builder("displayModule");
        builder.addButton("Return", builder.getCancelFunc());
        var moduleOptions = {
            id:"displayModule",
            el:"#dialogHolder",
            url: _url,
            buttons: builder.getButtons()
        };
        this.modules.popupDisplay= new kyt.PopupDisplayModule(moduleOptions);
    },

});