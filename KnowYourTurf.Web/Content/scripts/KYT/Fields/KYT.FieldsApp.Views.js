/**
 * Created by JetBrains RubyMine.
 * User: Owner
 * Date: 2/26/12
 * Time: 11:24 AM
 * To change this template use File | Settings | File Templates.
 */

KYT.Views.CalendarView = KYT.Views.View.extend({
    render:function(){
        if(this.onPreRender)this.onPreRender();
       KYT.repository.ajaxGet(this.options.url, this.options.data).done($.proxy(this.renderCallback));
    },
    renderCallback:function(result){
        if(result.LoggedOut){
            window.location.replace(result.RedirectUrl);
            return;
        }
        $(this.el).html(result);
        if(extraFormOptions){
            $.extend(true, this.options, extraFormOptions);
        }

        var calContainer = this.options.calendarDef.calendarContainer;
        this.options.calendarDef.id=this.id;
        $(calContainer,this.el).asCalendar(this.options.calendarDef);
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
        KYT.repository.ajaxGet(this.options.calendarDef.EventChangedUrl,data).done($.proxy(function(result){this.changeEventCallback(result,revertFunc)},this));
    },
    eventResize:function( event, dayDelta, minuteDelta, revertFunc, jsEvent, ui, view ){
        var data = {"EntityId":event.EntityId,
            "ScheduledDate":$.fullCalendar.formatDate( event.start,"M/d/yyyy hh:mm TT"),
            "StartTime":$.fullCalendar.formatDate( event.start,"M/d/yyyy hh:mm TT"),
            "EndTime":$.fullCalendar.formatDate( event.end,"M/d/yyyy hh:mm TT")
        };
        KYT.repository.ajaxGet(this.options.calendarDef.EventChangedUrl,data).done($.proxy(function(result){this.changeEventCallback(result,revertFunc)},this));
    },
    dayClick:function(date, allDay, jsEvent, view) {
        var data = {"ScheduledDate" : $.fullCalendar.formatDate( date,"M/d/yyyy"),
            "ScheduledStartTime": $.fullCalendar.formatDate( date,"hh:mm TT")
        };
        this.editEvent(this.options.calendarDef.AddUpdateUrl,data);
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
            url: this.options.calendarDef.DisplayUrl,
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
        this.editEvent(this.options.calendarDef.AddUpdateUrl,data);
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
            KYT.repository.ajaxGet(this.options.calendarDef.deleteUrl,{"EntityId":entityId}).done(function(result){
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
        this.editEvent(this.options.calendarDef.AddUpdateUrl+"/"+id);
    },

    reload:function(){
        $(this.options.calendarDef.calendarContainer,this.el).fullCalendar( 'refetchEvents' )
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

KYT.Views.EmployeeDashboardView = KYT.Views.AjaxFormView.extend({
    events:_.extend({
    }, KYT.Views.AjaxFormView.prototype.events),
    initialize:function(){
        this.options.noBubbleUp=true;
        this.options.notificationArea = new cc.NotificationArea(this.cid,"#errorMessagesForm","#errorMessagesForm", KYT.vent);
        this._super("initialize",arguments);
    },
    viewLoaded:function(){
        this.loadTokenizers();
        this.pendingGridView = new KYT.Views.DahsboardGridView({el:"#pendingTaskGridContainer",
            url:this.model.pendingGridUrl(),
            gridContainer: "#pendingGridContainer",
            route:"task",
            id:"pending"
        });
        this.completedGridView = new KYT.Views.DahsboardGridView({el:"#completedTaskGridContainer",
          url:this.model.completedGridUrl(),
            gridContainer: "#completedGridContainer",
            id:"completed",
            route:"taskdisplay"});
        this.pendingGridView.render();
        this.completedGridView.render();
        this.storeChild(this.pendingGridView);
        this.storeChild(this.completedGridView);
    },
    loadTokenizers: function(){
        var options = $.extend({el:"#rolesInputRoot"}, this.options.rolesOptions);
        this.tokenView = new KYT.Views.TokenView(options);
        this.tokenView.render();
        this.storeChild(this.tokenView);
    },
    callbackAction: function(){
        this.pendingGridView.callbackAction();
        this.completedGridView.callbackAction();
    }
});

KYT.Views.FieldDashboardView = KYT.Views.AjaxFormView.extend({
    events:_.extend({
    }, KYT.Views.AjaxFormView.prototype.events),
    viewLoaded:function(){
        var rel = KYT.State.get("Relationships");
        $('#FieldColor',this.el).miniColors();
        this.pendingGridView = new KYT.Views.DahsboardGridView({
            el:"#pendingTaskGridContainer",
            url:this.options.pendingGridUrl,
            gridContainer: "#pendingGridHolder",
            id:"pending",
            parentId:rel.entityId,
            rootId: rel.parentId,
            route:"task"
        });
        this.completedGridView = new KYT.Views.DahsboardGridView({
            el:"#completedTaskGridContainer",
            url:this.options.completedGridUrl,
            gridContainer: "#completedGridHolder",
            id:"completed",
            parentId:rel.entityId,
            rootId: rel.parentId,
            route:"taskdisplay"
        });
        this.photoGridView = new KYT.Views.DahsboardGridView({
            el:"#photoGridContainer",
            url:this.options.photoGridUrl,
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
            url:this.options.documentGridUrl,
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
        this.options.errorMessagesContainer=this.options.messageContainer?this.options.messageContainer:this.options.errorMessagesContainer;
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

KYT.Views.TaskFormView = KYT.Views.AjaxFormView.extend({
    viewLoaded:function(){
        this.loadTokenizers();
        KYT.calculator.applyTaskTransferData(this.$el);
    },
    loadTokenizers:function(){
        var employeeTokenOptions = {
            id:"employee",
            el:this.$el.find("#employeeTokenizer"),
            availableItems:this.options.employeeOptions.availableItems,
            selectedItems:this.options.employeeOptions.selectedItems,
            inputSelector:this.options.employeeOptions.inputSelector
        };

        var equipmentTokenOptions = {
            id:"equipment",
            el:this.$el.find("#equipmentTokenizer"),
            availableItems:this.options.equipmentOptions.availableItems,
            selectedItems:this.options.equipmentOptions.selectedItems,
            inputSelector:this.options.equipmentOptions.inputSelector
        };

        this.employeeToken = new KYT.Views.TokenView(employeeTokenOptions);
        this.employeeToken.render();
        this.storeChild(this.employeeToken);

        this.equipmentToken = new KYT.Views.TokenView(equipmentTokenOptions);
        this.equipmentToken.render();

        this.storeChild(this.equipmentToken);
    }
});

KYT.Views.EmailJobFormView = KYT.Views.AjaxFormView.extend({
    viewLoaded:function(){
        this.loadTokenizers();
    },
    loadTokenizers:function(){
        var employeeTokenOptions = {
            id:"employee",
            el:this.$el.find("#employeeTokenizer"),
            availableItems:this.options.employeeOptions.availableItems,
            selectedItems:this.options.employeeOptions.selectedItems,
            inputSelector:this.options.employeeOptions.inputSelector
        };
        this.employeeToken = new KYT.Views.TokenView(employeeTokenOptions);
        this.employeeToken.render();
        this.storeChild(this.employeeToken);
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

KYT.Views.DocumentFormView = KYT.Views.AjaxFormView.extend({
     initialize:function(){
        this.options.notificationArea = new cc.NotificationArea(this.cid,"#errorMessagesDocGrid","#errorMessagesForm", KYT.vent);
        this._super("initialize",arguments);
    }
});

KYT.Views.PhotoFormView = KYT.Views.AjaxFormView.extend({
    initialize:function(){
       this.options.notificationArea = new cc.NotificationArea(this.cid,"#errorMessagesPhotoGrid","#errorMessagesForm", KYT.vent);
       this._super("initialize",arguments);
   }
});

KYT.Views.CalculatorFormView = KYT.Views.AjaxFormView.extend({
    events:_.extend({
        'click #createTask':'addTask'
    }, KYT.Views.AjaxFormView.prototype.events),

    addTask:function(){
        var fieldId = this.$el.find("[name=Field]").val();
        KYT.calculator.setTaskTransferData(this.$el);
        KYT.vent.trigger("route",KYT.generateRoute("task", 0, fieldId),true);
    },
    //calculate success handler
    successHandler:function(result){
        KYT.calculator.successHandler(result);
    }
});

KYT.Views.AppointmentView = KYT.Views.AjaxFormView.extend({
    events:_.extend({
        'change [name="Appointment.Length"]':'handleTimeChange',
        'change [name="sHour"]':'handleTimeChange',
        'change [name="sMinutes"]':'handleTimeChange',
        'change [name="sAMPM"]':'handleTimeChange'

    }, KYT.Views.AjaxFormView.prototype.events),

    viewLoaded:function(){
        this.loadTokenizers();
    },
    handleTimeChange:function(){
        var startTime = this.getStartTime();
        var endTimeString = this.getEndTimeString(startTime);
        $("#endTime").text(endTimeString);
    },
    getEndTimeString:function(startTime){
        var min;
        switch($("[name='Appointment.Length']").val()){
            case "Hour":
                min = 60;
                break;
            case "Half Hour":
                min = 30;
                break;
            case "Hour and a Half":
                min = 90;
                break;
        }
        var endTime = startTime.addMinutes(min);
        var endHour = endTime.getHours();
            var amPm = "AM";
            if(endHour>12){
                endHour-=12;
                amPm="PM";
            }
            var min = endTime.getMinutes().toString();
            if(min == "0"){
                min="00";
            }
            return endHour+":"+min+" "+amPm;
    },
    getStartTime:function(){
        var hour = $("[name='sHour']").val();
        var min = $("[name='sMinutes']").val();
        if($("[name='sAMPM']").val()=="PM"){
            hour = new Number(hour)+12;
        }
        return new XDate().setHours(hour).setMinutes(min);
    },

    loadTokenizers: function(){
        var options = $.extend({},this.options,{el:"#clients"});

        this.tokenView = new KYT.Views.TokenView(options);
        this.tokenView.render();
        this.storeChild(this.tokenView);
    }
});

KYT.Views.PaymentListView = KYT.Views.GridView.extend({
    addNew:function(){
        var parentId = $(this.el).find("#ParentId").val();
        KYT.vent.trigger("route",this.options.addUpate+"/0/"+parentId ,true);
    },
    editItem:function(id,itemType){
        var parentId = $(this.el).find("#ParentId").val();
        var _itemType = itemType?itemType:"";
        // fix this for all assets
        KYT.vent.trigger("route",this.options.addUpate+"/"+id+"/"+parentId,true);
    }
});

KYT.Views.PaymentFormView = KYT.Views.AjaxFormView.extend({
    events:_.extend({
    },KYT.Views.AjaxFormView.prototype.events),
    viewLoaded:function(){
        $("#fullHour").change($.proxy(function(e){
            this.calculateTotal("FullHour","#fullHourTotal",e.target);
        },this));
        $("#halfHour").change($.proxy(function(e){
            this.calculateTotal("HalfHour","#halfHourTotal",e.target);
        },this));
        $("#fullHourTenPack").change($.proxy(function(e){
            this.calculateTotal("FullHourTenPack","#fullHourTenPackTotal",e.target);
        },this));
        $("#halfHourTenPack").change($.proxy(function(e){
            this.calculateTotal("HalfHourTenPack","#halfHourTenPackTotal",e.target);
        },this));
        $("#pair").change($.proxy(function(e){
            this.calculateTotal("Pair","#pairTotal",e.target);
        },this));

        this.calculateTotal("FullHour","#fullHourTotal","#fullHour");
        this.calculateTotal("HalfHour","#halfHourTotal","#halfHour");
        this.calculateTotal("FullHourTenPack","#fullHourTenPackTotal","#fullHourTenPack");
        this.calculateTotal("HalfHourTenPack","#halfHourTenPackTotal","#halfHourTenPack");
        this.calculateTotal("Pair","#pairTotal","#pair");

    },
    calculateTotal:function(type, totalSelector, numberSelector){
        var number = $(numberSelector).val();
        var itemTotal = (this.options.sessionRates[type] * number);
        $(totalSelector).text("$" + itemTotal);
        var total = parseInt($("#fullHourTotal").text().substring(1))
            + parseInt($("#halfHourTotal").text().substring(1))
            + parseInt($("#fullHourTenPackTotal").text().substring(1))
            + parseInt($("#halfHourTenPackTotal").text().substring(1))
            + parseInt($("#pairTotal").text().substring(1));
        $("#total").val(total);

    }
});

KYT.Views.TrainerFormView = KYT.Views.AjaxFormView.extend({
     events:_.extend({
        'click #trainerPayments' : 'trainerPayments',
         'click #payTrainer' : 'payTrainer'
    }, KYT.Views.AjaxFormView.prototype.events),
    viewLoaded:function(){
        this.$el.find(this.options.crudFormSelector).data().crudForm.setBeforeSubmitFuncs(this.clientRateBeforeSubmit);
        this.loadPlugins();
        this.loadTokenizers();
    },
    clientRateBeforeSubmit:function(arr){
        var items =$("[name='ClientsInput']").data('selectedItems');
        if(items&&$(items).size()>0){
            $.each(items, function(i,item){
            arr.push({"name":"SelectedClients["+i+"].id","value":item.id});
            arr.push({"name":"SelectedClients["+i+"].name","value":item.name});
            arr.push({"name":"SelectedClients["+i+"].percentage","value":item.percentage});
            })
        }
    },
    loadTokenizers: function(){
        var clientOptions = $.extend({el:"#clients", id:"clientToken"},this.options.clientOptions);
        var userRoleOptions = $.extend({el:"#userRoles"},this.options.userRolesOptions);
        this.clientsView = new KYT.Views.TrainerEditableTokenView(clientOptions);
        this.clientsView.render();
        this.storeChild(this.clientsView);

        this.userRolesView = new KYT.Views.TokenView(userRoleOptions);
        this.userRolesView.render();
        this.storeChild(this.userRolesView);
    },
    loadPlugins:function(){
        $('#color',"#detailArea").miniColors();
    },
    trainerPayments:function(){
        var id = $(this.el).find("#EntityId").val();
        KYT.vent.trigger("route","trainerpaymentlist/"+id,true);
    },
    payTrainer:function(){
        var id = $(this.el).find("#EntityId").val();
        KYT.vent.trigger("route","paytrainerlist/"+id,true);
    }
});

KYT.Views.TrainerEditableTokenView = KYT.Views.EditableTokenView.extend({
     events:_.extend({
        'click .tokenEditor' : 'tokenEditor'
    }, KYT.Views.EditableTokenView.prototype.events),
    internalTokenMarkup: function(item) {
        var cssClass = "class='selectedItem'";
        return "<p><a " + cssClass + ">" + item.name+" ( "+item.percentage + " )</a><a href='javascript:void(0);' class='tokenEditor' >&nbsp;-- Edit</a><input id='itemId' type='hidden' value='"+item.id+"' </p>";
    },
    render:function(){
        KYT.vent.bind("popup:templatePopup:save",this.tokenSave,this);
        KYT.vent.bind("popup:templatePopup:cancel",this.tokenCancel,this);
    },
    onClose:function(){
        KYT.vent.unbind("popup:templatePopup:save");
        KYT.vent.unbind("popup:templatePopup:cancel");
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
    tokenEditor:function(e){
        this.options.currentlyEditing = $(e.target).prev("a");
        var id = $(e.target).next("input#itemId").val();
        var data = $(this.options.inputSelector,this.el).data("selectedItems");
        var dataItem;
        $.each(data,function(i,item){
            if(item.id == id) dataItem = item;
        });
        var buttons = this.options.buttons?this.options.buttons:KYT.Views.popupButtonBuilder.builder("templatePopup").standardEditButons();
        var popupOptions = {
            id:"templatePopup",
            buttons: buttons,
            data:dataItem,
            template:"#percentageTemplate"
        };
        this.templatedPopupView = new KYT.Views.TemplatedPopupView(popupOptions);
        this.templatedPopupView.render();
        this.storeChild(this.templatedPopupView);

    },
    tokenSave:function(){
        var id = $("#editingId").val();
        var data = $(this.options.inputSelector,this.options.el).data("selectedItems");
        var dataItem;
        $.each(data,function(i,item){
            if(item.id == id) dataItem = item;
        });
        dataItem.percentage = $("#newTrainerPercentage").val();
        var anchor = $(this.options.currentlyEditing).text();
        var newText = anchor.substr(0,anchor.indexOf('(')) +"( "+$("#newTrainerPercentage").val()+" ) ";
        $(this.options.currentlyEditing).text(newText);
//        KYT.vent.unbind("popup:templatePopup:save");
        this.templatedPopupView.close();
    },
    tokenCancel:function(){
        this.templatedPopupView.close();
    }

});

