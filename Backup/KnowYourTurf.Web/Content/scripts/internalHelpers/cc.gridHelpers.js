
cc.gridMultiSelect = (function(){
    return {
        getCheckedBoxes: function(gridContainerName) {
            var name = gridContainerName?"#"+gridContainerName: "#gridContainer";
            var ids = [];
            $($(name).jqGrid('getGridParam', 'selarrrow')).each(function(idx, item) { ids.push(item) });
            return ids;
        }
    }
}());


cc.gridHelper= (function(){
    return {
        adjustSize: function(gridSelector){
            var $el = $(gridSelector).parents(".content-inner:eq(0)");
            var h = $(window).height()-$("#main-header").height();
            $el.height(h-80);
            $(gridSelector).setGridHeight($el.height()-31  );
            $(gridSelector).setGridWidth($el.width()-2);
        },
        adjustSortStyles: function(index,iCol,sortorder) {
            $('.ui-jqgrid th').removeClass('is-sorted');
            $('.ui-jqgrid th[aria-selected="true"]').addClass('is-sorted');
        }
    }

}());
