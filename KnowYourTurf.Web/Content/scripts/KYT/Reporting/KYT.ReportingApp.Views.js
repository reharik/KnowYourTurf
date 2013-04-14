
KYT.Views.TaskReportView = KYT.Views.View.extend({
    initialize:function(){
        KYT.mixin(this, "ajaxFormMixin");
        KYT.mixin(this, "modelAndElementsMixin");
        KYT.mixin(this, "reportMixin");
        KYT.vent.bind("model:"+this.id+"modelLoaded", $.proxy(this.modelLoaded,this));
    },
    modelLoaded:function(){
        this.model.StartDate("");
        this.model.EndDate("");
    },
    createUrl:function(){
        var field = this.model.FieldEntityId() || 0;
        var product = this.model.ProductEntityId() || 0;
        var employee = this.model.EmployeeEntityId() || 0;
        var taskType = this.model.TaskTypeEntityId() || 0;
        var startDate = this.model.StartDate()||"1/1/1800";
        var endDate = this.model.EndDate()||"1/1/1800";
        return this.model.ReportUrl() + "?FieldId=" + field + "&" +
            "ClientId=" + this.model.ClientId() + "&" +
            "StartDate=" + startDate + "&" +
            "EndDate=" + endDate + "&" +
            "ProductId=" + product + "&" +
            "EmployeeId=" + employee + "&" +
            "TaskTypeId=" + taskType;
    }
});

KYT.Views.EquipmentTaskReportView = KYT.Views.View.extend({
    initialize:function(){
        KYT.mixin(this, "ajaxFormMixin");
        KYT.mixin(this, "modelAndElementsMixin");
        KYT.mixin(this, "reportMixin");
        KYT.vent.bind("model:"+this.id+"modelLoaded", $.proxy(this.modelLoaded,this));
    },
    modelLoaded:function(){
        this.model.StartDate("");
        this.model.EndDate("");
    },
    createUrl:function(){
        var equipment = this.model.EquipmentEntityId() || 0;
        var employee = this.model.EmployeeEntityId() || 0;
        var taskType = this.model.TaskTypeEntityId() || 0;
        var startDate = this.model.StartDate()||"1/1/1800";
        var endDate = this.model.EndDate()||"1/1/1800";
        return this.model.ReportUrl() + "?EquipmentId=" + equipment + "&" +
            "ClientId=" + this.model.ClientId() + "&" +
            "StartDate=" + startDate + "&" +
            "EndDate=" + endDate + "&" +
            "Complete=" + this.model.Complete() + "&" +
            "EmployeeId=" + employee + "&" +
            "TaskTypeId=" + taskType;
    }
});

KYT.Views.EmployeeDailyTasksFormView = KYT.Views.View.extend({
    initialize:function(){
        KYT.mixin(this, "ajaxFormMixin");
        KYT.mixin(this, "modelAndElementsMixin");
        KYT.mixin(this, "reportMixin");
    },
    createUrl:function(){
        return this.model.ReportUrl()+ "?Date="+this.model.Date()+"&" +
            "ClientId="+this.model.ClientId();
    }
});

KYT.Views.TDAView = KYT.Views.View.extend({
    initialize:function(){
        KYT.mixin(this, "ajaxFormMixin");
        KYT.mixin(this, "modelAndElementsMixin");
        KYT.mixin(this, "reportMixin");
    },
    createUrl:function(){
        return this.model.ReportUrl()+  "?StartDate=" + this.model.StartDate()+ "&" +
            "EndDate=" + this.model.EndDate()+ "&" +
            "ClientId="+this.model.ClientId();
    }
});
