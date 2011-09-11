using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using KnowYourTurf.Core;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Html;

namespace KnowYourTurf.Web.Controllers
{
    public class TaskRunnerController : Controller
    {
        private readonly IRepository _repository;

        public TaskRunnerController()
        {
            _repository = new Repository();
        }

        public ActionResult GetWeather(ViewModel input)
        {
            var companies = _repository.FindAll<Company>();
            var webClient = new WebClient();
            var jss = new JavaScriptSerializer();

            companies.Each(x => loadWeatherObject(jss, webClient, x));
            _repository.UnitOfWork.Commit();
            return null;
        }

        private void loadWeatherObject(JavaScriptSerializer jss, WebClient webClient, Company item)
        {
            var url = "http://www.climate.gov/cwaw/GSODLookup";
            webClient.QueryString = new NameValueCollection
                                        {
                                            {"lat", item.Latitude},
                                            {"lng", item.Longitude},
                                            {"date", DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd")},
                                        };
            var result = webClient.DownloadString(url);
            if (result.IsEmpty()) return;
            var companyWeatherInfoDto = jss.Deserialize<CompanyWeatherInfoDto>(result);
            if (companyWeatherInfoDto == null || companyWeatherInfoDto.values==null) return;
            var weather = new Weather
                              {
                                  CompanyId = item.EntityId,
                                  Date = DateTime.Now.AddDays(-1),
                              };
            var dewPoint = 0d;
            var maxTemp = 0d;
            var minTemp = 0d;
            var precip = 0d;
            var maxWindGust = 0d;
            Double.TryParse(companyWeatherInfoDto.values.dewPoint, out dewPoint);
            Double.TryParse(companyWeatherInfoDto.values.maxTemp, out maxTemp);
            Double.TryParse(companyWeatherInfoDto.values.minTemp, out minTemp);
            Double.TryParse(companyWeatherInfoDto.values.precip, out precip);
            Double.TryParse(companyWeatherInfoDto.values.maxWindGust, out maxWindGust);
            weather.DewPoint = dewPoint;
            weather.HighTemperature = maxTemp;
            weather.LowTemperature = minTemp;
            weather.RainPrecipitation = precip;
            weather.WindSpeed = maxWindGust;
            _repository.Save(weather);
        }
    }

    public class CompanyWeatherInfoDto
    {
        public WeatherInfoDto values { get; set; }
    }

    public class WeatherInfoDto
    {
        public string maxTemp { get; set; }
        public string maxWindGust { get; set; }
        public string precip { get; set; }
        public string minTemp { get; set; }
        public string dewPoint { get; set; }
    }

}