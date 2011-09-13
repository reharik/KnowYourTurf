if (typeof kyt == 'undefined') {
    kyt = function() {
    }
}

if (typeof kyt.grid == 'undefined') {
    kyt.grid = function() {
    }
}

(function($) {
    $.fn.AsGrid = function(gridDefinition, userOptions) {
        if(!this)return;
        var gridDefaultOptions = {
            height: "auto",
            //imgpath: pathToJQGridImages,
            url: gridDefinition.Url,
            datatype: 'json',
            mtype: 'GET',
            colNames: kyt.grid.columnService.columnNames(gridDefinition),
            colModel: kyt.grid.columnService.columnModel(gridDefinition),
            rowNum: 10,
            pginput:false,
            rowList: [3, 10, 20, 30],
            loadui: "disable",
            forceFit:true,
            //postData: { "entityId": entityId },
            onPaging: function(pgButton) {
                if (pgButton == 'records') {
                    this.page = 1;
                }
            },
            // here we move the value of the entityId to ParentId since it's the Id of the containing element
            // and set EntityId to RowId as RowId is for the Particular entity we want to change
            beforeSubmitCell: function(rowid, celname, value, iRow, iCol) { return { RootId: rootId, ParentId: entityId, EntityId: rowid, rowId: rowid, cellName: celname, cellValue: value} },
            afterSubmitCell: function(serverresponse, rowid, cellname, value, iRow, iCol) {
                eval("var cellEditResult =" + serverresponse.responseText);
                return [cellEditResult.success, cellEditResult.message];
            },
            caption:gridDefinition.Title?gridDefinition.Title:"Items",
            sortorder: "asc",
            viewrecords: true,
            pager: '#pager',
            jsonReader: {
                repeatitems: true,
                root: "items",
                cell: "cell",
                id: "id"
            }
        };

        if (gridDefinition.EditUrl) {
            $.extend(gridDefaultOptions, { cellEdit: true, cellurl: gridDefinition.EditUrl} || {});
        }
        var gridOptions = $.extend(gridDefaultOptions, userOptions || {});
        var grid = this;
        grid.jqGrid(gridOptions);
        grid.jqGrid('filterToolbar',{stringResult:true});
        $(this)[0].toggleToolbar();
        //grid.jqGrid('navGrid',gridOptions.pager,{edit:false,add:false,del:false,search:false,refresh:false});
//        grid.jqGrid('navButtonAdd',gridOptions.pager,{caption:"Toggle",title:"Toggle Search Toolbar", buttonicon :'ui-icon-pin-s',
//            onClickButton:function(){
//                $(this)[0].toggleToolbar();
//            }
//        });
//        grid.jqGrid('navButtonAdd',gridOptions.pager,{caption:"Clear",title:"Clear Search",buttonicon :'ui-icon-refresh',
//            onClickButton:function(){
//                $(this)[0].clearToolbar();
//            }
//        });

        return grid;
    }
})(jQuery);
