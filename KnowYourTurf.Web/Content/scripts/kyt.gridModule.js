/**
 * Created by .
 * User: RHarik
 * Date: 9/1/11
 * Time: 9:15 AM
 * To change this template use File | Settings | File Templates.
 */

if (typeof kyt == "undefined") {
            var kyt = {};
}

kyt.gridModule = (function(container,options){
    var _container = container;
    var myOptions = $.extend({}, kyt.gridDefaults, options || {});
    var modules = {};

    return{
        init: function(){
            this.delegateEvents();
        },
        destroy:function(){
            $.unsubscribeByHandle("gCont");
            for(var mod in modules){
                mod.destroy();
            }
            $(_container).empty();
        },
        delegateEvents: function(){
            var that = this;
            // hard coding name because it's scoped to this mod and cuz css uses it's name
            $(_container).find("#gridContainer").AsGrid(myOptions.gridDef, myOptions.gridOptions);
           // $(window).bind('resize', function() { cc.gridHelper.adjustSize("#gridContainer"); }).trigger('resize');
            $(_container).find("#addNew").unbind();
            $(_container).find("#addNew").click(that.addNew);

            $.subscribe('/grid/deleteItem', $.proxy(that.deleteItemCallback,that),"gCont");
            $.subscribe('/grid/viewItem', that.viewItem,"gCont");
            $.subscribe('/grid/editItem', that.editItem,"gCont");
        },
        addNew:function(){
            $.publish('/grid/addItem', [myOptions.addEditUrl]);
        },
        editItem:function(addEditUrl,id){
            $.publish('/grid/editItem', [addEditUrl,id]);
        },
        viewItem:function(addEditUrl,id){
            $.publish('/grid/viewItem', [addEditUrl,id]);
        },
        deleteItem:function(){
            $.publish("/grid/deleteItem",[result])
        },
        deleteItemCallback: function(result){
            this.reloadGrid();
        },
        redirect:function(url){window.location.href = url;},
        reloadGrid:function(data){
            $("#gridContainer").trigger("reloadGrid");
        },
        addOptions: function(data){
             $.extend(myOptions, data);
        }
    }
});

kyt.gridDefaults = {
};
