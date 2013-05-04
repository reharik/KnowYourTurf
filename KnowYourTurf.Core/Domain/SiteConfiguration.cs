using System.Configuration;
using System.Web.Script.Serialization;

namespace KnowYourTurf.Core.Domain
{
    public class SiteConfiguration 
    {
        public virtual string Name { get; set; }
        public virtual string Host { get; set; }
        public virtual string KYTUrl { get; set; }
        public virtual string LanguageDefault { get; set; }
        public virtual string ScriptsPath { get; set; }
        public virtual string CssPath { get; set; }
        public virtual string ImagesPath { get; set; }
        public virtual string WebSiteRoot { get; set; }         
    }

    public static class SiteConfig
    {
        public static SiteConfiguration Settings()
        {
            var appSetting = ConfigurationSettings.AppSettings["SystemSupport.SiteConfiguration"];
            var jss = new JavaScriptSerializer();
            var siteConfiguration = jss.Deserialize<SiteConfiguration>(appSetting);
            return siteConfiguration;
        }
    }
}