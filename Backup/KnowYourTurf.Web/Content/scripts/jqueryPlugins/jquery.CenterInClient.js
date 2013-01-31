(function($) {
    $.fn.centerInClient = function(options) {
        /// <summary>Centers the selected items in the browser window. Takes into account scroll position.
        /// Ideally the selected set should only match a single element.
        /// </summary>    
        /// <param name="fn" type="Function">Optional function called when centering is complete. Passed DOM element as parameter</param>    
        /// <param name="forceAbsolute" type="Boolean">if true forces the element to be removed from the document flow 
        ///  and attached to the body element to ensure proper absolute positioning. 
        /// Be aware that this may cause ID hierachy for CSS styles to be affected.
        /// </param>
        /// <returns type="jQuery" />
        var opt = { forceAbsolute: false,
            container: window,    // selector of element to center in
            completed: null
        };
        $.extend(opt, options);

        return this.each(function(i) {
            var el = $(this);
            var jWin = $(opt.container);
            var isWin = opt.container == window;

            // force to the top of document to ENSURE that 
            // document absolute positioning is available
            if (opt.forceAbsolute) {
                if (isWin)
                    el.remove().appendTo("body");
                else
                    el.remove().appendTo(jWin.get(0));
            }

            // have to make absolute
            el.css("position", "absolute");

            var test;


            // height is off a bit so fudge it
            var heightFudge = 2.2;

            // var outhgt = jWin.outerHeight();
            // alert(outhgt);

            // alert(isWin +"? "+jWin.height()+ " : "+ jWin.outerHeight()+") / "+heightFudge +"-"+ el.outerHeight() +"/ 2");
            // alert("isWin:" + isWin + " jWin.height(): " + jWin.height() + " jWin.outerHeight(): " + jWin.outerHeight())


          /*  test = "isWin:" + isWin + "<br />";
            test = test + " jWin.height(): " + jWin.height() + "<br />";
            test = test + " jWin.outerHeight(): " + jWin.outerHeight() + "<br />";
            test = test + isWin + "? " + jWin.height() + " : " + jWin.outerHeight() + ") / " + heightFudge + "-" + el.outerHeight() + "/ 2 <br />";
            var testy = jWin.height() / heightFudge - el.outerHeight() / 2;
            test = test + "y=" + testy + "<br />";*/

            var x = (isWin ? jWin.width() : jWin.outerWidth()) / 2 - el.outerWidth() / 2;
            var y = (isWin ? jWin.height() : jWin.outerHeight()) / heightFudge - el.outerHeight() / 2;
           // test = test + "Orig y=" + y + "<br />";

          //  test = test + "+Scrolltop: " + jWin.scrollTop() + "<br />";

          //KM fix for frame positioning
            if ((y + jWin.scrollTop()) < 0) {
                el.css({ left: x + jWin.scrollLeft(), top: 0 });
            } else {
            el.css({ left: x + jWin.scrollLeft(), top: y + jWin.scrollTop() });
            }

          //  $("#aDebug").html(test);
      
         //   el.css({ left: x + jWin.scrollLeft(), top: y + jWin.scrollTop() });

            var zi = el.css("zIndex");
            if (!zi || zi == "auto")
                el.css("zIndex", 1);

            // if specified make callback and pass element
            if (opt.completed)
                opt.completed(this);
        });
    }

})(jQuery);