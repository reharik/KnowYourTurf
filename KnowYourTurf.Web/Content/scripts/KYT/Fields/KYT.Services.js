/**
 * Created by JetBrains RubyMine.
 * User: Owner
 * Date: 7/8/12
 * Time: 3:14 PM
 * To change this template use File | Settings | File Templates.
 */


KYT.calculator = (function(){
    var calculatorService = {
        successHandler:function(model,result){
            successHandlers[result._calculatorName](model,result);
        },
        setTaskTransferData:function(model){
            var wfs = KYT.WorkflowManager.workflowState().state();
            wfs.set("calculatorName",model._calculatorName());
            wfs.set("taskData",taskTransferData[model._calculatorName()](model));
        },
        applyTaskTransferData:function(model){
            var wfs = KYT.WorkflowManager.workflowState().state();
            if(wfs.get("calculatorName")){
                applyTaskData[wfs.get("calculatorName")](model,wfs.get("taskData"));
            }
        }
    };
    var successHandlers = {
        FertilizerNeeded: function(model, result){
            model.N(result.N);
            model.P(result.P);
            model.K(result.K);
            model.BagsNeeded(result.BagsNeeded);
            model.BagSize(result.BagSize);
            model.FieldArea(result.FieldArea);
            $("#createTask").removeClass('ui-state-disabled').removeAttr("disabled");
        },
        Materials: function(model, result){
           model.FieldArea(result.FieldArea);
           model.TotalMaterials(result.TotalMaterials);
            $("#createTask").removeClass('ui-state-disabled').removeAttr("disabled");
        },
        Sand: function(model,result){
            model.TotalSand(result.TotalSand);
            $("#createTask").removeClass('ui-state-disabled').removeAttr("disabled");
        },
        OverseedBagsNeeded:function(model, result){
            model.BagSize(result.BagSize);
            model.FieldArea(result.FieldArea);
            model.BagsNeeded(result.BagsNeeded);
            $("#createTask").removeClass('ui-state-disabled').removeAttr("disabled");
        },
        OverseedRateNeeded:function(model,result){
            model.BagSize(result.BagSize);
            model.FieldArea(result.FieldArea);
            model.SeedRate(result.SeedRate);
            $("#createTask").removeClass('ui-state-disabled').removeAttr("disabled");
        }
    };
    var taskTransferData = {
        FertilizerNeeded: function(model){
           return {FieldEntityId : model.FieldEntityId(),
                ProductEntityId:model.ProductEntityId(),
                Quantity: model.BagsNeeded()
            };
        },
        Materials: function(model){
           return {FieldEntityId : model.FieldEntityId(),
                Quantity: model.TotalMaterials()
            };
        },
        Sand: function(model){
           return {FieldEntityId : model.FieldEntityId(),
                Quantity: model.TotalSand()
            };
        },
        OverseedBagsNeeded:function(model){
           return {FieldEntityId : model.FieldEntityId(),
                ProductEntityId:model.ProductEntityId(),
                Quantity: model.BagsNeeded()
            };
        },
        OverseedRateNeeded:function(model){
           return {FieldEntityId : model.FieldEntityId(),
                ProductEntityId:model.ProductEntityId(),
                Quantity: model.BagsUsed()
            };
        }
    };
    var applyTaskData = {
        FertilizerNeeded: function(model, taskData){
            model.FieldEntityId(taskData.FieldEntityId);
            model.InventoryProductProductEntityId(taskData.ProductEntityId);
            model.QuantityNeeded(taskData.Quantity);
        },
        Materials: function(model, taskData){
            model.InventoryProductProductEntityId(taskData.ProductEntityId);
            model.QuantityNeeded(taskData.Quantity);
        },
        Sand: function(model, taskData){
            model.InventoryProductProductEntityId(taskData.ProductEntityId);
            model.QuantityNeeded(taskData.Quantity);
        },
        OverseedBagsNeeded:function(model, taskData){
            model.FieldEntityId(taskData.FieldEntityId);
            model.InventoryProductProductEntityId(taskData.ProductEntityId);
            model.QuantityNeeded(taskData.Quantity);
        },
        OverseedRateNeeded:function(model, taskData){
            model.FieldEntityId(taskData.FieldEntityId);
            model.InventoryProductProductEntityId(taskData.ProductEntityId);
            model.QuantityNeeded(taskData.Quantity);
        }
    };

    return calculatorService;
}());

KYT.loadTemplateAndModel = function(view){
    var d = new $.Deferred();
    $.when(Backbone.Marionette.TemplateCache.get(view.options.route,view.options.templateUrl),
        KYT.repository.ajaxGet(view.options.url, view.options.data))
    .done(function(html,data){
            view.$el.html(html);
            view.model = data[0];
                d.resolve();
        });
    return d.promise();
};
