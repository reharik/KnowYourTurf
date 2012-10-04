using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using CC.Core.Utilities;
using KnowYourTurf.Core.Html.Expressions;

namespace KnowYourTurf.Core.Html
{
    public static class HtmlExtensions
    {
        public static MetaExpression MetaTag(this ViewPage viewPage)
        {
            return new MetaExpression();
        }

        public static LinkExpression LinkTag<T>(this ViewPage<T> viewPage)
        {
            return new LinkExpression();
        }

        public static LinkExpression CSS(this WebViewPage viewPage, string url)
        {
            return new LinkExpression().Href(url).AsStyleSheet();
        }

        public static LinkExpression SiteCSS(this ViewMasterPage viewMasterPage, string url)
        {
            return new LinkExpression().Href(url).AsStyleSheet();
        }

        public static ScriptReferenceExpression Script(this ViewPage viewPage, string url)
        {
            return new ScriptReferenceExpression(url);
        }

        public static ScriptReferenceExpression SiteScript(this ViewMasterPage viewMasterPage, string url)
        {
            return new ScriptReferenceExpression(url);
        }

        public static string ActionUrl<CONTROLLER>(this ViewPage viewPage, Expression<Func<CONTROLLER, object>> actionExpression)
            where CONTROLLER : class
        {
            var accessor = ReflectionHelper.GetMethod(actionExpression);
            var action = accessor.Name;
            var controller = accessor.DeclaringType.Name.Replace("Controller", "");
            return ("~/" + controller + "/" + action).ToFullUrl();
        }

        public static string ActionUrl<CONTROLLER>(this ViewUserControl viewPage, Expression<Func<CONTROLLER, object>> actionExpression)
            where CONTROLLER : class
        {
            var accessor = ReflectionHelper.GetMethod(actionExpression);
            var action = accessor.Name;
            var controller = accessor.DeclaringType.Name.Replace("Controller", "");
            return ("~/" + controller + "/" + action).ToFullUrl();
        }

        public static FormExpression FormFor<CONTROLLER>(this ViewPage view, Expression<Func<CONTROLLER, object>> actionExpression)
            where CONTROLLER : class
        {
            var actionUrl = ActionUrl(view, actionExpression);
            return new FormExpression(actionUrl);
        }

        public static FormExpression FormFor<CONTROLLER>(this ViewUserControl view, Expression<Func<CONTROLLER, object>> actionExpression)
            where CONTROLLER : class
        {
            var actionUrl = ActionUrl(view, actionExpression);
            return new FormExpression(actionUrl);
        }

        // TODO: Just a temp work around to get the form to use the correct Url
        public static FormExpression FormFor(this ViewPage view, string actionUrl)
        {
            return new FormExpression(actionUrl);
        }

        public static StyledButtonExpression StyledButtonFor(this ViewPage viewPage, string name)
        {
            return new StyledButtonExpression(name).ElementId(name);
        }

        public static StyledButtonExpression StyledButtonFor(this ViewMasterPage viewPage, string name)
        {
            return new StyledButtonExpression(name).ElementId(name);
        }
        
        public static StyledButtonExpression StyledButtonFor(this ViewUserControl viewUserControl, string name)
        {
            return new StyledButtonExpression(name).ElementId(name);
        }
        
        public static SubmitButtonExpression SubmitButton(this ViewPage viewPage, string value, string name)
        {
            return new SubmitButtonExpression(value, name);
        }

        public static string EndForm(this ViewPage view)
        {
            return "</form>";
        }

        
    }
}