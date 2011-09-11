$(document).ready(function() {
    // this must be on the form view if it's retrieved by ajax. don't know how to make it a delegate.
    $('.CRUD').crudForm({});
    $(".datepicker").datepicker({ changeMonth: true, changeYear: true, yearRange: '1950:' + new Date().getFullYear() });
    });

    if (typeof kyt == "undefined") {
        var kyt = {};
    }

    kyt.namespace = function() {
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
    kyt.namespace("kyt.utilities");

    kyt.crudHelpers = {
        selectboxPickerHandler: function(arr){
            $(".selectboxPicker").each(function(i,item){
                var name = $(item).attr("id")+"SelectBoxPickerDTO.Selected";
                $(item).find(".selectboxselected option").each(function(idx,opt){
                    var itemName = name+"["+idx+"]";
                    arr.push({"name":itemName,"value":opt.value})
                });
            });
        }
    };


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
            var notification = myOptions.notification ? myOptions.notification : kyt.utilities.messageHandling.notificationResult();
            var mySubmitHandler = function(form) {
                var data = myOptions.metaData?myOptions.metaData.getSubmitData():{};
                var ajaxOptions = {dataType: 'json',
                    success: function(result){
                        notification.result(result,form)
                    },
                    beforeSubmit:  beforSubmitCallback,
                    data: data
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
                validClass: "KYT_valid_field",
                errorClass: "KYT_invalid_field"

            });
        };

        $.fn.crudForm.defaults = {
            dataType: 'json',
            errorContainer: '#errorMessages',
            beforeSubmitCallbackFunctions: [kyt.crudHelpers.selectboxPickerHandler]
        };
    })(jQuery);

