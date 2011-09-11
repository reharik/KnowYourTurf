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

        public ActionResult AddEdit(ViewModel input)
        {
            var weather = input.EntityId > 0 ? _repository.Find<Weather>(input.EntityId) : new Weather();
            var model = new WeatherViewModel
            {
                Weather = weather,
            };
            return PartialView("WeatherAddUpdate", model);
        }
      
        public ActionResult Display(ViewModel input)
        {
            var weather = _repository.Find<Weather>(input.EntityId);
            var model = new WeatherViewModel
                            {
                                Weather = weather,
                                AddEditUrl = UrlContext.GetUrlForAction<WeatherController>(x => x.AddEdit(null)) + "/" + weather.EntityId
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
            var weather = input.Weather.EntityId > 0 ? _repository.Find<Weather>(input.Weather.EntityId) : new Weather();
            var newTask = mapToDomain(input, weather);

            var crudManager = _saveEntityService.ProcessSave(newTask);
            var notification = crudManager.Finish();
            return Json(notification, JsonRequestBehavior.AllowGet);
        }

        private Weather mapToDomain(WeatherViewModel input, Weather weather)
        {
            var weatherModel = input.Weather;
            weather.DewPoint = weatherModel.DewPoint;
            weather.EvaporationRate = weatherModel.EvaporationRate;
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
        public Weather Weather { get; set; }
    }
}