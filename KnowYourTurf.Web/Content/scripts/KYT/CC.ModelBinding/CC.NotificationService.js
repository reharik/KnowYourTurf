/**
 * Created by JetBrains RubyMine.
 * User: Owner
 * Date: 7/15/12
 * Time: 10:43 AM
 * To change this template use File | Settings | File Templates.
 */

CC.NotificationService = function(){
    this.messageContainer = $("#messageContainer").get(0);
    this.viewmodel = {
        messages: ko.observableArray(),
        fadeOut: function(item){
            if(item.nodeType ==1){
                $(item).hide("slow");
            }
        }
    };
    ko.applyBindings(this.viewmodel,this.messageContainer);
};

$.extend(CC.NotificationService.prototype,{
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
        this.viewmodel.messages.remove(function(item){
            return item.elementCid()===msgObject.elementCid()
            && item.message() === msgObject.message();
        });
    },

    removeById:function(cid){
        this.viewmodel.messages.remove(function(item){
            return item.elementCid()===cid;
        });
    },

    removeAllErrorsById:function(cid){
         this.viewmodel.messages.remove(function(item){
            return item.elementCid()===cid && item.status()==='error';
        });
    },

    handleResult:function(result, cid){
        var that=this;
        if(!result.Success){
            if(result.Message){
                that.add(new CC.NotificationMessage(cid, result.Message,"error"));
            }
            if(result.Errors){
                _.each(result.Errors,function(item){
                    that.add(new CC.NotificationMessage(cid, item.ErrorMessage,"error"));
                })
            }
        }else{
            if(result.Message){
                that.add(new CC.NotificationMessage(cid, result.Message,"success",true));
                that.removeAllErrorsById(cid);
            }
        }
        return result.Success;
    }
});


CC.NotificationMessage = function(elementCid, message, status, _shouldSelfDestruct){
    this.message = ko.observable(message);
    this.elementCid = ko.observable(elementCid);
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


