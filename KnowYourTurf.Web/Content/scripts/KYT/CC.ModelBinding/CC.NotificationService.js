/**
 * Created by JetBrains RubyMine.
 * User: Owner
 * Date: 7/15/12
 * Time: 10:43 AM
 * To change this template use File | Settings | File Templates.
 */

CC={};
CC.NotificationService = function(){
        this.messageContainer = "#messageContainer";
        this.msgObjects = [];
};

$.extend(CC.NotificationService.prototype,{
    add:function(msgObject){
        var exists = _.any(this.msgObjects,function(msg){
            //TODO possibly replace this with CCElement.cid
            return msgObject.key === msg.key && msgObject.message === msg.message;
        });
        if(!exists){
            this.msgObjects.push(msgObject);
        }
    },
    remove:function(element){
        this. msgObjects = _.reject(this.msgObjects,function(item){
            return item.key === element.key && item.message === element.message;
        })
    }
});

CC.notification = new CC.NotificationService();
