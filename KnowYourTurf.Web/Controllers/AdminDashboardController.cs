using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using KnowYourTurf.Core;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Html;
using KnowYourTurf.Core.Html.Grid;
using KnowYourTurf.Core.Services;
using KnowYourTurf.Web.Grids;
using KnowYourTurf.Web.Models;
using StructureMap;

namespace KnowYourTurf.Web.Controllers
{
    public class AdminDashboardController:KYTController
    {
        private readonly IRepository _repository;
        private readonly IDynamicExpressionQuery _dynamicExpressionQuery;
        private readonly IEntityListGrid<Administrator> _grid;

        public AdminDashboardController(IRepository repository, IDynamicExpressionQuery dynamicExpressionQuery,
            IEntityListGrid<Administrator> grid  )
        {
            _repository = repository;
            _dynamicExpressionQuery = dynamicExpressionQuery;
            _grid = grid;
        }

        public ActionResult ViewAdmin(ViewModel input)
        {
            var admin = _repository.Find<Administrator>(input.EntityId);
            var model = new AdminViewModel
            {
                Administrator = admin,
                AddEditUrl = UrlContext.GetUrlForAction<TaskController>(x => x.AddEdit(null)) + "?ParentId=" + input.EntityId+"&From=Admin",
               
            };
            return View("AdminDashboard", model);
        }
    }
}