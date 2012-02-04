/**
 * Created by JetBrains RubyMine.
 * User: RHarik
 * Date: 10/7/11
 * Time: 3:17 PM
 * To change this template use File | Settings | File Templates.
 */

kyt.AjaxDisplayView = Backbone.View.extend({
    events:{
        'click .cancel' : 'cancel'
    },
    initialize: function(){
        this.options = $.extend({},kyt.displayDefaults,this.options);
        this.id=this.options.id;
        this.el=this.options.el;
    },
    render:function(){
        kyt.repository.ajaxGet(this.options.url, this.options.data, $.proxy(this.renderCallback,this));
    },
    renderCallback:function(result){
        if(result.LoggedOut){
            window.location.replace(result.RedirectUrl);
            return;
        }
        $(this.el).html(result);
        if(extraFormOptions){
            $.extend(true,this.options, extraFormOptions);
        }
        if(typeof this.options.runAfterRenderFunction == 'function'){
            this.options.runAfterRenderFunction.apply(this,[this.el]);
        }
        $.publish("/contentLevel/display_"+this.id+"/pageLoaded",[this.options]);
    },
    cancel:function(){
        $.publish("/contentLevel/display_"+this.id+"/cancel",[this.id]);
    }
});

kyt.displayDefaults = {
    id:"",
    data:{},
    runAfterRenderFunction: null
};