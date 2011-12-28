/**
 * Created by JetBrains RubyMine.
 * User: RHarik
 * Date: 10/6/11
 * Time: 3:24 PM
 * To change this template use File | Settings | File Templates.
 */
if (typeof kyt == "undefined") {
    var kyt = {};
}


kyt.GridView = Backbone.View.extend({
    events:{
        'click .new' : 'addNew',
        'click .delete' : 'deleteItems'
    },
    initialize: function(){
        this.options = $.extend({},kyt.gridDefaults,this.options);
        this.id=this.options.id;
        this.render();
    },
    render: function(){
        $(this.options.gridContainer).AsGrid(this.options.gridDef, this.options.gridOptions);
        $(window).bind('resize', function() { cc.gridHelper.adjustSize("#gridContainer"); }).trigger('resize');
        $(this.el).gridSearch({onClear:$.proxy(this.removeSearch,this),onSubmit:$.proxy(this.search,this)});
        return this;
    },
    addNew:function(){
        $.publish('/contentLevel/grid_'+this.options.gridName+'/AddUpdateItem', [this.options.addUpdateUrl]);
    },
    deleteItems:function(){
        if (confirm("Are you sure you would like to delete this Item?")) {
            var ids = cc.gridMultiSelect.getCheckedBoxes(this.options.gridContainer);
            kyt.repository.ajaxGet(this.options.deleteMultipleUrl,
                $.param({"EntityIds":ids},true),
                $.proxy(function(result){
                    var notification = cc.utilities.messageHandling.notificationResult();
                    notification.setErrorContainer("#errorMessagesGrid");
                    notification.setSuccessContainer("#errorMessagesGrid");
                    notification.result(result);
                    this.reloadGrid();
                },this));
        }
    },

    search:function(v){
        var searchItem = {"field": this.options.searchField ,"data": v };
        var filter = {"group":"AND",rules:[searchItem]};
        var obj = {"filters":""  + JSON.stringify(filter) + ""};
        $(this.options.gridContainer).jqGrid('setGridParam',{postData:obj});
        this.reloadGrid();
    },
    removeSearch:function(){
        delete $(this.options.gridContainer).jqGrid('getGridParam' ,'postData')["filters"];
        this.reloadGrid();
        return false;
    },
    reloadGrid:function(){
        $(this.options.gridContainer).trigger("reloadGrid");
    },
    getUrl:function(){
        return $(this.options.gridContainer).getGridParam("url");
    },
    setUrl:function(url){
        $(this.options.gridContainer).setGridParam({url:url});
    }
});


kyt.gridDefaults = {
    searchField:"Name",
    showSearch:true,
    gridContainer:"#gridContainer",
    gridName:""
};