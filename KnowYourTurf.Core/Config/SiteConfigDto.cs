using System;
using System.Configuration;
using DecisionCritical.Core.Domain;

namespace DecisionCritical.Core.Config
{
    [Serializable]
    public class SiteConfigDto
    {
        public SiteConfigDto()
        {


            // Defaults
            themeDefault = "default";
            scriptsPath = "~/content/scripts/";
            cssFilePath = "~/content/Css/";
            imagesPath = "~/content/images/";
        }

        public int id { get; set; }
        public string name { get; set; }
        public string host { get; set; }
        public string scriptsPath { get; set; }
        public string cssFilePath { get; set; }
        public string imagesPath { get; set; }
        public string languageDefault { get; set; }
        public string themeDefault { get; set; }
        public string webSiteRoot { get; set; }

        public void ToSiteConfiguration(SiteConfiguration config)
        {
            config.EntityId = id;
            config.Name = name;
            config.Host = host;
            config.LanguageDefault = languageDefault;
            config.ScriptsPath = scriptsPath;
            config.CssPath = cssFilePath;
            config.ImagesPath = imagesPath;
            config.WebSiteRoot = webSiteRoot;
        }
    }
}
