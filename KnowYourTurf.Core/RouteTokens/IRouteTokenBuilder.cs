namespace KnowYourTurf.Core.RouteTokens
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Web.Mvc;

    using CC.Core;
    using CC.Core.DomainTools;
    using CC.Core.Html;
    using CC.Security.Interfaces;

    using KnowYourTurf.Core.Domain;
    using KnowYourTurf.Core.Enums;
    using KnowYourTurf.Core.Services;

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
            this._authorizationService = authorizationService;
            this._sessionContext = sessionContext;
            this._repository = repository;
            this.Items = new List<RouteToken>();
        }

        private RouteToken _currentItem;
        private bool _withOutPermissions;

        private RouteToken currentItem
        {
            get { return this._currentItem ?? (this._currentItem = new RouteToken()); }
            set { this._currentItem = value; }
        }

        public IList<RouteToken> Items { get; set; }

        public IRouteTokenBuilder TokenForList<CONTROLLER>(Expression<Func<CONTROLLER, object>> action, AreaName areaName = null) where CONTROLLER : Controller
        {
            this.currentItem.url = UrlContext.GetUrlForAction(action,areaName);
            var controllerName = typeof(CONTROLLER).Name.Replace("Controller", "");
            this.currentItem.itemName = controllerName + "View";
            var itemName = controllerName.ToLowerInvariant();
            this.currentItem.id = itemName;
            this.currentItem.viewName = "GridView";
            this.currentItem.route = itemName;
            this.currentItem.addUpdate = itemName.Replace("list", "");
            this.currentItem.display = itemName.Replace("list", "display");
            return this;
        }

        public IRouteTokenBuilder TokenForForm<CONTROLLER>(Expression<Func<CONTROLLER, object>> action, AreaName areaName = null) where CONTROLLER : Controller
        {
            this.currentItem.url = UrlContext.GetUrlForAction(action, areaName);
            var controllerName = typeof (CONTROLLER).Name.Replace("Controller", "");
            this.currentItem.itemName = controllerName+"FormView";
            var itemName = controllerName.ToLowerInvariant();
            this.currentItem.id = itemName;
            this.currentItem.viewName = "AjaxFormView";
            this.currentItem.route = itemName;
            this.currentItem.isChild = true;
            return this;
        }

        public IRouteTokenBuilder UrlForDisplay<CONTROLLER>(Expression<Func<CONTROLLER, object>> action, AreaName areaName = null) where CONTROLLER : Controller
        {
            this.currentItem.url = UrlContext.GetUrlForAction(action, areaName);
            var itemName = typeof(CONTROLLER).Name.Replace("Controller", "").ToLowerInvariant()+"display";
            this.currentItem.id = itemName;
            this.currentItem.viewName = "AjaxDisplayView";
            this.currentItem.route = itemName;
            this.currentItem.isChild = true;
            return this;
        }

        public IRouteTokenBuilder Url<CONTROLLER>(Expression<Func<CONTROLLER, object>> action, AreaName areaName = null) where CONTROLLER : Controller
        {
            this.currentItem.url = UrlContext.GetUrlForAction(action, areaName);
            var controllerName = typeof(CONTROLLER).Name.Replace("Controller", "");
            var itemName = controllerName.ToLowerInvariant();
            this.currentItem.id = itemName;
            this.currentItem.viewName = "AjaxFormView";
            this.currentItem.route = itemName;
            return this;
        }

        public IRouteTokenBuilder RouteToken(string route)
        {
            this.currentItem.route = route;
            return this;
        }

        public IRouteTokenBuilder ViewName(string viewName)
        {
            this.currentItem.viewName = viewName;
            return this;
        }

        public IRouteTokenBuilder SubViewName(string subViewName)
        {
            this.currentItem.subViewName = subViewName;
            return this;
        }

        public IRouteTokenBuilder ViewId(string ViewId)
        {
            this.currentItem.id = ViewId;
            return this;
        }

        public IRouteTokenBuilder AddUpdateToken(string addUpdate)
        {
            this.currentItem.addUpdate = addUpdate;
            return this;
        }

        public IRouteTokenBuilder IsChild(bool isChild = true)
        {
            this.currentItem.isChild = isChild;
            return this;
        }

        public IRouteTokenBuilder NoBubbleUp()
        {
            this.currentItem.noBubbleUp = true;
            return this;
        }

        public IRouteTokenBuilder Operation(string operation)
        {
            this.currentItem.Operation = operation;
            return this;
        }

        public IRouteTokenBuilder NoModel()
        {
            this.currentItem.noModel = true;
            return this;
        }

        public IRouteTokenBuilder NoTemplate()
        {
            this.currentItem.noTemplate = true;
            return this;
        }

        public IRouteTokenBuilder JustRoute(string route)
        {
            this.currentItem.route = route.ToLowerInvariant();
            this.currentItem.viewName= route + "View";
            return this;
        }

        public IRouteTokenBuilder SubRouteToken(string route)
        {
            this.currentItem.subViewRoute = route.ToLowerInvariant();
            return this;
        }

        public IRouteTokenBuilder GridId(string gridId)
        {
            this.currentItem.gridId = gridId;
            return this;
        }

        public IRouteTokenBuilder End()
        {
            if (this._withOutPermissions || this.currentItem.Operation.IsEmpty())
            {
                this.Items.Add(this.currentItem);
            }
            else
            {
                if (this._authorizationService.IsAllowed(this._repository.Find<User>(this._sessionContext.GetUserId()),"/Schedule/MenuItem" + this.currentItem.Operation)) 
                {
                    this.Items.Add(this.currentItem);
                }
            }
            this.currentItem = new RouteToken();
            return this;
        }

        public void WithoutPermissions(bool withOutPermissions)
        {
            this._withOutPermissions = withOutPermissions;
        }
    }
}