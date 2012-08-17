/**
 * Created by JetBrains RubyMine.
 * User: Owner
 * Date: 2/26/12
 * Time: 11:24 AM
 * To change this template use File | Settings | File Templates.
 */

KYT.Views.CalendarView = KYT.Views.View.extend({
    render:function(){
       KYT.repository.ajaxGet(this.options.url, this.options.data)
           .done($.proxy(this.renderCallback,this));
    },
    renderCallback:function(result){
        $(this.el).html($('<div>').attr('id','calendar'));
        this.model = result.CalendarDefinition;
        this.model.id=this.id;
        $("#calendar",this.el).asCalendar(this.model);
        //callback for render
        this.viewLoaded();
        //general notification of pageloaded
        KYT.vent.trigger("calendar:"+this.id+":pageLoaded",this.options);
        this.calendarBindings();
    },
    calendarBindings:function(){
        KYT.vent.bind("calendar:"+this.id+":eventDrop",this.eventDrop,this);
        KYT.vent.bind("calendar:"+this.id+":eventResize",this.eventResize,this);
        KYT.vent.bind("calendar:"+this.id+":dayClick",this.dayClick,this);
        KYT.vent.bind("calendar:"+this.id+":eventClick",this.eventClick,this);
        KYT.vent.bind("ajaxPopupFormModule:editModule:success",this.formSuccess,this);
        KYT.vent.bind("ajaxPopupFormModule:editModule:cancel",this.formCancel,this);
        KYT.vent.bind("ajaxPopupDisplayModule:displayModule:cancel",this.displayCancel,this);
        KYT.vent.bind("popup:displayModule:edit",this.displayEdit,this);
    },
    onClose:function(){
        KYT.vent.unbind("calendar:"+this.id+":eventDrop");
        KYT.vent.unbind("calendar:"+this.id+":eventResize");
        KYT.vent.unbind("calendar:"+this.id+":eventClick");
        KYT.vent.unbind("calendar:"+this.id+":dayClick");
        KYT.vent.unbind("ajaxPopupFormModule:editModule:success");
        KYT.vent.unbind("ajaxPopupFormModule:editModule:cancel");
        KYT.vent.unbind("ajaxPopupDisplayModule:displayModule:cancel");
        KYT.vent.unbind("popup:displayModule:edit");
    },
    eventDrop:function(event, dayDelta,minuteDelta,allDay,revertFunc) {
        var data = {"EntityId":event.EntityId,
            "ScheduledDate":$.fullCalendar.formatDate( event.start,"M/d/yyyy hh:mm TT"),
            "StartTime":$.fullCalendar.formatDate( event.start,"M/d/yyyy hh:mm TT"),
            "EndTime":$.fullCalendar.formatDate( event.end,"M/d/yyyy hh:mm TT")};
        KYT.repository.ajaxGet(this.model.EventChangedUrl,data).done($.proxy(function(result){this.changeEventCallback(result,revertFunc)},this));
    },
    eventResize:function( event, dayDelta, minuteDelta, revertFunc, jsEvent, ui, view ){
        var data = {"EntityId":event.EntityId,
            "ScheduledDate":$.fullCalendar.formatDate( event.start,"M/d/yyyy hh:mm TT"),
            "StartTime":$.fullCalendar.formatDate( event.start,"M/d/yyyy hh:mm TT"),
            "EndTime":$.fullCalendar.formatDate( event.end,"M/d/yyyy hh:mm TT")
        };
        KYT.repository.ajaxGet(this.model.EventChangedUrl,data).done($.proxy(function(result){this.changeEventCallback(result,revertFunc)},this));
    },
    dayClick:function(date, allDay, jsEvent, view) {
        var data = this.getIds(0);
        data.ScheduledDate= $.fullCalendar.formatDate( date,"M/d/yyyy");
        data.ScheduledStartTime= $.fullCalendar.formatDate( date,"hh:mm TT");
        this.editEvent(this.model.AddUpdateUrl,data);
    },
    eventClick:function(calEvent, jsEvent, view) {
        var data = {"EntityId": calEvent.EntityId, popup:true};
        var builder =  KYT.Views.popupButtonBuilder.builder("displayModule");
        builder.addButton("Delete", $.proxy(this.deleteItem,this));
        builder.addEditButton();
        builder.addButton("Copy Event",$.proxy(this.copyItem,this));
        builder.addCancelButton();

        var formOptions = {
            id: "displayModule",
            url: this.model.DisplayUrl,
            route: this.model.DisplayRoute,
            templateUrl: this.model.DisplayUrl+"_Template?Popup=true",
            view: this.options.subViewName?"Display" + this.options.subViewName:"",
            AddUpdateUrl: this.model.AddUpdateUrl,
            data:data,
            buttons: builder.getButtons()
        };
        this.ajaxPopupDisplay = new KYT.Views.AjaxPopupDisplayModule(formOptions);
        this.ajaxPopupDisplay.render();
        this.storeChild(this.ajaxPopupDisplay);
        $(this.el).append(this.ajaxPopupDisplay.el);

    },
    editEvent:function(url, data){
        data.Popup = true;
        var formOptions = {
            id: "editModule",
            route: this.model.AddUpdateRoute,
            url: url,
            templateUrl: url+"_Template?Popup=true",
            data:data,
            view:this.options.subViewName,
            buttons: KYT.Views.popupButtonBuilder.builder("editModule").standardEditButons()
        };
        this.ajaxPopupFormModule = new KYT.Views.AjaxPopupFormModule(formOptions);
        this.ajaxPopupFormModule.render();
        this.storeChild(this.ajaxPopupFormModule);

    },

    changeEventCallback:function(result,revertFunc){
        if(!result.Success){
            alert(result.Message);
            revertFunc();
        }
    },

    copyItem:function(){
        var data = this.getIds(this.ajaxPopupDisplay.popupDisplay.model.EntityId());
        data.Copy = true;
        this.displayCancel();
        this.editEvent(this.model.AddUpdateUrl,data);
        //this feels retarded for some reason
        KYT.vent.bind("form:editModule:pageLoaded", function(){
            this.ajaxPopupFormModule.popupForm.model.EntityId(0);
            KYT.vent.unbind("form:editModule:pageLoaded");
        },this);
    },

    deleteItem: function(){
        var that = this;
        if (confirm("Are you sure you would like to delete this Item?")) {

            KYT.repository.ajaxGet(this.model.DeleteUrl,this.getIds(this.ajaxPopupDisplay.popupDisplay.model.EntityId())).done(function(result){
                that.displayCancel();
//                if(!result.Success){
//                    alert(result.Message);
//                }else{
                   that.reload();
//                }
            });
        }
    },
    displayEdit:function(event){
        this.displayCancel();
        this.editEvent(this.model.AddUpdateUrl,this.getIds(this.ajaxPopupDisplay.popupDisplay.model.EntityId()));
    },

    getIds:function(entityId){
        var rel = KYT.State.get("Relationships");
        var rootId = rel.rootId;
        var parentId = rel.parentId;
        return {
            entityId:entityId,
            parentId:parentId,
            rootId:rootId
        };
    },

    reload:function(){
        $('#calendar',this.el).fullCalendar( 'refetchEvents' )
    },

    formSuccess:function(){
        this.formCancel();
        this.reload();
        this.removeChildView(this.ajaxPopupFormModule)
    },
    formCancel:function(){
        this.ajaxPopupFormModule.close();
        this.removeChildView(this.ajaxPopupFormModule)
    },
    displayCancel:function(){
        this.ajaxPopupDisplay.close();
        this.removeChildView(this.ajaxPopupDisplay)
    }
});

