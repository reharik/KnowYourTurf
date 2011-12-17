/**
 * Created by JetBrains RubyMine.
 * User: RHarik
 * Date: 10/7/11
 * Time: 7:44 AM
 * To change this template use File | Settings | File Templates.
 */

if (typeof kyt == "undefined") {
    var kyt = {};
}

kyt.BaseFormView = Backbone.View.extend({
    events:{
        'click #save' : 'saveItem',
        'click .cancel' : 'cancel'
    },

    initialize: function(){
       this.options = $.extend({},kyt.formDefaults,this.options);
       this.id=this.options.id;
    },
    config:function(){
        if(extraFormOptions){
            $.extend(true, this.options, extraFormOptions);
        }

        this.options.crudFormOptions.successHandler = $.proxy(this.successHandler,this);
        $(this.options.crudFormSelector,this.el).crudForm(this.options.crudFormOptions);
        if(this.options.crudFormOptions.additionBeforeSubmitFunc){
            var array = !$.isArray(this.options.crudFormOptions.additionBeforeSubmitFunc) ? [this.options.crudFormOptions.additionBeforeSubmitFunc] : this.options.crudFormOptions.additionBeforeSubmitFunc;
            $(array).each($.proxy(function(i,item){
                $(this.options.crudFormSelector,this.el).data('crudForm').setBeforeSubmitFuncs(item);
            },this));
        }
        $(".rte").cleditor();
    },
    saveItem:function(){
        $(this.options.crudFormSelector,this.el).submit();
    },
    cancel:function(){
        $.publish("/contentLevel/form_"+this.id+"/cancel",[this.id]);
    },
    successHandler:function(result, form, notification){
        var emh = cc.utilities.messageHandling.messageHandler();
        var message = cc.utilities.messageHandling.mhMessage("success",result.Message,"");
        emh.addMessage(message);
        emh.showAllMessages(notification.getSuccessContainer());
        $.publish("/contentLevel/form_"+this.id+"/success",[result,form]);
    }
});

kyt.FormView = kyt.BaseFormView.extend({
    initialize: function(){
        this.options = $.extend({},kyt.formDefaults,this.options);
        this.id=this.options.id;
        this.config();
        this.render();
    },
    render: function(){
        $.publish("/contentLevel/form_"+this.id+"/pageLoaded",[this.options]);
        return this;
    }
});
kyt.AjaxFormView = kyt.BaseFormView.extend({
    render:function(){
        kyt.repository.ajaxGet(this.options.url, this.options.data, $.proxy(function(result){this.renderCallback(result)},this));
    },
    renderCallback:function(result){
        if(result.LoggedOut){
            window.location.replace(result.RedirectUrl);
            return;
        }
        $(this.el).html(result);
        this.config();
        //callback for render
        this.viewLoaded();
        //general notification of pageloaded
        $.publish("/contentLevel/form_"+this.id+"/pageLoaded",[this.options]);
    }
});

