using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;
using CC.Core;
using CC.Core.DomainTools;
using CC.Core.Html;
using CC.Security.Interfaces;
using KnowYourTurf.Core;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Enums;
using KnowYourTurf.Core.Services;
using KnowYourTurf.Web.Services.RouteTokens;

namespace KnowYourTurf.Web.Services.ViewOptions
{
    public interface IRouteTokenBuilder
    {
        IList<RouteToken> Items { get; set; }
        IRouteTokenBuilder TokenForList<CONTROLLER>(Expression<Func<CONTROLLER, object>> action, AreaName areaName = null) where CONTROLLER : Controller;
        IRouteTokenBuilder TokenForForm<CONTROLLER>(Expression<Func<CONTROLLER, object>> action, AreaName areaName = null) where CONTROLLER : Controller;
        IRouteTokenBuilder UrlForDisplay<CONTROLLER>(Expression<Func<CONTROLLER, object>> action, AreaName areaName = null) where CONTROLLER : Controller;
        IRouteTokenBuilder Url<CONTROLLER>(Expression<Func<CONTROLLER, object>> action, AreaName areaName=null) where CONTROLLER : Controller;
        IRouteTokenBuilder RouteToken(string route);
        IRouteTokenBuilder ViewName(string viewName);
        IRouteTokenBuilder SubViewName(string subViewName);
        IRouteTokenBuilder ViewId(string ViewId);
        IRouteTokenBuilder AddUpdateToken(string addUpdate);
        IRouteTokenBuilder IsChild(bool isChild = true);
        IRouteTokenBuilder NoBubbleUp();
        IRouteTokenBuilder Operation(string operation);
        IRouteTokenBuilder End();
        void WithoutPermissions(bool withOutPermissions);
        IRouteTokenBuilder NoModel();
        IRouteTokenBuilder NoTemplate();
        IRouteTokenBuilder JustRoute(string route);
        IRouteTokenBuilder SubRouteToken(string route);
        IRouteTokenBuilder GridId(string gridId);
    }

    public class RouteTokenBuilder : IRouteTokenBuilder
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly ISessionContext _sessionContext;
        private readonly IRepository _repository;

        public RouteTokenBuilder(IAuthorizationService authorizationService,
            ISessionContext sessionContext,
            IRepository repository)
        {
            _authorizationService = authorizationService;
            _sessionContext = sessionContext;
            _repository = repository;
            Items = new List<RouteToken>();
        }

        private RouteToken _currentItem;
        private bool _withOutPermissions;

        private RouteToken currentItem
        {
            get { return _currentItem ?? (_currentItem = new RouteToken()); }
            set { _currentItem = value; }
        }

        public IList<RouteToken> Items { get; set; }

        public IRouteTokenBuilder TokenForList<CONTROLLER>(Expression<Func<CONTROLLER, object>> action, AreaName areaName = null) where CONTROLLER : Controller
        {
            currentItem.url = UrlContext.GetUrlForAction(action,areaName);
            var controllerName = typeof(CONTROLLER).Name.Replace("Controller", "");
            currentItem.itemName = controllerName + "View";
            var itemName = controllerName.ToLowerInvariant();
            currentItem.id = itemName;
            currentItem.viewName = "GridView";
            currentItem.route = itemName;
            currentItem.addUpdate = itemName.Replace("list", "");
            currentItem.display = itemName.Replace("list", "display");
            return this;
        }

        public IRouteTokenBuilder TokenForForm<CONTROLLER>(Expression<Func<CONTROLLER, object>> action, AreaName areaName = null) where CONTROLLER : Controller
        {
            currentItem.url = UrlContext.GetUrlForAction(action, areaName);
            var controllerName = typeof (CONTROLLER).Name.Replace("Controller", "");
            currentItem.itemName = controllerName+"FormView";
            var itemName = controllerName.ToLowerInvariant();
            currentItem.id = itemName;
            currentItem.viewName = "AjaxFormView";
            currentItem.route = itemName;
            currentItem.isChild = true;
            return this;
        }

        public IRouteTokenBuilder UrlForDisplay<CONTROLLER>(Expression<Func<CONTROLLER, object>> action, AreaName areaName = null) where CONTROLLER : Controller
        {
            currentItem.url = UrlContext.GetUrlForAction(action, areaName);
            var itemName = typeof(CONTROLLER).Name.Replace("Controller", "").ToLowerInvariant()+"display";
            currentItem.id = itemName;
            currentItem.viewName = "AjaxDisplayView";
            currentItem.route = itemName;
            currentItem.isChild = true;
            return this;
        }

        public IRouteTokenBuilder Url<CONTROLLER>(Expression<Func<CONTROLLER, object>> action, AreaName areaName = null) where CONTROLLER : Controller
        {
            currentItem.url = UrlContext.GetUrlForAction(action, areaName);
            var controllerName = typeof(CONTROLLER).Name.Replace("Controller", "");
            var itemName = controllerName.ToLowerInvariant();
            currentItem.id = itemName;
            currentItem.viewName = "AjaxFormView";
            currentItem.route = itemName;
            return this;
        }

        public IRouteTokenBuilder RouteToken(string route)
        {
            currentItem.route = route;
            return this;
        }

        public IRouteTokenBuilder ViewName(string viewName)
        {
            currentItem.viewName = viewName;
            return this;
        }

        public IRouteTokenBuilder SubViewName(string subViewName)
        {
            currentItem.subViewName = subViewName;
            return this;
        }

        public IRouteTokenBuilder ViewId(string ViewId)
        {
            currentItem.id = ViewId;
            return this;
        }

        public IRouteTokenBuilder AddUpdateToken(string addUpdate)
        {
            currentItem.addUpdate = addUpdate;
            return this;
        }

        public IRouteTokenBuilder IsChild(bool isChild = true)
        {
            currentItem.isChild = isChild;
            return this;
        }

        public IRouteTokenBuilder NoBubbleUp()
        {
            currentItem.noBubbleUp = true;
            return this;
        }

        public IRouteTokenBuilder Operation(string operation)
        {
            currentItem.Operation = operation;
            return this;
        }

        public IRouteTokenBuilder NoModel()
        {
            currentItem.noModel = true;
            return this;
        }

        public IRouteTokenBuilder NoTemplate()
        {
            currentItem.noTemplate = true;
            return this;
        }

        public IRouteTokenBuilder JustRoute(string route)
        {
            currentItem.route = route.ToLowerInvariant();
            currentItem.viewName= route + "View";
            return this;
        }

        public IRouteTokenBuilder SubRouteToken(string route)
        {
            currentItem.subViewRoute = route.ToLowerInvariant();
            return this;
        }

        public IRouteTokenBuilder GridId(string gridId)
        {
            currentItem.gridId = gridId;
            return this;
        }

        public IRouteTokenBuilder End()
        {
            if (_withOutPermissions || currentItem.Operation.IsEmpty())
            {
                Items.Add(currentItem);
            }
            else
            {
                if (_authorizationService.IsAllowed(_repository.Find<User>(_sessionContext.GetUserId()),"/Schedule/MenuItem" + currentItem.Operation)) 
                {
                    Items.Add(currentItem);
                }
            }
            currentItem = new RouteToken();
            return this;
        }

        public void WithoutPermissions(bool withOutPermissions)
        {
            _withOutPermissions = withOutPermissions;
        }
    }
}