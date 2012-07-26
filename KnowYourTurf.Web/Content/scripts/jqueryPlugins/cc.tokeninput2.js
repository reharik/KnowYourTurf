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
    inputToken: "token-input-input-token"
};

// Input box position "enum"
var POSITION = {
    BEFORE: 0,
    AFTER: 1,
    END: 2
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

// Additional public (exposed) methods
var methods = {
    init: function(url_or_data_or_function, options) {
        var settings = $.extend({}, DEFAULT_SETTINGS, options || {});

        return this.each(function () {
            $(this).data("tokenInputObject", new $.TokenList(this, url_or_data_or_function, settings));
        });
    },
    clear: function() {
        this.data("tokenInputObject").clear();
        return this;
    },
    add: function(item) {
        this.data("tokenInputObject").add(item);
        return this;
    },
    remove: function(item) {
        this.data("tokenInputObject").remove(item);
        return this;
    },
    addToAvailableList: function(item) {
        this.data("tokenInputObject").addToAvailableList(item);
        return this;
    }
};

// Expose the .tokenInput function to jQuery as a plugin
$.fn.tokenInput = function (method) {
    // Method calling and initialization logic
    if(methods[method]) {
        return methods[method].apply(this, Array.prototype.slice.call(arguments, 1));
    } else {
        return methods.init.apply(this, arguments);
    }
};

// TokenList class for each input
$.TokenList = function (input, viewModel, settings) {
    //
    // Initialization_.any
    //
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
    var token_count = 0;

    // Keep track of the timeout, old vals
    var timeout;
    var input_val;

    // Keep a reference to the selected token and dropdown item
    var selected_token = $(dropdown_ul).find("li").find("."+settings.classes.selectedToken);
    var selected_token_index = _.indexOf($(dropdown_ul).find("li"),selected_token);
    var selected_dropdown_item = null;


    //////
    // build the selected items template
    /////
    var token_list = $("<ul />")
        .addClass(settings.classes.tokenList)
        .delegate("span","click",function(){delete_token($(this).parent());})
        .attr("data-bind","foreach: selectedItems")
        .click(function (event) {
            var li = $(event.target).closest("li");
            if(li && li.get(0) && $.data(li.get(0), "tokeninput")) {
                toggle_select_token(li);
            } else {
                // Deselect selected token
                if(selected_token) {
                    deselect_token($(selected_token), POSITION.END);
                }
                // Focus input box
                input_box.focus();
            }
        })
        .mouseover(function (event) {
            var li = $(event.target).closest("li");
            if(li && selected_token !== this) {
                li.addClass(settings.classes.highlightedToken);
            }
        })
        .mouseout(function (event) {
            var li = $(event.target).closest("li");
            if(li && selected_token !== this) {
                li.removeClass(settings.classes.highlightedToken);
            }
        })
        .appendTo(controlContainer);
    var li = $("<li>").addClass(settings.classes.token)
        .append(settings.internalTokenMarkup());
    // The 'delete token' button
    var span = $("<span>" + settings.deleteText + "</span>")
        .addClass(settings.classes.tokenDelete)
        .appendTo(li);
    token_list.append(li);

    //////
    // build the main input element
    /////
    var input_box = $("<input type=\"text\"  autocomplete=\"off\">")
        .css({
            outline: "none"
        })
        .attr("id", settings.idPrefix + input.id)
        .blur(function () {
            hide_dropdown();
            $(this).val("");
        })
        //.bind("keyup keydown blur update", resize_input)
        .keydown(function (event) {
            var previous_token;
            var next_token;

            switch(event.keyCode) {
                case KEY.LEFT:
                case KEY.RIGHT:
                case KEY.UP:
                case KEY.DOWN:
                    if(!$(this).val()) {
                        previous_token = selected_token.prev();
                        next_token = selected_token.next();

                        if((previous_token.length && previous_token.get(0) === selected_token) || (next_token.length && next_token.get(0) === selected_token)) {
                           // Check if there is a previous/next token and it is selected
                            if(event.keyCode === KEY.LEFT || event.keyCode === KEY.UP) {
                                deselect_token($(selected_token), POSITION.BEFORE);
                            } else {
                                deselect_token($(selected_token), POSITION.AFTER);
                            }
                        } else if((event.keyCode === KEY.LEFT || event.keyCode === KEY.UP) && previous_token.length) {
                            // We are moving left, select the previous token if it exists
                            select_token($(previous_token.get(0)));
                        } else if((event.keyCode === KEY.RIGHT || event.keyCode === KEY.DOWN) && next_token.length) {
                            // We are moving right, select the next token if it exists
                            select_token($(next_token.get(0)));
                        }
                    } else {
                        var dropdown_item = null;

                        if(event.keyCode === KEY.DOWN || event.keyCode === KEY.RIGHT) {
                            dropdown_item = $(selected_token).next();
                        } else {
                            dropdown_item = $(selected_token).prev();
                        }

//                        if(event.keyCode === KEY.DOWN || event.keyCode === KEY.RIGHT) {
//                            dropdown_item = $(selected_dropdown_item).next();
//                        } else {
//                            dropdown_item = $(selected_dropdown_item).prev();
//                        }
                        if(dropdown_item.length) {
                            select_dropdown_item(dropdown_item);
                        }
                        return false;
                    }
                    break;

                case KEY.BACKSPACE:
                    previous_token = selected_token.prev();

                    if(!$(this).val().length) {
                        if(selected_token) {
                            delete_token($(selected_token));
                        } else if(previous_token.length) {
                            select_token($(previous_token.get(0)));
                        }

                        return false;
                    } else if($(this).val().length === 1) {
                        hide_dropdown();
                    } else {
                        // set a timeout just long enough to let this function finish.
                        setTimeout(function(){do_search();}, 5);
                    }
                    break;

                case KEY.TAB:
                case KEY.ENTER:
                case KEY.NUMPAD_ENTER:
                case KEY.COMMA:
                  if(selected_dropdown_item) {
                    //add_token($(selected_dropdown_item).data("tokeninput"));
                    return false;
                  }
                  break;

                case KEY.ESCAPE:
                  hide_dropdown();
                  return true;

                default:
                    if(String.fromCharCode(event.which)) {
                        // set a timeout just long enough to let this function finish.
                        setTimeout(function(){do_search();}, 5);
                    }
                    break;
            }
        });

    //////
    // build the container for the main input element
    /////
    var input_div = $("<div/>")
        .click(function () {
            if (settings.tokenLimit === null || settings.tokenLimit !== token_count) {
                populate_resultsModel(settings.viewModel.availableItems());
                show_and_decorate_dropdown(null);
                 //show_dropdown_hint();
            }
        })
        .addClass(settings.classes.inputToken)
        .append(input_box)
        .insertAfter(token_list);


    //////
    // build the container for the available items
    /////
    var dropdown = $("<div>")
        .addClass(settings.classes.dropdown)
        .hide()
        .insertAfter(input_div);
    var dropdown_ul = $("<ul>")
        .attr("data-bind","foreach: resultsItems")
        .mouseover(function (event) {
            select_dropdown_item($(event.target).closest("li"));
        })
        .mousedown(function (event) {
            var modelItem = find_modelItem_for_available_token($(event.target).closest("li"));
            add_token(modelItem);
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
        token_list.children("li").each(delete_token);
    };

    this.add = function(item) {
        add_token(item);
    };

    this.remove = function(item) {
        token_list.children("li").each(function() {
                var currToken = $(this).data("tokeninput");
                var match = true;
                for (var prop in item) {
                    if (item[prop] !== currToken[prop]) {
                        match = false;
                        break;
                    }
                }
                if (match) {
                    delete_token($(this));
                }
        });
    };

    this.addToAvailableList= function(item){
        // args for any callback may be wrong
        if(!_.any(settings.viewModel.availableItems, function(i,existingItem){
            return(existingItem.id() == item.id);
        }))
        settings.viewModel.availableItems.push(item);
    };

    //
    // Private functions
    //

    function checkTokenLimit() {
        if(settings.tokenLimit !== null && token_count >= settings.tokenLimit) {
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

    // Inner function to a token to the list
    function insert_token(item) {
        token_count += 1;
        var clone = {id:ko.observable(item.id()), name:ko.observable(item.name())};
        settings.viewModel.selectedItems.push(clone);
        settings.afterTokenSelectedFunction(clone);
        return clone;
    }

    // Add a token to the token list based on user input
    // item is modelItem
    function add_token (item) {
        var callback = settings.onAdd;

        // See if the token already exists and select it if we don't want duplicates
        if(token_count > 0 && settings.preventDuplicates) {
            var found_existing_token = _.find(settings.viewModel.selectedItems(), function(target,i){
                return item().id === target().id;
            });

            if(found_existing_token) {
                select_token(found_existing_token);
                input_div.insertAfter(found_existing_token);
                input_box.focus();
                return;
            }
        }

        // Insert the new tokens
        insert_token(item);
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

    // Select a token in the token list
    function select_token (token) {
        token.addClass(settings.classes.selectedToken);
        selected_token = token.get(0);

        // Hide input box
        input_box.val("");

        // Hide dropdown if it is visible (eg if we clicked to select token)
        hide_dropdown();
    }

    // Deselect a token in the token list
    // using token
    function deselect_token (token, position) {
        token.removeClass(settings.classes.selectedToken);
        selected_token = null;

        if(position === POSITION.BEFORE) {
            input_div.insertBefore(token);
            selected_token_index--;
        } else if(position === POSITION.AFTER) {
            input_div.insertAfter(token);
            selected_token_index++;
        } else {
            input_div.appendTo(token_list);
            selected_token_index = token_count;
        }

        // Show the input box and give it focus again
        input_box.focus();
    }

    // Toggle selection of a token in the token list
    function toggle_select_token(token) {
        var previous_selected_token = selected_token;

        if(selected_token) {
            deselect_token($(selected_token), POSITION.END);
        }

        if(previous_selected_token === token.get(0)) {
            deselect_token(token, POSITION.END);
        } else {
            select_token(token);
        }
    }

    function find_modelItem_for_selected_token (token){
        return _.find(settings.viewModel.selectedItems(), function(item,i){
            return $(token).find("p").text()===item.name();
        })
    }

    function find_modelItem_for_available_token (token){
        return _.find(settings.viewModel.availableItems(), function(item,i){
            return $(token).attr("id")===item.id();
        })
    }


    // Delete a token from the token list
    function delete_token (token) {

        var modelItem = find_modelItem_for_selected_token(token);
        var callback = settings.onDelete;

        var index = token.prevAll().length;
        if(index > selected_token_index) index--;

        // Delete the token
        settings.viewModel.selectedItems.remove(modelItem);
        selected_token = null;

        // Show the input box and give it focus again
        input_box.focus();

        if(index < selected_token_index) selected_token_index--;

        token_count -= 1;

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

    // Hide and clear the results dropdown
    function hide_dropdown () {
        dropdown.hide();
        $(dropdown_ul).html();
        $(dropdown).children("*").not("ul").remove();
        selected_dropdown_item = null;
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
            autoHideDropdown();
    }

    function show_dropdown_searching () {
        if(settings.searchingText) {
            dropdown.prepend("<p>"+settings.searchingText+"</p>");
            show_dropdown();
        }
    }

    function show_dropdown_hint () {
        if(settings.hintText) {
            dropdown.prepend("<p>"+settings.hintText+"</p>");
            show_dropdown();
        }
    }

    // Highlight the query part of the search term
    function highlight_term(value, term) {
        return value.replace(new RegExp("(?![^&;]+;)(?!<[^<>]*)(" + term + ")(?![^<>]*>)(?![^&;]+;)", "gi"), "<b>$1</b>");
    }

    // Populate the results dropdown with some results
    function show_and_decorate_dropdown (query) {
        if(settings.viewModel.resultsItems().length>0) {
            //clear_dropdown_ul();
            $.each($(dropdown_ul).find("li"), function(index, li) {
                var $li = $(li);
                // added () on value.name
                highlight_term($li.text(), query);
                if(index % 2) {
                    $li.addClass(settings.classes.dropdownItem);
                } else {
                    $li.addClass(settings.classes.dropdownItem2);
                }
                if(index === 0) {
                    select_dropdown_item(li);
                }
            });

            show_dropdown();

            if(settings.animateDropdown) {
                dropdown_ul.slideDown("fast");
            } else {
                dropdown_ul.show();
            }
        } else {
            if(settings.noResultsText) {
                $(dropdown).append("<p>"+settings.noResultsText+"</p>");
                show_dropdown();
            }
        }
    }

    function clear_resultsModel(){
        var temp = settings.viewModel.resultsItems().splice(0);
        _.each(temp,function(item,i){settings.viewModel.resultsItems.remove(item);});
    var x ="";
    }
    function populate_resultsModel(source){
        _.each(source, function(item,i){
            if(!_.any(settings.viewModel.resultsItems(), function(s,i){
                return s.id() === item.id();
            })){
                settings.viewModel.resultsItems.push(item);
            }
        });
    }

    // Highlight an item in the results dropdown
    // using token
    function select_dropdown_item (item) {
        if(item) {
            if(selected_dropdown_item) {
                deselect_dropdown_item($(selected_dropdown_item));
            }

            $(item).addClass(settings.classes.selectedDropdownItem);
            selected_dropdown_item = item;
        }
    }

    // Remove highlighting from an item in the results dropdown
    // using token
    function deselect_dropdown_item (item) {
        item.removeClass(settings.classes.selectedDropdownItem);
        selected_dropdown_item = null;
    }

    // Do a search and show the "searching" dropdown if the input is longer
    // than settings.minChars
    function do_search() {
        var query = input_box.val().toLowerCase();

        if(query && query.length) {
            if(selected_token) {
                deselect_token($(selected_token), POSITION.AFTER);
            }

            if(query.length >= settings.minChars) {
                show_dropdown_searching();
                clearTimeout(timeout);

                timeout = setTimeout(function(){
                    run_search(query);
                }, settings.searchDelay);
            } else {
                hide_dropdown();
            }
        }
    }

    // Do the actual search
    function run_search(query) {
        clear_resultsModel();
            // Are we doing an ajax search or local data search?
        if(settings.url) {
            // Extract exisiting get params
            var ajax_params = {};
            ajax_params.data = {};
            if(settings.url.indexOf("?") > -1) {
                var parts = settings.url.split("?");
                ajax_params.url = parts[0];

                var param_array = parts[1].split("&");
                $.each(param_array, function (index, value) {
                    var kv = value.split("=");
                    ajax_params.data[kv[0]] = kv[1];
                });
            } else {
                ajax_params.url = settings.url;
            }

            // Prepare the request
            ajax_params.data[settings.queryParam] = query;
            ajax_params.type = settings.method;
            ajax_params.dataType = settings.contentType;
            if(settings.crossDomain) {
                ajax_params.dataType = "jsonp";
            }

            // Attach the success callback
            ajax_params.success = function(results) {
              if($.isFunction(settings.onResult)) {
                  results = settings.onResult.call(hidden_input, results);
              }
              cache.add(query, settings.jsonContainer ? results[settings.jsonContainer] : results);

              // only populate the dropdown if the results are associated with the active search query
              if(input_box.val().toLowerCase() === query) {
                  show_and_decorate_dropdown(query, settings.jsonContainer ? results[settings.jsonContainer] : results);
              }
            };

            // Make the request
            $.ajax(ajax_params);
        } else {
            // Do the search through local data
            var results= $.grep(settings.viewModel.availableItems(), function (row) {
                // added () to value
                return row.name().toLowerCase().indexOf(query.toLowerCase()) > -1;
            });
            populate_resultsModel(results);
            
    
            if($.isFunction(settings.onResult)) {
                var callbackResult = settings.onResult.call(settings.viewModel.resultsItems);
                if(callbackResult){
                   populate_resultsModel(callbackResult);
                }
            }
            show_and_decorate_dropdown(query);
        }
    }
};
}(jQuery));
