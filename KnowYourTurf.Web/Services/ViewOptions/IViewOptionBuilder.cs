using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;
using KnowYourTurf.Core;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Enums;
using KnowYourTurf.Core.Html;
using KnowYourTurf.Core.Services;
using KnowYourTurf.Security.Interfaces;

namespace KnowYourTurf.Web.Services.ViewOptions
{
    public interface IViewOptionBuilder
    {
        IList<ViewOption> Items { get; set; }
        IViewOptionBuilder UrlForList<CONTROLLER>(Expression<Func<CONTROLLER, object>> action, AreaName areaName = null) where CONTROLLER : Controller;
        IViewOptionBuilder UrlForForm<CONTROLLER>(Expression<Func<CONTROLLER, object>> action, AreaName areaName = null) where CONTROLLER : Controller;
        IViewOptionBuilder UrlForDisplay<CONTROLLER>(Expression<Func<CONTROLLER, object>> action, AreaName areaName = null) where CONTROLLER : Controller;
        IViewOptionBuilder Url<CONTROLLER>(Expression<Func<CONTROLLER, object>> action, AreaName areaName=null) where CONTROLLER : Controller;
        IViewOptionBuilder RouteToken(string route);
        IViewOptionBuilder ViewName(string viewName);
        IViewOptionBuilder SubViewName(string subViewName);
        IViewOptionBuilder ViewId(string ViewId);
        IViewOptionBuilder AddUpdateToken(string addUpdate);
        IViewOptionBuilder IsChild(bool isChild = true);
        IViewOptionBuilder NoBubbleUp();
        IViewOptionBuilder Operation(string operation);
        IViewOptionBuilder End();
        void WithoutPermissions(bool withOutPermissions);
    }

    public class ViewOptionBuilder : IViewOptionBuilder
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly ISessionContext _sessionContext;
        private readonly IRepository _repository;

        public ViewOptionBuilder(IAuthorizationService authorizationService,
            ISessionContext sessionContext,
            IRepository repository)
        {
            _authorizationService = authorizationService;
            _sessionContext = sessionContext;
            _repository = repository;
            Items = new List<ViewOption>();
        }

        private ViewOption _currentItem;
        private bool _withOutPermissions;

        private ViewOption currentItem
        {
            get { return _currentItem ?? (_currentItem = new ViewOption()); }
            set { _currentItem = value; }
        }

        public IList<ViewOption> Items { get; set; }

        public IViewOptionBuilder UrlForList<CONTROLLER>(Expression<Func<CONTROLLER, object>> action, AreaName areaName = null) where CONTROLLER : Controller
        {
            currentItem.url = UrlContext.GetUrlForAction(action, areaName);
            var itemName = typeof(CONTROLLER).Name.Replace("Controller", "").ToLowerInvariant();
            currentItem.id = itemName;
            currentItem.viewName = "GridView";
            currentItem.route = itemName;
            currentItem.addUpate = itemName.Replace("list", "");
            currentItem.display = itemName.Replace("list", "display");
            return this;
        }

        public IViewOptionBuilder UrlForForm<CONTROLLER>(Expression<Func<CONTROLLER, object>> action, AreaName areaName = null) where CONTROLLER : Controller
        {
            currentItem.url = UrlContext.GetUrlForAction(action, areaName);
            var itemName = typeof(CONTROLLER).Name.Replace("Controller", "").ToLowerInvariant();
            currentItem.id = itemName;
            currentItem.viewName = "AjaxFormView";
            currentItem.route = itemName;
            currentItem.isChild = true;
            return this;
        }

        public IViewOptionBuilder UrlForDisplay<CONTROLLER>(Expression<Func<CONTROLLER, object>> action, AreaName areaName = null) where CONTROLLER : Controller
        {
            currentItem.url = UrlContext.GetUrlForAction(action, areaName);
            var itemName = typeof(CONTROLLER).Name.Replace("Controller", "").ToLowerInvariant()+"display";
            currentItem.id = itemName;
            currentItem.viewName = "AjaxDisplayView";
            currentItem.route = itemName;
            currentItem.isChild = true;
            return this;
        }

        public IViewOptionBuilder Url<CONTROLLER>(Expression<Func<CONTROLLER, object>> action, AreaName areaName = null) where CONTROLLER : Controller
        {
            currentItem.url = UrlContext.GetUrlForAction(action, areaName);
            return this;
        }

        public IViewOptionBuilder RouteToken(string route)
        {
            currentItem.route = route;
            return this;
        }

        public IViewOptionBuilder ViewName(string viewName)
        {
            currentItem.viewName = viewName;
            return this;
        }

        public IViewOptionBuilder SubViewName(string subViewName)
        {
            currentItem.subViewName = subViewName;
            return this;
        }

        public IViewOptionBuilder ViewId(string ViewId)
        {
            currentItem.id = ViewId;
            return this;
        }

        public IViewOptionBuilder AddUpdateToken(string addUpdate)
        {
            currentItem.addUpate = addUpdate;
            return this;
        }

        public IViewOptionBuilder IsChild(bool isChild = true)
        {
            currentItem.isChild = isChild;
            return this;
        }

        public IViewOptionBuilder NoBubbleUp()
        {
            currentItem.noBubbleUp = true;
            return this;
        }

        public IViewOptionBuilder Operation(string operation)
        {
            currentItem.Operation = operation;
            return this;
        }

        public IViewOptionBuilder End()
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
            currentItem = new ViewOption();
            return this;
        }

        public void WithoutPermissions(bool withOutPermissions)
        {
            _withOutPermissions = withOutPermissions;
        }
    }
}