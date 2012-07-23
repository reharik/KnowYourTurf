/**
 * Created by JetBrains RubyMine.
 * User: Owner
 * Date: 7/15/12
 * Time: 10:40 AM
 * To change this template use File | Settings | File Templates.
 */

if (typeof CC == "undefined") {
    var CC = {};
}
CC.Elements={};



CC.Elements.Element = function($container){
     this.$container = $container;
     this.trimFieldName = function(){
        var name = this.$container.find("label").text();
        name = name.replace(":","");
        name = name.replace("*","");
        return $.trim(name);
    }
};
CC.Elements.Element.extend = Backbone.View.extend;
$.extend(CC.Elements.Element.prototype,{
    init:function(){
        this.cid = _.uniqueId("c");
        this.type = "Element";
        this.fieldName = this.trimFieldName();
    },
    validate:function(){
        CC.ValidationRunner.runElement(this);
    },
    getValue:function(){
        return this.$input.val();
    },
    setValidState: function(isValid){
        if(isValid){
            this.$input.removeClass("invalid");
            if(!this.$input.hasClass("valid"))
            this.$input.addClass("valid")
        }else{
            this.$input.removeClass("valid");
            if(!this.$input.hasClass("invalid"))
            this.$input.addClass("invalid")
        }
        this.isValid = isValid;
    }
});

CC.Elements.Textbox = CC.Elements.Element.extend({
    init:function(){
        var that = this;
        this._super("init",arguments);
        this.type = "textbox";
        this.$input = this.$container.find("input");
        $(this.$input).on("keyup",function(){that.validate();});
    }
});

CC.Elements.Password= CC.Elements.Element.extend({
    init:function(){
        var that = this;
        this._super("init",arguments);
        this.type = "textbox";
        this.$input = this.$container.find("input");
        $(this.$input).on("keyup",function(){that.validate();});
    }
});

CC.Elements.Select = CC.Elements.Element.extend({
    init:function(){
        var that = this;
        this._super("init",arguments);
        this.type = "select";
        this.$input = this.$container.find("select");
        this.$input.chosen();
        this.$input.chosen().change(function(){that.validate()});
    }
});

_.each(["Element", "Textbox"], function(klass) {
    CC.Elements[klass].prototype._super = Backbone.View.prototype._super;
});