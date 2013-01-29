namespace KnowYourTurf.Core.RouteTokens
{
    using System;
    using System.Linq.Expressions;
    using System.Web.Mvc;
    using System.Web.Script.Serialization;

    using CC.Core.Html;

    using KnowYourTurf.Core.Enums;

    public class RouteToken
    {
        public string url { get; set; }
        public string viewName { get; set; }
        public string subViewName { get; set; }
        public string id { get; set; }
        public string route { get; set; }
        public string addUpdate { get; set; }
        public string display { get; set; }
        public bool isChild { get; set; }
        public bool noBubbleUp { get; set; }
        [ScriptIgnore]
        public string Operation { get; set; }

        public string itemName { get; set; }
        public bool noModel { get; set; }
        public bool noTemplate { get; set; }
        public string subViewRoute { get; set; }
        public string gridId { get; set; }

        public void CreateUrl<CONTROLLER>(Expression<Func<CONTROLLER, object>> expression, AreaName areaName = null) where CONTROLLER : Controller
        {
            this.url = UrlContext.GetUrlForAction(expression, areaName);
        }

    }


}