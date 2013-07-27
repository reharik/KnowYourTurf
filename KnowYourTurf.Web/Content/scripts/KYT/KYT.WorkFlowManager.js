/**
 * Created by JetBrains RubyMine.
 * User: Owner
 * Date: 2/26/12
 * Time: 10:55 AM
 * To change this template use File | Settings | File Templates.
 */
KYT.WorkflowManager = (function(KYT, Backbone){
    var WFM =  Backbone.Model.extend({
        defaults: {
            parentStack: [],
            workflowState: KYT.WorkflowState
        },
        addChildView:function(child){
            var parent = KYT.State.get("currentView");
            if(!parent)return null;
            KYT.State.set({"currentView":child});
            KYT.State.set({"childView":child});
            var stack =  this.get("parentStack");
            stack.push(parent);
            $.when(child.render()).then(function () {
                $(parent.el).after(child.el);
            });
            $(parent.el).hide();
            return child;
        },
        returnParentView:function(result, triggerCallback){
            var stack =  this.get("parentStack");
            var parent = stack.pop();
            KYT.State.get("currentView").close();
            if(!parent){return;}
            if(triggerCallback&&parent.callbackAction){
                parent.callbackAction(result);
            }
            $(parent.el).show();
            KYT.vent.trigger("route",parent.options.route,false);
            KYT.State.set({"currentView":parent});
        },
        loadBottomLevel:function(url){
            var last = _.last(this.get("parentStack"));
            if(last && last.options.url == url){
                this.returnParentView("",false);
                return false;
            }
            return true;
        },
        workflowState:function(){return this.get("workflowState"); },

        cleanAllViews:function(){
            var currentView = KYT.State.get("currentView");
            if(currentView)currentView.close();
            var stack =  this.get("parentStack");
            while (stack.length>0){
                stack.pop().close();
            }
            this.get("workflowState").clearState();
        }
    });
    return new WFM();

})(KYT, Backbone);

