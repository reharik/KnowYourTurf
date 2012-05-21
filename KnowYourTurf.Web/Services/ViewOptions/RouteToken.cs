using System;
using System.Linq.Expressions;
using System.Web.Script.Serialization;
using KnowYourTurf.Core.Enums;
using KnowYourTurf.Core.Html;
using KnowYourTurf.Web.Controllers;

namespace KnowYourTurf.Web.Services.ViewOptions
{
    public class RouteToken
    {
        public string url { get; set; }
        public string viewName { get; set; }
        public string subViewName { get; set; }
        public string id { get; set; }
        public string route { get; set; }
        public string addUpate { get; set; }
        public string display { get; set; }
        public bool isChild { get; set; }
        public bool noBubbleUp { get; set; }
        [ScriptIgnore]
        public string Operation { get; set; }

        public void CreateUrl<CONTROLLER>(Expression<Func<CONTROLLER, object>> expression, AreaName areaName = null) where CONTROLLER : KYTController
        {
            url = UrlContext.GetUrlForAction(expression, areaName);
        }

    }


}