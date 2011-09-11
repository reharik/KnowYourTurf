if (typeof kyt == "undefined") {
            var kyt = {};
}

if (typeof kyt.weatherService == "undefined") {
            kyt.weatherService = {};
}

kyt.weatherService.controller = (function(){
           var addScript =function(url) {
               $('script').
    var script = document.createElement('script');
    script.src = url;
    document.body.appendChild(script);
};
    return {
        init: function(model){
            $(model.CompanyDtos).each(function(i,item){
                var data ={
                    lat : item.lat,
                    lng:item.lng,
                    date : item.date
                };
                addScript("http://www.climate.gov/cwaw/GSODLookup?lat=30.263250&lng=-97.714501&date=2011-07-02&_=1309720766042");
                //kyt.weatherService.repository.getWeatherCall(model.WeatherServiceUrl,data);

            });
        },

    }
}());

kyt.weatherService.repository = (function(){
    var getWeatherCallback = function(result){
       var x="";
    };
    return{
        getWeatherCall:function(url,data){
            $.ajax({url:url,data:data, type: "GET",error:getWeatherCallback,dataType: "jsonp", jsonp: 'jsonp'});
        }
    }
}());