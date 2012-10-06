using System.Linq;
using System.Web.Mvc;
using CC.Core.CoreViewModelAndDTOs;
using CC.Core.DomainTools;
using CC.Core.Services;
using KnowYourTurf.Core;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Web.Models;
using StructureMap;

namespace KnowYourTurf.Web.Controllers
{
    public class FieldController : KYTController
    {
        private readonly IRepository _repository;
        private readonly ISaveEntityService _saveEntityService;

        public FieldController(IRepository repository,
            ISaveEntityService saveEntityService)
        {
            _repository = repository;
            _saveEntityService = saveEntityService;
        }

        public ActionResult AddUpdate(ViewModel input)
        {
//            var field = input.EntityId > 0 ? _repository.Find<Field>(input.EntityId) : new Field();
//            var model = new FieldViewModel
//            {
//                Item = field,
//                ParentId = input.ParentId,
//                AddUpdateUrl = UrlContext.GetUrlForAction<FieldController>(x => x.AddUpdate(null)) + "/" + field.EntityId,
//                _Title = WebLocalizationKeys.FIELD_INFORMATION.ToString()
//            };
//            return PartialView("FieldAddUpdate", model);
            return null;
        }

        public ActionResult Delete(ViewModel input)
        {
            var field = _repository.Find<Field>(input.EntityId);
            var rulesEngineBase = ObjectFactory.Container.GetInstance<RulesEngineBase>("DeleteFieldRules");
            var rulesResult = rulesEngineBase.ExecuteRules(field);
            if (!rulesResult.Success)
            {
                var notification = new RulesNotification(rulesResult);
                return Json(notification);
            } 
            _repository.Delete(field);
            _repository.UnitOfWork.Commit();
            return null;
        }

        public ActionResult Save(FieldViewModel input)
        {
            var category = _repository.Find<Category>(input.RootId);
            Field field;
            if (input.EntityId > 0) { field = category.Fields.FirstOrDefault(x => x.EntityId == input.EntityId); }
            else
            {
                field = new Field();
                category.AddField(field);
            }
            field.Description = input.Description;
            field.Name = input.Name;
            field.Abbreviation= input.Abbreviation;
            field.Size = input.Size;
            field.FieldColor= input.FieldColor;
            
            var crudManager = _saveEntityService.ProcessSave(category);
            var notification = crudManager.Finish();
            return Json(notification,"text/plain");
        }

    }
}