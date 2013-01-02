/**
 * Created by .
 * User: Harik
 * Date: 11/27/11
 * Time: 2:29 PM
 * To change this template use File | Settings | File Templates.
 */


kyt.TrainerController = kyt.CrudController.extend({
    events:_.extend({
    }, kyt.CrudController.prototype.events),

    registerAdditionalSubscriptions:function(){
        $.subscribe('/contentLevel/form_mainForm/pageLoaded', $.proxy(this.loadTokenizers,this), this.cid);
        $.subscribe('/contentLevel/form_mainForm/pageLoaded', $.proxy(this.loadPlugins,this), this.cid);
    },

    loadTokenizers: function(formOptions){
        var options = $.extend({},formOptions,{el:"#userRoles"});
        this.views.roles = new kyt.TokenView(options);
    },
    loadPlugins:function(){
        $('#color',"#detailArea").miniColors();
    }
});
