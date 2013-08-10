/**
 * Created by JetBrains RubyMine.
 * User: Owner
 * Date: 2/26/12
 * Time: 11:24 AM
 * To change this template use File | Settings | File Templates.
 */

KYT.Views.CalendarView = KYT.Views.View.extend({
    initialize: function(){
        KYT.mixin(this, "modelAndElementsMixin");
    },
    events:{
        'change [name=TaskType]' : 'resetCalendar'
    },
    render:function(){
        $.when(KYT.loadTemplateAndModel(this))
            .done($.proxy(this.renderCallback,this));
//       KYT.repository.ajaxGet(this.options.url, this.options.data)
//           .done($.proxy(this.renderCallback,this));
    },
    renderCallback:function(result){
        this.model = this.rawModel;
        this.model.id= this.model.CalendarDefinition.id = this.id;
        $("div.form-scroll-inner").height(window.innerHeight-160);
        $("#calendar",this.el).asCalendar(this.model.CalendarDefinition);
        this.bindSpecificModelAndElements({TaskType:this.model.TaskType,
            _TaskTypeList:this.model._TaskTypeList,
            _Title:this.model._Title,
            EntityId:0
        });
        //callback for render
        this.viewLoaded();
        $("#calendar",this.el).fullCalendar('option', 'height', $("div.form-scroll-inner").height());
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
        KYT.repository.ajaxGet(this.model.CalendarDefinition.EventChangedUrl,data).done($.proxy(function(result){this.changeEventCallback(result,revertFunc)},this));
    },
    eventResize:function( event, dayDelta, minuteDelta, revertFunc, jsEvent, ui, view ){
        var data = {"EntityId":event.EntityId,
            "ScheduledDate":$.fullCalendar.formatDate( event.start,"M/d/yyyy hh:mm TT"),
            "StartTime":$.fullCalendar.formatDate( event.start,"M/d/yyyy hh:mm TT"),
            "EndTime":$.fullCalendar.formatDate( event.end,"M/d/yyyy hh:mm TT")
        };
        KYT.repository.ajaxGet(this.model.CalendarDefinition.EventChangedUrl,data).done($.proxy(function(result){this.changeEventCallback(result,revertFunc)},this));
    },
    dayClick:function(date, allDay, jsEvent, view) {
        var data = this.getIds(0);
        data.ScheduledDate= $.fullCalendar.formatDate( date,"M/d/yyyy");
        data.ScheduledStartTime= $.fullCalendar.formatDate( date,"hh:mm TT");
        this.editEvent(this.model.CalendarDefinition.AddUpdateUrl,data);
    },
    eventClick:function(calEvent, jsEvent, view) {
        if(this.functionIsPaused("eventClickDCPrevention")){
            console.log("paused");
            return;
        }
        this.pauseFunction("eventClickDCPrevention");
        console.log("not paused");
        var data = {"EntityId": calEvent.EntityId, popup:true};
        var builder =  KYT.Views.popupButtonBuilder.builder("displayModule");
        builder.addButton("Delete", $.proxy(this.deleteItem,this));
        builder.addEditButton();
        builder.addButton("Copy Task",$.proxy(this.copyItem,this));
        builder.addCancelButton();

        var formOptions = {
            id: "displayModule",
            url: this.model.CalendarDefinition.DisplayUrl,
            route: this.model.CalendarDefinition.DisplayRoute,
            title: this.model.CalendarDefinition.PopupTitle,
            templateUrl: this.model.CalendarDefinition.DisplayUrl+"_Template?Popup=true",
            view: this.options.subViewName?this.options.subViewName + "DisplayView" :"",
            AddUpdateUrl: this.model.CalendarDefinition.AddUpdateUrl,
            data:data,
            buttons: builder.getButtons()
        };
        this.ajaxPopupDisplay = new KYT.Views.AjaxPopupDisplayModule(formOptions);
        this.ajaxPopupDisplay.render();
        this.storeChild(this.ajaxPopupDisplay);
    },
    editEvent:function(url, data){
        data.Popup = true;
        var formOptions = {
            id: "editModule",
            route: this.model.CalendarDefinition.AddUpdateRoute,
            url: url,
            title: this.model.CalendarDefinition.PopupTitle,
            templateUrl: url+"_Template?Popup=true",
            data:data,
            view:this.options.subViewName+"FormView",
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
        this.editEvent(this.model.CalendarDefinition.AddUpdateUrl,data);
        //this feels retarded for some reason
        KYT.vent.bind("form:editModule:pageLoaded", function(){
            this.ajaxPopupFormModule.popupForm.model.EntityId(0);
            KYT.vent.unbind("form:editModule:pageLoaded");
        },this);
    },

    deleteItem: function(){
        var that = this;
        if (confirm("Are you sure you would like to delete this Item?")) {

            KYT.repository.ajaxGet(this.model.CalendarDefinition.DeleteUrl,this.getIds(this.ajaxPopupDisplay.popupDisplay.model.EntityId())).done(function(result){
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
        this.editEvent(this.model.CalendarDefinition.AddUpdateUrl,this.getIds(this.ajaxPopupDisplay.popupDisplay.model.EntityId()));
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
    resetCalendar:function(){
        var taskTypeId = this.viewModel.TaskType();
        this.replaceSource({url : this.model.CalendarDefinition.Url, data:{taskType:taskTypeId} });
        this.reload();
    },

    reload:function(){
        $('#calendar',this.el).fullCalendar( 'refetchEvents' )
    },

    replaceSource:function(source){
        $("#calendar",this.el).fullCalendar( 'replaceEventSource', source )
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
        KYT.vent.bind("form:"+this.id+":success",this.reload,this);
    },
    viewLoaded:function(){
        this.pendingGridView = new KYT.Views.DahsboardGridView({el:"#pendingTaskGridContainer",
            url:this.model._pendingGridUrl(),
            route:"task",
            gridId:"pendingTaskList",
            gridOptions:{
                multiselect:false
            }
        });
        this.completedGridView = new KYT.Views.DahsboardGridView({el:"#completedTaskGridContainer",
          url:this.model._completedGridUrl(),
            gridId:"completedTaskList",
            route:"taskdisplay",
            gridOptions:{
                multiselect:false
            }
        });
        this.pendingEMGridView = new KYT.Views.DahsboardGridView({el:"#pendingEquipmentTaskGridContainer",
            url:this.model._pendingEMGridUrl(),
            route:"equipmenttask",
            gridId:"pendingEquipmentTaskList",
            gridOptions:{
                multiselect:false
            }
        });
        this.completedEMGridView = new KYT.Views.DahsboardGridView({el:"#completedEquipmentTaskGridContainer",
            url:this.model._completedEMGridUrl(),
            gridId:"completedEquipmentTaskList",
            route:"equipmenttaskdisplay",
            gridOptions:{
                multiselect:false
            }
        });
        this.pendingGridView.render();
        this.completedGridView.render();
        this.pendingEMGridView.render();
        this.completedEMGridView.render();
        this.storeChild(this.pendingGridView);
        this.storeChild(this.completedGridView);
        this.storeChild(this.pendingEMGridView);
        this.storeChild(this.completedEMGridView);
    },
    callbackAction: function(){
        this.pendingGridView.callbackAction();
        this.completedGridView.callbackAction();
    },
    reload:function(){
        this.render();
    }
});

KYT.Views.ProductDashboardView = KYT.Views.View.extend({
    initialize:function(){
        KYT.mixin(this, "formMixin");
        KYT.mixin(this, "ajaxFormMixin");
        KYT.mixin(this, "modelAndElementsMixin");
    },
    viewLoaded:function(){
        this.addIdsToModel();
        var rel = KYT.State.get("Relationships");
        $('#colorPickerInput',this.el).miniColors();
        this.photoGridView = new KYT.Views.DahsboardGridView({
            el:"#photoGridContainer",
            url:this.model._photoGridUrl(),
            gridId:"photolist",
            route:"photo",
            parentId:rel.entityId,
            rootId: rel.parentId,
            parent:this.model.Product()
        });
        this.documentGridView = new KYT.Views.DahsboardGridView({
            el:"#documentGridContainer",
            url:this.model._documentGridUrl(),
            gridId:"documentlist",
            route:"document",
            parentId:rel.entityId,
            rootId: rel.parentId,
            parent:this.model.Product()
        });

        this.photoGridView.render();
        this.documentGridView.render();
        this.storeChild(this.photoGridView);
        this.storeChild(this.documentGridView);

        KYT.vent.bind("photolist:delete",this.removePicturesFromGallery, this);
        KYT.vent.bind("form:photo:success", this.reloadPictureGallery, this);

    },
    reloadPictureGallery:function(result){
        if(result.Payload){
            var gallery = $("#photoGallery").data("galleria");
            gallery.removeItem(result.Payload.ImageId);
            var newImage ={
                image: result.Payload.FileUrl_Large,
                thumb: result.Payload.FileUrl_Thumb,
                imageId:result.Payload.ImageId.toString()
            };
            gallery.addNewItem(newImage);
        }
    },

    removePicturesFromGallery: function(result){
        if(result.length>0){
            var gallery = $("#photoGallery").data("galleria");
            $.each(result,function(i,item){
                gallery.removeItem(item);
            });
//            gallery.reloadGallery();
        }
    },

    callbackAction: function(){
        this.photoGridView.callbackAction();
        this.documentGridView.callbackAction();
    }
});


KYT.Views.FieldDashboardView = KYT.Views.View.extend({
    initialize:function(){
        KYT.mixin(this, "formMixin");
        KYT.mixin(this, "ajaxFormMixin");
        KYT.mixin(this, "modelAndElementsMixin");
    },
    _setupBindings:function(){
        KYT.vent.bind(this.options.gridId+":Redirect",this.commitPO,this);
        KYT.vent.bind(this.options.gridId + ":AddUpdateItem", this.editItem, this);
    },
    _unbindBindings:function(){
        KYT.vent.unbind(this.options.gridId+":Redirect",this.commitPO,this);
        KYT.vent.unbind(this.options.gridId+ ":AddUpdateItem", this.editItem, this);
    },
    viewLoaded:function(){
        this.addIdsToModel();
        var rel = KYT.State.get("Relationships");
        $('#colorPickerInput',this.el).miniColors();
        this.pendingGridView = new KYT.Views.DahsboardGridView({
            el:"#pendingTaskGridContainer",
            url:this.model._pendingGridUrl(),
            gridId:"pendingTaskList",
            parentId:rel.entityId,
            rootId: rel.parentId,
            route:"task"
        });
        this.completedGridView = new KYT.Views.DahsboardGridView({
            el:"#completedTaskGridContainer",
            url:this.model._completedGridUrl(),
            gridId:"completedTaskList",
            parentId:rel.entityId,
            rootId: rel.parentId,
            gridOptions:{
                multiselect:false
            },
            route:"taskdisplay"
        });
        this.photoGridView = new KYT.Views.DahsboardGridView({
            el:"#photoGridContainer",
            url:this.model._photoGridUrl(),
            gridId:"photolist",
            route:"photo",
            parentId:rel.entityId,
            rootId: rel.parentId,
            parent:"Field"
        });
        this.documentGridView = new KYT.Views.DahsboardGridView({
            el:"#documentGridContainer",
            url:this.model._documentGridUrl(),
            gridId:"documentlist",
            route:"document",
            parentId:rel.entityId,
            rootId: rel.parentId,
            parent:"Field"
        });

        this.pendingGridView.render();
        this.completedGridView.render();
        this.photoGridView.render();
        this.documentGridView.render();
        this.storeChild(this.pendingGridView);
        this.storeChild(this.completedGridView);
        this.storeChild(this.photoGridView);
        this.storeChild(this.documentGridView);

        KYT.vent.bind("photolist:delete",this.removePicturesFromGallery, this);
        KYT.vent.bind("form:photo:success", this.reloadPictureGallery, this);

    },
    reloadPictureGallery:function(result){
        if(result.Payload){
            var gallery = $("#photoGallery").data("galleria");
            gallery.removeItem(result.Payload.ImageId);
            var newImage ={
                image: result.Payload.FileUrl_Large,
                thumb: result.Payload.FileUrl_Thumb,
                imageId:result.Payload.ImageId.toString()
            };
            gallery.addNewItem(newImage);
        }
    },

    removePicturesFromGallery: function(result){
        if(result.length>0){
            var gallery = $("#photoGallery").data("galleria");
            $.each(result,function(i,item){
                gallery.removeItem(item);
            });
//            gallery.reloadGallery();
        }
    },

    callbackAction: function(){
        this.pendingGridView.callbackAction();
        this.completedGridView.callbackAction();
        this.photoGridView.callbackAction();
        this.documentGridView.callbackAction();
    }
});

KYT.Views.EquipmentDashboardView = KYT.Views.View.extend({
    initialize:function(){
        KYT.mixin(this, "formMixin");
        KYT.mixin(this, "ajaxFormMixin");
        KYT.mixin(this, "modelAndElementsMixin");
    },
    viewLoaded:function(){
        this.addIdsToModel();
        var rel = KYT.State.get("Relationships");
        this.pendingGridView = new KYT.Views.DahsboardGridView({
            el:"#pendingEquipmentTaskGridContainer",
            url:this.model._pendingGridUrl(),
            gridId:"pendingEquipmentTaskList",
            parentId:rel.entityId,
            rootId: rel.parentId,
            route:"equipmenttask"
        });
        this.completedGridView = new KYT.Views.DahsboardGridView({
            el:"#completedEquipmentTaskGridContainer",
            url:this.model._completedGridUrl(),
            gridId:"completedEquipmentTaskList",
            parentId:rel.entityId,
            rootId: rel.parentId,
            gridOptions:{
                multiselect:false
            },
            route:"equipmenttaskdisplay"
        });
        this.photoGridView = new KYT.Views.DahsboardGridView({
            el:"#photoGridContainer",
            url:this.model._photoGridUrl(),
            gridId:"photolist",
            route:"photo",
            parentId:rel.entityId,
            rootId: rel.parentId,
            parent:"Equipment"
        });
        this.documentGridView = new KYT.Views.DahsboardGridView({
            el:"#documentGridContainer",
            url:this.model._documentGridUrl(),
            gridId:"documentlist",
            route:"document",
            parentId:rel.entityId,
            rootId: rel.parentId,
            parent:"Equipment"
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
        this.options.gridOptions=this.options.gridOptions||{};
        // this is to override the grid resize from cc.jquery.jqgrid
        this.options.gridOptions.gridComplete=function(){
             $(this).find(".cbox").parent().addClass("jqg_cb");
         };
         this.options.gridOptions.height="300px";
        KYT.mixin(this, "dashboardGridMixin");
        KYT.mixin(this, "setupGridMixin");
        KYT.mixin(this, "defaultGridEventsMixin");
        KYT.mixin(this, "setupGridSearchMixin");
    },
    viewLoaded:function(){
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
    deleteItems: function () {
        if (confirm("Are you sure you would like to delete this Item?")) {
            var ids = cc.gridMultiSelect.getCheckedBoxes(this.options.gridId);
            KYT.repository.ajaxGet(this.options.deleteMultipleUrl,
                $.param({ "EntityIds": ids,
                    "ParentId":this.options.parentId,
                    "RootId":this.options.rootId,
                    "Var":this.options.parent}, true))
                .done($.proxy(function () {
                KYT.vent.trigger(this.options.gridId +":delete",ids);
                this.reloadGrid() }, this));
        }
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
    events:{
        'click #save' : 'saveItem',
        'click #cancel' : 'cancel',
        'change #InventoryProductEntityId' : 'chemicalSubSection'
    },
    chemicalSubSection:function(){
        var id = this.model.InventoryProductEntityId();
        var isChem;
        _.find(this.model._InventoryProductEntityIdList.groups(),function(item){
            if(item.label()=="Chemicals"){
                isChem = _.find(item.children(),function(chemId){
                    return chemId.Value() == id;
                });
            }
        });
        if(isChem){
            $("#chemicalReport",this.$el).show("slow");
        }else{
            $("#chemicalReport",this.$el).hide("slow");
        }
    },
    viewLoaded:function(){
        KYT.calculator.applyTaskTransferData(this.model,this.$el);
        KYT.vent.bind("popup:editModule:loaded",this.chemicalSubSection,this);
    },
    onClose:function(){
        KYT.vent.unbind("popup:editModule:loaded",this.chemicalSubSection,this);
    }
});

KYT.Views.TaskDisplayView = KYT.Views.View.extend({
    initialize:function(){
        KYT.mixin(this, "displayMixin");
        KYT.mixin(this, "ajaxDisplayMixin");
        KYT.mixin(this, "modelAndElementsMixin");
    },
    viewLoaded:function(){
        if(this.model.Popup()){
            KYT.vent.bind("popup:"+this.options.id+":loaded",this.toggleChemicals,this);
            KYT.vent.bind("popup:"+this.options.id+":loaded",this.checkPermOnEdit,this);
        }else{
            this.toggleChemicals();
        }
    },
    checkPermOnEdit:function(popup){
        if(!this.model.AllowEdit()){
            var buttons = $(popup.el).dialog("option", "buttons");
            delete buttons.Edit;
            $(popup.el).dialog("option", "buttons", buttons);
        }
    },
    toggleChemicals:function(){
        if(this.model._IsChemical()){
            $("#chemicalReport",this.$el).show("slow");
        }else{
            $("#chemicalReport",this.$el).hide("slow");
        }
    },
    onClose:function(){
        KYT.vent.unbind("popup:"+this.options.id+":loaded",this.toggleChemicals,this);
        KYT.vent.unbind("popup:"+this.options.id+":loaded",this.checkPermOnEdit,this);
    }
});

KYT.Views.PurchaseOrderListView = KYT.Views.View.extend({
    _setupBindings:function(){
        KYT.vent.bind(this.options.gridId+":Redirect",this.commitPO,this);
        KYT.vent.bind(this.options.gridId + ":AddUpdateItem", this.editItem, this);
    },
    _unbindBindings:function(){
        KYT.vent.unbind(this.options.gridId+":Redirect",this.commitPO,this);
        KYT.vent.unbind(this.options.gridId+ ":AddUpdateItem", this.editItem, this);
    },
    editItem: function (id) {
        if(this.options.Var == "Completed"){
            KYT.vent.trigger("route", KYT.generateRoute("purchaseordercommit", id), true);
        }else{
            KYT.vent.trigger("route", KYT.generateRoute(this.options.addUpdate, id), true);
        }
    },
    initialize: function(){
        KYT.mixin(this, "ajaxGridMixin");
        KYT.mixin(this, "setupGridMixin");
        KYT.mixin(this, "defaultGridEventsMixin");
        KYT.mixin(this, "setupGridSearchMixin");
    },
    viewLoaded:function(){
        this.setupBindings();
    },
    onClose:function(){
        this.unbindBindings();
    },
    commitPO:function(id){
        KYT.vent.trigger("route",KYT.generateRoute("purchaseordercommit",id),true);
    }
});

KYT.Views.PurchaseOrderFormView = KYT.Views.View.extend({
    events:{
        'click #commit' : 'commitPO',
        'click #return' : 'cancelPO',
        'change #editFieldVendor' : 'selectVendor'
    },
    initialize:function(){
        KYT.mixin(this, "formMixin");
        KYT.mixin(this, "ajaxFormMixin");
        KYT.mixin(this, "modelAndElementsMixin");
    },
    viewLoaded:function(){
        this._setupBindings();
        this.showPOInfo(this.model.EntityId()==0);
    },
    _setupBindings:function(){
        KYT.vent.bind("productGrid:Other", this.addToOrder,this);
        KYT.vent.bind("poliGrid:Delete", this.removeItemFromPO,this);
        KYT.vent.bind("poliGrid:AddUpdateItem", this.editPOItem,this);
        KYT.vent.bind('ajaxPopupFormModule:editPOItem:success', this.editPOItemSuccess,this);
        KYT.vent.bind('ajaxPopupFormModule:editPOItem:cancel', this.editPOItemCancel,this);
    },
    _unbindBindings:function(){
        KYT.vent.unbind("productGrid:Other");
        KYT.vent.unbind("poliGrid:Delete");
        KYT.vent.unbind("poliGrid:AddUpdateItem");
        KYT.vent.unbind('ajaxPopupFormModule:editPOItem:success');
        KYT.vent.unbind('ajaxPopupFormModule:editPOItem:cancel');
    },
    onClose:function(){
        this._unbindBindings();
    },
    showVendorProducts:function(){
        $("#products").show();
        this.productsGridView = new KYT.Views.DahsboardGridView({
            el:"#productGridArea",
            url:this.model._vendorProductsUrl() + "/" + this.model.VendorEntityId(),
            grouping:true,
            gridOptions:{
                multiselect:false,
                groupingView : {
                    groupField : ['InstantiatingType'],
                    groupColumnShow : [false]
                }
            },
            gridId:"vendorProductsGrid",
            route:"taskdisplay"});
        this.productsGridView.render();
        this.storeChild(this.productsGridView);
    },
    showPOLI:function(){
        $("#selectedItems").show();
        this.POLIGridView = new KYT.Views.POLIGridView({
            el:"#poliGridArea",
            gridOptions:{
                multiselect:false
            },
            url:this.model._POLIUrl() + "/" + this.model.EntityId(),
            gridId:"poliGrid"});
        this.POLIGridView.render();
        this.storeChild(this.POLIGridView);
    },
    showPOInfo:function(editable){
        if(editable){
            $("#viewPOID",this.$el).hide();
            $("#viewFieldVendor",this.$el).hide();
            $("#editFieldVendor",this.$el).show();
            $("#products").hide();
            $("#selectedItems").hide();
            $(".footer", this.$el).hide();
        }else{
            $("#viewPOID",this.$el).show();
            $("#viewFieldVendor",this.$el).show();
            $("#editFieldVendor",this.$el).hide();
            $(".footer", this.$el).show();
            this.showVendorProducts();
            this.showPOLI();
        }
    },
    selectVendor:function(){
        if(this.model.VendorEntityId()>0){
            this.showPOInfo(false);
            this.model.VendorClient(this.$el.find("#editFieldVendor :selected").text());
        }
    },
    addToOrder:function(id){
        KYT.repository.ajaxGet(this.model._addToOrderUrl()+"/"+id,{"ParentId":this.model.EntityId(),"RootId":this.model.VendorEntityId()})
            .done($.proxy(this.addToOrderCallback,this));
    },
    addToOrderCallback:function(result){
        if(this.model.EntityId()==0 && result.EntityId>0){
            this.setupNewPO(result.EntityId);
        }
        this.POLIGridView.reloadGrid();
    },
    setupNewPO:function(id){
        this.model.EntityId(id);
        var url = $("#"+this.POLIGridView.options.gridId).getGridParam("url");
        url = url.substr(0,url.lastIndexOf("/")+1) + this.model.EntityId();
        $("#"+this.POLIGridView.options.gridId).setGridParam({"url":url});
        var currentRoute = KYT.Routing.getCurrentRoute();
        currentRoute = currentRoute.substr(0,currentRoute.indexOf("/")+1)+this.model.EntityId()+"/"+this.model.ParentId()+"/"+this.model.VendorEntityId();
        KYT.vent.trigger("route", currentRoute,false);
    },
    removeItemFromPO:function(id){
        if (confirm("Are you sure you would like to remove this Item from the Purchase Order?")) {
            KYT.repository.ajaxGet(this.model._removePOLItemUrl()+"/"+id,{"ParentId":this.model.EntityId()})
                .done($.proxy(this.POLIGridView.reloadGrid,this.POLIGridView));
        }
    },
    editPOItem:function(id){
        var moduleOptions = {
            id:"editPOItem",
            url: this.model._editPOLItemUrl()+"/"+id,
            title: this.model._editPOLItemTitle(),
            templateUrl: this.model._editPOLItemUrl()+"_Template",
            data:{"ParentId":this.model.EntityId()}
        };
        this.editPOItem = new KYT.Views.AjaxPopupFormModule(moduleOptions);
        this.editPOItem.render();
    },
    editPOItemSuccess:function(){
       this.editPOItem.close();
       this.POLIGridView.reloadGrid();
    },
    editPOItemCancel: function(){
       this.editPOItem.close()
    },
    commitPO:function(){
        KYT.vent.trigger("route","purchaseordercommit/"+this.model.EntityId(),true);
    },
    cancelPO:function(){
        KYT.vent.trigger("form:"+this.id+":cancel");
        if(!this.options.noBubbleUp) {KYT.WorkflowManager.returnParentView(null,true);}
    }
});

KYT.Views.POLIGridView = KYT.Views.DahsboardGridView.extend({
    reloadGrid: function () {
        $("#" + this.options.gridId).trigger("reloadGrid");
    },
    // used by children to update parent grid
    callbackAction: function () {
        this.reloadGrid();
    }
});

KYT.Views.PurchaseOrderCommitFormView = KYT.Views.View.extend({
     events:{
        'click #closePO' : 'closePO'
    },
    initialize:function(){
        KYT.mixin(this, "ajaxFormMixin");
        KYT.mixin(this, "modelAndElementsMixin");
    },
    viewLoaded:function(){
        this.showPOLI();
    },
    showPOLI:function(){
        this.POLIGridView = new KYT.Views.CommitPOGridView({
            el:"#commitPoliGridArea",
            gridOptions:{
                multiselect:false
            },
            url:this.model._POLIUrl(),
            addUpdate:this.options.addUpdate,
            gridId:"commitPoliGrid",
            parentId:this.model.EntityId()
        });
        this.POLIGridView.render();
        this.storeChild(this.POLIGridView);
    },
    closePO:function(){
        KYT.repository.ajaxGet(this.model._ClosePOUrl())
            .done($.proxy(this.closePOCallback,this));
    },
    closePOCallback:function(_result){
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
            KYT.vent.trigger("PO:"+this.id+":closed");
            KYT.vent.trigger("route","purchaseorderlist",true);
        }
    },
    // used by children to update parent grid
    callbackAction: function () {
        this.POLIGridView.reloadGrid();
    }

});

KYT.Views.CommitPOGridView = KYT.Views.View.extend({
    initialize:function(){
        KYT.mixin(this, "ajaxGridMixin");
        KYT.mixin(this, "setupGridMixin");
        KYT.mixin(this, "setupGridSearchMixin");
    },
    viewLoaded:function(){
         KYT.vent.bind(this.options.gridId+ ":AddUpdateItem", this.editItem, this);
    },
    onClose:function(){
       KYT.vent.unbind(this.options.gridId+ ":AddUpdateItem", this.editItem, this);
    },
    editItem: function (id) {
        KYT.vent.trigger("route", KYT.generateRoute(this.options.addUpdate, id,this.options.parentId), true);
    },
    reloadGrid: function () {
        $("#" + this.options.gridId).trigger("reloadGrid");
    },
    // used by children to update parent grid
    callbackAction: function () {
        this.reloadGrid();
    }
});

KYT.Views.NoMultiSelectGrid = KYT.Views.View.extend({
    initialize:function(){
        KYT.mixin(this, "ajaxGridMixin");
        KYT.mixin(this, "setupGridMixin");
        KYT.mixin(this,"defaultGridEventsMixin");
        KYT.mixin(this, "setupGridSearchMixin");
        this.options.gridOptions = {
            multiselect:false
        }
    },
    viewLoaded:function(){
        this.setupBindings();
    },
    onClose:function(){
        this.unbindBindings();
    }
});

KYT.Views.EmailJobFormView = KYT.Views.View.extend({
    initialize:function(){
        KYT.mixin(this, "formMixin");
        KYT.mixin(this, "ajaxFormMixin");
        KYT.mixin(this, "modelAndElementsMixin");
    }
});

KYT.Views.FieldListView = KYT.Views.View.extend({
    initialize:function(){
        KYT.mixin(this, "ajaxGridMixin");
        KYT.mixin(this, "setupGridMixin");
        KYT.mixin(this, "defaultGridEventsMixin");
        KYT.mixin(this, "setupGridSearchMixin");
    },
    viewLoaded:function(){
        KYT.vent.bind(this.options.gridId+":Redirect",this.showDashboard,this);
        this.setupBindings();
    },
    onClose:function(){
        KYT.vent.unbind(this.options.gridId+":Redirect",this.showDashboard,this);
        this.unbindBindings();
    },
    showDashboard:function(id){
        KYT.vent.trigger("route",KYT.generateRoute("fielddashboard",id,this.options.ParentId),true);
    }
});

KYT.Views.EquipmentListView = KYT.Views.View.extend({
    initialize:function(){
        KYT.mixin(this, "ajaxGridMixin");
        KYT.mixin(this, "setupGridMixin");
        KYT.mixin(this, "defaultGridEventsMixin");
        KYT.mixin(this, "setupGridSearchMixin");
    },
    viewLoaded:function(){
        KYT.vent.bind(this.options.gridId+":Redirect",this.showDashboard,this);
        this.setupBindings();
    },
    onClose:function(){
        KYT.vent.unbind(this.options.gridId+":Redirect",this.showDashboard,this);
        this.unbindBindings();
    },
    showDashboard:function(id){
        KYT.vent.trigger("route",KYT.generateRoute("equipmentdashboard",id,this.options.ParentId),true);
    }
});

KYT.Views.VendorListView = KYT.Views.View.extend({
    initialize:function(){
        KYT.mixin(this, "ajaxGridMixin");
        KYT.mixin(this, "setupGridMixin");
        KYT.mixin(this, "defaultGridEventsMixin");
        KYT.mixin(this, "setupGridSearchMixin");
    },
    viewLoaded:function(){
         KYT.vent.bind(this.options.gridId+":Redirect",this.showContacts,this);
        this.setupBindings();
    },
    onClose:function(){
        KYT.vent.unbind(this.options.gridId+":Redirect",this.showContacts,this);
        this.unbindBindings();
    },
    showContacts:function(id){
        KYT.vent.trigger("route",KYT.generateRoute("vendorcontactlist",0,id),true);
    }
});

KYT.Views.EquipmentVendorListView = KYT.Views.View.extend({
    initialize:function(){
        KYT.mixin(this, "ajaxGridMixin");
        KYT.mixin(this, "setupGridMixin");
        KYT.mixin(this, "defaultGridEventsMixin");
        KYT.mixin(this, "setupGridSearchMixin");
    },
    viewLoaded:function(){
        KYT.vent.bind(this.options.gridId+":Redirect",this.showContacts,this);
        this.setupBindings();
    },
    onClose:function(){
        KYT.vent.unbind(this.options.gridId+":Redirect",this.showContacts,this);
        this.unbindBindings();
    },
    showContacts:function(id){
        KYT.vent.trigger("route",KYT.generateRoute("vendorcontactlist",0,id),true);
    }
});

KYT.Views.DocumentFormView = KYT.Views.View.extend({
    initialize:function(){
        KYT.mixin(this, "formMixin");
        KYT.mixin(this, "ajaxFormMixin");
        KYT.mixin(this, "modelAndElementsMixin");
    },
    viewLoaded:function(){
        this.addIdsToModel();
    }
});

KYT.Views.PhotoFormView = KYT.Views.View.extend({
    initialize:function(){
        KYT.mixin(this, "formMixin");
        KYT.mixin(this, "ajaxFormMixin");
        KYT.mixin(this, "modelAndElementsMixin");
    },
    viewLoaded:function(){
        this.addIdsToModel();
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
        KYT.vent.bind("grid:equipmenttasktypelist:pageLoaded",function(options){
             options.deleteMultipleUrl = this.model._deleteMultipleEquipTaskTypeUrl();
        },this);
        KYT.vent.bind("grid:equipmentttypelist:pageLoaded",function(options){
             options.deleteMultipleUrl = this.model._deleteMultipleEquipTypeUrl();
        },this);
        KYT.vent.bind("grid:partlist:pageLoaded",function(options){
            options.deleteMultipleUrl = this.model._deleteMultiplePartsUrl();
        },this);
        KYT.vent.bind("grid:grasstypelist:pageLoaded",function(options){
            options.deleteMultipleUrl = this.model._deleteMultipleGrassTypeUrl();
        },this);
        this.taskTypeGridView = new KYT.Views.DahsboardGridView({
            el:"#taskTypeGridContainer",
            url:this.model._taskTypeGridUrl(),
            gridId:"tasktypelist",
            route:"tasktype"
        });
        this.eventTypeGridView = new KYT.Views.DahsboardGridView({
            el:"#eventTypeGridContainer",
            url:this.model._eventTypeGridUrl(),
            gridId:"eventtypelist",
            route:"eventtype"
        });
        this.photoCategoryGridView = new KYT.Views.DahsboardGridView({
            el:"#photoCategoryGridContainer",
            url:this.model._photoCategoryGridUrl(),
            gridId:"photocategorylist",
            route:"photocategory"
        });
        this.documentCategoryGridView = new KYT.Views.DahsboardGridView({
            el:"#documentCategoryGridContainer",
            url:this.model._documentCategoryGridUrl(),
            gridId:"documentcategorylist",
            route:"documentcategory"
        });
        this.equipmentTaskTypeGridView = new KYT.Views.DahsboardGridView({
            el:"#equipmentTaskTypeGridContainer",
            url:this.model._equipmentTaskTypeGridUrl(),
            gridId:"equipmenttasktypelist",
            route:"equipmenttasktype"
        });
        this.equipmentTypeGridView = new KYT.Views.DahsboardGridView({
            el:"#equipmentTypeGridContainer",
            url:this.model._equipmentTypeGridUrl(),
            gridId:"equipmenttypelist",
            route:"equipmenttype"
        });
        this.partGridView = new KYT.Views.DahsboardGridView({
            el:"#partsGridContainer",
            url:this.model._partsGridUrl(),
            gridId:"partlist",
            route:"part"
        });
        this.grassTypesGridView = new KYT.Views.DahsboardGridView({
            el:"#grassTypesGridContainer",
            url:this.model._grassTypesGridUrl(),
            gridId:"grasstypelist",
            route:"grasstype"
        });


        this.taskTypeGridView.render();
        this.eventTypeGridView.render();
        this.photoCategoryGridView.render();
        this.documentCategoryGridView.render();
        this.equipmentTaskTypeGridView.render();
        this.equipmentTypeGridView.render();
        this.partGridView.render();
        this.grassTypesGridView.render();
        this.storeChild(this.taskTypeGridView);
        this.storeChild(this.eventTypeGridView);
        this.storeChild(this.photoCategoryGridView);
        this.storeChild(this.documentCategoryGridView);
        this.storeChild(this.equipmentTaskTypeGridView);
        this.storeChild(this.equipmentTypeGridView);
        this.storeChild(this.partGridView);
        this.storeChild(this.grassTypesGridView);
    },
    callbackAction: function(){
        this.taskTypeGridView.callbackAction();
        this.eventTypeGridView.callbackAction();
        this.photoCategoryGridView.callbackAction();
        this.documentCategoryGridView.callbackAction();
        this.equipmentTaskTypeGridView.callbackAction();
        this.equipmentTypeGridView.callbackAction();
        this.partGridView.callbackAction();
        this.grassTypesGridView.callbackAction();
    }
});

KYT.Views.EventTypeFormView = KYT.Views.View.extend({
    initialize:function(){
        KYT.mixin(this, "formMixin");
        KYT.mixin(this, "ajaxFormMixin");
        KYT.mixin(this, "modelAndElementsMixin");
    },
    viewLoaded:function(){
        $('#colorPickerInput',this.el).miniColors();
    }
});

KYT.Views.TaskTypeFormView = KYT.Views.View.extend({
    initialize:function(){
        KYT.mixin(this, "formMixin");
        KYT.mixin(this, "ajaxFormMixin");
        KYT.mixin(this, "modelAndElementsMixin");
    },
    viewLoaded:function(){
        $('#colorPickerInput',this.el).miniColors();
    }
});

KYT.Views.EquipmentTypeFormView = KYT.Views.View.extend({
    initialize:function(){
        KYT.mixin(this, "formMixin");
        KYT.mixin(this, "ajaxFormMixin");
        KYT.mixin(this, "modelAndElementsMixin");
    },
    viewLoaded:function(){
        $('#colorPickerInput',this.el).miniColors();
    }
});

KYT.Views.InventoryDisplayView = KYT.Views.View.extend({
    initialize:function(){
        KYT.mixin(this, "displayMixin");
        KYT.mixin(this, "ajaxDisplayMixin");
        KYT.mixin(this, "modelAndElementsMixin");
        this.options.templateUrl += this.options.url.substr(this.options.url.indexOf("Display/")+7);
    },
    viewLoaded:function(){
        $('#colorPickerInput',this.el).miniColors();
    }
});

KYT.Views.WeatherListView = KYT.Views.View.extend({
    initialize:function(){
        this.options.gridOptions ={multiselect:false,
            sortorder:"desc",sortname:"Date"
        };
        KYT.mixin(this, "ajaxGridMixin");
        KYT.mixin(this, "setupGridMixin");
        KYT.mixin(this, "defaultGridEventsMixin");
        KYT.mixin(this, "setupGridSearchMixin");
    },
    viewLoaded:function(){this.setupBindings();},
    onClose:function(){this.unbindBindings();}
});

KYT.Views.PendingTaskListView = KYT.Views.View.extend({
    initialize: function(){
       this.options.gridId="pendingTaskList";
        KYT.mixin(this, "ajaxGridMixin");
        KYT.mixin(this, "setupGridMixin");
        KYT.mixin(this, "defaultGridEventsMixin");
        KYT.mixin(this, "setupGridSearchMixin");
    },
    viewLoaded:function(){
        this.setupBindings();},
    onClose:function(){this.unbindBindings();},
    callbackAction: function () {
        this.reloadGrid();
    }

});

KYT.Views.CompletedTaskListView = KYT.Views.View.extend({
    initialize: function(){
        this.options.gridId="completedTaskList";
        KYT.mixin(this, "ajaxGridMixin");
        KYT.mixin(this, "setupGridMixin");
        KYT.mixin(this, "defaultGridEventsMixin");
        KYT.mixin(this, "setupGridSearchMixin");
    },
    viewLoaded:function(){
        this.setupBindings();},
    onClose:function(){this.unbindBindings();}
});

KYT.Views.PendingEquipmentTaskListView = KYT.Views.View.extend({
    initialize: function(){
       this.options.gridId="pendingEquipmentTaskList";
        KYT.mixin(this, "ajaxGridMixin");
        KYT.mixin(this, "setupGridMixin");
        KYT.mixin(this, "defaultGridEventsMixin");
        KYT.mixin(this, "setupGridSearchMixin");
    },
    viewLoaded:function(){
        this.setupBindings();},
    onClose:function(){this.unbindBindings();},
    callbackAction: function () {
        this.reloadGrid();
    }

});

KYT.Views.CompletedEquipmentTaskListView = KYT.Views.View.extend({
    initialize: function(){
        this.options.gridId="completedEquipmentTaskList";
        KYT.mixin(this, "ajaxGridMixin");
        KYT.mixin(this, "setupGridMixin");
        KYT.mixin(this, "defaultGridEventsMixin");
        KYT.mixin(this, "setupGridSearchMixin");
    },
    viewLoaded:function(){
        this.setupBindings();},
    onClose:function(){this.unbindBindings();}
});

