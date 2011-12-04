/**
 * Created by JetBrains RubyMine.
 * User: RHarik
 * Date: 10/17/11
 * Time: 2:20 PM
 * To change this template use File | Settings | File Templates.
 */

if (typeof kyt == "undefined") {
    var kyt = {};
}
Backbone.View.prototype.renderCallback = function(){};
Backbone.View.prototype.viewLoaded = function(){};


    kyt.Controller = Backbone.View.extend({
    defaults:function(){
        return {
            views:{},
            viewModels:{},
            modules:{},
            isDestroyed:false,
            destroy:function(){
                if(this.isDestroyed){return;}
                this.isDestroyed = true;
                $.each(this.views,function(i,item){
                    item.remove();
                });
                this.viewModels={};
                $.each(this.modules,function(i,item){
                    item.destroy();
                });
                $.unsubscribeByHandle(this.cid);
            }
        }
    }
});

kyt.Module = Backbone.View.extend({
    defaults:function(){
        return {
            views:{},
            viewModels:{},
            events:{
            },
            isDestroyed:false,
            destroy:function(){
                if(this.isDestroyed){return;}
                this.isDestroyed = true;
                $.each(this.views,function(i,item){
                    item.remove();
                });
                this.viewModels={};
                $.unsubscribeByHandle(this.cid);
            }
        }
    }
});






// var Addresses = {
//                    "addressItems":[{
//                        Address1:"123 Main St",
//                        Address2:null,
//                        Archived:false,
//                        ChangeDate:"/Date(1318624890000)/",
//                        ChangedBy:0,
//                        City:"Austin",
//                        CreateDate:"/Date(1318624890000)/",
//                        EntityId:1,
//                        IsDefault:false,
//                        OrgId:0,
//                        State:"TX",
//                        TenantId:0,
//                        Type:null,
//                        ZipCode:"78704"
//                    }]
//                };
//        kytViewModel =  ko.mapping.fromJS(Addresses);
