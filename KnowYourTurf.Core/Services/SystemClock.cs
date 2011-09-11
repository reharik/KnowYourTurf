using System;

namespace KnowYourTurf.Core.Services
{
    public interface ISystemClock
    {
        DateTime Now { get; set; }
    }

    public class SystemClock : ISystemClock
    {
        private DateTime? _specificDate;

        public DateTime Now
        {
            get { return _specificDate ?? DateTime.Now; }
            set { _specificDate = value; }
        }

        public static SystemClock For(DateTime specificDate)
        {
            return new SystemClock { Now = specificDate };
        }
    }
}