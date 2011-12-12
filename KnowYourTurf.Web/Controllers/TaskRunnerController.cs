using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using KnowYourTurf.Core;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Html;
using System.Linq;
using KnowYourTurf.Core.Services;
using KnowYourTurf.Web.Services.EmailHandlers;
using StructureMap;

namespace KnowYourTurf.Web.Controllers
{
    public class TaskRunnerController : Controller
    {
        private readonly IEmailTemplateService _emailService;
        private readonly IRepository _repository;

        public TaskRunnerController( IEmailTemplateService emailService)
        {
            _emailService = emailService;
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
            var url = "http://api.wunderground.com/api/8c25a57f987344bd/yesterday/q/"+ item.ZipCode+".json";
            var result = webClient.DownloadString(url);
            if (result.IsEmpty()) return;
            var companyWeatherInfoDto = jss.Deserialize<CompanyWeatherInfoDto>(result);
            if (companyWeatherInfoDto == null || companyWeatherInfoDto.History==null || companyWeatherInfoDto.History.DailySummary==null) return;
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
            var humidity = 0d;
            var dailySummary = companyWeatherInfoDto.History.DailySummary.FirstOrDefault();
            Double.TryParse(dailySummary.maxdewpti, out dewPoint);
            Double.TryParse(dailySummary.maxtempi, out maxTemp);
            Double.TryParse(dailySummary.mintempi, out minTemp);
            Double.TryParse(dailySummary.rain, out precip);
            Double.TryParse(dailySummary.maxwspdi, out maxWindGust);
            Double.TryParse(dailySummary.maxhumidity, out humidity);
            weather.DewPoint = dewPoint;
            weather.HighTemperature = maxTemp;
            weather.LowTemperature = minTemp;
            weather.RainPrecipitation = precip;
            weather.WindSpeed = maxWindGust;
            weather.Humidity = humidity;
            _repository.Save(weather);
        }

        public ActionResult ProcessEmail(ViewModel input)
        {
            var notification = new Notification { Success = true };
            var emailJob = _repository.Find<EmailJob>(input.EntityId);
            var emailTemplateHandler = ObjectFactory.Container.GetInstance<IEmailTemplateHandler>(emailJob.Name + "Handler");
            try
            {
                emailJob.GetSubscribers().Each(x =>
                {
                    var model = emailTemplateHandler.CreateModel(emailJob, x);
                    _emailService.SendSingleEmail(model);
                });
            }
            catch (Exception ex)
            {
                notification.Success = false;
                notification.Message = ex.Message;
            }
            return Json(notification, JsonRequestBehavior.AllowGet);
        }
    }

    public class CompanyWeatherInfoDto
    {
        public WeatherHistory History { get; set; }
    }

    public class WeatherHistory
    {
        public IEnumerable<DailySummary> DailySummary { get; set; }
    }

    public class DailySummary
    {
        public string rain { get; set; }
        public string maxtempi { get; set; }
        public string mintempi { get; set; }
        public string maxhumidity { get; set; }
        public string maxdewpti { get; set; }
        public string maxwspdi { get; set; }
    }

}