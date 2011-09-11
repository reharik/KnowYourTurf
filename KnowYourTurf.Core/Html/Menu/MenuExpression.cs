using System;
using System.Collections.Generic;
using System.Text;
using KnowYourTurf.Core.Html.Expressions;

namespace KnowYourTurf.Core.Html.Menu
{
    public class MenuExpression : HtmlCommonExpressionBase
    {
        private readonly IList<MenuItem> _items;

        public MenuExpression(IList<MenuItem> items)
        {
            _items = items;
        }


        //public MenuExpression<MODEL, ITEMS> For(Func<MODEL, IList<ITEMS>> expression)
        //{
        //    _nodes = expression(_model);
        //    return this; 
        //}

        public override string ToString()
        {
            CssClasses.Add("entity-link-list");

            return @"<div{0}><ul class=""sf-menu sf-vertical"">{1}</ul></div>".ToFormat(GetHtmlAttributesString(),
                renderListItems(_items));
        }

        private string renderListItems(IEnumerable<MenuItem> items)
        {
            var stringBuilder = new StringBuilder("");
            if (items == null) return "";
            foreach (var t in items)
            {
                stringBuilder.AppendFormat(@"<li><a href=""{0}"">{1}</a>", t.Url, t.Text);
                if (t.Children != null)
                {
                    stringBuilder.AppendFormat("<ul>");
                    stringBuilder.AppendFormat(renderListItems((IEnumerable<MenuItem>) t.Children));
                    stringBuilder.AppendFormat("</ul>");

                }
                stringBuilder.AppendFormat("</li>");
            }
            return stringBuilder.ToString();
        }
    }
}