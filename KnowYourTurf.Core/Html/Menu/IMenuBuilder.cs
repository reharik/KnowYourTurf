using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using FubuMVC.Core.Util;
using KnowYourTurf.Core.Localization;

namespace KnowYourTurf.Core.Html.Menu
{
    public interface IMenuBuilder
    {
        IList<MenuItem> MenuTree();
        IMenuBuilder HasChildren();
        IMenuBuilder EndChildren();
        IMenuBuilder CreateNode<CONTROLLER>(Expression<Func<CONTROLLER, object>> action, StringToken text) where CONTROLLER : Controller;
        IMenuBuilder CreateNode<CONTROLLER>(Expression<Func<CONTROLLER, object>> action, StringToken text, string urlParam) where CONTROLLER : Controller;
        IMenuBuilder CreateNode(StringToken text);
    }

    public class MenuBuilder : IMenuBuilder
    {
        private IList<MenuItem> _items = new List<MenuItem>();
        private IList<MenuItem> _parentItems = new List<MenuItem>();
        public IMenuBuilder HasChildren()
        {
            var _itemList = getList();
            var lastItem = _itemList.LastOrDefault();
            lastItem.Children = new List<MenuItem>();
            _parentItems.Add(lastItem);            
            return this;
        }

        public IMenuBuilder EndChildren()
        {
            var lastItem = _parentItems.LastOrDefault();
            _parentItems.Remove(lastItem);
            return this;
        }

        public IMenuBuilder CreateNode(StringToken text)
        {
            var _itemList = getList();
            _itemList.Add(new MenuItem
            {
                Text = text.DefaultValue,
                Url="#"
            });
            return this;
        }

        private IList<MenuItem> getList()
        {
            var lastParentItem = _parentItems.LastOrDefault();
            return lastParentItem != null ? lastParentItem.Children : _items;
        }

        public IMenuBuilder CreateNode<CONTROLLER>(Expression<Func<CONTROLLER, object>> action, StringToken text) where CONTROLLER : Controller
        {
            return CreateNode(action, text, "");
        }

        public IMenuBuilder CreateNode<CONTROLLER>(Expression<Func<CONTROLLER, object>> action, StringToken text, string urlParam) where CONTROLLER : Controller
        {
            string param;
            if (urlParam.Contains("="))
            {
                param = urlParam.IsNotEmpty() ? "?" + urlParam : "";
            }
            else
            {
                param = urlParam.IsNotEmpty() ? "/" + urlParam : "";
            }

            var _itemList = getList();
            _itemList.Add(new MenuItem
                           {
                               Text = text.DefaultValue,
                               Url = UrlContext.GetUrlForAction(action) + param
                           });
            return this;
        }

        public IList<MenuItem> MenuTree()
        {
            return _items;
        }
    }

    public class MenuItem
    {
        public string Text { get; set; }
        public string Url { get; set; }
        public IList<MenuItem> Children { get; set; }
    }
}