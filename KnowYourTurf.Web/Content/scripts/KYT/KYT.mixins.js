/**
 * Created by JetBrains RubyMine.
 * User: Owner
 * Date: 8/5/12
 * Time: 9:53 AM
 * To change this template use File | Settings | File Templates.
 */
KYT.mixins = {};

KYT.mixin = function(target, mixin, preserveRender){

    var mixinObj = KYT.mixins[mixin];
    for (var prop in mixinObj) {
        if (mixinObj.hasOwnProperty(prop) && !target[prop] )
            if(prop=="render"&&preserveRender==true) {
                return;
            }
            target[prop] = mixinObj[prop];
    }
//
//    if(target.events && KYT.mixins[mixin].events){
//        var events = $.extend({}, target.events, KYT.mixins[mixin].events)
//    }
//
//    $.extend(target,KYT.mixins[mixin],overrides||{});
//
//
//    if(events){
//        target.events = events;
//    }
};

KYT.mixins.modelAndElementsMixin = {
    bindModelAndElements:function(arrayOfIgnoreItems){
        // make sure to apply ids prior to ko mapping.
        var that = this;
        this.mappingOptions ={ ignore:[] };
        if(arrayOfIgnoreItems){
             _.each(arrayOfIgnoreItems,function(item){
                 that.mappingOptions.ignore.push(item);});
        }
        this.model = ko.mapping.fromJS(this.rawModel,this.mappingOptions);
        this.extendModel();
        ko.applyBindings(this.model,this.el);
        this.elementsViewmodel = CC.elementService.getElementsViewmodel(this);
        var ignore = _.filter(_.keys(this.model),function(item){
            return (item.indexOf('_') == 0 && item != "__ko_mapping__");
        });
        _.each(ignore,function(item){
            that.mappingOptions.ignore.push(item);});
        this.mappingOptions.ignore.push("_availableItems");
        this.mappingOptions.ignore.push("_resultsItems");
        this.addIdsToModel();

    },
    extendModel:function(){
        this.model._createdText = ko.computed(function() {
            if(this.model.EntityId()>0 && this.model.DateCreated()){
                return "Added " + new XDate(this.model.DateCreated()).toString("MMM d, yyyy");
            }
            return "";
        }, this);
        this.model._title = ko.computed(function(){
            if(this.model.EntityId()<=0 && this.model._Title()){
                return "Add New "+this.model._Title();
            }
            return this.model._Title() ? this.model._Title() : "";
        },this);
    },
    addIdsToModel:function(){
        var rel = KYT.State.get("Relationships");
        if(!rel){return;}
        this.model.EntityId(rel.entityId);
        this.model.ParentId(rel.parentId);
        this.model.RootId(rel.rootId);
        this.model.Var(rel.extraVar);
    }
};

KYT.mixins.formMixin = {
    events:{
        'click #save' : 'saveItem',
        'click #cancel' : 'cancel'
    },

    saveItem:function(){
        var isValid = CC.ValidationRunner.runViewModel(this.elementsViewmodel);
        if(!isValid){return;}
        var data;
        var fileInputs = $('input:file', this.$el);
        if(fileInputs.length > 0 && _.any(fileInputs, function(item){return $(item).val();})){
            var that = this;
            data = ko.mapping.toJS(this.model,this.mappingOptions);
            var ajaxFileUpload = new CC.AjaxFileUpload(fileInputs[0],{
                action:that.model._saveUrl(),
                onComplete:function(file,response){that.successHandler(response);}
            });
            ajaxFileUpload.setData(data);
            ajaxFileUpload.submit()
        }
        else{
            data = JSON.stringify(ko.mapping.toJS(this.model,this.mappingOptions));
            var promise = KYT.repository.ajaxPostModel(this.model._saveUrl(),data);
            promise.done($.proxy(this.successHandler,this));
        }
    },
    cancel:function(){
        KYT.vent.trigger("form:"+this.id+":cancel");
        if(!this.options.noBubbleUp) {KYT.WorkflowManager.returnParentView();}
    },
    successHandler:function(_result){
        var result = typeof _result =="string" ? JSON.parse(_result) : _result;
        if(!CC.notification.handleResult(result,this.cid)){
            return;
        }
        KYT.vent.trigger("form:"+this.id+":success",result);
        if(!this.options.noBubbleUp){KYT.WorkflowManager.returnParentView(result,true);}
    }
};

KYT.mixins.ajaxFormMixin = {
    render:function(){
        $.when(KYT.loadTemplateAndModel(this))
         .done($.proxy(this.renderCallback,this));
    },
    renderCallback:function(){
        this.bindModelAndElements();
        this.viewLoaded();
        KYT.vent.trigger("form:"+this.id+":pageLoaded",this.options);
        $(this.el).find("form :input:visible:enabled:first").focus();
    }
};
