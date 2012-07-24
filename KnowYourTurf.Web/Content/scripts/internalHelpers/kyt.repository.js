/**
 * Created by JetBrains RubyMine.
 * User: RHarik
 * Date: 10/13/11
 * Time: 10:52 AM
 * To change this template use File | Settings | File Templates.
 */
if (typeof KYT == "undefined") {
    var KYT = {};
}

KYT.repository= (function(){
    var repositoryCallback = function(result,callback){
        if(result.LoggedOut){
            window.location.replace(result.RedirectUrl);
            return null;
        }
        return result;
        //callback(result);
    };
    return {
        ajaxPost:function(url, data){
             KYT.showThrob=true;
            $.post(url,data).done(repositoryCallback);
        },
        ajaxGet:function(url, data){
             KYT.showThrob=true;
            return $.get(url,data).done(repositoryCallback);
        },
        ajaxPostModel:function(url, data){
            KYT.showThrob=true;
            return $.ajax({
                type:"post",
                url: url,
                data:data,
                contentType:  "application/json; charset=utf-8",
                traditional:true
            }).done(repositoryCallback);
        }
    }
}());
