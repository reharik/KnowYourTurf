if (typeof kyt == "undefined") {
            var kyt = {};
}

if (typeof kyt.employeeDashboard == "undefined") {
            kyt.employeeDashboard = {};
}

kyt.employeeDashboard.controller= (function(){
    return{
        init:function(){

            pendingGridContainerMetaData.name= "pendingGridContainer";
            pendingGridContainerMetaData.setDisplayButtonBuilder(function(builder){
                builder.addEditButton();
                builder.addButton("Copy Task", kyt.popupCrud.controller.copyItem);
                builder.addCancelButton();
                return  builder.getButtons();
            });
            pendingGridContainerMetaData.addRunAfterSuccess(
                function(result,metaData){ $("#completeGridContainer").trigger("reloadGrid");
                    $("#pendingGridContainer").trigger("reloadGrid");
                });
            pendingGridContainerMetaData.addRunAfterRender(function(){
                $("#EmployeeInput").tokenInput(availableEmployees?availableEmployees:{},{prePopulate: selectedEmployees,
                    internalTokenMarkup:function(item){return "<p><a class='selectedItem'>"+ item.name +"</a></p>";}});
                $("#EquipmentInput").tokenInput(availableEquipment?availableEquipment:{},{prePopulate: selectedEquipment,
                    internalTokenMarkup:function(item){return "<p><a class='selectedItem'>"+ item.name +"</a></p>";}});
                });
            pendingGridContainerMetaData.setGridName("#pendingGridContainer");

            completeGridContainerMetaData.name="completeGridContainer";
            completeGridContainerMetaData.setDisplayButtonBuilder(function(builder){
                builder.addButton("Copy Task", kyt.popupCrud.controller.copyItem);
                builder.addCancelButton();
                return  builder.getButtons();
            });
             completeGridContainerMetaData.addRunAfterSuccess(
                function(result,metaData){ $("#completeGridContainer").trigger("reloadGrid");
                    $("#pendingGridContainer").trigger("reloadGrid");
                });
            completeGridContainerMetaData.addRunAfterRender(function(){
                $("#EmployeeInput").tokenInput(availableEmployees?availableEmployees:{},{prePopulate: selectedEmployees,
                    internalTokenMarkup:function(item){return "<p><a class='selectedItem'>"+ item.name +"</a></p>";}});
                $("#EquipmentInput").tokenInput(availableEquipment?availableEquipment:{},{prePopulate: selectedEquipment,
                    internalTokenMarkup:function(item){return "<p><a class='selectedItem'>"+ item.name +"</a></p>";}});
                });
            completeGridContainerMetaData.setGridName("#completeGridContainer");

            $("#addNew").click(function(){kyt.popupCrud.controller.itemCRU(null,pendingGridContainerMetaData)});
        }
    }
}());