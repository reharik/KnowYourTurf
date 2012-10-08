

using System.Collections.Generic;
using FluentNHibernate.Utils;
using HtmlTags;
using KnowYourTurf.Core;
using KnowYourTurf.Core.Html.Expressions;
using KnowYourTurf.Core.Html.Menu;

namespace KnowYourTurf.Core.Html.Menu
{
    public class MenuExpression : HtmlTagExpressionBase
    {
        private readonly IList<MenuItem> _items;
        private bool _superFish;
        private bool _filGroup;
        private bool _vertical;

        public MenuExpression(IList<MenuItem> items)
            : base(new DivTag(""))
        {
            _items = items;
        }

        public override string ToString()
        {
            return ToHtmlTag().ToString();
        }

        public HtmlTag ToHtmlTag()
        {
            AddClass("entity-link-list");
            var rootTag = ToHtmlTagBase();
            var ul = new HtmlTag("ul");
            if (_filGroup)
            {
                ul.AddClasses(new[] { "ccMenu" });
            }
            else
            {
                ul.AddClasses(new[] { "main-tabs" });
                ul.Id("main-tabs");
            }
            renderListItems(ul, _items);
            rootTag.Children.Add(ul);
            return rootTag;
        }

        private void renderListItems(HtmlTag ul, IEnumerable<MenuItem> items)
        {
            if (_items == null) return;
            items.ForEachItem(x => ul.Children.Add(getLiItem(x)));
        }
        private HtmlTag getLiItem(MenuItem item)
        {
            if (item == null) return null;
            var li = new HtmlTag("li");
            if (item.CssClass.IsNotEmpty())
                li.AddClass(item.CssClass);
            var anchor = new HtmlTag("a");
            anchor.Attr("href", "#");
            anchor.Attr("rel", item.Url);
            anchor.Text(item.Text);
            li.Children.Add(anchor);
            if (item.Children != null)
            {
                var ul = new HtmlTag("ul");
                renderListItems(ul, item.Children);
                li.Children.Add(ul);
            }
            return li;
        }

        public MenuExpression SuperFish(bool vertical = false)
        {
            _superFish = true;
            _filGroup = false;
            _vertical = vertical;
            return this;
        }

        public MenuExpression filGroup()
        {
            _superFish = false;
            _filGroup = true;
            return this;
        }
    }
}