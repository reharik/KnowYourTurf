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
        if(typeof this.options.runAfterRenderFunction == 'function'){
            this.options.runAfterRenderFunction.apply(this,[this.el]);
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