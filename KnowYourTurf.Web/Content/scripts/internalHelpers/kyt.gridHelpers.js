kyt.gridMultiSelect = (function(){
    var notificationResult = kyt.utilities.messageHandling.notificationResult();
    return {
        getCheckedBoxes: function(gridContainerName, reload) {
            var _reload = reload;
            var _gridContainer = gridContainerName;
            if(!_gridContainer) _gridContainer="gridContainer";
            notificationResult.setSuccessHandler(function(result) {
                if(_reload)
                    $("#" + _gridContainer).trigger("reloadGrid");
                if(result.Message){
                    var emh = kyt.utilities.messageHandling.messageHandler();
                    var message = kyt.utilities.messageHandling.mhMessage("success",result.Message,"");
                    emh.addMessage(message);
                    emh.showAllMessages($('#errorMessages'));
                }
            });
            var ids = [];
            $($("#"+_gridContainer).jqGrid('getGridParam', 'selarrrow')).each(function(idx, item) { ids.push(item) });
            return ids;
        }
    }
}());


