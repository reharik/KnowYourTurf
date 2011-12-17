using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Enumerations;
using HtmlTags;
using Rhino.Security.Interfaces;

namespace KnowYourTurf.Core.Html.Grid
{
    public class ImageButtonColumn<ENTITY> : ImageColumn<ENTITY> where ENTITY : IGridEnabledClass
    {
        private string _actionUrl;
        public string ActionUrl
        {
            get { return _actionUrl; }
            set { _actionUrl = value; }
        }
        private string _action;
        public ImageButtonColumn<ENTITY> ForAction<CONTROLLER>(Expression<Func<CONTROLLER, object>> expression, AreaName area = null) where CONTROLLER : Controller
        {
            var actionUrl = UrlContext.GetUrlForAction(expression,area);
            _actionUrl = actionUrl;
            return this;
        }

        public ImageButtonColumn<ENTITY> ToPerformAction(ColumnAction action)
        {
            _action = action.ToString();
            return this;
        }

        public override HtmlTag BuildColumn(object item, User user, IAuthorizationService _authorizationService)
        {
            var _item = (ENTITY) item;
            var value = FormatValue(_item, user, _authorizationService);
            if (value.Text().IsEmpty()) return null;
            var divTag = BuildDiv();
            var anchor = buildAnchor(_item);
            var image = BuildImage();
            divTag.Children.Add(image);
            anchor.Children.Add(divTag);
            return anchor;
        }

        private HtmlTag buildAnchor(ENTITY item)
        {
            var anchor = new HtmlTag("a");
            anchor.Attr("onclick",
                        "$.publish('/contentLevel/grid/" + _action + "',['" + _actionUrl + "/" + item.EntityId + "']);");
            return anchor;
        }
    }
}