kyt.ComplianceItemsFormView = kyt.FormView.extend({
    events:
        _.extend({
        'change [name="ComplianceNotificationSchedule.DaysBeforeExpiration"]':'daysBefore',
        'change [name="ComplianceNotificationSchedule.RepeatDaysUntilExpiration"]':'daysUntil',
        'change [name="ComplianceNotificationSchedule.RepeatDaysAfterExpiration"]':'daysAfter',
        'change [name="ComplianceNotificationSchedule.EndDaysAfterExpiration"]':'endDay'
    }, kyt.FormView.prototype.events),
    render:function(){
        $.publish("/contentLevel/form_"+this.id+"/pageLoaded",[this.options]);
        this.attachPlugins();
        this.prePopulate();
    },
    attachPlugins:function(){
        $("#daysBefore", this.el).slider({min:0,max:60,slide:function(event,ui){$("[name='ComplianceNotificationSchedule.DaysBeforeExpiration']").val(ui.value);}});
        $("#repeatDaysUntil", this.el).slider({min:0,max:60,slide:function(event,ui){$("[name='ComplianceNotificationSchedule.RepeatDaysUntilExpiration']").val(ui.value);}});
        $("#repeatDaysAfter", this.el).slider({min:0,max:60,slide:function(event,ui){$("[name='ComplianceNotificationSchedule.RepeatDaysAfterExpiration']").val(ui.value);}});
        $("#endPeriod", this.el).slider({min:0,max:60,slide:function(event,ui){$("[name='ComplianceNotificationSchedule.EndDaysAfterExpiration']").val(ui.value);}});

    },
    prePopulate: function(){
        $("#daysBefore", this.el ).slider( "option", "value", $("[name='ComplianceNotificationSchedule.DaysBeforeExpiration']").val());
        $( "#repeatDaysUntil", this.el ).slider( "option", "value", $("[name='ComplianceNotificationSchedule.RepeatDaysUntilExpiration']").val());
        $( "#repeatDaysAfter", this.el ).slider( "option", "value", $("[name='ComplianceNotificationSchedule.RepeatDaysAfterExpiration']").val());
        $( "#endPeriod", this.el ).slider( "option", "value", $("[name='ComplianceNotificationSchedule.EndDaysAfterExpiration']").val());
    },
    daysBefore:function(){
        $("#daysBefore", this.el).slider( "option", "value", $(this).val() );
    },
    daysUntil:function(){
        $("#repeatDaysUntil", this.el).slider( "option", "value", $(this).val() );
    },
    daysAfter:function(){
         $("#repeatDaysAfter", this.el).slider( "option", "value", $(this).val() );
    },
    endDay:function(){
        $("#endPeriod", this.el).slider( "option", "value", $(this).val() );
    }
});

kyt.AssetFormView = kyt.AjaxFormView.extend({
    events:_.extend({
        'click .delete':'deleteItem',
        'click .print':'print'
    }, kyt.AjaxFormView.prototype.events),
    deleteItem:function(){
        if (confirm("Are you sure you would like to delete this Item?")) {
            var entityId = $(this.el).find("#EntityId").val();
            kyt.repository.ajaxGet(this.options.deleteUrl,{"EntityId":entityId}, $.proxy(function(result){
            $.publish("/contentLevel/form_"+this.id+"/success",[result])
            },this));
         }
    },
    print:function(){
        $(this.el).jqprint();
    }
});

