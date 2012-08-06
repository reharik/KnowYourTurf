/**
 * Created by JetBrains RubyMine.
 * User: Owner
 * Date: 2/26/12
 * Time: 11:11 AM
 * To change this template use File | Settings | File Templates.
 */

KYT.Views = {};
// use like this
// this._super("testMethod",arguments);

(function(Backbone) {

  // The super method takes two parameters: a method name
  // and an array of arguments to pass to the overridden method.
  // This is to optimize for the common case of passing 'arguments'.
  function _super(methodName, args) {

    // Keep track of how far up the prototype chain we have traversed,
    // in order to handle nested calls to _super.
    this._superCallObjects || (this._superCallObjects = {});
    var currentObject = this._superCallObjects[methodName] || this,
        parentObject  = findSuper(methodName, currentObject);
    this._superCallObjects[methodName] = parentObject;
    if(parentObject[methodName]){
        var result = parentObject[methodName].apply(this, args || []);
    }
    delete this._superCallObjects[methodName];
    return result;
  }

  // Find the next object up the prototype chain that has a
  // different implementation of the method.
  function findSuper(methodName, childObject) {
    var object = childObject;
    while (object[methodName] === childObject[methodName]) {
      object = object.constructor.__super__;
    }
    return object;
  }

  _.each(["Model", "Collection", "View", "Router"], function(klass) {
    Backbone[klass].prototype._super = _super;
  });


})(Backbone);

KYT.Views.View = Backbone.View.extend({
    // Remove the child view and close it
    removeChildView: function(item){
      var view = this.children[item.cid];
      if (view){
        view.close();
        delete this.children[item.cid];
      }
    },

    // Store references to all of the child `itemView`
    // instances so they can be managed and cleaned up, later.
    storeChild: function(view){
      if (!this.children){
        this.children = {};
      }
      this.children[view.cid] = view;
    },

    // Handle cleanup and other closing needs for
    // the collection of views.
    close: function(){
      this.unbind();
      //this.unbindAll();
      if(this.elementsViewmodel){
        this.elementsViewmodel.destroy();
      }
      this.remove();

      this.closeChildren();

      if (this.onClose){
        this.onClose();
      }
    },

    closeChildren: function(){
      if (this.children){
        _.each(this.children, function(childView){
          childView.close();
        });
      }
    },

    viewLoaded:function(){
    }
  });


KYT.Views.FormView = KYT.Views.View.extend({
    initialize: function(){
        KYT.mixin(this, "formMixin");
        KYT.mixin(this, "modelAndElementsMixin");
    },
    render: function(){
        this.bindModelAndElements();
        this.viewLoaded();
        KYT.vent.trigger("form:"+this.id+":pageLoaded",this.options);
        return this;
    }
});

KYT.Views.AjaxFormView = KYT.Views.View.extend({
    initialize: function(){
        KYT.mixin(this, "formMixin");
        KYT.mixin(this, "ajaxFormMixin");
        KYT.mixin(this, "modelAndElementsMixin");
    }
});

KYT.Views.AjaxDisplayView = KYT.Views.View.extend({
    initialize:function(){
        KYT.mixin(this, "baseFormView");
        KYT.mixin(this, "ajaxFormMixin");
    },
    render:function(){
        KYT.repository.ajaxGet(this.options.url, this.options.data).done($.proxy(this.renderCallback));
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
         //callback for render
        this.viewLoaded();
        KYT.vent.trigger("display:"+this.id+":pageLoaded",this.options);
    },
    cancel:function(){
        KYT.vent.trigger("display:"+this.id+":cancel",this.id);
        if(!this.options.noBubbleUp)
            KYT.WorkflowManager.returnParentView();
    }
});

