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
        if(prop=="render"){
            if(preserveRender!=true){
                target[prop] = mixinObj[prop];
            }
        }else if(!target[prop] && mixinObj.hasOwnProperty(prop)){
            target[prop] = mixinObj[prop];
        }
    }
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
        KYT.vent.trigger("model:"+this.id+"modelLoaded");
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

KYT.mixins.reportMixin = {
    events:{
        'click #viewReport' : 'viewReport'
    },
    successSelector:"#messageContainer",
    errorSelector:"#messageContainer",
    viewReport:function(){
        var isValid = CC.ValidationRunner.runViewModel(this.cid, this.elementsViewmodel,this.errorSelector);
        if(!isValid){return;}
        var url = this.createUrl();
        $("#reportBody").attr("src",url);
    },
    createUrl:function(data){
    }
}

KYT.mixins.formMixin = {
    events:{
        'click #save' : 'saveItem',
        'click #cancel' : 'cancel'
    },
    successSelector:"#messageContainer",
    errorSelector:"#messageContainer",
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
        if(!result.Success){
            if(result.Message && !$.noty.getByViewIdAndElementId(this.cid)){
                $(this.errorSelector).noty({type: "error", text: result.Message, viewId:this.cid});
            }
            if(result.Errors && !$.noty.getByViewIdAndElementId(this.cid)){
                _.each(result.Errors,function(item){
                    $(this.errorSelector).noty({type: "error", text:item.ErrorMessage, viewId:this.cid});
                })
            }
        }else{
            if(result.Message){
                var note = $(this.successSelector).noty({type: "success", text:result.Message, viewId:this.cid});
                note.setAnimationSpeed(1000);
                note.setTimeout(3000);
                $.noty.closeAllErrorsByViewId(this.cid);
            }
            KYT.vent.trigger("form:"+this.id+":success",result);
            if(!this.options.noBubbleUp){KYT.WorkflowManager.returnParentView(result,true);}
        }
    }

};

KYT.mixins.displayMixin = {
    events:{
        'click #cancel' : 'cancel'
    },
    cancel:function(){
        KYT.vent.trigger("display:"+this.id+":cancel");
        if(!this.options.noBubbleUp) {KYT.WorkflowManager.returnParentView();}
    }
};


KYT.mixins.ajaxDisplayMixin = {
    render:function(){
        $.when(KYT.loadTemplateAndModel(this))
         .done($.proxy(this.renderCallback,this));
    },
    renderCallback:function(){
        this.bindModelAndElements();
        this.viewLoaded();
        KYT.vent.trigger("display:"+this.id+":pageLoaded",this.options);
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
    }
};

KYT.mixins.ajaxGridMixin = {
    render:function(){
        KYT.repository.ajaxGet(this.options.url, this.options.data)
            .done($.proxy(this.renderCallback,this));
    },
    renderCallback:function(result){
        $(this.el).html($("#gridTemplate").tmpl(result));
        $.extend(this.options,result,KYT.gridDefaults);
        this.setupGrid();
        this.viewLoaded();
        KYT.vent.trigger("grid:"+this.id+":pageLoaded",this.options);

    }
};

KYT.mixins.setupGridMixin = {
    setupGrid: function() {
        $.each(this.options.headerButtons, $.proxy(function(i, item) {
            $(this.el).find("." + item).show();
        }, this));
        // if we have more then one grid, jqgrid doesn't scope so we need different names.
        if (this.options.gridId) {
            this.$el.find("#gridContainer").attr("id", this.options.gridId);
        } else {
            this.options.gridId = "gridContainer";
        }

        $("#" + this.options.gridId, this.el).AsGrid(this.options.gridDef, this.options.gridOptions);
        ///////
        $(this.el).gridSearch({onClear:$.proxy(this.removeSearch, this),onSubmit:$.proxy(this.search, this)});
    }
};

KYT.mixins.defaultGridEventsMixin = {
    events: {
        'click .new': 'addNew',
        'click .delete': 'deleteItems'
    },
    setupBindings: function () {
        KYT.vent.bind(this.options.gridId + ":AddUpdateItem", this.editItem, this);
        KYT.vent.bind(this.options.gridId + ":DisplayItem", this.displayItem, this);
        if ($.isFunction(this._setupBindings)) {
            this._setupBindings();
        }
    },
    unbindBindings: function () {
        KYT.vent.unbind(this.options.gridId + ":AddUpdateItem", this.editItem, this);
        KYT.vent.unbind(this.options.gridId + ":DisplayItem", this.displayItem, this);
        if ($.isFunction(this._unbindBindings)) {
            this._unbindBindings();
        }
    },
    addNew: function () {
        KYT.vent.trigger("route", KYT.generateRoute(this.options.addUpdate), true);
    },
    editItem: function (id) {
        KYT.vent.trigger("route", KYT.generateRoute(this.options.addUpdate, id), true);
    },
    displayItem: function (id) {
        KYT.vent.trigger("route", KYT.generateRoute(this.options.display, id), true);
    },
    deleteItems: function () {
        if (confirm("Are you sure you would like to delete this Item?")) {
            var ids = cc.gridMultiSelect.getCheckedBoxes(this.options.gridId);
            KYT.repository.ajaxGet(this.options.deleteMultipleUrl, $.param({ "EntityIds": ids }, true))
                .done($.proxy(function () { this.reloadGrid() }, this));
        }
    },
    reloadGrid: function () {
        KYT.vent.unbind(this.options.gridId + ":AddUpdateItem", this.editItem, this);
        KYT.vent.unbind(this.options.gridId + ":DisplayItem", this.displayItem, this);
        $("#" + this.options.gridId).trigger("reloadGrid");
        KYT.vent.bind(this.options.gridId + ":AddUpdateItem", this.editItem, this);
        KYT.vent.bind(this.options.gridId + ":DisplayItem", this.displayItem, this);
    },
    // used by children to update parent grid
    callbackAction: function () {
        this.reloadGrid();
    }
};

KYT.mixins.setupGridSearchMixin = {
    search:function(v){
        var searchItem = {"field": this.options.searchField ,"data": v };
        var filter = {"group":"AND",rules:[searchItem]};
        var obj = {"filters":""  + JSON.stringify(filter) + ""};
        $("#"+this.options.gridId).jqGrid('setGridParam',{postData:obj});
        this.reloadGrid();
    },
    removeSearch:function(){
        delete $("#"+this.options.gridId).jqGrid('getGridParam' ,'postData')["filters"];
        this.reloadGrid();
        return false;
    }
};

