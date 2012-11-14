using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Web.Script.Serialization;
using CC.Core;
using CC.Core.DomainTools;
using KnowYourTurf.Core;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Web;
using KnowYourTurf.Web.Controllers;
using StructureMap;
using log4net.Config;

namespace KnowYourTurf.WeatherTask
{
    public class Program
    {
        private static IRepository _repository;
        private static ILogger _logger;

        static void Main(string[] args)
        {
            Initialize();

            _repository = ObjectFactory.Container.GetInstance<IRepository>("SpecialInterceptorNoFilters");
            _logger = ObjectFactory.Container.GetInstance<ILogger>();
            GetWeather();
        }

        private static void Initialize()
        {
            ObjectFactory.Initialize(x =>
            {
                x.AddRegistry(new KYTWebRegistry());
            });
            XmlConfigurator.ConfigureAndWatch(new FileInfo(locateFileAsAbsolutePath("log4net.config")));

        }

        private static string locateFileAsAbsolutePath(string filename)
        {
            if (Path.IsPathRooted(filename))
                return filename;
            string applicationBase = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            string path = Path.Combine(applicationBase, filename);
            if (!File.Exists(path))
            {
                path = Path.Combine(Path.Combine(applicationBase, "bin"), filename);
                if (!File.Exists(path))
                    path = Path.Combine(Path.Combine(applicationBase, ".."), filename);
            }
            return path;
        }

        public static void GetWeather()
        {
            var companies = _repository.FindAll<Company>();
            var webClient = new WebClient();
            var jss = new JavaScriptSerializer();

            companies.ForEachItem(x =>
            {
                _logger.LogInfo(x.Name+", "+DateTime.Now.ToString());
                loadWeatherObject(jss, webClient, x);
                loadLastWeeksWeatherObject(jss, webClient, x);
            });
            _repository.UnitOfWork.Commit();
        }

        private static void loadWeatherObject(JavaScriptSerializer jss, WebClient webClient, Company company)
        {
            var url = "http://api.wunderground.com/api/8c25a57f987344bd/yesterday/q/" + company.ZipCode + ".json";
            var date = DateTime.Now.Date.AddDays(-1);
            var weather = _repository.Query<Weather>(x => x.Date == date && x.CompanyId == company.EntityId).FirstOrDefault() ??
                          new Weather { CompanyId = company.EntityId, Date = date };
            loadWeather(jss, webClient, weather, url);
        }
        private static void loadLastWeeksWeatherObject(JavaScriptSerializer jss, WebClient webClient, Company company)
        {
            var date = DateTime.Now.Date;
            var url = "http://api.wunderground.com/api/8c25a57f987344bd/history_" + date.ToString("yyyyMMd") + "/q/" + company.ZipCode + ".json";
            var weather = _repository.Query<Weather>(x => x.Date == date && x.CompanyId == company.EntityId).FirstOrDefault() ??
                          new Weather { CompanyId = company.EntityId, Date = date };
            loadWeather(jss, webClient, weather, url);
        }

        private static void loadWeather(JavaScriptSerializer jss, WebClient webClient, Weather weather, string url)
        {
            var result = webClient.DownloadString(url);
            Thread.Sleep(10000);
            if (result.IsEmpty()) return;
            var companyWeatherInfoDto = jss.Deserialize<CompanyWeatherInfoDto>(result);
            if (companyWeatherInfoDto == null || companyWeatherInfoDto.History == null || companyWeatherInfoDto.History.DailySummary == null) return;
            var dewPoint = 0d;
            var maxTemp = 0d;
            var minTemp = 0d;
            var precip = 0d;
            var maxWindGust = 0d;
            var humidity = 0d;
            var meanPressure = 0d;
            var dailySummary = companyWeatherInfoDto.History.DailySummary.FirstOrDefault();
            Double.TryParse(dailySummary.maxdewpti, out dewPoint);
            Double.TryParse(dailySummary.maxtempi, out maxTemp);
            Double.TryParse(dailySummary.mintempi, out minTemp);
            Double.TryParse(dailySummary.precipi, out precip);
            Double.TryParse(dailySummary.maxwspdi, out maxWindGust);
            Double.TryParse(dailySummary.maxhumidity, out humidity);
            Double.TryParse(dailySummary.meanpressurei, out meanPressure);
            weather.DewPoint = dewPoint;
            weather.HighTemperature = maxTemp;
            weather.LowTemperature = minTemp;
            weather.RainPrecipitation = precip;
            weather.WindSpeed = maxWindGust;
            weather.Humidity = humidity;
            weather.Pressure = meanPressure;
            _repository.Save(weather);
        }

    }

    public class CompanyWeatherInfoDto
    {
        public WeatherHistoryDto History { get; set; }
    }

    public class WeatherHistoryDto
    {
        public IEnumerable<DailySummaryDto> DailySummary { get; set; }
    }

    public class DailySummaryDto
    {
        public string precipi { get; set; }
        public string maxtempi { get; set; }
        public string mintempi { get; set; }
        public string maxhumidity { get; set; }
        public string maxdewpti { get; set; }
        public string maxwspdi { get; set; }
        public string meanpressurei { get; set; }
    }

}