KYT.Views.EmployeeDashboardView = KYT.Views.View.extend({
    initialize:function(){
        KYT.mixin(this, "formMixin");
        KYT.mixin(this, "ajaxFormMixin");
        KYT.mixin(this, "modelAndElementsMixin");
        this.options.noBubbleUp=true;
    },
    viewLoaded:function(){
        this.pendingGridView = new KYT.Views.DahsboardGridView({el:"#pendingTaskGridContainer",
            url:this.model._pendingGridUrl(),
            gridContainer: "#pendingGridContainer",
            route:"task",
            id:"pending"
        });
        this.completedGridView = new KYT.Views.DahsboardGridView({el:"#completedTaskGridContainer",
          url:this.model._completedGridUrl(),
            gridContainer: "#completedGridContainer",
            id:"completed",
            route:"taskdisplay"});
        this.pendingGridView.render();
        this.completedGridView.render();
        this.storeChild(this.pendingGridView);
        this.storeChild(this.completedGridView);
    },
    callbackAction: function(){
        this.pendingGridView.callbackAction();
        this.completedGridView.callbackAction();
    }
});

KYT.Views.FieldDashboardView = KYT.Views.View.extend({
    initialize:function(){
        KYT.mixin(this, "formMixin");
        KYT.mixin(this, "ajaxFormMixin");
        KYT.mixin(this, "modelAndElementsMixin");
    },
    viewLoaded:function(){
        this.addIdsToModel();
        var rel = KYT.State.get("Relationships");
        $('#FieldColor',this.el).miniColors();
        this.pendingGridView = new KYT.Views.DahsboardGridView({
            el:"#pendingTaskGridContainer",
            url:this.model._pendingGridUrl(),
            gridContainer: "#pendingGridHolder",
            id:"pending",
            parentId:rel.entityId,
            rootId: rel.parentId,
            route:"task"
        });
        this.completedGridView = new KYT.Views.DahsboardGridView({
            el:"#completedTaskGridContainer",
            url:this.model._completedGridUrl(),
            gridContainer: "#completedGridHolder",
            id:"completed",
            parentId:rel.entityId,
            rootId: rel.parentId,
            route:"taskdisplay"
        });
        this.photoGridView = new KYT.Views.DahsboardGridView({
            el:"#photoGridContainer",
            url:this.model._photoGridUrl(),
            gridContainer: "#photoGridHolder",
            id:"photolist",
            route:"photo",
            parentId:rel.entityId,
            rootId: rel.parentId,
            parent:"Field",
            messageContainer:"errorMessagesPhotoGrid"
        });
        this.documentGridView = new KYT.Views.DahsboardGridView({
            el:"#documentGridContainer",
            url:this.model._documentGridUrl(),
            gridContainer: "#documentGridHolder",
            id:"documentlist",
            route:"document",
            parentId:rel.entityId,
            rootId: rel.parentId,
            parent:"Field",
            messageContainer:"errorMessagesDocGrid"
        });

        this.pendingGridView.render();
        this.completedGridView.render();
        this.photoGridView.render();
        this.documentGridView.render();
        this.storeChild(this.pendingGridView);
        this.storeChild(this.completedGridView);
        this.storeChild(this.photoGridView);
        this.storeChild(this.documentGridView);
    },
    callbackAction: function(){
        this.pendingGridView.callbackAction();
        this.completedGridView.callbackAction();
        this.photoGridView.callbackAction();
        this.documentGridView.callbackAction();
    }
});

