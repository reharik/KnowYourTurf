/**
 * Created by .
 * User: RHarik
 * Date: 9/15/11
 * Time: 10:09 AM
 * To change this template use File | Settings | File Templates.
 */


$.fn.gridSearch = function(opts){
    var defaults = {
        default_empty_message:'Start typing to search',
        lightFontColor: "#CCCCCC",
        fontColor: "#000000",
        onClear:function() {},
        onSubmit:function(v) {},
        searchSelector: ".search"
    };

    return this.each(function() {
        var context = this;
        var $context = $(this);
        var button;
        var lastSubmitted;
        context.options = $.extend(defaults, opts);
        var $searchElement = $context.find(context.options.searchSelector);

        this.render = function() {
            $searchElement.parent().append('<img class="clear-search" src="/content/images/clear-search.png">');
            button = $searchElement.parent().find('img.clear-search');
            if ($.trim($searchElement.val())=='') {
                $searchElement.val(context.options.default_empty_message);
            }
            if ($searchElement.val()==context.options.default_empty_message) {
                $searchElement.css("color",context.options.lightFontColor);
            } else { $searchElement.css("color",context.options.fontColor); }
        };

        this.registerEventHandlers = function() {
            $searchElement.keyup(function(event) {
                if (event.keyCode == '13') {
                    context.submitSearch($(this).val());
                    $searchElement.next().focus();
                } else {
                    if ($.trim($(this).val())!='' && $(this).val()!=context.options.default_empty_message) {
                        button.show();
                    } else {
                        button.hide();
                    }
                    lastSubmitted = null;
                }
            });

            $searchElement.blur(function() {
                if ($.trim($(this).val())=='') {
                    $searchElement.val(context.options.default_empty_message).css("color",context.options.lightFontColor);
                    button.hide();
                } else {
                    this.submitSearch($searchElement.val());
                }
            });

            $searchElement.focus(function() {
                if ($(this).val()==context.options.default_empty_message) {
                    $searchElement.val('').css("color",context.options.fontColor);
                }
            });

            button.click(function() {
                context.clearSearch();
        	});
        };

        this.clearSearch = function() {
            $searchElement.val('');
            $searchElement.focus();
            context.options.onClear();
        };

        this.submitSearch =  function(v) {
            if (v!='' && v!=context.options.default_empty_message && v!=lastSubmitted) {
                this.options.onSubmit(v);
                lastSubmitted = v;
            }
        };

        function init() {
             context.render();
             context.registerEventHandlers();
        }

       init();

    });
};

