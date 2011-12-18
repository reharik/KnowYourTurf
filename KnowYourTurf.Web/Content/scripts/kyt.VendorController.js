/**
 * Created by JetBrains RubyMine.
 * User: Owner
 * Date: 11/6/11
 * Time: 5:27 PM
 * To change this template use File | Settings | File Templates.
 */
if (typeof kyt == "undefined") {
    var kyt = {};
}

kyt.VendorController = kyt.CrudController.extend({
    events:_.extend({
    }, kyt.CrudController.prototype.events),
    additionalSubscriptions:function(){
        $.subscribe("/contentLevel/popupFormModule_editModule/popupLoaded",$.proxy(this.loadTokenizers,this));
    },
    loadTokenizers:function(formOptions){
        var chemicalTokenOptions = {
            id:this.id+"chemical",
            el:"#chemicalTokenizer",
            availableItems:formOptions.chemicalOptions.availableItems,
            selectedItems:formOptions.chemicalOptions.selectedItems,
            inputSelector:formOptions.chemicalOptions.inputSelector
        };
        var fertilizerTokenOptions = {
            id:this.id+"fertilizer",
            el:"#fertilizerTokenizer",
            availableItems:formOptions.fertilizerOptions.availableItems,
            selectedItems:formOptions.fertilizerOptions.selectedItems,
            inputSelector:formOptions.fertilizerOptions.inputSelector
        };
        var materialTokenOptions = {
            id:this.id+"material",
            el:"#materialTokenizer",
            availableItems:formOptions.materialOptions.availableItems,
            selectedItems:formOptions.materialOptions.selectedItems,
            inputSelector:formOptions.materialOptions.inputSelector
        };

        this.views.chemicalToken= new kyt.TokenView(chemicalTokenOptions);
        this.views.fertilizerToken= new kyt.TokenView(fertilizerTokenOptions);
        this.views.materialToken = new kyt.TokenView(materialTokenOptions);

    },
    redirectItem:function(url,data){
        var _url = url ? url : this.options.redirectUrl;
        _url = _url + "?ParentId=" + data.ParentId;
        window.location.href = _url;
    }

});