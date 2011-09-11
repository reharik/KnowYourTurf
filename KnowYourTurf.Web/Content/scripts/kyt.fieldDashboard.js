/**
 * Created by .
 * User: Harik
 * Date: 8/11/11
 * Time: 10:10 PM
 * To change this template use File | Settings | File Templates.
 */

if (typeof kyt == "undefined") {
            var kyt = {};
}

if (typeof kyt.fieldDashboard == "undefined") {
            kyt.fieldDashboard = {};
}

kyt.fieldDashboard.controller= (function(){
    return{
        init:function(){
            kyt.employeeDashboard.controller.init();
            documentGridContainerMetaData.name= "documentGridContainer";
            documentGridContainerMetaData.setDisplayButtonBuilder(function(builder){
                builder.addEditButton();
                builder.addCancelButton();
                return  builder.getButtons();
            });
            documentGridContainerMetaData.addRunAfterSuccess(
                function(result,metaData){ $("#documentGridContainer").trigger("reloadGrid");});
            documentGridContainerMetaData.setGridName("#documentGridContainer");
            documentGridContainerMetaData.addSubmitData({"From":"Field","ParentId":entityId});

            photoGridContainerMetaData.name="photoGridContainer";
            photoGridContainerMetaData.setDisplayButtonBuilder(function(builder){
                builder.addEditButton();
                builder.addCancelButton();
                return  builder.getButtons();
            });
             photoGridContainerMetaData.addRunAfterSuccess(
                function(result,metaData){ $("#photoGridContainer").trigger("reloadGrid");});
            photoGridContainerMetaData.setGridName("#photoGridContainer");
            photoGridContainerMetaData.addSubmitData({"From":"Field","ParentId":entityId});
            $("#addNewPhoto").click(function(){kyt.popupCrud.controller.itemCRU(null,photoGridContainerMetaData)});
            $("#addNewDocument").click(function(){kyt.popupCrud.controller.itemCRU(null,documentGridContainerMetaData)});

        }
    }
}());