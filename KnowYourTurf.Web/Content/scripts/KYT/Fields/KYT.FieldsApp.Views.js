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
        var data = {"ScheduledDate" : $.fullCalendar.formatDate( date,"M/d/yyyy"),
            "ScheduledStartTime": $.fullCalendar.formatDate( date,"hh:mm TT")
        };
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
            data:data,
            buttons:builder.getButtons()
        };
        this.ajaxPopupDisplay = new KYT.Views.AjaxPopupDisplayModule(formOptions);
        this.ajaxPopupDisplay.render();
        this.storeChild(this.ajaxPopupDisplay);
        $(this.el).append(this.ajaxPopupDisplay.el);

    },
    editEvent:function(url, data){
        var rootId = KYT.State.get("Relationships").rootId;
        var _data = $.extend({"RootId":rootId, Popup:true},data,{});
        var formOptions = {
            id: "editModule",
            url: url,
            data:_data,
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
        var entityId = $("#EntityId",this.ajaxPopupDisplay.el).val();
        var data = {"EntityId":entityId,"Copy":true};
        this.editEvent(this.model.AddUpdateUrl,data);
        this.ajaxPopupDisplay.close();
        //this feels retarded for some reason
        KYT.vent.bind("form:editModule:pageLoaded", function(){
            $(this.ajaxPopupFormModule.el).find("#EntityId").val(0);
            KYT.vent.unbind("form:editModule:pageLoaded");
        },this);
    },

    deleteItem: function(){
        var that = this;
        if (confirm("Are you sure you would like to delete this Item?")) {
            var entityId = $("#EntityId").val();
            KYT.repository.ajaxGet(this.model.DeleteUrl,{"EntityId":entityId}).done(function(result){
                that.ajaxPopupDisplay.close();
//                if(!result.Success){
//                    alert(result.Message);
//                }else{
                   that.reload();
//                }
            });
        }
    },
    displayEdit:function(event){
        var id = $("#EntityId",this.ajaxPopupDisplay.el).val();
        this.ajaxPopupDisplay.close();
        this.editEvent(this.model.AddUpdateUrl+"/"+id);
    },

    reload:function(){
        $('#calendar',this.el).fullCalendar( 'refetchEvents' )
    },

    formSuccess:function(){
        this.formCancel();
        this.reload();
    },
    formCancel:function(){
        this.ajaxPopupFormModule.close();
    },
    displayCancel:function(){
        this.ajaxPopupDisplay.close();
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

KYT.Views.DahsboardGridView = KYT.Views.GridView.extend({
    events:{
        'click .new' : 'addNew',
        'click .delete' : 'deleteItems'
    },
    initialize:function(){
        this._super("initialize",arguments);
    },

    viewLoaded:function(){
        KYT.vent.bind(this.options.id+":AddUpdateItem",this.editItem,this);
        KYT.vent.bind(this.options.id+":DisplayItem",this.displayItem,this);
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
        KYT.vent.unbind("AddUpdateItem");
        KYT.vent.unbind("DisplayItem");
        KYT.vent.unbind(this.options.id+":AddUpdateItem");
        KYT.vent.unbind(this.options.id+":DisplayItem");
    }
});

KYT.Views.TaskFormView = KYT.Views.View.extend({
    initialize:function(){
        KYT.mixin(this, "formMixin");
        KYT.mixin(this, "ajaxFormMixin");
        KYT.mixin(this, "modelAndElementsMixin");
    },
    viewLoaded:function(){
        KYT.calculator.applyTaskTransferData(this.model);
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

KYT.Views.DocumentFormView = KYT.Views.View.extend({
    initialize:function(){
        KYT.mixin(this, "formMixin");
        KYT.mixin(this, "ajaxFormMixin");
        KYT.mixin(this, "modelAndElementsMixin");
    },
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
        KYT.mixin(this, "formMixin",{
            saveItem:function(){
                var data = JSON.stringify(ko.mapping.toJS(this.model,this.mappingOptions));
                var promise = KYT.repository.ajaxPostModel(this.model._calculateUrl(),data);
                promise.done($.proxy(this.successHandler,this));
            },
            successHandler:function(result){
                KYT.calculator.successHandler(this.model,result);
            }
        });
        KYT.mixin(this, "ajaxFormMixin");
        KYT.mixin(this, "modelAndElementsMixin");
        this.options.templateUrl += this.options.url.substring(this.options.url.lastIndexOf("/"));
    },
    events:{'click #createTask':'addTask'},
    addTask:function(){
        var fieldId = this.model.FieldEntityId();
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


