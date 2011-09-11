if (typeof kyt == "undefined") {
            var kyt = {};
}

if (typeof kyt.listType == "undefined") {
            kyt.listType = {};
}

kyt.listType.controller = (function(){
    var setupGrids=function(){
        $("#EventTypeGrid").AsGrid(etGridDef,{pager:"eventTypePager"});
        $("#TaskTypeGrid").AsGrid(ttGridDef,{pager:"taskTypePager"});
        $("#DocumentCategoryGrid").AsGrid(dcGridDef,{pager:"docCatPager"});
        $("#PhotoCategoryGrid").AsGrid(pcGridDef,{pager:"photoCatPager"});
    };
    var setupGridMetaData=function(){
        EventTypeGridMetaData.name = "eventType";
        EventTypeGridMetaData.setGridName("#EventTypeGrid");
        EventTypeGridMetaData.addRunAfterSuccess(
            function(result,metaData){ if($(metaData.getGridName()).length > 0) $(metaData.getGridName()).trigger("reloadGrid")});
        EventTypeGridMetaData.setPopupTitle(popupTitleET);

        TaskTypeGridMetaData.name = "TaskType";
        TaskTypeGridMetaData.setGridName("#TaskTypeGrid");
        TaskTypeGridMetaData.addRunAfterSuccess(
            function(result,metaData){ if($(metaData.getGridName()).length > 0) $(metaData.getGridName()).trigger("reloadGrid")});
        TaskTypeGridMetaData.setPopupTitle(popupTitleTT);

        DocumentCategoryGridMetaData.name = "docCat";
        DocumentCategoryGridMetaData.setGridName("#DocumentCategoryGrid");
        DocumentCategoryGridMetaData.addRunAfterSuccess(
            function(result,metaData){ if($(metaData.getGridName()).length > 0) $(metaData.getGridName()).trigger("reloadGrid")});
        DocumentCategoryGridMetaData.setPopupTitle(popupTitleDC);

        PhotoCategoryGridMetaData.name = "photoCat";
        PhotoCategoryGridMetaData.setGridName("#PhotoCategoryGrid");
        PhotoCategoryGridMetaData.addRunAfterSuccess(
            function(result,metaData){ if($(metaData.getGridName()).length > 0) $(metaData.getGridName()).trigger("reloadGrid")});
        PhotoCategoryGridMetaData.setPopupTitle(popupTitlePC);

    };
    var setupEvents=function(){
        $("#addNewTaskType").click(function(){
            kyt.popupCrud.controller.itemCRU(null,TaskTypeGridMetaData)});
        $("#addNewEventType").click(function(){kyt.popupCrud.controller.itemCRU(null,EventTypeGridMetaData)});
        $("#addNewDocumentCategory").click(function(){kyt.popupCrud.controller.itemCRU(null,DocumentCategoryGridMetaData)});
        $("#addNewPhotoCategory").click(function(){kyt.popupCrud.controller.itemCRU(null,PhotoCategoryGridMetaData)});
    };
    return{
        init:function(){
            setupGrids();
            setupGridMetaData();
            setupEvents();
        }
    }
}());