KYT.Views.GridView = KYT.Views.View.extend({
    events:{
        'click .new' : 'addNew',
        'click .delete' : 'deleteItems'
    },
    initialize: function(){
        this.options = $.extend({},KYT.gridDefaults,this.options);
    },
    render:function(){
        KYT.repository.ajaxGet(this.options.url, this.options.data)
            .done($.proxy(this.renderCallback,this));
    },
    renderCallback:function(result){
        result.errorMessagesContainer = this.options.errorMessagesContainer;
        $(this.el).html($("#gridTemplate").tmpl(result));
        $.extend(this.options,result);

        $.each(this.options.headerButtons,$.proxy(function(i,item){
            $(this.el).find("."+item).show();
        },this));

        var gridContainer = this.options.gridContainer;
        // if we have more then one grid, jqgrid doesn't scope so we need different names.
        if(this.options.gridContainer!="#gridContainer"){
            this.$el.find("#gridContainer").attr("id",this.options.gridContainer.replace("#",""));
        }

        $(gridContainer,this.el).AsGrid(this.options.gridDef, this.options.gridOptions);
        ///////
        $(this.el).gridSearch({onClear:$.proxy(this.removeSearch,this),onSubmit:$.proxy(this.search,this)});
        this.viewLoaded();
        KYT.vent.trigger("grid:"+this.id+":pageLoaded",this.options);
        KYT.vent.bind(this.id+":AddUpdateItem",this.editItem,this);
        KYT.vent.bind(this.id+":DisplayItem",this.displayItem,this);
    },
    addNew:function(){
        KYT.vent.trigger("route",KYT.generateRoute(this.options.addUpdate),true);
    },
    editItem:function(id){
        KYT.vent.trigger("route",KYT.generateRoute(this.options.addUpdate,id), true);
    },
    displayItem:function(id){
        KYT.vent.trigger("route",KYT.generateRoute(this.options.display,id), true);
    },
    deleteItems:function(){
        if (confirm("Are you sure you would like to delete this Item?")) {
            var ids = cc.gridMultiSelect.getCheckedBoxes();
            KYT.repository.ajaxGet(this.options.deleteMultipleUrl, $.param({"EntityIds":ids},true))
                .done($.proxy(function(){this.reloadGrid()},this));
        }
    },
    search:function(v){
        var searchItem = {"field": this.options.searchField ,"data": v };
        var filter = {"group":"AND",rules:[searchItem]};
        var obj = {"filters":""  + JSON.stringify(filter) + ""};
        $(this.options.gridContainer).jqGrid('setGridParam',{postData:obj});
        this.reloadGrid();
    },
    removeSearch:function(){
        delete $(this.options.gridContainer).jqGrid('getGridParam' ,'postData')["filters"];
        this.reloadGrid();
        return false;
    },
    reloadGrid:function(){
        KYT.vent.unbind(this.id+":AddUpdateItem",this.editItem,this);
        KYT.vent.unbind(this.id+":DisplayItem",this.displayItem,this);
        $(this.options.gridContainer).trigger("reloadGrid");
        KYT.vent.bind(this.id+":AddUpdateItem",this.editItem,this);
        KYT.vent.bind(this.id+":DisplayItem",this.displayItem,this);
    },
    callbackAction:function(){
        this.reloadGrid();
    },
    onClose:function(){
        KYT.vent.unbind("AddUpdateItem");
        KYT.vent.unbind("DisplayItem");
    }
});

KYT.Views.AjaxPopupFormModule  = KYT.Views.View.extend({
    initialize:function(){
        this.registerSubscriptions();
    },

    render: function(){
            this.options.noBubbleUp=true;
            this.options.isPopup=true;
        this.popupForm = this.options.view ? new KYT.Views[this.options.view](this.options) : new KYT.Views.AjaxFormView(this.options);
        this.popupForm.render();
        this.storeChild(this.popupForm);
        $(this.el).append(this.popupForm.el);
    },
    registerSubscriptions: function(){
       KYT.vent.bind("form:"+this.id+":pageLoaded", this.loadPopupView, this);
        KYT.vent.bind("popup:"+this.id+":cancel", this.popupCancel, this);
        KYT.vent.bind("popup:"+this.id+":save", this.formSave, this);
        KYT.vent.bind("form:"+this.id+":success", this.formSuccess, this);
    },
    onClose:function(){
         KYT.vent.unbind("form:"+this.id+":pageLoaded");
         KYT.vent.unbind("popup:"+this.id+":cancel");
        KYT.vent.unbind("popup:"+this.id+":save");
        KYT.vent.unbind("form:"+this.id+":success");
    },
    loadPopupView:function(formOptions){
        var buttons = formOptions.buttons?formOptions.buttons:KYT.Views.popupButtonBuilder.builder(formOptions.id).standardEditButons();
        var popupOptions = {
            id:this.id,
            el:this.el, // we pass the el here so we can call the popup on it
            buttons: buttons,
            title:formOptions.title
        };
        var view = new KYT.Views.PopupView(popupOptions);
        view.render();
        this.storeChild(view);
    },
    formSave:function(){
        this.popupForm.saveItem();
    },
    //just catching and re triggering with the module name
    formSuccess:function(result){
        KYT.vent.trigger("ajaxPopupFormModule:"+this.id+":success",result);
    },
    popupCancel:function(){
        KYT.vent.trigger("ajaxPopupFormModule:"+this.id+":cancel",[]);
    }
});