kyt.PortfolioFormView = kyt.AjaxFormView.extend({
    events:_.extend({
        'click .delete':'deleteItem',
        'click .print':'print',
        'click .preview':'previewItem',
        'click .addToPortfolio':'addAllItems',
        'click .rmvpage':'removeAllItems',
        'click .download':'downloadItem',
        'click .mail':'emailForm',
        'click #changePictureButton':'changePictureClick',
        'change [name="Item.HeadShot.EntityId"]':'headshotChange',
        'click #addHeadShot':'setupNewHeadshot'

    }, kyt.AjaxFormView.prototype.events),

    viewLoaded:function(){
        if(!$("#headShotSelector").val()){
            $("#changePictureButton").hide();
            $("#headShotSelector").show();
        }else{
            $("#changePictureButton").show();
            $("#headShotSelector").hide();
        }
    },
    setupNewHeadshot: function(){
        $.publish("/contentLevel/form_"+this.id+"/setupNewHeadshot",[]);
        return false;
    },
    changePictureClick:function(){
        $("#changePictureButton").hide();
        $("#headShotSelector").show();
    },

    headshotChange:function(e){
        var id = $(e.target).val();
        $.each(this.options.headShotDtos,function(i,item){
            if(item.EntityId == id){
                $("a#FileUrl img").attr("src",item.Url);
            }
        });
        $("#changePictureButton").show();
        $("#headShotSelector").hide();
    },

    deleteItem:function(){
        if (confirm("Are you sure you would like to delete this Item?")) {
            var entityId = $(this.el).find("#EntityId").val();
            kyt.repository.ajaxGet(this.options.deleteUrl,{"EntityId":entityId}, $.proxy(function(result){
                $.publish("/contentLevel/form_"+this.id+"/success",[result])
            },this));
         }
    },
    print:function(){
        var id = $("#EntityId",this.el).val();
        if(id<=0){
            this.saveFirstAlert();
            return false;
        }
        $.publish("/contentLevel/form_"+this.id+"/print",[]);
    },
    emailForm:function(){
        var id = $("#EntityId",this.el).val();
        if(id<=0){
            this.saveFirstAlert();
            return false;
        }
        $.publish("/contentLevel/form_"+this.id+"/emailPortfolio",[]);
    },
    previewItem:function(){
        var id = $("#EntityId",this.el).val();
        if(id<=0){
            this.saveFirstAlert();
            return false;
        }
        $.publish("/contentLevel/form_"+this.id+"/preview",[]);
    },
    downloadItem:function(){
        var id = $("#EntityId",this.el).val();
        if(id<=0){
            this.saveFirstAlert();
            return false;
        }
        window.open(this.options.downloadUrl+"/"+id);
    },
    addAllItems:function(){
        var id = $("#EntityId",this.el).val();
        if(id<=0){
            this.saveFirstAlert();
            return false;
        }
        kyt.repository.ajaxGet(this.options.addAllItemsUrl,{"EntityId":id},$.proxy(function(){
           $.publish("/contentLevel/form_"+this.id+"/addAllItems",[]);
        },this));
    },
    removeAllItems: function(){
        var id = $("#EntityId",this.el).val();
        if(id<=0){
            this.saveFirstAlert();
            return false;
        }
        kyt.repository.ajaxGet(this.options.removeAllItemsUrl,{"EntityId":id},$.proxy(function(){
            $.publish("/contentLevel/form_"+this.id+"/removeAllItems",[]);
        },this))
    },
    saveFirstAlert:function(){
        alert("You must save your portfolio before perfoming this function!");
    }


});


