using System;
using System.Collections.Generic;
using System.Web.Mvc;
using KnowYourTurf.Core;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Html;
using KnowYourTurf.Core.Services;

namespace KnowYourTurf.Web.Controllers
{
    public class WeatherController:KYTController
    {
        private readonly IRepository _repository;
        private readonly ISaveEntityService _saveEntityService;

        public WeatherController(IRepository repository,
            ISaveEntityService saveEntityService)
        {
            _repository = repository;
            _saveEntityService = saveEntityService;
        }

        public ActionResult AddUpdate(ViewModel input)
        {
            var weather = input.EntityId > 0 ? _repository.Find<Weather>(input.EntityId) : new Weather();
            var model = new WeatherViewModel
            {
                Item = weather,
                Title = WebLocalizationKeys.WEATHER_INFORMATION.ToString()
            };
            return PartialView("WeatherAddUpdate", model);
        }
      
        public ActionResult Display(ViewModel input)
        {
            var weather = _repository.Find<Weather>(input.EntityId);
            var model = new WeatherViewModel
                            {
                                Item = weather,
                                AddUpdateUrl = UrlContext.GetUrlForAction<WeatherController>(x => x.AddUpdate(null)) + "/" + weather.EntityId,
                                Title = WebLocalizationKeys.WEATHER_INFORMATION.ToString()
                            };
            return PartialView("WeatherView", model);
        }

        public ActionResult Delete(ViewModel input)
        {
            var weather = _repository.Find<Weather>(input.EntityId);
            _repository.HardDelete(weather);
            _repository.UnitOfWork.Commit();
            return null;
        }

        public ActionResult Save(WeatherViewModel input)
        {
            var weather = input.Item.EntityId > 0 ? _repository.Find<Weather>(input.Item.EntityId) : new Weather();
            var newTask = mapToDomain(input, weather);

            var crudManager = _saveEntityService.ProcessSave(newTask);
            var notification = crudManager.Finish();
            return Json(notification, JsonRequestBehavior.AllowGet);
        }

        private Weather mapToDomain(WeatherViewModel input, Weather weather)
        {
            var weatherModel = input.Item;
            weather.DewPoint = weatherModel.DewPoint;
            weather.HighTemperature = weatherModel.HighTemperature;
            weather.Humidity = weatherModel.Humidity;
            weather.LowTemperature = weatherModel.LowTemperature; 
            weather.RainPrecipitation = weatherModel.RainPrecipitation;
            weather.WindSpeed = weatherModel.WindSpeed;
            return weather;
        }
    }

    public class WeatherViewModel:ViewModel
    {
        public Weather Item { get; set; }
    }
}