KYT.Views.AjaxPopupDisplayModule  = KYT.Views.View.extend({
    initialize:function(){
        this.registerSubscriptions();
    },
    render: function(){
        this.popupDisplay = this.options.view ? new this.options.view(this.options) : new KYT.Views.AjaxDisplayView(this.options);
        this.popupDisplay.render();
        this.storeChild(this.popupDisplay);
        $(this.el).append(this.popupDisplay.el);
    },

    registerSubscriptions: function(){
        KYT.vent.bind("display:"+this.id+":pageLoaded", this.loadPopupView, this);
        KYT.vent.bind("popup:"+this.id+":cancel", this.popupCancel, this);
    },
    onClose:function(){
         KYT.vent.unbind("display:"+this.id+":pageLoaded");
         KYT.vent.unbind("popup:"+this.id+":cancel");
    },

    loadPopupView:function(formOptions){
        var buttons = formOptions.buttons?formOptions.buttons:KYT.Views.popupButtonBuilder.builder(formOptions.id).standardEditButons();
        var popupOptions = {
            id:this.id,
            el:this.el, // we pass the el here so we can call the popup on it
            buttons: buttons,
            width:this.options.popupWidth,
            title:formOptions.title
        };
        var view = new KYT.Views.PopupView(popupOptions);
        view.render();
        this.storeChild(view);
    },
    popupCancel:function(){
        KYT.vent.trigger("module:"+this.id+":cancel",[]);
    }
});

KYT.Views.PopupView = KYT.Views.View.extend({
    render:function(){
        $(".ui-dialog").remove();
        var errorMessages = $("div[id*='errorMessages']", this.el);
        if(errorMessages){
            var id = errorMessages.attr("id");
            errorMessages.attr("id","errorMessagesPU").removeClass(id).addClass("errorMessagesPU");
        }

        $(this.el).dialog({
            modal: true,
            width: this.options.width||550,
            buttons:this.options.buttons,
            title: this.options.title,
            close:function(){
                KYT.vent.trigger("popup:"+id+":cancel");
            }
        });
        return this;
    },
    close:function(){
        $(this.el).dialog("close");
    }
});


KYT.Views.TemplatedPopupView = KYT.Views.View.extend({

    initialize: function(){
        this.options = $.extend({},KYT.opupDefaults,this.options);
    },
    render:function(){
        $(this.el).append($(this.options.template).tmpl(this.options.data));
        var popupOptions = {
            id:this.id,
            el:this.el, // we pass the el here so we can call the popup on it
            buttons: this.options.buttons,
            width:this.options.popupWidth,
            title:this.options.title
        };
        var view = new KYT.Views.PopupView(popupOptions);
        view.render();
        this.storeChild(view);

    }
});



KYT.Views.popupButtonBuilder = (function(){
    return {
        builder: function(id){
        var buttons = {};
        var _addButton = function(name,func){ buttons[name] = func; };
        var saveFunc = function() {
            KYT.vent.trigger("popup:"+id+":save");
        };
        var editFunc = function(event) {KYT.vent.trigger("popup:"+id+":edit");};
        var cancelFunc = function(){
                            KYT.vent.trigger("popup:"+id+":cancel");
                            $(this).dialog("close");
                            $(".ui-dialog").remove();
                        };
        return{
            getButtons:function(){return buttons;},
            getSaveFunc:function(){return saveFunc;},
            getCancelFunc:function(){return cancelFunc;},
            addSaveButton:function(){_addButton("Save",saveFunc); return this},
            addEditButton:function(){_addButton("Edit",editFunc);return this},
            addCancelButton:function(){_addButton("Cancel",cancelFunc);return this},
            addButton:function(name,func){_addButton(name,func);return this},
            clearButtons:function(){buttons = {};return this},
            standardEditButons: function(){
                _addButton("Save",saveFunc);
                _addButton("Cancel",cancelFunc);
                return buttons;
            },
            standardDisplayButtons: function(){
                _addButton("Cancel",cancelFunc);
                return buttons;
            }
        };
    }
    }
}());

KYT.Views.TokenizerModule = KYT.Views.View.extend({
    render:function() {
        this.registerSubscriptions();
        this.tokenView = new KYT.Views.TokenView(this.options);
        this.storeChild(this.tokenView);
        },
    registerSubscriptions: function() {
        KYT.vent.bind("token:" + this.id + ":addUpdate", this.addUpdateItem, this);
        KYT.vent.bind("ajaxPopupFormModule:" + this.id + ":success", this.formSuccess, this);
        KYT.vent.bind("ajaxPopupFormModule:" + this.id + ":cancel", this.formCancel, this);
    },
    onClose:function(){
         KYT.vent.unbind("token:" + this.id + ":addUpdate");
         KYT.vent.unbind("ajaxPopupFormModule:" + this.id + ":success");
        KYT.vent.unbind("ajaxPopupFormModule:" + this.id + ":cancel");
    },
//from tolkneizer
    addUpdateItem:function() {
        var options = {
            id: this.id,
            url: this.options.tokenizerUrls[this.id + "AddUpdateUrl"],
            crudFormOptions: { errorContainer:"#errorMessagesPU",successContainer:"#errorMessagesForm"}
        };
        this.popupModule = new KYT.Views.AjaxPopupFormModule(options);
        this.popupModule.render();
        this.storeChild(this.popupModule);
        $(this.el).append(this.popupModule.el);
    },
    formSuccess:function(result) {
        this.tokenView.successHandler(result);
        this.popupModule.close();
        this.formCancel();
        return false;
    },
    formCancel:function() {
        this.popupModule.close();
        return false;
// $.publish("/contentLevel/tokenizer_" + this.id + "/formCancel",[this.id]);
    }
});

