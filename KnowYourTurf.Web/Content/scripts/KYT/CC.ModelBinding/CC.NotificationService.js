/**
 * Created by JetBrains RubyMine.
 * User: Owner
 * Date: 7/15/12
 * Time: 10:43 AM
 * To change this template use File | Settings | File Templates.
 */

CC.NotificationService = function(){
    this.viewmodel = {
        messages: ko.observableArray(),
        fadeOut: function(item){
            if(item.nodeType ==1){
                $(item).hide("slow");
            }
        }
    };
};

$.extend(CC.NotificationService.prototype,{
    render:function(selector){
        ko.applyBindings(this.viewmodel,selector);
    },
    add:function(msgObject){
        var exists = _.any(this.viewmodel.messages(),function(msg){
            return msgObject.elementCid() === msg.elementCid() && msgObject.message() === msg.message();
        });
        if(!exists){
            this.viewmodel.messages.push(msgObject);
            if(msgObject.shouldSelfDestruct){
                msgObject.parent = this;
                msgObject.selfDestruct();

            }
        }
    },
    remove:function(msgObject){
        this.viewmodel.messages.remove(function(msg){
            return msg.elementCid()===msgObject.elementCid()
            && msg.message() === msgObject.message();
        });
    },

    removeById:function(cid){
        this.viewmodel.messages.remove(function(item){
            return item.elementCid()===cid;
        });
    },

    removeAllErrorsByViewId:function(viewId){
         this.viewmodel.messages.remove(function(item){
            return item.viewId()===viewId&& item.status()==='error';
        });
    },

    handleResult:function(result, cid){
        var that=this;
        if(!result.Success){
            if(result.Message){
                that.add(new CC.NotificationMessage("",cid, result.Message,"error"));
            }
            if(result.Errors){
                _.each(result.Errors,function(item){
                    that.add(new CC.NotificationMessage("",cid, item.ErrorMessage,"error"));
                })
            }
        }else{
            if(result.Message){
                that.add(new CC.NotificationMessage("",cid, result.Message,"success",true));
                that.removeAllErrorsByViewId(cid);
            }
        }
        return result.Success;
    }
});


CC.NotificationMessage = function(elementCid, viewId, message, status, _shouldSelfDestruct){
    this.message = ko.observable(message);
    this.elementCid = ko.observable(elementCid);
    this.viewId = ko.observable(viewId);
    this.status = ko.observable(status);
    this.parent = null;
    this.shouldSelfDestruct = _shouldSelfDestruct;
    this.selfDestruct = function(time){
        var that = this;
        setTimeout(function(){
            that.parent.remove(that);
        }, time ? time : 2000);
    };

};