kyt.userProfileViewModel =  {
    AddressItem: function(data) {
        ko.mapping.fromJS(data, {}, this);
        this.removeItem = function() {
            kyt.userProfileViewModel.addressItems.remove(this)
        }
    },
    EmailItem: function(data) {
        ko.mapping.fromJS(data, {}, this);
        this.removeItem = function() {
            kyt.userProfileViewModel.emailItems.remove(this)
        }
    },
    PhoneItem: function(data) {
        ko.mapping.fromJS(data, {}, this);
        this.removeItem = function() {
            kyt.userProfileViewModel.phoneItems.remove(this)
        }
    },
    addAddress:function() {
        var el = $("#addressContainer");
        $("form",el).validate({
            errorContainer: $(kyt.formDefaults.crudFormOptions.errorContainer),
            errorLabelContainer: $(kyt.formDefaults.crudFormOptions.errorContainer).find("ul"),
            wrapper: 'li',
            validClass: "valid_field",
            errorClass: "invalid_field"
        });
        if($("form", el).valid()){

            var addressItem = {
                Type:$("[name='Type']", el).val(),
                IsDefault:$("[name='IsDefault']", el).val(),
                Address1:$("[name='Address1']", el).val(),
                Address2:$("[name='Address2']", el).val(),
                City:$("[name='City']", el).val(),
                State:$("[name='State']", el).val(),
                ZipCode:$("[name='ZipCode']", el).val(),
                EntityId:$("[name='EntityId']", el).val(),
                AddressType:{EntityId:$("[name='AddressType'] :selected", el).val(),
                    Name:$("[name='AddressType'] :selected", el).text()}
            };
            var newAddress = new this.AddressItem(addressItem);
            this.addressItems.push(newAddress);
            el.clearForm();
        }
    },
    addPhone:function() {
        var el = $("#phoneContainer");
        $("form",el).validate({
           errorContainer: $(kyt.formDefaults.crudFormOptions.errorContainer),
           errorLabelContainer: $(kyt.formDefaults.crudFormOptions.errorContainer).find("ul"),
           wrapper: 'li',
           validClass: "valid_field",
           errorClass: "invalid_field"
        });
        if($("form", el).valid()){

        var phoneItem = {
            Type:$("[name='Type']", el).val(),
            IsDefault:$("[name='IsDefault']", el).val(),
            PhoneNumber:$("[name='PhoneNumber']", el).val(),
            EntityId:$("[name='EntityId']", el).val(),
            PhoneType:{EntityId:$("[name='PhoneType'] :selected", el).val(),
                Name:$("[name='PhoneType'] :selected", el).text()}
        };
        var newPhone = new this.PhoneItem(phoneItem);
        this.phoneItems.push(newPhone);
        el.clearForm();
        }
    },
    addEmail:function() {
        var el = $("#emailContainer");

        $("form",el).validate({
           errorContainer: $(kyt.formDefaults.crudFormOptions.errorContainer),
           errorLabelContainer: $(kyt.formDefaults.crudFormOptions.errorContainer).find("ul"),
           wrapper: 'li',
           validClass: "valid_field",
           errorClass: "invalid_field"
        });
        if($("form", el).valid()){
            var emailItem = {
                IsDefault:$("[name='IsDefault']", el).val(),
                EmailAddress:$("[name='EmailAddress']", el).val(),
                EntityId:$("[name='EntityId']", el).val()
            };
            var newEmail = new this.EmailItem(emailItem);
            this.emailItems.push(newEmail);
            el.clearForm();
        }
    },
    mapping:{
        'addressItems': {
            create: function(options) {
                return new kyt.userProfileViewModel.AddressItem(options.data);
            }
        },
        'emailItems': {
            create: function(options) {
                return new kyt.userProfileViewModel.EmailItem(options.data);
            }
        },
        'phoneItems': {
            create: function(options) {
                return new kyt.userProfileViewModel.PhoneItem(options.data);
            }
        }
    }
};


kyt.UserProfileView = kyt.AjaxFormView.extend({
    events:_.extend({
    }, kyt.AjaxFormView.prototype.events),
    initialize: function(){
        this.options = $.extend({},kyt.formDefaults,this.options);
        this.id=this.options.id;
        this.options.crudFormOptions.additionBeforeSubmitFunc = this.beforeSubmitFunc;
    },
    viewLoaded:function(){
        ko.mapping.defaultOptions().ignore =["TenantId", "OrgId", "CreateDate", "ChangeDate", "ChangedBy", "Archived"];
        ko.mapping.fromJS(this.options.repeaters, kyt.userProfileViewModel.mapping, kyt.userProfileViewModel);
        ko.applyBindings(kyt.userProfileViewModel);
        $.toDictionary(ko.mapping.toJSON(kyt.userProfileViewModel.addressItems))
    },
    beforeSubmitFunc:function(arr){
        $.each($.toDictionary(ko.mapping.toJS(kyt.userProfileViewModel.addressItems),"User.Addresses"),function(){arr.push(this)});
        $.each($.toDictionary(ko.mapping.toJS(kyt.userProfileViewModel.emailItems),"User.Emails"),function(){arr.push(this)});
        $.each($.toDictionary(ko.mapping.toJS(kyt.userProfileViewModel.phoneItems),"User.Phones"),function(){arr.push(this)});
    }
});

kyt.formDefaults = {
    id:"",
    data:{},
    crudFormSelector:"#CRUDForm",
    crudFormOptions:{
        errorContainer:"#errorMessagesForm",
        successContainer:"#errorMessagesGrid",
        additionBeforeSubmitFunc:null
    },
    runAfterRenderFunction: null
};