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
            if(element.nodeName  == "SPAN"){
                observable($(element).text());
            }
            if(element.nodeName  == "INPUT"){
                observable($(element).val());
            }
        });

        //handle disposal (if KO removes by the template binding)
        ko.utils.domNodeDisposal.addDisposeCallback(element, function() {
            $(element).timepicker("destroy");
        });

        // handle .net crappy json
        var valueUnwrapped = ko.utils.unwrapObservable(valueAccessor());
        var formattedDate = valueUnwrapped? new XDate(new XDate(valueUnwrapped)).toString("MM/dd/yyyy"):"";
        if(element.nodeName  == "SPAN"){
           $(element).text(formattedDate).change();
        }
        if(element.nodeName  == "INPUT"){
            $(element).val(formattedDate).change();
        }

    },
    //update the control when the view model changes
    update: function(element, valueAccessor) {
        var value = ko.utils.unwrapObservable(valueAccessor());
        if(element.nodeName  == "SPAN"){
           $(element).text(value);
        }
        if(element.nodeName  == "INPUT"){
            $(element).val(value);
        }

    }
};

ko.bindingHandlers.timeString = {
    init: function(element, valueAccessor) {
        //handle the field changing
        ko.utils.registerEventHandler(element, "change", function () {
            var observable = valueAccessor();
            if(element.nodeName  == "SPAN"){
               observable($(element).text());
            }
            if(element.nodeName  == "INPUT"){
                observable($(element).val());
            }

        });

        //handle disposal (if KO removes by the template binding)
        ko.utils.domNodeDisposal.addDisposeCallback(element, function() {
            $(element).datepicker("destroy");
        });

        // handle .net crappy json
        var valueUnwrapped = ko.utils.unwrapObservable(valueAccessor());
        var formattedTime = valueUnwrapped? new XDate(new Date(valueUnwrapped)).toString("hh:mm TT"):"";
        if(element.nodeName  == "SPAN"){
           $(element).text(formattedTime).change();
        }
        if(element.nodeName  == "INPUT"){
            $(element).val(formattedTime).change();
        }
    },
    //update the control when the view model changes
    update: function(element, valueAccessor) {
        var value = ko.utils.unwrapObservable(valueAccessor());
        if(element.nodeName  == "SPAN"){
           $(element).text(value);
        }
        if(element.nodeName  == "INPUT"){
            $(element).val(value);
        }
        $(element).val(value);
    }
};

    function ensureDropdownSelectionIsConsistentWithModelValue(element, modelValue, preferModelValue) {
        if (preferModelValue) {
            if (modelValue !== ko.selectExtensions.readValue(element))
                ko.selectExtensions.writeValue(element, modelValue);
        }

        // No matter which direction we're syncing in, we want the end result to be equality between dropdown value and model value.
        // If they aren't equal, either we prefer the dropdown value, or the model value couldn't be represented, so either way,
        // change the model value to match the dropdown.
        if (modelValue !== ko.selectExtensions.readValue(element))
            ko.utils.triggerEvent(element, "change");
    }

ko.bindingHandlers['groupedSelect'] = {
    'update': function (element, valueAccessor, allBindingsAccessor) {
        if (ko.utils.tagNameLower(element) !== "select")
            throw new Error("options binding applies only to SELECT elements");

        var selectWasPreviouslyEmpty = element.length == 0;
        var previousSelectedValues = ko.utils.arrayMap(ko.utils.arrayFilter(element.childNodes, function (node) {
            return node.tagName && (ko.utils.tagNameLower(node) === "option") && node.selected;
        }), function (node) {
            return ko.selectExtensions.readValue(node) || node.innerText || node.textContent;
        });
        var previousScrollTop = element.scrollTop;

        var value = ko.utils.unwrapObservable(valueAccessor());
        value = value.groups();
        var selectedValue = element.value;

        // Remove all existing <option>s.
        // Need to use .remove() rather than .removeChild() for <option>s otherwise IE behaves oddly (https://github.com/SteveSanderson/knockout/issues/134)
        while (element.length > 0) {
            ko.cleanNode(element.options[0]);
            element.remove(0);
        }
        while(element.children.length >0){
            ko.cleanNode(element.children[0]);
            $(element.children[0]).remove();
        }

        if (value) {
            var allBindings = allBindingsAccessor();
            if (typeof value.length != "number")
                value = [value];
            if (allBindings['optionsCaption']) {
                var option = document.createElement("option");
                ko.utils.setHtml(option, allBindings['optionsCaption']);
                ko.selectExtensions.writeValue(option, undefined);
                element.appendChild(option);
            }
            for (var a= 0, b = value.length; a < b; a++) {
                var optGroup = document.createElement("optgroup");
                ko.bindingHandlers['attr'].update(optGroup, ko.observable({label: value[a].label()}));
                var children = ko.utils.unwrapObservable(value[a].children());
                for (c=0, d=children.length; c<d; c++){
                    var option = document.createElement("option");

                    // Apply a value to the option element
                    var optionValue = typeof allBindings['optionsValue'] == "string" ? value[a].children()[c][allBindings['optionsValue']] : value[a].children()[c];
                    optionValue = ko.utils.unwrapObservable(optionValue);
                    ko.selectExtensions.writeValue(option, optionValue);

                    // Apply some text to the option element
                    var optionsTextValue = allBindings['optionsText'];
                    var optionText;
                    if (typeof optionsTextValue == "function")
                        optionText = optionsTextValue(value[a].children()[c]); // Given a function; run it against the data value
                    else if (typeof optionsTextValue == "string")
                        optionText = value[a].children()[c][optionsTextValue]; // Given a string; treat it as a property name on the data value
                    else
                        optionText = optionValue;				 // Given no optionsText arg; use the data value itself
                    if ((optionText === null) || (optionText === undefined))
                        optionText = "";

                    ko.utils.setTextContent(option, optionText);
                    optGroup.appendChild(option);
                }
                element.appendChild(optGroup);
            }

            // IE6 doesn't like us to assign selection to OPTION nodes before they're added to the document.
            // That's why we first added them without selection. Now it's time to set the selection.
            var newOptions = element.getElementsByTagName("option");
            var countSelectionsRetained = 0;
            for (var i = 0, j = newOptions.length; i < j; i++) {
                if (ko.utils.arrayIndexOf(previousSelectedValues, ko.selectExtensions.readValue(newOptions[i])) >= 0) {
                    ko.utils.setOptionNodeSelectionState(newOptions[i], true);
                    countSelectionsRetained++;
                }
            }

            element.scrollTop = previousScrollTop;

            if (selectWasPreviouslyEmpty && ('value' in allBindings)) {
                // Ensure consistency between model value and selected option.
                // If the dropdown is being populated for the first time here (or was otherwise previously empty),
                // the dropdown selection state is meaningless, so we preserve the model value.
                ensureDropdownSelectionIsConsistentWithModelValue(element, ko.utils.unwrapObservable(allBindings['value']), /* preferModelValue */ true);
            }

            // Workaround for IE9 bug
            ko.utils.ensureSelectElementIsRenderedCorrectly(element);
        }
    }
};

