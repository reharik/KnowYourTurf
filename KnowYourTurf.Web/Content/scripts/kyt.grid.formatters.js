kyt.grid.formatterHelpers = (function () {
    var redirectCall = function (url) {
        window.location.href = url;
    };
    var deleteCall = function (url, gid) {
        if (confirm("Are you sure you would like to delete this Item?")) {
            $.get(url, function (data) {
                $("#" + gid).trigger("reloadGrid");
            });
        }
    };
    return {
        clickEvent: function (action, actionUrl, gid) {
            var metaData = gid + "MetaData";
            metaData = metaData.replace('#', '');
            var gridMetaData;
            if (eval("typeof " + metaData + " == 'object'")) {
                gridMetaData = eval(metaData);
            }
            else {
                gridMetaData = kyt.popupMetaData.metaData();
            }
            switch (action) {
                case "Redirect":
                    redirectCall(actionUrl);
                    break;
                case "Edit":
                    kyt.popupCrud.controller.itemCRU(actionUrl, gridMetaData, gridMetaData ? gridMetaData.getLoadData() : {});
                    break;
                case "Display":
                    gridMetaData.setIsDisplay(true);
                    kyt.popupCrud.controller.itemCRU(actionUrl, gridMetaData, gridMetaData ? gridMetaData.getLoadData() : {});
                    break;
                case "Delete":
                    deleteCall(actionUrl, gid);
                    break;
                 case "Other":
                    var altFunc = gridMetaData.getAltClickFunction();
                    altFunc(actionUrl, gid);
                    break;
            }
        }
    }
} ());