KYT.Views.DahsboardGridView = KYT.Views.View.extend({
     initialize: function(){
        KYT.mixin(this, "ajaxGridMixin");
        KYT.mixin(this, "setupGridMixin");
        KYT.mixin(this, "defaultGridEventsMixin");
        KYT.mixin(this, "setupGridSearchMixin");
        this.setupBindings();
    },
    addNew:function(){
        KYT.vent.trigger("route",KYT.generateRoute(this.options.route, 0, this.options.parentId,this.options.rootId,this.options.parent),true);
    },
    editItem:function(id){
        KYT.vent.trigger("route",KYT.generateRoute(this.options.route, id, this.options.parentId,this.options.rootId,this.options.parent),true);
    },
    displayItem:function(id){
        KYT.vent.trigger("route",KYT.generateRoute(this.options.route, id, this.options.parentId,this.options.rootId),true);
    },
    onClose:function(){
        this.unbindBindings();
    }
});

KYT.Views.TaskFormView = KYT.Views.View.extend({
    initialize:function(){
        KYT.mixin(this, "formMixin");
        KYT.mixin(this, "ajaxFormMixin");
        KYT.mixin(this, "modelAndElementsMixin");
    },
    viewLoaded:function(){
        KYT.calculator.applyTaskTransferData(this.model,this.$el);
    }
});

