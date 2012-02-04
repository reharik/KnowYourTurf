/**
 * Created by JetBrains RubyMine.
 * User: Owner
 * Date: 11/5/11
 * Time: 4:10 PM
 * To change this template use File | Settings | File Templates.
 */


if (typeof kyt == "undefined") {
    var kyt = {};
}

kyt.WeatherController = kyt.CrudController.extend({
    events:_.extend({
    }, kyt.CrudController.prototype.events),
    addEditItem: function(url, data){
        var _url = url?url:this.options.addUpdateUrl;
        var builder = kyt.popupButtonBuilder.builder("editModule");
        builder.addButton("Return",kyt.popupCrud.buttonBuilder.getCancelFunc);
                    
        $("#masterArea").after("<div id='dialogHolder'/>");
        var moduleOptions = {
            id:"editModule",
            el:"#dialogHolder",
            url: _url,
            data:data,
            buttons:builder.getButtons()
        };
        this.modules.popupForm = new kyt.PopupFormModule(moduleOptions);
    }
});