KYT.Views.TokenView = KYT.Views.View.extend({
    events:{
        'click #addNew' : 'addNew'
    },
    initialize:function(){
        this.options = $.extend({},KYT.tokenDefaults,this.options);
    },
    render: function(){

        if(!this.options.availableItems || this.options.availableItems.length==0) {
            $("#noAssets",this.el).show();
            $("#hasAssets",this.el).hide();
        }else{
            $("#noAssets",this.el).hide();
            $("#hasAssets",this.el).show();
            this.inputSetup();
        }
    },
    inputSetup:function(){
        $(this.options.inputSelector, this.el).tokenInput(this.options.availableItems, {prePopulate: this.options.selectedItems,
            internalTokenMarkup:$.proxy(this.internalTokenMarkup,this),
            afterTokenSelectedFunction:$.proxy(this.afterTokenSelectedFunction,this),
            onDelete:$.proxy(this.deleteToken,this)
        });
        this.options.instantiated = true;
        if(this.postSetup)this.postSetup();
    },
    internalTokenMarkup:function(item) {
        var cssClass = "class='selectedItem'";
        return "<p><a " + cssClass + ">" + item.name + "</a></p>";
    },
    afterTokenSelectedFunction:function(item) {},
    deleteToken:function(hidden_input,token_data) {},
    addNew:function(e){
        e.preventDefault();
        KYT.vent.trigger("token:"+this.id+":addUpdate",this.id);
    },
    successHandler: function(result){
        if(!this.options.instantiated){
            $("#noAssets",this.el).hide();
            $("#hasAssets",this.el).show();
            this.inputSetup();
        }
        $(this.options.inputSelector,this.el).tokenInput("add",{id:result.EntityId, name:result.Variable});
        $(this.options.inputSelector,this.el).tokenInput("addToAvailableList",{id:result.EntityId, name:result.Variable});
    }
});

KYT.Views.EditableTokenView = KYT.Views.TokenView.extend({
    events:_.extend({
        'click .tokenEditor' : 'tokenEditor'
    }, KYT.Views.TokenView.prototype.events),
    internalTokenMarkup: function(item) {
        var cssClass = "class='selectedItem'";
        return "<p><a " + cssClass + ">" + item.name + "</a><a href='javascript:void(0);' class='tokenEditor' > -- Edit</a> </p>";
    },
    afterTokenSelectedFunction:function(item) {
        if(!$(this.options.inputSelector,this.el).data("selectedItems"))$(this.options.inputSelector,this.el).data("selectedItems",[]);
        $(this.options.inputSelector,this.el).data("selectedItems").push(item);
    },
    deleteToken:function(hidden_input,token_data) {
        var data = $(this.options.inputSelector,this.el).data("selectedItems");
        var idx=0;
        $.each(data,function(i,item){
            if(item.id == hidden_input.id){
                idx=i;
            }
        });
        data.splice(idx,1);
    },
    tokenEditor:function(e){}
});


KYT.Views.NotificationView = KYT.Views.View.extend({
    events:_.extend({
        "click #trapezoid":"toggle"
    }, KYT.Views.View.prototype.events),
    render: function(){
        var $trap = $("<div>").attr("id","trapezoid");
        $trap.text("vaslasdflasdf");
      //  $trap.hide();
        this.$el.html($trap);
        $("#main-content").prepend(this.$el);
        this.viewLoaded();
        KYT.vent.trigger("form:"+this.id+":pageLoaded",this.options);
        return this;
    },
    toggle:function(){
        $("#trapezoid").hide("blind",{},2000).delay(800).show("blind",{},2000);
    },
    show:function(){
        $("#trapezoid").show("blind",{},2000);
    },
    hide:function(){
        $("#trapezoid").hide("blind",{},2000);
    }
});

KYT.tokenDefaults = {
    availableItems:[],
    selectedItems: [],
    tooltipAjaxUrl:"",
    inputSelector:"#Input"
};

KYT.popupDefaults = {
    title:""
};

KYT.gridDefaults = {
    searchField:"Name",
    gridContainer: "#gridContainer",
    showSearch:true,
    id:"",
    errorMessagesContainer:"errorMessagesGrid"
};

