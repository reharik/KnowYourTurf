

cc.namespace("cc.grid");

cc.grid.columnService = (function() {
    return {
        columnNames: function(gridDefinition) {
            return $.map(gridDefinition.Columns, function(item, i) {
                return item.header;
            });
        },

        columnModel: function(gridDefinition) {
            var map = $.map(gridDefinition.Columns, function(item, i) {
                if (item.formatoptions && typeof item.formatoptions == "string") {
                    item.formatoptions = JSON.parse(item.formatoptions);
                }
                item.search = item.search != "false";
                item.sortable= item.sortable != "false";
                if (!item.width) { item.width = "100%";}
                if (item.editRules) {
                    item.editable = true;
                }
                return item;
            });
            return map;
        },
        defaultSortColumnName:function(gridDefinition){
            var col = _.detect(gridDefinition.Columns, function(col){
                if(col.sortColumn){
                    return col.sortColumn;
                }
            });
            if(col) {return col.sortColumn;}
        }
    };
})();