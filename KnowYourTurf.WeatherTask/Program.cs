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
    using System.Xml.Linq;

    public class Program
    {
        private static IRepository _repository;
        private static ILogger _logger;

        static void Main(string[] args)
        {
            Initialize();
            var arg = GetConnectionStringStatic("KnowYourTurf.sql_server_connection_string");
            var sessionFactoryConfiguration =
                   ObjectFactory.Container.With("connectionStr")
                                .EqualTo(arg)
                                .GetInstance<ISessionFactoryConfiguration>();
            ObjectFactory.Container.Inject(sessionFactoryConfiguration);
            _repository = ObjectFactory.Container.GetInstance<IRepository>("SpecialInterceptorNoFilters");
            _logger = ObjectFactory.Container.GetInstance<ILogger>();
//            GetWeatherTest();
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

        private static string GetConnectionStringStatic(string key)
        {
            // should work for production. please test
            var xdoc = XDocument.Load(@"..\..\appSettings.config");
//            var xdoc = XDocument.Load(@"..\..\..\appSettings.config");
            var connStrings = xdoc.Descendants("add").Where(x => x.Attribute("key").Value.Equals(key));
            return connStrings.FirstOrDefault().Attribute("value").Value;
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
            var companies = _repository.FindAll<Client>();
            var webClient = new WebClient();
            var jss = new JavaScriptSerializer();

            companies.ForEachItem(x =>
            {
                _logger.LogInfo(x.Name+", "+DateTime.Now.ToString());
                _logger.LogInfo(x.Name + ", Get Weather for Yesterday");
                loadWeatherObject(jss, webClient, x);
                _logger.LogInfo(x.Name + ", Recieved Weather for Yesterday");
                _logger.LogInfo(x.Name + ", Get Weather for Last Week");
                loadLastWeeksWeatherObject(jss, webClient, x);
                _logger.LogInfo(x.Name + ", Recieved Weather for Last Week");
            });
            _repository.UnitOfWork.Commit();
        }

        public static void GetWeatherTest()
        {
            var webClient = new WebClient();
            var jss = new JavaScriptSerializer();
            var x = _repository.Find<Client>(1);
            loadWeatherObject(jss, webClient, x);
//            loadLastWeeksWeatherObject(jss, webClient, x);
            _repository.UnitOfWork.Commit();
        }


        private static void loadWeatherObject(JavaScriptSerializer jss, WebClient webClient, Client client)
        {
            var date = DateTime.Now.Date.AddDays(-1);
            var url = "http://api.wunderground.com/api/8c25a57f987344bd/history_" + date.ToString("yyyyMMd") + "/q/" + client.ZipCode + ".json";
            var weather = _repository.Query<Weather>(x => x.Date == date && x.ClientId == client.EntityId).FirstOrDefault() ??
                          new Weather { ClientId = client.EntityId, Date = date };
            loadWeather(jss, webClient, weather, url);
        }
        private static void loadLastWeeksWeatherObject(JavaScriptSerializer jss, WebClient webClient, Client client)
        {
            var date = DateTime.Now.Date;
            for (int i = 1; i <= 7; i++)
            {
                date = date.AddDays(-1);
                var url = "http://api.wunderground.com/api/8c25a57f987344bd/history_" + date.ToString("yyyyMMd") + "/q/" +
                          client.ZipCode + ".json";
                var weather =
                    _repository.Query<Weather>(x => x.Date == date && x.ClientId == client.EntityId).FirstOrDefault() ??
                    new Weather {ClientId = client.EntityId, Date = date};
                loadWeather(jss, webClient, weather, url);
            }
        }

        private static void loadWeather(JavaScriptSerializer jss, WebClient webClient, Weather weather, string url)
        {
            _logger.LogInfo("Client Id: " + weather.ClientId + ", url: " + url + ", Time: " + DateTime.Now.ToString());
            var result = webClient.DownloadString(url);
            _logger.LogInfo("Client Id: " + weather.ClientId + ", Result Has Value: " + result.IsNotEmpty() + ", Time: " + DateTime.Now.ToString());
            _logger.LogInfo("Result: " + result);
            _logger.LogInfo("jss is null: " + (jss==null));
            Thread.Sleep(10000);
            if (result.IsEmpty()) return;
            var clientWeatherInfoDto = jss.Deserialize<ClientWeatherInfoDto>(result);
            if (clientWeatherInfoDto == null || clientWeatherInfoDto.History == null || clientWeatherInfoDto.History.DailySummary == null) return;
            _logger.LogInfo("Daily Summary: " + clientWeatherInfoDto.History.DailySummary.FirstOrDefault());

            var dewPoint = 0d;
            var maxTemp = 0d;
            var minTemp = 0d;
            var precip = 0d;
            var maxWindGust = 0d;
            var humidity = 0d;
            var meanPressure = 0d;
            var dailySummary = clientWeatherInfoDto.History.DailySummary.FirstOrDefault();
            Double.TryParse(dailySummary.maxdewpti, out dewPoint);
            Double.TryParse(dailySummary.maxtempi, out maxTemp);
            Double.TryParse(dailySummary.mintempi, out minTemp);
            Double.TryParse(dailySummary.precipi, out precip);
            Double.TryParse(dailySummary.maxwspdi, out maxWindGust);
            Double.TryParse(dailySummary.maxhumidity, out humidity);
            Double.TryParse(dailySummary.meanpressurei, out meanPressure);
            weather.WindDirection = dailySummary.wdire;
            weather.DewPoint = dewPoint;
            weather.HighTemperature = maxTemp;
            weather.LowTemperature = minTemp;
            weather.RainPrecipitation = precip;
            weather.WindSpeed = maxWindGust;
            weather.Humidity = humidity;
            weather.Pressure = meanPressure;
            _repository.Save(weather);
            _logger.LogInfo("Client Id: " + weather.ClientId + ", Weather Saved, Time: " + DateTime.Now.ToString());

        }

    }

    public class ClientWeatherInfoDto
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
        public string wdire { get; set; }
    }

}
