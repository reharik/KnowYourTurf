using System;
namespace KnowYourTurf.Core.Services
{
    public class DateTimeUtilities
    {
        public static string MilitaryToStandard(int military)
        {
            var hour = military.ToString().Substring(0, military.ToString().Length - 2);
            var minutes = military.ToString().Substring(military.ToString().Length - 2);
            var AmPm = military >= 1200 ? " PM" : " AM";
            var standard = Int32.Parse(hour) >= 13 ? (Int32.Parse(hour) - 12).ToString() : hour;
            standard += ":" + minutes + AmPm;
            return standard;
        }

        public static int StandardToMilitary(string standard)
        {
            var hour = standard.Substring(0, standard.Length - 6);
            var minutes = standard.Substring(standard.Length - 5, 2);
            var AmPm = standard.Substring(standard.Length - 2);
            var hourAsInt = Int32.Parse(hour) * 100;
            var military = AmPm == "PM" ? hourAsInt + 1200 : hourAsInt;
            military += Int32.Parse(minutes);
            return military;
        }

        public static DateTime ConvertFromUnixTimestamp(double timestamp)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return origin.AddSeconds(timestamp);
        }


        public static double ConvertToUnixTimestamp(DateTime date)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            TimeSpan diff = date - origin;
            return Math.Floor(diff.TotalSeconds);
        }
    }
}