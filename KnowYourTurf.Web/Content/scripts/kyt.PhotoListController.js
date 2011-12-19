
/**
 * Created by JetBrains RubyMine.
 * User: Owner
 * Date: 11/5/11
 * Time: 2:47 PM
 * To change this template use File | Settings | File Templates.
 */

if (typeof kyt == "undefined") {
    var kyt = {};
}

kyt.PhotoListController = kyt.CrudController.extend({
    events:_.extend({
    }, kyt.CrudController.prototype.events),
    registerAdditionalSubscriptions:function(){
        $.subscribe('/contentLevel/grid_photoGrid/AddUpdateItem',$.proxy(this.addUpdateItem,this), this.cid);
    }
});