KYT.Views.PurchaseOrderFormView = KYT.Views.View.extend({
    events:{
        'click #save' : 'saveItem',
        'click #cancel' : 'cancel'
    },
    initialize:function(){
        KYT.mixin(this, "formMixin");
        KYT.mixin(this, "ajaxFormMixin");
        KYT.mixin(this, "modelAndElementsMixin");
        this.setupBindings();
    },
    viewLoaded:function(){
        this.showPOInfo();
    },
    setupBindings:function(){
        KYT.vent.bind("productGrid:Other", this.addToOrder,this);
        KYT.vent.bind("productGrid:Display", this.displayProduct,this);
        KYT.vent.bind("poliGrid:Delete", this.deleteItem,this);
        KYT.vent.bind("poliGrid:AddUpdateItem", this.editItem,this);
        KYT.vent.bind("poliGrid:Display", this.displayItem,this);
        KYT.vent.bind('popup_displayModule:cancel', this.displayCancel,this);
        KYT.vent.bind('popup_displayModule:edit', this.displayEdit,this);
        KYT.vent.bind('form_editModule:success', this.itemSuccess,this);
        this._super("setupBindings",arguments);
    },
    unbindBindings:function(){
        KYT.vent.unbind("productGrid:Other", this.addToOrder,this);
        KYT.vent.unbind("productGrid:Display", this.displayProduct,this);
        KYT.vent.unbind("poliGrid:Delete", this.deleteItem,this);
        KYT.vent.unbind("poliGrid:AddUpdateItem", this.editItem,this);
        KYT.vent.unbind("poliGrid:Display", this.displayItem,this);
        KYT.vent.unbind('popup_displayModule:cancel', this.displayCancel,this);
        KYT.vent.unbind('popup_displayModule:edit', this.displayEdit,this);
        KYT.vent.unbind('form_editModule:success', this.itemSuccess,this);
        this._super("unbindBindings",arguments);
    },
    showVendorProducts:function(){
        $("#productGridArea").show();
        this.productsGridView = new KYT.Views.GridView({
            el:"#productGridArea",
            gridContainer: "#productGrid",
            url:this.model._VendorProductsUrl() + "/" + this.model.VendorEntityId(),
            grouping:true,
            groupingView : {
                groupField : ['InstantiatingType'],
                groupColumnShow : [false]
            },
            id:"vendorProductsGrid",
            route:"taskdisplay"});
        this.productsGridView.render();
        this.storeChild(this.productsGridView);
    },
    showPOLI:function(){
        $("#poliGridArea").show();
        this.POLIGridView = new KYT.Views.GridView({
            el:"#poliGridArea",
            gridContainer: "#poliGrid",
            url:this.model._POLIUrl() + "/" + this.model.EntityId(),
            id:"poliGrid"});
        this.POLIGridView.render();
        this.storeChild(this.POLIGridView);
    },
    showPOInfo:function(){
        if(this.model.EntityId()>0){
            $("#viewPOID",this.$el).show();
            $("#viewVendor",this.$el).show();
            $("#editVendor",this.$el).hide();
            this.showVendorProducts();
            this.showPOLI();
        }else{
            $("#viewPOID",this.$el).hide();
            $("#viewVendor",this.$el).hide();
            $("#editVendor",this.$el).show();
            $("#poliGridArea").hide();
            $("#productGridArea").hide();
        }
    },
    addToOrder:function(id){
        kyt.repository.ajaxGet(this.model._AddToOrderId()+"/"+id,{"PurchaseOrderId":this.model.EntityId(),"VendorId":this.model.VendorEntityId()})
            .done($.proxy(this.addToOrderCallback,this));
    },
    addToOrderCallback:function(result){
        if($("#Item_EntityId").val()==0){
            var vendorName = $("#vendor :selected").text();
            $("#viewVendor").find("span", ".KYT_view_display").text(vendorName);
        }
        $("#Item_EntityId").val(result.EntityId);
        this.showPOData();
        var url = this.views.poliGridView.getUrl();
        url = url.substr(0,url.indexOf("?EntityId"));
        url = url+"?EntityId="+result.EntityId;
        this.views.poliGridView.setUrl(url);
        this.views.poliGridView.reloadGrid();
    },
    displayProduct:function(id){
        var builder = kyt.popupButtonBuilder.builder("displayModule");
        var button = builder.addButton("Return",builder.addCancelButton()).getButtons();
        var moduleOptions = {
            id:"displayProductPopup",
            url: this.model._displayProductUrl()+"/"+id,
            buttons: button
        };
        this.modules.displayProductPopup= new kyt.AjaxPopupDisplayModule(moduleOptions);
    },

    displayItem:function(id){
        var moduleOptions = {
            id:"displayItemPopup",
            url: this.model._displayItemUrl()+"/"+id,
            buttons: kyt.popupButtonBuilder.builder("displayModule").standardDisplayButtons()
        };
        this.modules.displayItemPopup = new kyt.AjaxPopupDisplayModule(moduleOptions);
    },
        //from display
    displayProductCancel:function(){
        this.displayProductPopup.close();
    },

    displayEdit:function(id){
        this.modules.popupDisplay.destroy();
        this.editItem(data);
    },

    deleteItem:function(id){
        if (confirm("Are you sure you would like to delete this Item?")) {
            KYT.repository.ajaxGet(this.model._removePOLItemUrl()).done($.proxy(this.views.poliGridView.reloadGrid,this));
        }
    },
    editItem:function(id){
        var moduleOptions = {
            id:"editModule",
            url: this.model._editPOLItemUrl()+"/"+id,
            data:{"ParentId":this.model.EntityId()}
        };
        this.modules.popupForm = new kyt.AjaxPopupFormModule(moduleOptions);
    },

    itemSuccess:function(){
       this.itemCancel();
       this.views.poliGridView.reloadGrid();
    },
    itemCancel: function(){
       this.modules.popupForm.destroy();
    },
    commitPO:function(){
        KYT.vent.trigger("route",this.model._commitPOUrl()+"/"+this.model.EntityId());
    }
});




