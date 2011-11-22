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
        'click #addNew': 'addNew'
    },
    initialize: function(){
        this.options = $.extend({},kyt.gridDefaults,this.options);
        this.id = this.options.id;
        this.render();
    },
    render: function(){
        $(this.options.gridContainer,this.el).AsGrid(this.options.gridDef, this.options.gridOptions);
        return this;
    },
    addNew:function(){
        $.publish('/grid_'+this.id+'/AddNewItem', [this.options.addEditUrl]);
    },
    deleteItem:function(){
        if (confirm("Are you sure you would like to delete this Item?")) {
            $.get(this.options.deleteMultipleUrl,
                $.param({"EntityIds":ids},true),
                $.proxy(this.reloadGrid,this));
        }
    },
    reloadGrid:function(){
        $(this.options.gridContainer,this.el).trigger("reloadGrid");
    },
    getUrl:function(){
        return $(this.options.gridContainer,this.el).getGridParam("url");
    },
    setUrl:function(url){
        $(this.options.gridContainer,this.el).setGridParam({"url":url});
    }
});

kyt.gridDefaults = {
    id:"",
    searchField:"Name",
    showSearch:true,
    gridContainer:"#gridContainer"
};