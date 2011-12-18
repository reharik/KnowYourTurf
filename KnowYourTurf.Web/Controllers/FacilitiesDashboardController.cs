using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using KnowYourTurf.Core;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Html;
using KnowYourTurf.Core.Html.Grid;
using KnowYourTurf.Core.Services;
using KnowYourTurf.Web.Models;
using StructureMap;

namespace KnowYourTurf.Web.Controllers
{
    public class FacilitiesDashboardController:KYTController
    {
        private readonly IRepository _repository;
        private readonly IDynamicExpressionQuery _dynamicExpressionQuery;

        public FacilitiesDashboardController(IRepository repository, IDynamicExpressionQuery dynamicExpressionQuery)
        {
            _repository = repository;
            _dynamicExpressionQuery = dynamicExpressionQuery;
        }

        public ActionResult ViewFacilities(ViewModel input)
        {
            var facilities = _repository.Find<User>(input.EntityId);
            var model = new UserViewModel
            {
                User = facilities,
                AddEditUrl = UrlContext.GetUrlForAction<TaskController>(x => x.AddEdit(null)) + "?ParentId=" + input.EntityId+"&From=Facilities",
               
            };
            return View("FacilitiesDashboard", model);
        }
    }
}