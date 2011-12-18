/*

	jQuery pub/sub plugin by Peter Higgins (dante@dojotoolkit.org)

	Loosely based on Dojo publish/subscribe API, limited in scope. Rewritten blindly.

	Original is (c) Dojo Foundation 2004-2010. Released under either AFL or new BSD, see:
	http://dojofoundation.org/license for more information.

*/

(function(d){

	// the topic/subscription hash
	var cache = {};

	d.publish = function(/* String */topic, /* Array? */args){
		// summary:
		//		Publish some data on a named topic.
		// topic: String
		//		The channel to publish on
		// args: Array?
		//		The data to publish. Each array item is converted into an ordered
		//		arguments on the subscribed functions.
		//
		// example:
		//		Publish stuff on '/some/topic'. Anything subscribed will be called
		//		with a function signature like: function(a,b,c){ ... }
		//
		//	|		$.publish("/some/topic", ["a","b","c"]);
		cache[topic] && d.each(cache[topic], function(){
			this.func.apply(d, args || []);
		});
	};

	d.subscribe = function(/* String */topic, /* Function */callback, /*hook name */ handle, name){
		// summary:
		//		Register a callback on a named topic.
		// topic: String
		//		The channel to subscribe to
		// callback: Function
		//		The handler event. Anytime something is $.publish'ed on a
		//		subscribed channel, the callback will be called with the
		//		published array as ordered arguments.
		//
		// returns: Array
		//		A handle which can be used to unsubscribe this particular subscription.
		//
		// example:
		//	|	$.subscribe("/some/topic", function(a, b, c){ /* handle data */ });
		//
		if(!cache[topic]){
			cache[topic] = [];
		}
		cache[topic].push({"handle":handle, "func":callback,"name":name});
        return [topic, callback]; // Array
	};

	d.unsubscribe = function( /* String */topic,/* Array */handle){
		// summary:
		//		Disconnect a subscribed function for a topic.
		// handle: Array
		//		The return value from a $.subscribe call.
		// example:
		//	|	var handle = $.subscribe("/something", function(){});
		//	|	$.unsubscribe(handle);

		var t = topic;
		cache[t] && d.each(cache[t], function(idx){
			if(this.handle == handle){
				cache[t].splice(idx, 1);
			}
		});
	};

    d.unsubscribeByPrefix = function(firstToken){
        for(var propertyName in cache) {
           if(propertyName.indexOf(firstToken)>=0){
               delete cache[propertyName];
           }
        }
    };

    d.unsubscribeByHandle = function(handle){
        var _handle = handle;
        $.each(cache,function(i,item){
            var _item = item;
            var removeFuncIndexs =[];
            $.each(_item, function(idx,obj){
                if(obj && obj.handle == _handle){
                    removeFuncIndexs.push(idx);
                }
            });
            $.each(removeFuncIndexs,function(idx,index){
                if(_item.length<=1){
                    _item.pop();
                }else{
                    _item.splice(index, 1);
                }
            })
        });
    };

    d.clearPubSub = function(){
        cache={};
    }


})(jQuery);

