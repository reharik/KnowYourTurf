if (typeof kyt == "undefined") {
            var kyt = {};
}

kyt.crudHelpers = {
    documentHandler: function(arr){
        if($("#DocumentInput").val())
        {
            arr.push({"name":"DocumentTokenViewModel.DocumentInput","value":$("#DocumentInput").val()})
        }
    },
    photoHandler: function(arr){
        if($("#PhotoInput").val())
        {
            arr.push({"name":"PhotoTokenViewModel.PhotoInput","value":$("#PhotoInput").val()})
        }
    },
    membershipPositionHandler: function(arr){
        // this is to handle blank rows
        var indx = 0;
        $(".multirow-input-tbl tr").each(function(i,item){
            var name = $(item).find("td [name='MembershipPosition.Name']").val();
            var heldFrom = $(item).find("td [name='MembershipPosition.HeldFrom']").val();
            var heldTo = $(item).find("td [name='MembershipPosition.HeldTo']").val();
            var entityId = $(item).find("td [name='MembershipPosition.EntityID']").val()?$(item).find("td[name='MembershipPosition.EntityID']").val():0;
            if(name){
                var objName = "MembershipDtos["+ indx +"]";
                indx++;
                arr.push({"name":objName+".Name","value":name});
                arr.push({"name":objName+".HeldFrom","value":heldFrom});
                arr.push({"name":objName+".HeldTo","value":heldTo});
                arr.push({"name":objName+".EntityId","value":entityId});
            }
        });
    },
    publicationAuthorHandler: function(arr){
        // this is to handle blank rows
        var indx = 0;
        $(".multirow-input-tbl tr").each(function(i,item){
            var rank = $(item).find("td [name='PublicationAuthor.Rank']").val();
            var fname = $(item).find("td [name='PublicationAuthor.FirstName']").val();
            var mi = $(item).find("td [name='PublicationAuthor.MiddleInitial']").val();
            var lname = $(item).find("td [name='PublicationAuthor.LastName']").val();
            var title = $(item).find("td [name='PublicationAuthor.Title']").val();
            var org = $(item).find("td [name='PublicationAuthor.Organization']").val();
            var entityId = $(item).find("td [name='PublicationAuthor.EntityId']").val()?$(item).find("td [name='PublicationAuthor.EntityId']").val():0;
            if(lname){
                var objName = "PublicationAuthorDtos["+ indx +"]";
                indx++;
                arr.push({"name":objName+".Rank","value":rank});
                arr.push({"name":objName+".FirstName","value":fname});
                arr.push({"name":objName+".MiddleInitial","value":mi});
                arr.push({"name":objName+".LastName","value":lname});
                arr.push({"name":objName+".Title","value":title});
                arr.push({"name":objName+".Organization","value":org});
                arr.push({"name":objName+".EntityId","value":entityId});
            }
        });
    },

    fundedActivityAuthorHandler: function(arr){
        // this is to handle blank rows
        var indx = 0;
        $(".multirow-input-tbl tr").each(function(i,item){
            var rank = $("td [name='FundedActivityAuthor.Rank'],item").val();
            var fname = $("td [name='FundedActivityAuthor.FirstName']",item).val();
            var mi = $("td [name='FundedActivityAuthor.MiddleInitial']",item).val();
            var lname = $("td [name='FundedActivityAuthor.LastName']",item).val();
            var creds = $("td [name='FundedActivityAuthor.Credentials']",item).val();
            var org = $("td [name='FundedActivityAuthor.Organization']",item).val();
            var entityId = $("td [name='FundedActivityAuthor.EntityId']",item).val()?$("td [name='FundedActivityAuthor.EntityId']",item).val():0;
            if(lname){
                var objName = "FundedActivityAuthorDtos["+ indx +"]";
                indx++;
                arr.push({"name":objName+".Rank","value":rank});
                arr.push({"name":objName+".FirstName","value":fname});
                arr.push({"name":objName+".MiddleInitial","value":mi});
                arr.push({"name":objName+".LastName","value":lname});
                arr.push({"name":objName+".Credentials","value":creds});
                arr.push({"name":objName+".Organization","value":org});
                arr.push({"name":objName+".EntityId","value":entityId});
            }
        });
    }
};

$(document).ready(function() {
    // this must be on the form view if it's retrieved by ajax. don't know how to make it a delegate.
//    $('.kyt_CRUD').crudForm({});
    $(".kyt_datepicker").datepicker({ changeMonth: true, changeYear: true, yearRange: '1950:' + new Date().getFullYear() });
    });

    if (typeof cc == "undefined") {
        var cc = {};
    }

    cc.namespace = function() {
        var a = arguments, o = null, i, j, d;
        for (i = 0; i < a.length; i = i + 1) {
            d = a[i].split(".");
            o = window;
            for (j = 0; j < d.length; j = j + 1) {
                o[d[j]] = o[d[j]] || {};
                o = o[d[j]];
            }
        }
        return o;
    };
    cc.namespace("cc.utilities");

(function($) {

    $.fn.crudForm = function(options) {
        return this.each(function()
        {
            var elem = $(this);
            if (!elem.data('crudForm')) {
              elem.data('crudForm', new CrudFunction(options, elem));
            }
        });
    };

    var CrudFunction = function(options, elem){
        var myOptions = $.extend({}, $.fn.crudForm.defaults, options || {});
        var errorContainer = myOptions.errorContainer;
        var successContainer = myOptions.successContainer?myOptions.successContainer:myOptions.errorContainer;
        // do not move this into crudForm.defaults.  it end up using the same one for every call.
        var notification = myOptions.notification ? myOptions.notification : cc.utilities.messageHandling.notificationResult();
        var mySubmitHandler = function(form) {
            var ajaxOptions = {dataType: 'json',
                success: function(result){
                    notification.result(result,form)
                },
                beforeSubmit:  beforSubmitCallback
            };
            $(form).ajaxSubmit(ajaxOptions);
        };

        $(errorContainer).hide();
        $(successContainer).hide();
        if(myOptions.successHandler){
            notification.setSuccessHandler(myOptions.successHandler)
        }
        notification.setErrorContainer(myOptions.errorContainer);
        notification.setSuccessContainer(myOptions.successContainer);

        if(myOptions.submitHandler){
            mySubmitHandler=myOptions.submitHandler;
        }
        var beforSubmitCallback = function(arr, form, options){
            var _arr = arr;
            $(myOptions.beforeSubmitCallbackFunctions).each(function(i,item){
                if(typeof(item) === 'function') item(_arr);
            });
        };

        this.setBeforeSubmitFuncs = function(beforeSubmitFunc){
             var array = !$.isArray(beforeSubmitFunc) ? [beforeSubmitFunc] : beforeSubmitFunc;
            $(array).each(function(i,item){
                if($.inArray(item,myOptions.beforeSubmitCallbackFunctions)<=0){
                    myOptions.beforeSubmitCallbackFunctions.push(item);
                }
            });};

        $(elem).validate({
            submitHandler: mySubmitHandler,
            errorContainer: $(errorContainer),
            errorLabelContainer: $(errorContainer).find("ul"),
            wrapper: 'li',
            validClass: "valid_field",
            errorClass: "invalid_field"

        });
    };

    $.fn.crudForm.defaults = {
        dataType: 'json',
        errorContainer: '#errorMessagesForm',
        beforeSubmitCallbackFunctions: [kyt.crudHelpers.documentHandler,kyt.crudHelpers.photoHandler]
    };
})(jQuery);

