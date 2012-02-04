/**
 * Created by JetBrains RubyMine.
 * User: Owner
 * Date: 11/18/11
 * Time: 7:13 AM
 * To change this template use File | Settings | File Templates.
 */
kyt.PurchaseOrderCommitController = kyt.CrudController.extend({
    events:_.extend({
    }, kyt.CrudController.prototype.events),
    closePO:function(){
        var purchaseOrderId = $("#PurchaseOrder_EntityId").val();
        kyt.repository.ajaxGet(this.options.closePOUrl,{"EntityId":purchaseOrderId},function(result){$.address.value( result.RedirectUrl);});
    },
    registerAdditionalSubscriptions:function(){
        $("#savePO").click($.proxy(this.closePO,this));
    }



});
