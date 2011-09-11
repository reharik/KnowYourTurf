if (!kyt) var kyt = {};
if (!kyt.calculator) kyt.calculator= {};

kyt.calculator.successHandlers = (function(){
    return{
        success:function(result){
            var calculatorName = $("#CalculatorName").val();
            kyt.calculator.successHandlers[calculatorName](result);
        },
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
           if(!result.Success){
                notification.result(result);
                return;
            }
            $("#BagSize").text(result.BagSize.toString());
            $("#FieldArea").text(result.FieldArea.toString());
            $("#BagsNeeded").text(result.BagsNeeded.toString()).addClass("KYT_boldResult");
            $(":button:contains('Create Task')").removeClass('ui-state-disabled').removeAttr("disabled");
        },
        OverseedRateNeeded:function(result){
           if(!result.Success){
                notification.result(result);
                return;
            }
            $("#BagSize").text(result.BagSize.toString());
            $("#FieldArea").text(result.FieldArea.toString());
            $("#SeedRate").text(result.SeedRate.toString()).addClass("KYT_boldResult");
            $(":button:contains('Create Task')").removeClass('ui-state-disabled').removeAttr("disabled");
        }

    }
}());


kyt.calculator.handleAddTask = (function(){
    return{
        addTask:function(){
            var metaData = $(".ui-dialog").data().metaData;
            var taskMetaData = metaData.getMetaDatas()[0];
            taskMetaData.addRunAfterRender(function() {
                $("#EmployeeInput").tokenInput(availableEmployees?availableEmployees:{},{prePopulate: selectedEmployees,
                  internalTokenMarkup:function(item){return "<p><a class='selectedItem'>"+ item.name +"</a></p>";}});
                $("#EquipmentInput").tokenInput(availableEquipment?availableEquipment:{},{prePopulate: selectedEquipment,
                  internalTokenMarkup:function(item){return "<p><a class='selectedItem'>"+ item.name +"</a></p>";}});
            });
            var calculatorName = $("#CalculatorName").val();
            kyt.calculator.handleAddTask[calculatorName](taskMetaData);
        },
        FertilizerNeeded: function(metaData){
            var data = {Field : $("[name=Field]").val(),
                Product:$("[name=Product]").val()+ "_Fertilizers",
                Quantity: $("#BagsNeeded").text()
            };
            kyt.popupCrud.repository.itemCall(createATaskUrl, metaData,data);
        },
        Materials: function(metaData){
            var data = {Field : $("[name=Field]").val(),
                Quantity: $("#TotalMaterials").text()
            };
            kyt.popupCrud.repository.itemCall(createATaskUrl, metaData,data);
        },
        Sand: function(metaData){
            var data = {Field : $("[name=Field]").val(),
                Quantity: $("#TotalSand").text()
            };
            kyt.popupCrud.repository.itemCall(createATaskUrl, metaData,data);
        },
        OverseedBagsNeeded:function(metaData){
            var data = {Field : $("[name=Field]").val(),
                Product:$("[name=Product]").val()+ "_Seeds",
                Quantity: $("#BagsNeeded").text()
            };
            kyt.popupCrud.repository.itemCall(createATaskUrl, metaData,data);
        },
        OverseedRateNeeded:function(metaData){
            var data = {Field : $("[name=Field]").val(),
                Product:$("[name=Product]").val()+ "_Seeds",
                Quantity: $("#BagsUsed").val()
            };
            kyt.popupCrud.repository.itemCall(createATaskUrl, metaData,data);
        }
    }
}());
