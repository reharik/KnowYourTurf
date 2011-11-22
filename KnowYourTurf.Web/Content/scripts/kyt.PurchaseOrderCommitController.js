/**
 * Created by JetBrains RubyMine.
 * User: Owner
 * Date: 11/18/11
 * Time: 7:13 AM
 * To change this template use File | Settings | File Templates.
 */
kyt.PurchaseOrderController = kyt.CrudController.extend({
    events:_.extend({
        "click #savePOButton": "closePO"
    }, kyt.Controller.prototype.events),
    closePO:function(){
        $("#CRUDForm", this.el).submit();
    }


});
