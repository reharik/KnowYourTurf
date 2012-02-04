using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using FubuMVC.Core.Util;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Localization;
using KnowYourTurf.Core.Services;
using Rhino.Security.Interfaces;

namespace KnowYourTurf.Core.Html.Menu
{
    public interface IMenuBuilder
    {
        IList<MenuItem> MenuTree(bool withoutPermissions = false);
        IMenuBuilder HasChildren();
        IMenuBuilder EndChildren();
        IMenuBuilder CreateNode<CONTROLLER>(Expression<Func<CONTROLLER, object>> action, StringToken text) where CONTROLLER : Controller;
        IMenuBuilder CreateNode<CONTROLLER>(Expression<Func<CONTROLLER, object>> action, StringToken text, string urlParam) where CONTROLLER : Controller;
        IMenuBuilder CreateNode(StringToken text, string url, string cssClass = null);
        IMenuBuilder CreateNode(StringToken text, string cssClass = null);

    }

    public class MenuBuilder : IMenuBuilder
    {
        private readonly IRepository _repository;
        private readonly IAuthorizationService _authorizationService;
        private readonly ISessionContext _sessionContext;

        public MenuBuilder(IRepository repository, IAuthorizationService authorizationService, ISessionContext sessionContext)
        {
            _repository = repository;
            _authorizationService = authorizationService;
            _sessionContext = sessionContext;
        }

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

        public IMenuBuilder CreateNode(StringToken text, string cssClass = null)
        {
            var _itemList = getList();
            _itemList.Add(new MenuItem
            {
                Text = text.DefaultValue,
                Url = "#",
                CssClass = cssClass,

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

        public IMenuBuilder CreateNode(StringToken text, string url, string cssClass = null)
        {
            var _itemList = getList();
            _itemList.Add(new MenuItem
            {
                Text = text.DefaultValue,
                Url = url
            });
            return this;
        }


        public IList<MenuItem> MenuTree(bool withoutPermissions = false)
        {
            if (withoutPermissions) return _items;
            IList<MenuItem> permittedItems =  modifyListForPermissions();
            return permittedItems;
        }

        private IList<MenuItem> modifyListForPermissions()
        {
            var permittedItems = new List<MenuItem>();
            var userId = _sessionContext.GetUserId();
            var user = _repository.Find<User>(userId);
            _items.Each(x =>
                            {
                                var operationName = "/MenuItem/"+x.Text.RemoveWhiteSpace();
                                if (_authorizationService.IsAllowed(user, operationName))
                                {
                                    permittedItems.Add(x);
                                }
                            });
            return permittedItems;
        }
    }

    public class MenuItem
    {
        public string Text { get; set; }
        public string Url { get; set; }
        public string CssClass { get; set; }
        public IList<MenuItem> Children { get; set; }
    }
}