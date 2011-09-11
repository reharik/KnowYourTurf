using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;
using FubuMVC.Core.Util;
using KnowYourTurf.Core;
using KnowYourTurf.Core.Html.Expressions;
using KnowYourTurf.Core.Html.Menu;
using KnowYourTurf.Core.Localization;
using KnowYourTurf.Core.Services;

namespace KYT.Web
{
    public abstract class CustomWebViewPage<T> : WebViewPage<T>
    {
        public static MetaExpression MetaTag()
        {
            return new MetaExpression();
        }

        public static LinkExpression LinkTag<T>()
        {
            return new LinkExpression();
        }

        public static LinkExpression CSS(string url)
        {
            return new LinkExpression().Href(url).AsStyleSheet();
        }

        public static ScriptReferenceExpression Script(string url)
        {
            return new ScriptReferenceExpression().Add(url);
        }

        public static ScriptReferenceExpression Script(IEnumerable<string> scriptLinks)
        {
            var expr = new ScriptReferenceExpression();
            scriptLinks.Each(l => expr.Add(l));
            return expr;
        }

        public static string ActionUrl<CONTROLLER>(Expression<Func<CONTROLLER, object>> actionExpression)
            where CONTROLLER : class
        {
            var accessor = ReflectionHelper.GetMethod(actionExpression);
            var action = accessor.Name;
            var controller = accessor.DeclaringType.Name.Replace("Controller", "");
            return ("~/" + controller + "/" + action).ToFullUrl();
        }

        public static FormExpression FormFor<CONTROLLER>(Expression<Func<CONTROLLER, object>> actionExpression)
            where CONTROLLER : class
        {
            var actionUrl = ActionUrl(actionExpression);
            return new FormExpression(actionUrl);
        }

        // TODO: Just a temp work around to get the form to use the correct Url
        public static FormExpression FormFor(string actionUrl)
        {
            return new FormExpression(actionUrl.ToFullUrl());
        }

        public static StyledButtonExpression StyledButtonFor(string name, string value)
        {
            return new StyledButtonExpression(name).NonLocalizedText(value);
        }

        public static StyledButtonExpression StyledButtonFor(string name, StringToken text)
        {
            return new StyledButtonExpression(name).LocalizedText(text);
        }

        public static string EndForm()
        {
            return "</form>";
        }

        public static string MenuItems(IList<MenuItem> items)
        {
            return new MenuExpression(items).ToString();
        }

        public static SelectBoxPickerExpression SelectBoxPicker(SelectBoxPickerDto dto)
        {
            return new SelectBoxPickerExpression(dto);
        }
    }
}