
KYT.Views.TasksByFieldView = KYT.Views.View.extend({
    initialize:function(){
        KYT.mixin(this, "ajaxFormMixin");
        KYT.mixin(this, "modelAndElementsMixin");
        KYT.mixin(this, "reportMixin");
    },
    createUrl:function(){
        return this.model.ReportUrl()+ "?FieldId="+this.model.FieldEntityId();
    }
});