KYT.Views.EmailJobFormView = KYT.Views.View.extend({
    initialize:function(){
        KYT.mixin(this, "formMixin");
        KYT.mixin(this, "ajaxFormMixin");
        KYT.mixin(this, "modelAndElementsMixin");
    }
});

KYT.Views.FieldListView = KYT.Views.GridView.extend({
    viewLoaded:function(){
        KYT.vent.bind(this.id+":Redirect",this.showDashboard,this);
    },
    showDashboard:function(id){
        KYT.vent.trigger("route",KYT.generateRoute("fielddashboard",+id,this.options.ParentId),true);
    }
});

KYT.Views.EmployeeListView = KYT.Views.GridView.extend({
    viewLoaded:function(){
        KYT.vent.bind(this.id+":Redirect",this.showDashboard,this);
    },
    showDashboard:function(id){
        KYT.vent.trigger("route",KYT.generateRoute("employeedashboard",+id),true);
    }
});

KYT.Views.VendorListView = KYT.Views.GridView.extend({
    viewLoaded:function(){
        KYT.vent.bind(this.id+":Redirect",this.showContacts,this);
    },
    showContacts:function(id){
        KYT.vent.trigger("route",KYT.generateRoute("vendorcontactlist",0,+id),true);
    }
});

KYT.Views.DocumentFormView = KYT.Views.View.extend({
    initialize:function(){
        KYT.mixin(this, "formMixin");
        KYT.mixin(this, "ajaxFormMixin");
        KYT.mixin(this, "modelAndElementsMixin");
    }
});

KYT.Views.PhotoFormView = KYT.Views.View.extend({
    initialize:function(){
        KYT.mixin(this, "formMixin");
        KYT.mixin(this, "ajaxFormMixin");
        KYT.mixin(this, "modelAndElementsMixin");
    }
});

KYT.Views.CalculatorFormView = KYT.Views.View.extend({
    initialize:function(){
        KYT.mixin(this, "formMixin");
        KYT.mixin(this, "ajaxFormMixin");
        KYT.mixin(this, "modelAndElementsMixin");
        this.options.templateUrl += this.options.url.substring(this.options.url.lastIndexOf("/"));
    },
    saveItem:function(){
        var data = JSON.stringify(ko.mapping.toJS(this.model,this.mappingOptions));
        var promise = KYT.repository.ajaxPostModel(this.model._calculateUrl(),data);
        promise.done($.proxy(this.successHandler,this));
    },
    successHandler:function(result){
        KYT.calculator.successHandler(this.model,result);
    },
    events:{'click #createTask':'addTask',
    'click #save' : 'saveItem',
        'click #cancel' : 'cancel'},
    addTask:function(){
        var fieldId = this.model.FieldEntityId?this.model.FieldEntityId():0;
        KYT.calculator.setTaskTransferData(this.model);
        KYT.vent.trigger("route",KYT.generateRoute("task", 0, fieldId),true);
    }

});

