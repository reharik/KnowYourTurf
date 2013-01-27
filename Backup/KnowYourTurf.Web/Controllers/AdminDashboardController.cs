using System.Web.Mvc;
using CC.Core.CoreViewModelAndDTOs;
using CC.Core.DomainTools;
using CC.Core.Services;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Services;

namespace KnowYourTurf.Web.Controllers
{
    public class AdminDashboardController:KYTController
    {
        private readonly IRepository _repository;
        private readonly IDynamicExpressionQuery _dynamicExpressionQuery;
        private readonly IEntityListGrid<User> _grid;

        public AdminDashboardController(IRepository repository, IDynamicExpressionQuery dynamicExpressionQuery,
            IEntityListGrid<User> grid  )
        {
            _repository = repository;
            _dynamicExpressionQuery = dynamicExpressionQuery;
            _grid = grid;
        }

        public ActionResult ViewAdmin(ViewModel input)
        {
//            var admin = _repository.Find<User>(input.EntityId);
//            var model = new UserViewModel
//            {
//                Item = admin,
//                AddUpdateUrl = UrlContext.GetUrlForAction<TaskController>(x => x.AddUpdate(null)) + "?ParentId=" + input.EntityId+"&From=Admin",
//               
//            };
//            return View("AdminDashboard", model);
            return null;
        }
    }

    

}