using System;
using System.Configuration;
using System.Web.Script.Serialization;
using CC.Core.Services;

namespace KnowYourTurf.Core.Config
{
    public class SiteConfiguration : SiteConfigurationBase
    {
        public virtual string NumberOfFieldsPerCategory { get; set; }
        public virtual string CustomerPhotosEmployeePath { get; set; }
        public virtual string CustomerPhotosFacilitiesPath { get; set; }
    }

    public static class SiteConfig
    {
        public static SiteConfiguration Settings()
        {
            var appSetting = ConfigurationSettings.AppSettings["KnowYourTurf.SiteConfiguration"];
            var jss = new JavaScriptSerializer();
            var siteConfiguration = jss.Deserialize<SiteConfiguration>(appSetting);
            return siteConfiguration;
        }
    }

    public class InjectableSiteConfig : IInjectableSiteConfig
    {
        public SiteConfigurationBase Settings()
        {
            return SiteConfig.Settings();
        }
    }

}