KYT.Views.ForumView = KYT.Views.View.extend({
    render:function(){
        var iframe = $('<iframe>')
            .attr('name','KYTForum')
            .attr('height','600px')
            .attr('width','100%')
            .attr("frameBorder",0)
            .attr('scrolling','yes')
            .attr('style','overflow-x:hidden')
            .attr('src','http://kytforum.websitetoolbox.com/');
        this.$el.append(iframe)
    }
});

KYT.Views.ListTypeListView = KYT.Views.View.extend({
    initialize:function(){
        KYT.mixin(this, "ajaxFormMixin");
        KYT.mixin(this, "modelAndElementsMixin");
    },
    viewLoaded:function(){
        // the gridview rendercallback over writes deleteMultipleUrl so we have to load it here
        KYT.vent.bind("grid:tasktypelist:pageLoaded",function(options){
             options.deleteMultipleUrl = this.model._deleteMultipleTaskTypesUrl();
        },this);
        KYT.vent.bind("grid:eventtypelist:pageLoaded",function(options){
             options.deleteMultipleUrl = this.model._deleteMultipleEventTypesUrl();
        },this);
        KYT.vent.bind("grid:photocategorylist:pageLoaded",function(options){
             options.deleteMultipleUrl = this.model._deleteMultiplePhotoCatUrl();
        },this);
        KYT.vent.bind("grid:documentcategorylist:pageLoaded",function(options){
             options.deleteMultipleUrl = this.model._deleteMultipleDocCatUrl();
        },this);
        this.taskTypeGridView = new KYT.Views.DahsboardGridView({
            el:"#taskTypeGridContainer",
            url:this.model._taskTypeGridUrl(),
            gridContainer: "#taskTypeGridHolder",
            id:"tasktypelist",
            route:"tasktype"

        });
        this.eventTypeGridView = new KYT.Views.DahsboardGridView({
            el:"#eventTypeGridContainer",
            url:this.model._eventTypeGridUrl(),
            gridContainer: "#eventTypeGridHolder",
            id:"eventtypelist",
            route:"eventtype"
        });
        this.photoCategoryGridView = new KYT.Views.DahsboardGridView({
            el:"#photoCategoryGridContainer",
            url:this.model._photoCategoryGridUrl(),
            gridContainer: "#photoCategoryGridHolder",
            id:"photocategorylist",
            route:"photocategory"
        });
        this.documentCategoryGridView = new KYT.Views.DahsboardGridView({
            el:"#documentCategoryGridContainer",
            url:this.model._documentCategoryGridUrl(),
            gridContainer: "#documentCategoryGridHolder",
            id:"documentcategorylist",
            route:"documentcategory"
        });

        this.taskTypeGridView.render();
        this.eventTypeGridView.render();
        this.photoCategoryGridView.render();
        this.documentCategoryGridView.render();
        this.storeChild(this.taskTypeGridView);
        this.storeChild(this.eventTypeGridView);
        this.storeChild(this.photoCategoryGridView);
        this.storeChild(this.documentCategoryGridView);
    },
    callbackAction: function(){
        this.taskTypeGridView.callbackAction();
        this.eventTypeGridView.callbackAction();
        this.photoCategoryGridView.callbackAction();
        this.documentCategoryGridView.callbackAction();
    }
});

KYT.Views.EventTypeFormView = KYT.Views.View.extend({
    initialize:function(){
        KYT.mixin(this, "formMixin");
        KYT.mixin(this, "ajaxFormMixin");
        KYT.mixin(this, "modelAndElementsMixin");
    },
    viewLoaded:function(){
        $('#EventColor',this.el).miniColors();
    }
});

KYT.Views.TaskTypeFormView = KYT.Views.View.extend({
    initialize:function(){
        KYT.mixin(this, "formMixin");
        KYT.mixin(this, "ajaxFormMixin");
        KYT.mixin(this, "modelAndElementsMixin");
    },
    viewLoaded:function(){
        $('#TaskColor',this.el).miniColors();
    }
});




