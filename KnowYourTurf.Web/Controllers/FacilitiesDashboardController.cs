using System.Web.Mvc;
using CC.Core.CoreViewModelAndDTOs;
using CC.Core.DomainTools;
using CC.Core.Services;

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
//            var facilities = _repository.Find<User>(input.EntityId);
//            var model = new UserViewModel
//            {
//                Item = facilities,
//                AddUpdateUrl = UrlContext.GetUrlForAction<TaskController>(x => x.AddUpdate(null)) + "?ParentId=" + input.EntityId+"&From=AddUpdate",
//               
//            };
//            return View("FacilitiesDashboard", model);
            return null;
        }
    }
}