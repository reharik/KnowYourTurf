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
        ajaxPost:function(url, data, callback){
             KYT.showThrob=true;
            $.post(url,data,function(result){ repositoryCallback(result,callback)});
        },
        ajaxGet:function(url, data, callback){
             KYT.showThrob=true;
            return $.when($.get(url,data)).done(repositoryCallback);
//                ,function(result){repositoryCallback(result,callback);
//            });
        },
        ajaxPostModel:function(url, data, callback){
            KYT.showThrob=true;
            $.ajax({
                type:"post",
                url: url,
                data:data,
                success:function(result){repositoryCallback(result,callback)},
                contentType:  "application/json; charset=utf-8",
                traditional:true
            });
        }
    }
}());
