using System;
using CC.Core.Domain;

namespace KnowYourTurf.Core.Domain
{
    public class Weather : DomainEntity, IPersistableObject
    {
        public virtual DateTime Date { get; set; }
        public virtual double HighTemperature { get; set; }
        public virtual double LowTemperature { get; set; }
        public virtual double WindSpeed { get; set; }
        public virtual string WindDirection { get; set; }
        public virtual double RainPrecipitation { get; set; }
        public virtual double Humidity { get; set; }
        public virtual double DewPoint { get; set; }
        public virtual double Pressure { get; set; }
    }
}