using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using KnowYourTurf.Security.Interfaces;
using HtmlTags;
using KnowYourTurf.Core.Domain;

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
        private string _jsonData;
        private string _gridName;
        public ImageButtonColumn<ENTITY> ForAction<CONTROLLER>(Expression<Func<CONTROLLER, object>> expression, string gridName = "") where CONTROLLER : Controller
        {
            _gridName = gridName;
            var actionUrl = UrlContext.GetUrlForAction(expression);
            _actionUrl = actionUrl;
            return this;
        }

        public ImageButtonColumn<ENTITY> ToPerformAction(ColumnAction action)
        {
            _action = action.ToString();
            return this;
        }

         public override string BuildColumn(object item, User user, IAuthorizationService _authorizationService, string gridName = "")
        {
            // if a name is given in the controller it overrides the name given in the grid declaration
            if (gridName.IsNotEmpty()) _gridName = gridName;
            var _item = (ENTITY)item;
            var value = FormatValue(_item, user, _authorizationService);
            if (value.IsEmpty()) return null;
            var divTag = BuildDiv();
            divTag.AddClasses(new[] { "imageButtonColumn", _action });
            var anchor = buildAnchor(_item);
            var image = BuildImage();
             divTag.Children.Add(image);
            anchor.Children.Add(divTag);
            return anchor.ToString();
        }
        
        private HtmlTag buildAnchor(ENTITY item)
        {
            var anchor = new HtmlTag("a");
            string data = string.Empty;
            if (_jsonData.IsNotEmpty())
            {
                data = "," + _jsonData;
            }
            anchor.Attr("onclick", "KYT.vent.trigger('" + _action + "'," + item.EntityId + data+ ")");

            return anchor;
        }

        public void AddDataToEvent(string jsonObject)
        {
            _jsonData = jsonObject;
        }
    }
}
