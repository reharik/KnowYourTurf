using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using CC.Core;
using CC.Core.DomainTools;
using CC.Core.Enumerations;
using CC.Core.Html.Menu;
using CC.Core.Localization;
using CC.Security;
using CC.Security.Interfaces;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Services;

namespace KnowYourTurf.Core.Html.Menu
{
    public interface IKYTMenuBuilder : IMenuBuilder
    {
        IKYTMenuBuilder CreateNode(StringToken text, string url, string cssClass = null);
        IKYTMenuBuilder addUrlParameter(string name, string value);
        IKYTMenuBuilder SiteGroupForIteration();
        IKYTMenuBuilder EndCategoryGroup();
        IKYTMenuBuilder HasChildren();
        IKYTMenuBuilder EndChildren();
        IKYTMenuBuilder CreateNode<CONTROLLER>(string urlPreface, Expression<Func<CONTROLLER, object>> action, StringToken text, AreaName areaName = null, string cssClass = null) where CONTROLLER : Controller;
        IKYTMenuBuilder CreateNode<CONTROLLER>(Expression<Func<CONTROLLER, object>> action, StringToken text, AreaName areaName = null, string cssClass = null) where CONTROLLER : Controller;
        IKYTMenuBuilder CreateNode<CONTROLLER>(Expression<Func<CONTROLLER, object>> action, StringToken text, string urlParam, AreaName areaName = null, string cssClass = null) where CONTROLLER : Controller;
        IKYTMenuBuilder CreateNode(StringToken text, string cssClass = null);
        IKYTMenuBuilder CreateTagNode<CONTROLLER>(StringToken text) where CONTROLLER : Controller;
        IKYTMenuBuilder Route(string route);
    }
    public class KYTMenuBuilder : MenuBuilder, IKYTMenuBuilder
    {
        private readonly IRepository _repository;
        private readonly ISessionContext _sessionContext;

        public KYTMenuBuilder(IRepository repository,
            IAuthorizationService authorizationService,
            ISessionContext sessionContext)
            : base(authorizationService)
        {
            _repository = repository;
            _sessionContext = sessionContext;
        }

        protected IList<MenuItem> _siteItems = new List<MenuItem>();
        protected int count = 0;
        protected IList<Site> _sites;

        public virtual IKYTMenuBuilder SiteGroupForIteration()
        {
            var user = (User)_sessionContext.GetCurrentUser();
            _sites = user.Client.Sites.ToList();
            count = _sites.Count;
            return this;
        }

        public IKYTMenuBuilder EndCategoryGroup()
        {
            //this is so createnode can get the _list rather than the _siteItems
            var sites = _sites;
            _sites = null;
            sites.ForEachItem(x =>
                                 {
                                     IList<MenuItem> parent = _items;
                                     if (count > 1)
                                     {
                                         CreateNode(x.Name,x.SiteOperation);
                                         _items.LastOrDefault().Children = new List<MenuItem>();
                                         parent = _items.LastOrDefault().Children;

                                     }
                                     itterateOverSiteItems(x.EntityId, _siteItems, parent);
                                 });

            return this;
        }
        public IMenuBuilder CreateNode(string text, string operation = null, string cssClass = null)
        {
            var _itemList = getList();
            _itemList.Add(new MenuItem
            {
                Text = text,
                Url = "#",
                CssClass = cssClass,
                Operation = operation
            });
            return this;
        }
        private void itterateOverSiteItems(int entityId, IList<MenuItem> items, IList<MenuItem> parent = null)
        {
            items.ForEachItem(c =>
                           {
                               // this is so that c doesn't get modified each itteration
                               var instanceC = copyMenuItem(c);
                               if (instanceC.Url=="#"&& instanceC.Children.Any())
                               {
                                   // this is so we can loop through and give the parentid to the children,
                                   // however, we need to remove and readd the child since it's not a reference
                                   var children = instanceC.Children;
                                   instanceC.Children=new List<MenuItem>();
                                   itterateOverSiteItems(entityId,children, instanceC.Children);
                               }
                               else
                               {
                                   instanceC.Url = instanceC.Url + "/0/0/" + entityId;
                               }
                               if (parent != null) parent.Add(instanceC);
                           });
        }

        protected override  IList<MenuItem> getList()
        {
            if (_sites != null && count > 0)
            {
                var lastCatParentItem = _parentItems.LastOrDefault();
                return lastCatParentItem != null ? lastCatParentItem.Children : _siteItems;
            }
            var lastParentItem = _parentItems.LastOrDefault();
            return lastParentItem != null ? lastParentItem.Children : _items;
        }


        private MenuItem copyMenuItem(MenuItem menuItem)
        {
            return new MenuItem
                       {
                           Url = menuItem.Url,
                           Operation = menuItem.Operation,
                           Children = menuItem.Children,
                           CssClass = menuItem.CssClass,
                           Text = menuItem.Text
                       };
        }


        public IKYTMenuBuilder addUrlParameter(string name, string value)
        {
            var _itemList = getList();
            var lastItem = _itemList.LastOrDefault();
            lastItem.Url = lastItem.Url + "?" + name + "=" + value;
            return this;
        }

        public IKYTMenuBuilder CreateNode(StringToken text, string url, string cssClass = null)
        {
            var _itemList = getList();
            _itemList.Add(new MenuItem
            {
                Text = text.DefaultValue,
                Url = url
            });
            return this;
        }

        public IKYTMenuBuilder HasChildren()
        {
            base.HasChildren();
            return this;
        }

        public IKYTMenuBuilder EndChildren()
        {
            base.EndChildren();
            return this;
        }

        public IKYTMenuBuilder CreateNode<CONTROLLER>(string urlPreface, Expression<Func<CONTROLLER, object>> action, StringToken text, AreaName areaName = null, string cssClass = null) where CONTROLLER : Controller
        {
            base.CreateNode(urlPreface, action, text, areaName, cssClass);
            return this;
        }

        public IKYTMenuBuilder CreateNode<CONTROLLER>(Expression<Func<CONTROLLER, object>> action, StringToken text, AreaName areaName = null, string cssClass = null) where CONTROLLER : Controller
        {
            base.CreateNode(action, text, areaName, cssClass);
            return this;
        }

        public IKYTMenuBuilder CreateNode<CONTROLLER>(Expression<Func<CONTROLLER, object>> action, StringToken text, string urlParam, AreaName areaName = null, string cssClass = null) where CONTROLLER : Controller
        {
            base.CreateNode(action, text, urlParam, areaName, cssClass);
            return this;
        }

        public IKYTMenuBuilder CreateNode(StringToken text, string cssClass = null)
        {
            base.CreateNode(text, cssClass);
            return this;
        }

        public IKYTMenuBuilder CreateTagNode<CONTROLLER>(StringToken text) where CONTROLLER : Controller
        {
            base.CreateTagNode<CONTROLLER>(text);
            return this;
        }

        public IKYTMenuBuilder Route(string route)
        {
            base.Route(route);
            return this;
        }
    }

}
