/**
 * Created by JetBrains RubyMine.
 * User: Owner
 * Date: 7/15/12
 * Time: 10:40 AM
 * To change this template use File | Settings | File Templates.
 */

CC.Elements = {};
CC.Elements.Textbox = function($container){
    this.$container = $container;
    this.trimFieldName = function(){
        var name = this.$container.find("label").text();
        name = name.replace(":","");
        name = name.replace("*","");
        return $(name).trim();
    }
};
$.extend(CC.Elements.Textbox.prototype,{
    init:function(){
        var that = this;
        this.cid = _.uniqueId("c");
        this.$input = this.$container.find("[name='"+this.name+"']");
        this.$el = this.$input.closest(".KYT_editor_root");
        this.fieldName = this.trimFieldName();
        this.$input.on("keyup",function(){that.validate(that);});
        this.value = this.$input.val();
    },
    validate:function(element){
        CC.ValidationRunner.run(element);
        var x = "";
    }
});