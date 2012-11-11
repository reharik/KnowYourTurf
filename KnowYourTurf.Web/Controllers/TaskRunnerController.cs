using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using CC.Core;
using CC.Core.CoreViewModelAndDTOs;
using CC.Core.DomainTools;
using FluentNHibernate.Utils;
using KnowYourTurf.Core;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Enums;
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

        public TaskRunnerController(IEmailTemplateService emailService)
        {
            _emailService = emailService;
            _repository = ObjectFactory.Container.GetInstance<IRepository>("SpecialInterceptorNoFilters");
        }

//        public ActionResult GetWeather(ViewModel input)
//        {
//            var companies = _repository.FindAll<Company>();
//            var webClient = new WebClient();
//            var jss = new JavaScriptSerializer();
//
//            companies.Each(x =>
//                               {
//                                   loadWeatherObject(jss, webClient, x);
//                                   loadLastWeeksWeatherObject(jss, webClient, x);
//                               });
//            _repository.UnitOfWork.Commit();
//            return null;
//        }
//
//        private void loadWeatherObject(JavaScriptSerializer jss, WebClient webClient, Company company)
//        {
//            var url = "http://api.wunderground.com/api/8c25a57f987344bd/yesterday/q/" + company.ZipCode + ".json";
//            var date = DateTime.Now.Date.AddDays(-1);
//            var weather = _repository.Query<Weather>(x => x.Date == date && x.CompanyId == company.EntityId).FirstOrDefault() ??
//                          new Weather { CompanyId = company.EntityId, Date = date };
//            loadWeather(jss,webClient,weather,url);
//        }
//        private void loadLastWeeksWeatherObject(JavaScriptSerializer jss, WebClient webClient, Company company)
//        {
//            var date = DateTime.Now.Date;
//            var url = "http://api.wunderground.com/api/8c25a57f987344bd/history_" + date.ToString("yyyyMMd") + "/q/" + company.ZipCode + ".json";
//            var weather = _repository.Query<Weather>(x => x.Date == date && x.CompanyId== company.EntityId).FirstOrDefault() ??
//                          new Weather { CompanyId = company.EntityId, Date = date };
//            loadWeather(jss, webClient, weather, url);
//        }
//
//        private void loadWeather(JavaScriptSerializer jss, WebClient webClient, Weather weather, string url)
//        {
//            var result = webClient.DownloadString(url);
//            Thread.Sleep(10000);
//            if (result.IsEmpty()) return;
//            var companyWeatherInfoDto = jss.Deserialize<CompanyWeatherInfoDto>(result);
//            if (companyWeatherInfoDto == null || companyWeatherInfoDto.History == null || companyWeatherInfoDto.History.DailySummary == null) return;
//            var dewPoint = 0d;
//            var maxTemp = 0d;
//            var minTemp = 0d;
//            var precip = 0d;
//            var maxWindGust = 0d;
//            var humidity = 0d;
//            var meanPressure = 0d;
//            var dailySummary = companyWeatherInfoDto.History.DailySummary.FirstOrDefault();
//            Double.TryParse(dailySummary.maxdewpti, out dewPoint);
//            Double.TryParse(dailySummary.maxtempi, out maxTemp);
//            Double.TryParse(dailySummary.mintempi, out minTemp);
//            Double.TryParse(dailySummary.precipi, out precip);
//            Double.TryParse(dailySummary.maxwspdi, out maxWindGust);
//            Double.TryParse(dailySummary.maxhumidity, out humidity);
//            Double.TryParse(dailySummary.meanpressurei, out meanPressure);
//            weather.DewPoint = dewPoint;
//            weather.HighTemperature = maxTemp;
//            weather.LowTemperature = minTemp;
//            weather.RainPrecipitation = precip;
//            weather.WindSpeed = maxWindGust;
//            weather.Humidity = humidity;
//            weather.Pressure = meanPressure;
//            _repository.Save(weather);
//        }
//
//        public ActionResult ProcessEmail(ViewModel input)
//        {
//            var notification = new Notification { Success = true };
//            var emailJobs = _repository.FindAll<EmailJob>();
//            emailJobs.Each(x =>
//                               {
//                                   if (x.Status == Status.Active.ToString() && (
//                                       x.Frequency == EmailFrequency.Daily.ToString()
//                                       || x.Frequency == EmailFrequency.Once.ToString()
//                                       || (x.Frequency == EmailFrequency.Weekly.ToString() && DateTime.Now.Day == 1)))
//                                   {
//                                       var emailTemplateHandler =
//                                           ObjectFactory.Container.GetInstance<IEmailTemplateHandler>(
//                                               x.EmailJobType.Name + "Handler");
//                                       try
//                                       {
//                                           x.Subscribers.Each(s =>
//                                                                  {
//                                                                      var model = emailTemplateHandler.CreateModel(x, s);
//                                                                      _emailService.SendSingleEmail(model);
//                                                                  });
//                                            if(x.Frequency == EmailFrequency.Once.ToString())
//                                            {
//                                                x.Status = Status.InActive.ToString();
//                                                _repository.Save(x);
//                                                _repository.Commit();
//                                            }
//                                       }
//                                       catch (Exception ex)
//                                       {
//                                           notification.Success = false;
//                                           notification.Message = ex.Message;
//                                       }
//                                   }
//                               });
//
//            return Json(notification, JsonRequestBehavior.AllowGet);
//        }
    }

   
}
