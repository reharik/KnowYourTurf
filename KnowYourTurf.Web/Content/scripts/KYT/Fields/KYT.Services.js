/**
 * Created by JetBrains RubyMine.
 * User: Owner
 * Date: 7/8/12
 * Time: 3:14 PM
 * To change this template use File | Settings | File Templates.
 */


KYT.calculator = (function(){
    var calculatorService = {
        successHandler:function(result){
            var calculatorName = $("#CalculatorName").val();
            successHandlers[calculatorName](result);
        },
        setTaskTransferData:function($el){
            var wfs = KYT.WorkflowManager.workflowState().state();
            var calculatorName = $el.find("#CalculatorName").val();
            wfs.set("calculatorName",calculatorName);
            wfs.set("taskData",taskTransferData[calculatorName]());
        },
        applyTaskTransferData:function($el){
            var wfs = KYT.WorkflowManager.workflowState().state();
            if(wfs.get("calculatorName")){
                applyTaskData[wfs.get("calculatorName")]($el,wfs.get("taskData"));
            }
        }
    };
    var successHandlers = {
        FertilizerNeeded: function(result){
            $("#N").text(result.N.toString()).addClass("KYT_boldResult");
            $("#P").text(result.P.toString()).addClass("KYT_boldResult");
            $("#K").text(result.K.toString()).addClass("KYT_boldResult");
            $("#BagsNeeded").text(result.BagsNeeded.toString()).addClass("KYT_boldResult");
            $("#BagSize").text(result.BagSize.toString());
            $("#FieldArea").text(result.FieldArea.toString());
            $(":button:contains('Create Task')").removeClass('ui-state-disabled').removeAttr("disabled");
        },
        Materials: function(result){
            $("#FieldArea").text(result.FieldArea.toString());
            $("#TotalMaterials").text(result.TotalMaterials.toString()).addClass("KYT_boldResult");
            $(":button:contains('Create Task')").removeClass('ui-state-disabled').removeAttr("disabled");
        },
        Sand: function(result){
            $("#TotalSand").text(result.TotalSand.toString()).addClass("KYT_boldResult");
            $(":button:contains('Create Task')").removeClass('ui-state-disabled').removeAttr("disabled");
        },
        OverseedBagsNeeded:function(result){
          $("#BagSize").text(result.BagSize.toString());
            $("#FieldArea").text(result.FieldArea.toString());
            $("#BagsNeeded").text(result.BagsNeeded.toString()).addClass("KYT_boldResult");
            $(":button:contains('Create Task')").removeClass('ui-state-disabled').removeAttr("disabled");
        },
        OverseedRateNeeded:function(result){
            $("#BagSize").text(result.BagSize.toString());
            $("#FieldArea").text(result.FieldArea.toString());
            $("#SeedRate").text(result.SeedRate.toString()).addClass("KYT_boldResult");
            $(":button:contains('Create Task')").removeClass('ui-state-disabled').removeAttr("disabled");
        }
    };
    var taskTransferData = {
        FertilizerNeeded: function(){
           return {Field : $("[name=Field]").val(),
                Product:$("[name=Product]").val(),
                Quantity: $("#BagsNeeded").text()
            };
        },
        Materials: function(){
            return {Field : $("[name=Field]").val(),
                Quantity: $("#TotalMaterials").text()
            };
        },
        Sand: function(){
            return {Field : $("[name=Field]").val(),
                Quantity: $("#TotalSand").text()
            };
        },
        OverseedBagsNeeded:function(){
            return {Field : $("[name=Field]").val(),
                Product:$("[name=Product]").val(),
                Quantity: $("#BagsNeeded").text()
            };
        },
        OverseedRateNeeded:function(){
            return {Field : $("[name=Field]").val(),
                Product:$("[name=Product]").val(),
                Quantity: $("#BagsUsed").val()
            };
        }
    };
    var applyTaskData = {
        FertilizerNeeded: function($el, taskData){
            $el.find("[name='Item.ReadOnlyField.EntityId']").val(taskData.Field);
            $el.find("[name='Item.ReadOnlyInventoryProduct.ReadOnlyProduct.EntityId']").val(taskData.Product);
            $el.find("[name='Item.QuantityNeeded']").val(taskData.Quantity);
        },
        Materials: function($el, taskData){
            $el.find("[name='Item.ReadOnlyInventoryProduct.ReadOnlyProduct.EntityId']").val(taskData.Field);
            $el.find("[name='Item.QuantityNeeded']").val(taskData.Quantity);
        },
        Sand: function($el, taskData){
            $el.find("[name='Item.ReadOnlyInventoryProduct.ReadOnlyProduct.EntityId']").val(taskData.Field);
            $el.find("[name='Item.QuantityNeeded']").val(taskData.Quantity);
        },
        OverseedBagsNeeded:function($el, taskData){
            $el.find("[name='Item.ReadOnlyField.EntityId']").val(taskData.Field);
            $el.find("[name='Item.ReadOnlyInventoryProduct.ReadOnlyProduct.EntityId']").val(taskData.Product);
            $el.find("[name='Item.QuantityNeeded']").val(taskData.Quantity);
        },
        OverseedRateNeeded:function($el, taskData){
            $el.find("[name='Item.ReadOnlyField.EntityId']").val(taskData.Field);
            $el.find("[name='Item.ReadOnlyInventoryProduct.ReadOnlyProduct.EntityId']").val(taskData.Product);
            $el.find("[name='Item.QuantityNeeded']").val(taskData.Quantity);
        }
    };

    return calculatorService;
}());

KYT.loadTemplateAndModel = function(options, $el, callback){
     var d = new $.Deferred();
        Backbone.Marionette.TemplateCache.get(options.route,options.templateUrl)
            .pipe(function(result){
                $el.html(result);
                d.resolve();
            });
        d.done(function(){
            var modelPromise = KYT.repository.ajaxGet(options.url, options.data);
            modelPromise.done(callback)
        });
};
