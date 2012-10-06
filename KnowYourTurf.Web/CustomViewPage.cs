using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;
using CC.Core;
using CC.Core.Html.Menu;
using CC.Core.Localization;
using CC.Core.Utilities;
using KnowYourTurf.Core.Enums;
using KnowYourTurf.Core.Html.Expressions;

namespace KnowYourTurf.Web
{
    public abstract class CustomWebViewPage<T> : WebViewPage<T>
    {
        public static MetaExpression MetaTag()
        {
            return new MetaExpression();
        }

        public static LinkExpression LinkTag()
        {
            return new LinkExpression();
        }

        public static LinkExpression CSS(string url)
        {
            return new LinkExpression().Href(url).AsStyleSheet();
        }

        public static ScriptReferenceExpression Script(string url)
        {
            return new ScriptReferenceExpression(url);
        }

        public static ImageExpression ImageExpression(string url)
        {
            return new ImageExpression(url);
        }

        public static string ActionUrl<CONTROLLER>(Expression<Func<CONTROLLER, object>> actionExpression, AreaName area = null)
            where CONTROLLER : class
        {
            var accessor = ReflectionHelper.GetMethod(actionExpression);
            var action = accessor.Name;
            var controller = accessor.DeclaringType.Name.Replace("Controller", "");
            var areaName = area != null ? area.Key + "/" : "";

            return ("~/" + areaName + controller + "/" + action).ToFullUrl();
        }

        public static FormExpression FormFor<CONTROLLER>(Expression<Func<CONTROLLER, object>> actionExpression, AreaName area = null)
            where CONTROLLER : class
        {
            var actionUrl = ActionUrl(actionExpression, area);
            return new FormExpression(actionUrl);
        }

        public static FormExpression FormFor(string actionUrl)
        {
            return new FormExpression(actionUrl);
        }

        public static StandardButtonExpression StandardButtonFor(string name, string value)
        {
            return new StandardButtonExpression(name).NonLocalizedText(value);
        }

        public static StandardButtonExpression StandardButtonFor(string name, StringToken text)
        {
            return new StandardButtonExpression(name).LocalizedText(text);
        }

        public MvcHtmlString EndForm()
        {
            return MvcHtmlString.Create("</form>");
        }

        public static MenuExpression MenuItems(IList<MenuItem> items)
        {
            return new MenuExpression(items);
        }

     
    }
}