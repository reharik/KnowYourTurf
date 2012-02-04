/**
 * Created by .
 * User: RHarik
 * Date: 4/20/11
 * Time: 11:22 AM
 * To change this template use File | Settings | File Templates.
 */
if (typeof kyt == "undefined") {
            var kyt = {};
}

if (typeof kyt.repeatableInputSection== "undefined") {
            kyt.repeatableInputSection= {};
}

// this is not a static instance!
kyt.repeatableInputSection.controller = function () {
    var myOptions;
    return {
        init: function(options) {
            myOptions = $.extend({ }, kyt.repeatableInputSection.defaults, options || { });
            $(myOptions.container).data().repeatableSection = this;
            $(myOptions.container).delegate("#addNewSection", "click", function(e) {
                $(this).closest("div.kyt_repeatableSection").data().repeatableSection.addNewSection(e) });
            $(myOptions.container).delegate(".kyt_remove", "click", function(e) { $(this).closest("div.kyt_repeatableSection").data().repeatableSection.removeSection(e) });
            this.addNewSection();
        },
        addNewSection: function(e) {
            var section = $(myOptions.template).children("li");
            var newSection = $(section).clone();
            cc.utilities.clearInputs(newSection);
            $(myOptions.container).find("ul").append(newSection);
            return newSection;
        },
        removeSection: function(e) {
            $(e.target).closest("li").remove();
        }
    };
};

kyt.repeatableInputSection.defaults = {
    container:"#repeatableItems",
    template:"#repeatableItemTemplate"
};