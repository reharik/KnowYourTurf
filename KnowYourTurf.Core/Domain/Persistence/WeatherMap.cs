namespace KnowYourTurf.Core.Domain.Persistence
{
    public class WeatherMap: DomainEntityMap<Weather>
    {
        public WeatherMap()
        {
            Map(x => x.Date);
            Map(x => x.HighTemperature);
            Map(x => x.LowTemperature);
            Map(x => x.WindSpeed);
            Map(x => x.RainPrecipitation);
            Map(x => x.Humidity);
            Map(x => x.DewPoint);
        }
    }
}