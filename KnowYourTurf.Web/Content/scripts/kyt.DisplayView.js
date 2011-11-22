/**
 * Created by JetBrains RubyMine.
 * User: RHarik
 * Date: 10/7/11
 * Time: 3:17 PM
 * To change this template use File | Settings | File Templates.
 */


kyt.DisplayView = Backbone.View.extend({
    events:{
        'click .cancel' : 'cancel'
    },
    initialize: function(){
        this.options = $.extend({},kyt.formDefaults,this.options);
        this.id=this.options.id;

        if(extraFormOptions){
            $.extend(true, this.options, extraFormOptions);
        }
        this.render();

    },
    render: function(){
        $.publish("/form_"+this.id+"/pageLoaded",[this.options]);
        return this;
    },
    cancel:function(){
        $.publish("/form_"+this.id+"/cancel",[this.id]);
    }
});



kyt.AjaxDisplayView = Backbone.View.extend({
    initialize: function(){
        this.options = $.extend({},kyt.formDefaults,this.options);
        this.id=this.options.id;
        kyt.repository.ajaxGet(this.options.url, this.options.data, $.proxy(this.initializeCallback,this));
    },

    initializeCallback:function(result){
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
        
        this.render();
    },

    render: function(){
        $.publish("/display_"+this.id+"/pageLoaded",[this.options]);
        return this;
    }
});