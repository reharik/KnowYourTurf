/**
 * Created by JetBrains RubyMine.
 * User: RHarik
 * Date: 7/25/12
 * Time: 8:28 AM
 * To change this template use File | Settings | File Templates.
 */

ko.bindingHandlers.MultiSelect = {
    init: function(element, valueAccessor, allBindingsAccessor, viewModel) {
         var model = valueAccessor();
        model._resultsItems = ko.observableArray();
        $(element).tokenInput(model, {
                internalTokenMarkup:function(){
                    var anchor = $("<a>").addClass("selectedItem").attr("data-bind",'text:name');
                    return $("<p>").append(anchor);
                }
        });

        ko.applyBindings(model, $(element).next("div")[0]);
    },
    update: function(element, valueAccessor, allBindingsAccessor, viewModel) {
        // This will be called once when the binding is first applied to an element,
        // and again whenever the associated observable changes value.
        // Update the DOM element based on the supplied values here.
    }

};

ko.bindingHandlers.dateString = {
    init: function(element, valueAccessor) {
        //handle the field changing
        ko.utils.registerEventHandler(element, "change", function () {
            var observable = valueAccessor();
            observable($(element).val());
        });

        //handle disposal (if KO removes by the template binding)
        ko.utils.domNodeDisposal.addDisposeCallback(element, function() {
            $(element).timepicker("destroy");
        });

        // handle .net crappy json
        var valueUnwrapped = ko.utils.unwrapObservable(valueAccessor());
        var formattedDate = valueUnwrapped? new XDate(new Date(parseInt(valueUnwrapped.substr(6)))).toString("MM/dd/yyyy"):"";
        $(element).val(formattedDate).change();
    },
    //update the control when the view model changes
    update: function(element, valueAccessor) {
        var value = ko.utils.unwrapObservable(valueAccessor());
        $(element).val(value);
    }
};

ko.bindingHandlers.timeString = {
    init: function(element, valueAccessor) {
        //handle the field changing
        ko.utils.registerEventHandler(element, "change", function () {
            var observable = valueAccessor();
            observable($(element).val());
        });

        //handle disposal (if KO removes by the template binding)
        ko.utils.domNodeDisposal.addDisposeCallback(element, function() {
            $(element).datepicker("destroy");
        });

        // handle .net crappy json
        var valueUnwrapped = ko.utils.unwrapObservable(valueAccessor());
        var formattedTime = valueUnwrapped? new XDate(new Date(parseInt(valueUnwrapped.substr(6)))).toString("hh:mm TT"):"";
        $(element).val(formattedTime).change();
    },
    //update the control when the view model changes
    update: function(element, valueAccessor) {
        var value = ko.utils.unwrapObservable(valueAccessor());
        $(element).val(value);
    }
};