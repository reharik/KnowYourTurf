/*
 * jQuery Plugin: Tokenizing Autocomplete Text Entry
 * Version 1.5.0
 *
 * Copyright (c) 2009 James Smith (http://loopj.com)
 * Licensed jointly under the GPL and MIT licenses,
 * choose which one suits your project best!
 *
 */

(function ($) {
// Default settings
var DEFAULT_SETTINGS = {
    hintText: "Type in a search term",
    noResultsText: "No results",
    searchingText: "Searching...",
    deleteText: "&times;",
    searchDelay: 300,
    minChars: 1,
    tokenLimit: null,
    jsonContainer: null,
    method: "GET",
    contentType: "json",
    queryParam: "q",
    tokenDelimiter: ",",
    preventDuplicates: true,
    prePopulate: null,
    processPrePopulate: false,
    animateDropdown: true,
    onResult: null,
    onAdd: null,
    onDelete: null,
    idPrefix: "token-input-",
    internalTokenMarkup:function(item){return "<p>"+ item.name +"</p>";},
    afterTokenSelectedFunction:function(){}
};

// Default classes to use when theming
var DEFAULT_CLASSES = {
    tokenList: "token-input-list",
    token: "token-input-token",
    tokenDelete: "token-input-delete-token",
    selectedToken: "token-input-selected-token",
    highlightedToken: "token-input-highlighted-token",
    dropdown: "token-input-dropdown",
    dropdownItem: "token-input-dropdown-item",
    dropdownItem2: "token-input-dropdown-item2",
    selectedDropdownItem: "token-input-selected-dropdown-item",
    inputToken: "token-input-container",
    tokenInput: "token-input-Input"
};

// Keys "enum"
var KEY = {
    BACKSPACE: 8,
    TAB: 9,
    ENTER: 13,
    ESCAPE: 27,
    SPACE: 32,
    PAGE_UP: 33,
    PAGE_DOWN: 34,
    END: 35,
    HOME: 36,
    LEFT: 37,
    UP: 38,
    RIGHT: 39,
    DOWN: 40,
    NUMPAD_ENTER: 108,
    COMMA: 188
};

// Expose the .tokenInput function to jQuery as a plugin
$.fn.tokenInput = function (viewmodel, options) {
    var settings = $.extend({}, DEFAULT_SETTINGS, options || {});
    return this.each(function () {
        $(this).data("tokenInputObject", new $.TokenList(this, viewmodel, settings));
    });
};

// TokenList class for each input
$.TokenList = function (input, viewModel, settings) {
    var id = $(input).attr("id");
    var controlContainer = $("<div></div>");
    $(input).after(controlContainer);
    
    settings.viewModel = viewModel;

    // Build class names
    if(settings.classes) {
        // Use custom class names
        settings.classes = $.extend({}, DEFAULT_CLASSES, settings.classes);
    } else if(settings.theme) {
        // Use theme-suffixed default class names
        settings.classes = {};
        $.each(DEFAULT_CLASSES, function(key, value) {
            settings.classes[key] = value + "-" + settings.theme;
        });
    } else {
        settings.classes = DEFAULT_CLASSES;
    }

    // Keep track of the number of tokens in the list
    var token_count = function(){return settings.viewModel.selectedItems().length;};

    // Keep track of the timeout, old vals
    var input_val;

    // Keep a reference to the selected token and dropdown item
    var selected_token = function(){return $(selected_token_list).find("li."+settings.classes.selectedToken);};
    var selected_dropdown_item = function(){return $(dropdown_ul).find("li."+settings.classes.selectedDropdownItem);};


    //////
    // build the selected items template
    /////
    var selected_token_list = $("<ul />")
        .addClass(settings.classes.tokenList)
        .attr("data-bind","foreach: selectedItems")
        .delegate("span","click",function(){delete_selected_listItem($(this).parent());})
        .delegate("li","click",function(){
            var $li = $(this);
            toggle_select_token($li);
            input_box.focus();
        })
        .delegate("li","mouseover", function () {
            var $li = $(this);
            if(selected_token() !== this) {
                $li.addClass(settings.classes.highlightedToken);
            }
        })
        .delegate("li","mouseout",function () {
            var $li = $(this);
            if(selected_token() !== this) {
                $li.removeClass(settings.classes.highlightedToken);
            }
        })
        .appendTo(controlContainer);
    var li = $("<li>").addClass(settings.classes.token)
        .attr("data-bind","attr:{id:id}")
        .append(settings.internalTokenMarkup());
    // The 'delete token' button
    var span = $("<span>" + settings.deleteText + "</span>")
        .addClass(settings.classes.tokenDelete)
        .appendTo(li);
    selected_token_list.append(li);

    //////
    // build the main input element
    /////
    var input_box = $("<input type=\"text\"  autocomplete=\"off\">")
        .addClass(settings.classes.tokenInput)
        .css({
            outline: "none"
        })
        .attr("id", settings.idPrefix + input.id)
        .blur(function () {
            hide_dropdown();
            $(this).val("");
            $(this).trigger(id+":tokenizer:blur",settings.viewModel.selectedItems);
        })
        //.bind("keyup keydown blur update", resize_input)
        .keydown(function (event) {
            var previous_token;
            var next_token;
            var _selectedToken = selected_dropdown_item();
            switch(event.keyCode) {
                case KEY.LEFT:
                case KEY.RIGHT:
                case KEY.UP:
                case KEY.DOWN:
                    if(!$(this).val()) {
                        previous_token = _selectedToken.prev();
                        next_token = _selectedToken.next();
                       if((event.keyCode === KEY.LEFT || event.keyCode === KEY.UP) && previous_token.length) {
                            // We are moving left, select the previous token if it exists
                            highlight_available_item($(previous_token.get(0)));
                        } else if((event.keyCode === KEY.RIGHT || event.keyCode === KEY.DOWN) && next_token.length) {
                            // We are moving right, select the next token if it exists
                            highlight_available_item($(next_token.get(0)));
                        }
                    } else {
                        var dropdown_item = null;

                        if(event.keyCode === KEY.DOWN || event.keyCode === KEY.RIGHT) {
                            dropdown_item = _selectedToken.next();
                        } else {
                            dropdown_item = _selectedToken.prev();
                        }
                        if(dropdown_item().length) {
                            highlight_available_item(dropdown_item);
                        }
                        return false;
                    }
                    break;
                case KEY.TAB:
                case KEY.ENTER:
                case KEY.NUMPAD_ENTER:
                case KEY.COMMA:
                  if(selected_dropdown_item().length) {
                      var modelItem = find_modelItem_for_available_listItem(selected_dropdown_item());
                      add_listItem_to_selected(modelItem);
                      hide_dropdown();
                      return false;
                  }
                  break;

                case KEY.ESCAPE:
                  hide_dropdown();
                  return true;

                default:
                    if(String.fromCharCode(event.which)) {
                        // set a timeout just long enough to let this function finish.
                        setTimeout(function(){show_dropdown_results();}, 5);
                    }
                    break;
            }
        });

    //////
    // build the container for the main input element
    /////
    var sprite_div = $("<div />").addClass("token-input-sprite");
    var input_div = $("<div/>")
        .click(function () {
            if (settings.tokenLimit === null || settings.tokenLimit !== token_count()) {
                show_dropdown_results();
            }
        })
        .addClass(settings.classes.inputToken)
        .append(input_box)
        .append(sprite_div)
        .insertAfter(selected_token_list);

    //////
    // build the container for the available items
    /////
    var dropdown = $("<div>")
        .addClass(settings.classes.dropdown)
        .hide()
        .insertAfter(input_div);
    var dropdown_ul = $("<ul>")
        .attr("data-bind","foreach: _resultsItems")
        .delegate("li","mouseover",function (event) {
            highlight_available_item($(this));
        })
        .delegate("li","mousedown",function (event) {
            var modelItem = find_modelItem_for_available_listItem(this);
            add_listItem_to_selected(modelItem);
            return false;
        })
        .hide()
        .appendTo(dropdown);
    $("<li>")
        .attr("data-bind",'text:name, attr:{id:id}')
        .appendTo(dropdown_ul);



    //
    // Public functions
    //

    this.clear = function() {
        selected_token_list.children("li").each(delete_selected_listItem);
    };

    this.add = function(item) {
        add_listItem_to_selected(item);
    };

    this.remove = function(item) {
        selected_token_list.children("li").each(function() {
                var currToken = $(this).data("tokeninput");
                var match = true;
                for (var prop in item) {
                    if (item[prop] !== currToken[prop]) {
                        match = false;
                        break;
                    }
                }
                if (match) {
                    delete_selected_listItem($(this));
                }
        });
    };

    this.addToAvailableList= function(item){
        // args for any callback may be wrong
        if(!_.any(settings.viewModel._availableItems, function(i,existingItem){
            return(existingItem.id() == item.id);
        }))
        settings.viewModel._availableItems.push(item);
    };

    //
    // Private functions
    //

    function checkTokenLimit() {
        if(settings.tokenLimit !== null && token_count() >= settings.tokenLimit) {
            input_box.hide();
            hide_dropdown();
        } else {
            input_box.focus();
        }
    }

    function resize_input() {
        if(input_val === (input_val = input_box.val())) {return;}

        // Enter new content into resizer and resize input accordingly
        var escaped = input_val.replace(/&/g, '&amp;').replace(/\s/g,' ').replace(/</g, '&lt;').replace(/>/g, '&gt;');
        input_resizer.html(escaped);
        input_box.width(input_resizer.width() + 30);
    }

    function is_printable_character(keycode) {
        return ((keycode >= 48 && keycode <= 90) ||     // 0-1a-z
                (keycode >= 96 && keycode <= 111) ||    // numpad 0-9 + - / * .
                (keycode >= 186 && keycode <= 192) ||   // ; = , - . / ^
                (keycode >= 219 && keycode <= 222));    // ( \ ) '
    }

    // Inner function to add a token to the list
    function add_modelItem_to_selected(item) {
        var clone = {id:ko.observable(item.id()), name:ko.observable(item.name())};
        settings.viewModel.selectedItems.push(clone);
        settings.afterTokenSelectedFunction(clone);
        return clone;
    }

    // Add a token to the token list based on user input
    // item is modelItem
    function add_listItem_to_selected (item) {
        var callback = settings.onAdd;
        // See if the token already exists and select it if we don't want duplicates
        if(token_count() > 0 && settings.preventDuplicates) {
            var found_existing_item = _.find(settings.viewModel.selectedItems(), function(target,i){
                return item.id() === target.id();
            });

            if(found_existing_item) {
                var token = selected_token_list.find("li[id='"+found_existing_item.id()+"']");
                highlight_selected_listItem(token);
                input_box.focus();
                hide_dropdown();
                return;
            }
        }

        // Insert the new tokens
        add_modelItem_to_selected(item);
        var newToken = selected_token_list.find("li[id='"+item.id()+"']");
        highlight_selected_listItem(newToken);
        checkTokenLimit();
        // Clear input box
        input_box.val("");
        // Don't show the help dropdown, they've got the idea
        hide_dropdown();
        // Execute the onAdd callback if defined
        if($.isFunction(callback)) {
            callback.call(item);
        }
    }

      // Delete a token from the token list
    function delete_selected_listItem (token) {

        var modelItem = find_modelItem_for_selected_listItem(token);
        var callback = settings.onDelete;
        // Delete the token
        settings.viewModel.selectedItems.remove(modelItem);
        // Show the input box and give it focus again
        input_box.focus();
        if(settings.tokenLimit !== null) {
            input_box
                .show()
                .val("")
                .focus();
        }

        // Execute the onDelete callback if defined
        if($.isFunction(callback)) {
            callback.call(args);
        }
    }

    function find_modelItem_for_selected_listItem (token){
        return _.find(settings.viewModel.selectedItems(), function(item,i){
            return $(token).find("p").text()===item.name();
        })
    }

    function find_modelItem_for_available_listItem (token){
        return _.find(settings.viewModel._availableItems(), function(item,i){
            return $(token).attr("id")===item.id();
        })
    }

  // Toggle selection of a token in the token list
    function toggle_select_token(token) {
        var previous_selected_token = selected_token().get(0);
        if(previous_selected_token === token.get(0)) {
            token.removeClass(settings.classes.selectedToken);
        } else {
            highlight_selected_listItem(token);
        }
        input_box.val("");
    }
    function highlight_selected_listItem(token){
        selected_token_list.find("li."+settings.classes.selectedToken).removeClass(settings.classes.selectedToken);
            token.addClass(settings.classes.selectedToken);
    }

    // Highlight an item in the results dropdown
    function highlight_available_item (item) {
        if(item) {
            if(selected_dropdown_item().length) {
                unhighlight_available_item(selected_dropdown_item());
            }
            $(item).addClass(settings.classes.selectedDropdownItem);
        }
    }

    // Remove highlighting from an item in the results dropdown
    function unhighlight_available_item (item) {
        item.removeClass(settings.classes.selectedDropdownItem);
    }

    // Highlight the query part of the search term
    function highlight_term(value, term) {
        if(term){
            value = value.replace(new RegExp("(?![^&;]+;)(?!<[^<>]*)(" + term + ")(?![^<>]*>)(?![^&;]+;)", "gi"), "<b>$1</b>");
        }
        return value;
    }

    function populate_resultsModel(source){
        $(dropdown).children("*").not("ul").remove();
        settings.viewModel._resultsItems.removeAll();
        _.each(source,function(item,i){settings.viewModel._resultsItems.push(item);});
    }

     // Hide and clear the results dropdown
    function hide_dropdown () {
        dropdown.hide();
        $(dropdown_ul).html();
        $(dropdown).children("*").not("ul").remove();
        dropdown_ul.find("li."+settings.classes.selectedDropdownItem).removeClass(settings.classes.selectedDropdownItem);
    }

    function show_dropdown() {
        //added delay for hiding dropdown RH
        function autoHideDropdown(){
            var t = setTimeout(function() {
                     hide_dropdown();
                },8000);
            dropdown.mouseenter(function(){clearTimeout(t)});
        }
        dropdown
            .show()
            .mouseleave(function(){autoHideDropdown()});
        if(settings.animateDropdown) {
            dropdown_ul.slideDown("fast");
        } else {
            dropdown_ul.show("slow");
        }
        autoHideDropdown();

    }

    function decorate_dropdown (query) {
        if(settings.viewModel._resultsItems().length>0) {
            $.each($(dropdown_ul).find("li"), function(index, li) {
                var $li = $(li);
                $li.html(highlight_term($li.text(), query));
                if(index % 2) {
                    $li.addClass(settings.classes.dropdownItem);
                } else {
                    $li.addClass(settings.classes.dropdownItem2);
                }
                if(index === 0) {
                    highlight_available_item(li);
                }
            });
        } else {
            if(settings.noResultsText) {
                $(dropdown).append("<p>"+settings.noResultsText+"</p>");
            }
        }
    }

    // Do a search and show the "searching" dropdown if the input is longer
    // than settings.minChars
    function show_dropdown_results() {
        var query = input_box.val().toLowerCase();
        if(!query || !query.length){
            populate_resultsModel(settings.viewModel._availableItems());
            decorate_dropdown("");
            show_dropdown();
            return;
        }
        if(query.length >= settings.minChars) { run_search(query); }
    }

    // Do the actual search
    function run_search(query) {
            // Do the search through local data
            var results= $.grep(settings.viewModel._availableItems(), function (row) {
                // added () to value
                return row.name().toLowerCase().indexOf(query.toLowerCase()) > -1;
            });
            populate_resultsModel(results);
            
    
            if($.isFunction(settings.onResult)) {
                var callbackResult = settings.onResult.call(settings.viewModel._resultsItems);
                if(callbackResult){
                   populate_resultsModel(callbackResult);
                }
            }
            decorate_dropdown(query);
            show_dropdown();
    }
};
}(jQuery));
