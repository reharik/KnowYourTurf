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

ko.bindingHandlers.option = {
    update: function(element, valueAccessor) {
       var value = ko.utils.unwrapObservable(valueAccessor());
       ko.selectExtensions.writeValue(element, value.Value());
    }
};

ko.bindingHandlers.selectedOption = {
    update: function(element, valueAccessor) {
       var value = ko.utils.unwrapObservable(valueAccessor());
         ko.utils.arrayForEach(element.options,function(item){
            item.selected = item.value == value;
        });
        ko.selectExtensions.writeValue(element, value);

    }
};
