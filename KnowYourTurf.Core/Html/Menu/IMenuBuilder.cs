using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using KnowYourTurf.Security.Interfaces;
using FubuMVC.Core;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Localization;
using KnowYourTurf.Core.Services;

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

        IMenuBuilder addUrlParameter(string name, string value);
        IMenuBuilder CategoryGroupForItteration();
        IMenuBuilder EndCategoryGroup();
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
        private IList<MenuItem> _categoryItems = new List<MenuItem>();
        private int index = 0;
        private int count = 0;
        private IList<Category> _categories;

        public IMenuBuilder HasChildren()
        {
            var _itemList = getList();
            var lastItem = _itemList.LastOrDefault();
            lastItem.Children = new List<MenuItem>();
            _parentItems.Add(lastItem);            
            return this;
        }
        public IMenuBuilder CategoryGroupForItteration()
        {
            var userId = _sessionContext.GetUserId();
            var user = _repository.Find<User>(userId);
            _categories = user.Company.Categories.ToList();
            count = _categories.Count;
            return this;
        }

        public IMenuBuilder EndCategoryGroup()
        {
            //this is so createnode can get the _list rather than the _categoryItems
            var categories = _categories;
            _categories = null;
            categories.Each(x =>
                                 {
                                     IList<MenuItem> parent = _items;
                                     if (count > 1)
                                     {
                                         CreateNode(x.Name);
                                         _items.LastOrDefault().Children = new List<MenuItem>();
                                         parent = _items.LastOrDefault().Children;

                                     }
                                     itterateOverCategoryItems(x.EntityId, _categoryItems, parent);
                                 });

            return this;
        }
        private void itterateOverCategoryItems(long entityId, IList<MenuItem> items, IList<MenuItem> parent = null)
        {
            items.Each(c =>
                           {
                               // this is so that c doesn't get modified each itteration
                               var instanceC = copyMenuItem(c);
                               if (instanceC.Url=="#"&& instanceC.Children.Any())
                               {
                                   // this is so we can loop through and give the parentid to the children,
                                   // however, we need to remove and readd the child since it's not a reference
                                   var children = instanceC.Children;
                                   instanceC.Children=new List<MenuItem>();
                                   itterateOverCategoryItems(entityId,children, instanceC.Children);
                               }
                               else
                               {
                                   instanceC.Url = instanceC.Url + "?ParentId=" + entityId;
                               }
                               if (parent != null) parent.Add(instanceC);
                           });
        }

        private MenuItem copyMenuItem(MenuItem menuItem)
        {
            return new MenuItem
                       {
                           Url = menuItem.Url,
                           Children = menuItem.Children,
                           CssClass = menuItem.CssClass,
                           Text = menuItem.Text
                       };
        }

        public IMenuBuilder EndChildren()
        {
            var lastItem = _parentItems.LastOrDefault();
            _parentItems.Remove(lastItem);
            return this;
        }

        public IMenuBuilder addUrlParameter(string name, string value)
        {
            var _itemList = getList();
            var lastItem = _itemList.LastOrDefault();
            lastItem.Url = lastItem.Url + "?" + name + "=" + value;
            return this;
        }
        public IMenuBuilder CreateNode(StringToken text, string cssClass = null)
        {
            return CreateNode(text.DefaultValue, cssClass);
        }

        public IMenuBuilder CreateNode(string text, string cssClass = null)
        {
            var _itemList = getList();
            _itemList.Add(new MenuItem
            {
                Text = text,
                Url = "#",
                CssClass = cssClass,

            });
            return this;
        }

        private IList<MenuItem> getList()
        {
            if (_categories != null && count > 0)
            {
                var lastCatParentItem = _parentItems.LastOrDefault();
                return lastCatParentItem != null ? lastCatParentItem.Children : _categoryItems;
            }
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

    public class GenMenuBuilder : IMenuBuilder
    {
        private readonly IRepository _repository;
        private readonly IAuthorizationService _authorizationService;
        private readonly ISessionContext _sessionContext;

        public GenMenuBuilder(IRepository repository, IAuthorizationService authorizationService, ISessionContext sessionContext)
        {
            _repository = repository;
            _authorizationService = authorizationService;
            _sessionContext = sessionContext;
        }

        private IList<MenuItem> _items = new List<MenuItem>();
        private IList<MenuItem> _parentItems = new List<MenuItem>();
        private IList<MenuItem> _categoryItems = new List<MenuItem>();
        private int index = 0;
        private int count = 0;
        private IList<Category> _categories;

        public IMenuBuilder HasChildren()
        {
            var _itemList = getList();
            var lastItem = _itemList.LastOrDefault();
            lastItem.Children = new List<MenuItem>();
            _parentItems.Add(lastItem);
            return this;
        }
        public IMenuBuilder CategoryGroupForItteration()
        {
            _categories = new[] {new Category {Name = "default", EntityId = 1}};
            count = _categories.Count;
            return this;
        }

        public IMenuBuilder EndCategoryGroup()
        {
            _categories.Each(x =>
            {
                IList<MenuItem> parent = _items;
                if (count > 1)
                {
                    CreateNode(x.Name);
                    parent = _items.LastOrDefault().Children;

                }
                itterateOverCategoryItems(x.EntityId, _categoryItems, parent);
            });

            return this;
        }
        private void itterateOverCategoryItems(long entityId, IList<MenuItem> items, IList<MenuItem> parent = null)
        {
            items.Each(c =>
            {
                if (c.Url.IsEmpty() && c.Children.Any())
                {
                    itterateOverCategoryItems(entityId, c.Children);
                }
                c.Url = c.Url + "?ParentId=" + entityId;
                if (parent != null) parent.Add(c);
            });
        }

        public IMenuBuilder EndChildren()
        {
            var lastItem = _parentItems.LastOrDefault();
            _parentItems.Remove(lastItem);
            return this;
        }

        public IMenuBuilder addUrlParameter(string name, string value)
        {
            var _itemList = getList();
            var lastItem = _itemList.LastOrDefault();
            lastItem.Url = lastItem.Url + "?" + name + "=" + value;
            return this;
        }
        public IMenuBuilder CreateNode(StringToken text, string cssClass = null)
        {
            return CreateNode(text.DefaultValue, cssClass);
        }

        public IMenuBuilder CreateNode(string text, string cssClass = null)
        {
            var _itemList = getList();
            _itemList.Add(new MenuItem
            {
                Text = text,
                Url = "#",
                CssClass = cssClass,

            });
            return this;
        }

        private IList<MenuItem> getList()
        {
            if (_categories != null && count > 1) { return _categoryItems; }
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
            IList<MenuItem> permittedItems = modifyListForPermissions();
            return permittedItems;
        }

        private IList<MenuItem> modifyListForPermissions()
        {
            var permittedItems = new List<MenuItem>();
            var userId = _sessionContext.GetUserId();
            var user = _repository.Find<User>(userId);
            _items.Each(x =>
            {
                var operationName = "/MenuItem/" + x.Text.RemoveWhiteSpace();
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