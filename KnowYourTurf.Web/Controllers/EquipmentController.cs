using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using CC.Core;
using CC.Core.CoreViewModelAndDTOs;
using CC.Core.CustomAttributes;
using CC.Core.DomainTools;
using CC.Core.Html;
using CC.Core.Services;
using Castle.Components.Validator;
using FluentNHibernate.Utils;
using KnowYourTurf.Core;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Domain.Persistence;
using KnowYourTurf.Core.Services;
using KnowYourTurf.Web.Services;

namespace KnowYourTurf.Web.Controllers
{
    using KnowYourTurf.Web.Config;

    public class EquipmentController : KYTController
    {
        private readonly IRepository _repository;
        private readonly ISaveEntityService _saveEntityService;
        private readonly IFileHandlerService _fileHandlerService;
        private readonly ISessionContext _sessionContext;
        private readonly ISelectListItemService _selectListItemService;

        public EquipmentController(IRepository repository,
            ISaveEntityService saveEntityService,
            IFileHandlerService fileHandlerService,
            ISessionContext sessionContext, ISelectListItemService selectListItemService)
        {
            _repository = repository;
            _saveEntityService = saveEntityService;
            _fileHandlerService = fileHandlerService;
            _sessionContext = sessionContext;
            _selectListItemService = selectListItemService;
        }

        public ActionResult AddUpdate_Template(ViewModel input)
        {
            return View("EquipmentAddUpdate", new EquipmentViewModel());
        }

        public ActionResult AddUpdate(ViewModel input)
        {
            var equipment = input.EntityId > 0 ? _repository.Find<Equipment>(input.EntityId) : new Equipment();
            var vendors = _selectListItemService.CreateList<EquipmentVendor>(x => x.Client, x => x.EntityId, true);
            var equipmentTypes = _selectListItemService.CreateList<EquipmentType>(x => x.Name, x => x.EntityId, true);
            var model = Mapper.Map<Equipment, EquipmentViewModel>(equipment);

            model._EquipmentVendorEntityIdList = vendors;
            model._EquipmentTypeEntityIdList= equipmentTypes;
            model._Title = WebLocalizationKeys.EQUIPMENT_INFORMATION.ToString();
            model._saveUrl = UrlContext.GetUrlForAction<EquipmentController>(x => x.Save(null));
            return new CustomJsonResult(model);
        }

//        public ActionResult Display(ViewModel input)
//        {
//            var equipment = _repository.Find<Equipment>(input.EntityId);
//            var model = new EquipmentViewModel
//            {
//                Item = equipment,
//                AddUpdateUrl = UrlContext.GetUrlForAction<EquipmentController>(x => x.AddUpdate(null)) + "/" + equipment.EntityId,
//                _Title = WebLocalizationKeys.EQUIPMENT_INFORMATION.ToString()
//            };
//            return PartialView("EquipmentView", model);
//        }

        public ActionResult Delete(ViewModel input)
        {
            var equipment = _repository.Find<Equipment>(input.EntityId);
            _repository.HardDelete(equipment);
            _repository.UnitOfWork.Commit();
            return null;
        }
        
        public ActionResult DeleteMultiple(BulkActionViewModel input)
        {
            var notification = new Notification { Success = true };
            input.EntityIds.Each(x =>
            {
                var item = _repository.Find<Equipment>(x);
                if (checkDependencies(item, notification))
                {
                    _repository.HardDelete(item);
                }
            });
            _repository.Commit();
            return new CustomJsonResult(notification);
        }

        private bool checkDependencies(Equipment item, Notification notification)
        {
            var tasks = _repository.Query<Task>(x=>x.Equipment.Any(i=>i==item));
            if(tasks.Any())
            {
                if(notification.Message.IsEmpty())
                {
                    notification.Success = false;
                    notification.Message = WebLocalizationKeys.COULD_NOT_DELETE_FOR_TASK.ToString();
                }
                return false;
            }
            return true;
        }

        public ActionResult Save(EquipmentViewModel input)
        {
            var equipment = input.EntityId > 0 ? _repository.Find<Equipment>(input.EntityId) : new Equipment();
            equipment.Name = input.Name;
            equipment.TotalHours = input.TotalHours;
            equipment.Threshold = input.Threshold;
            equipment.Description = input.Description;
            equipment.EquipmentType = input.EquipmentTypeEntityId > 0 ? _repository.Find<EquipmentType>(input.EquipmentTypeEntityId) : null;
            equipment.EquipmentVendor = input.EquipmentVendorEntityId > 0 ? _repository.Find<EquipmentVendor>(input.EquipmentVendorEntityId) : null;

            
            var crudManager = _saveEntityService.ProcessSave(equipment);
            var notification = crudManager.Finish();
            return new CustomJsonResult(notification);
        }
    }

    public class EquipmentViewModel:ViewModel
    {
        public EquipmentViewModel()
        {
            _PhotoHeaderButtons = new List<string>();
            _DocumentHeaderButtons = new List<string>();
        }
        
        [ValidateNonEmpty]
        public string Name { get; set; }
        [TextArea]
        public string Description { get; set; }
        public int EquipmentTypeEntityId { get; set; }
        public int EquipmentVendorEntityId { get; set; }
        [ValidateNonEmpty]
        [ValidateDouble]
        public double TotalHours { get; set; }
        public double Threshold { get; set; }
        public bool DeleteImage { get; set; }
    
        public string _completedGridUrl { get; set; }
        public string _documentGridUrl { get; set; }
        public string _photoGridUrl { get; set; }
        public string _pendingGridUrl { get; set; }
        public string _AddUpdatePhotoUrl { get; set; }
        public string _AddUpdateDocumentUrl { get; set; }
        public string _DeleteMultiplePhotosUrl { get; set; }
        public string _DeleteMultipleDocumentsUrl { get; set; }
        public List<string> _PhotoHeaderButtons { get; set; }
        public List<string> _DocumentHeaderButtons { get; set; }
        public IEnumerable<SelectListItem> _EquipmentVendorEntityIdList { get; set; }
        public IEnumerable<SelectListItem> _EquipmentTypeEntityIdList { get; set; }
        public IEnumerable<PhotoDto> _Photos { get; set; }

        public string _saveUrl { get; set; }

    }

}
