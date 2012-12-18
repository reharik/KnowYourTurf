/**
 * Created by JetBrains RubyMine.
 * User: RHarik
 * Date: 11/7/11
 * Time: 8:13 AM
 * To change this template use File | Settings | File Templates.
 */
if (typeof kyt == "undefined") {
            var kyt = {};
}


kyt.AssetWithToPresentLogicController  = kyt.AssetController .extend({
    events:_.extend({
    }, kyt.AssetController .prototype.events),
    formLoaded:function(){
        $("#masterArea").hide();
        if($("[name*='IsToPresent']").prop("checked")){
            $("[name*='ToDate']").val("").prop('disabled', true);
        }
        $("[name*='IsToPresent']").change(this.isToPresentEvent)
    },
    isToPresentEvent:function(){
        if($("[name*='IsToPresent']").prop("checked")){
            $("[name*='ToDate']").val("").prop('disabled', true);
        }else{
            $("[name*='ToDate']").prop('disabled', false);
        }
    }
});