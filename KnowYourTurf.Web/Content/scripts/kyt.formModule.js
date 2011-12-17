/**
 * Created by JetBrains RubyMine.
 * User: RHarik
 * Date: 10/10/11
 * Time: 9:17 AM
 * To change this template use File | Settings | File Templates.
 */

if (typeof kyt == "undefined") {
            var kyt = {};
}

kyt.FormModule  = kyt.Module.extend({
    events:_.extend({
    }, kyt.Module.prototype.events),

    initialize:function(){
            $.extend(this,this.defaults());

            this.registerSubscriptions();
            this.views.formView = new kyt.AjaxFormView(this.options);
            this.views.formView.render();
        },

        registerSubscriptions: function(){
            $.subscribe("/contentLevel/form_"+this.id+"/success", $.proxy(this.formSuccess,this), this.cid);
            $.subscribe("/contentLevel/form_"+this.id+"/cancel", $.proxy(this.formCancel,this), this.cid);
            this.registerAdditionalSubscriptions();
        },
        registerAdditionalSubscriptions:function(){},
        formSuccess:function(result){
            this.views.formView.remove();
            $.publish("/contentLevel/formModule_"+this.id+"/moduleSuccess",[result, this.id]);
        },
        formCancel: function(){
            this.views.formView.remove();
            $.publish("/contentLevel/formModule_"+this.id+"/moduleCancel",[this.id]);
        }
});