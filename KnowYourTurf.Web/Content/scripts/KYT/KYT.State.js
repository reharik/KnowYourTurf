/**
 * Created by JetBrains RubyMine.
 * User: Owner
 * Date: 2/26/12
 * Time: 10:54 AM
 * To change this template use File | Settings | File Templates.
 */


KYT.State = (function(KYT, Backbone){
    var State =  Backbone.Model.extend({
        defaults: {
            parentStack: []
        },
        application:"",
        currentView:""
    });
    return new State();
})(KYT, Backbone);

KYT.WorkflowState = (function(KYT, Backbone){
    var WorkflowState =  Backbone.Model.extend({});
    var workflowState;
    return {
        state:function(){
            return workflowState ||new WorkflowState();
        },
        clearState:function(){
            workflowState = new WorkflowState();
        }
    };
})(KYT, Backbone);
