if (typeof kyt == "undefined") {
            var kyt = {};
}

if (typeof kyt.purchaseOrder == "undefined") {
            kyt.purchaseOrder = {};
}

kyt.purchaseOrder.controller = (function(){
    var setupPage = function(){
        if($("#vendorId").val()<=0){$("#poliGridDiv").hide();}
        else {$("#poliGridDiv").show();}
        var poId = $("#PurchaseOrder_EntityId").val();
        kyt.purchaseOrder.controller.showPOData(poId);
    };
    var setupGrids = function(){
        $("#productGrid").AsGrid(productGridDef,{
            pager:"productPager",
            url:$("#vendorId").val()>0?productGridDef.Url+$("#vendorId").val():"",
            grouping:true,
            groupingView : {
                groupField : ['InstantiatingType'],
                groupColumnShow : [false]
            }
        });
        $("#poliGrid").AsGrid(poliGridDef,{
            pager:"poliPager",
            caption:"Purchase Order Line Items"});
        $("#productGrid").jqGrid('setGridState', $("#vendorId").val()<=0?'hidden':'visible');

    };
    var setupEvents = function(){
         $("#vendor").change(function(){
             var vendorId = $("#vendor").val();
             $("#vendorId").val(vendorId);
            if(vendorId>0){
                $("#productGrid").setGridParam({url:productGridDef.Url.substring(0,productGridDef.Url.lastIndexOf("=")+1) + vendorId});
                $("#productGrid").jqGrid('setGridState','visible');
                $("#productGrid").trigger("reloadGrid");
                productGridMetaData.getLoadData().RootId = vendorId;
            }
        });

        $("#commit").click(function(){kyt.purchaseOrder.controller.commitPO()});
        $("#return").click(function(){window.location.href = returnUrl;});
    };
    var setupMetadata = function(){
        poliGridMetaData.addRunAfterSuccess(
                function(result,metaData){ if($(metaData.getGridName()).length > 0) $(metaData.getGridName()).trigger("reloadGrid")});
        poliGridMetaData.setGridName("#poliGrid");
        productGridMetaData.setAltClickFunction(kyt.purchaseOrder.controller.savePO);
       poliGridMetaData.addLoadData({"RootId":$("#vendorId").val(),"ParentId":$("#poId").val()});

        productGridMetaData.setAltClickFunction(kyt.purchaseOrder.controller.savePO);
        productGridMetaData.addRunAfterSuccess(
            function(result,metaData){
                $("#poId").val(result.Data.poId);
                $("#poliGrid").setGridParam({url:poliGridDef.Url.substring(0,poliGridDef.Url.lastIndexOf("=")+1) + result.Data.poId});
                $("#poliGrid").trigger("reloadGrid");
                $("#poliGridDiv").show();
            });
        productGridMetaData.addLoadData({"RootId":$("#vendorId").val(),"ParentId":$("#poId").val()});
        productGridMetaData.setGridName("#productGrid")
    };
    return{
        init:function(){
            setupPage();
            setupGrids();
            setupEvents();
            setupMetadata();
        },
        savePO:function(actionUrl,gid){
            var data = $.param({
                VendorId: $("#vendor").val(),
                PurchaseOrderId:$("#PurchaseOrder_EntityId").val()},true);
            kyt.purchaseOrder.repository.saveCall(actionUrl,data);
        },
        commitPO:function(){
            var purchaseOrderId = $("#PurchaseOrder_EntityId").val();
            window.location.href = commitUrl+"?ParentId="+purchaseOrderId;
        },
        showPOData: function(poId) {
                if (poId > 0) {
                    $("#viewPOID").show();
                    $("#viewVendor").show();
                    $("#editVendor").hide();
                    $("#PurchaseOrder_EntityId").val(poId);
                    $("#POID").val(poId);
                    $("#viewPOID").find("span").text(poId);
                } else {
                    $("#viewPOID").hide();
                    $("#viewVendor").hide();
                    $("#editVendor").show();
                }
            },
        closePO:function(poId){
            kyt.purchaseOrder.repository.closePOCall(poId);
        }
    }
}());

kyt.purchaseOrder.repository = (function(){
    var notification = kyt.utilities.messageHandling.notificationResult();
    var saveCallback = function(result){
        if(!result.Success){
                notification.result(result);
                return;
        }
        $("#poliGridDiv").show();
        $("#poliGrid").setGridParam({url:poliGridDef.Url.substring(0,poliGridDef.Url.lastIndexOf("=")+1) + result.EntityId});
        $("#poliGrid").jqGrid('setGridState','visible');

        $("#poliGrid").trigger("reloadGrid");

        kyt.purchaseOrder.controller.showPOData(result.EntityId);
    };
    var closePOCallback = function(result){
        if(!result.Success){
                      notification.result(result);
                      return;
              }
        window.location = result.RedirectUrl;
    };
    return{
        saveCall:function(url,data){
            $.post(url,data,saveCallback)
        },
        closePOCall:function(poId){
            $.post(closePOUrl,{"EntityId":poId},closePOCallback)
        }
    }
}());