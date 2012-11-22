using System;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using CC.Core.CoreViewModelAndDTOs;
using CC.Core.DomainTools;
using CC.Core.Html;
using CC.Core.Services;
using FluentNHibernate.Utils;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Enums;
using KnowYourTurf.Core.Services;
using KnowYourTurf.Web.Models;

namespace KnowYourTurf.Web.Controllers
{
    public class EquipmentTaskController : KYTController
    {
        private readonly IRepository _repository;
        private readonly ISaveEntityService _saveEntityService;
        private readonly ISelectListItemService _selectListItemService;
        private readonly IInventoryService _inventoryService;
        private readonly IUpdateCollectionService _updateCollectionService;

        public EquipmentTaskController(IRepository repository,
            ISaveEntityService saveEntityService,
            ISelectListItemService selectListItemService,
            IInventoryService inventoryService,
            IUpdateCollectionService updateCollectionService)
        {
            _repository = repository;
            _saveEntityService = saveEntityService; 
            _selectListItemService = selectListItemService;
            _inventoryService = inventoryService;
            _updateCollectionService = updateCollectionService;
        }
        public ActionResult AddUpdate_Template(AddUpdateEquipmentTaskViewModel input)
        {
            return View("EquipmentTaskAddUpdate", new EquipmentTaskViewModel{Popup = input.Popup});
        }   

        public ActionResult AddUpdate(AddUpdateEquipmentTaskViewModel input)
        {
            var equipmentTask = input.EntityId > 0 ? _repository.Find<EquipmentTask>(input.EntityId) : new EquipmentTask();
            equipmentTask.ScheduledDate = input.ScheduledDate.HasValue ? input.ScheduledDate.Value : equipmentTask.ScheduledDate;
            var equipmentTaskTypes = _selectListItemService.CreateList<EquipmentTaskType>(x => x.Name, x => x.EntityId, true);
            var equipment = _selectListItemService.CreateList<Equipment>(x=>x.Name,x=>x.EntityId,true);
            var availableEmployees = _repository.Query<User>(x => x.UserLoginInfo.Status == Status.Active.ToString() && x.UserRoles.Any(y=>y.Name==UserType.Employee.ToString()))
                .Select(x => new TokenInputDto { id = x.EntityId.ToString(), name = x.FirstName + " " + x.LastName }).OrderBy(x=>x.name).ToList();
            var selectedEmployees = equipmentTask.Employees.Select(x => new TokenInputDto { id = x.EntityId.ToString(), name = x.FullName }).OrderBy(x => x.name);
            
            var availableParts = _repository.FindAll<Part>().Select(x => new TokenInputDto { id = x.EntityId.ToString(), name = x.Name }).OrderBy(x => x.name);
            var selectedParts = equipmentTask.Parts.Select(x => new TokenInputDto { id = x.EntityId.ToString(), name = x.Name }).OrderBy(x => x.name);
            var model = Mapper.Map<EquipmentTask, EquipmentTaskViewModel>(equipmentTask);
            model.Employees = new TokenInputViewModel { _availableItems = availableEmployees, selectedItems = selectedEmployees };
            model.Parts = new TokenInputViewModel { _availableItems = availableParts, selectedItems = selectedParts };
            model._EquipmentEntityIdList = equipment;
            model._TaskTypeEntityIdList = equipmentTaskTypes;
            model.ScheduledDate = equipmentTask.ScheduledDate.HasValue ? equipmentTask.ScheduledDate.Value.ToShortDateString():"";
            model._Title = WebLocalizationKeys.TASK_INFORMATION.ToString();
            model.Popup = input.Popup;
            model.RootId = input.RootId;

            model._saveUrl = UrlContext.GetUrlForAction<EquipmentTaskController>(x => x.Save(null));

            if (input.Copy)
            {
                // entityid is changed on view cuz the entity is in the NH graph here on server
                model.Complete = false;
            }
                
            return Json(model,JsonRequestBehavior.AllowGet);
        }


        public ActionResult Display_Template(ViewModel input)
        {
            return View("Display", new DisplayEquipmentTaskViewModel{Popup = input.Popup});
        }

        public ActionResult Display(ViewModel input)
        {
            var equipmentEquipmentTask = _repository.Find<EquipmentTask>(input.EntityId);
            var model = Mapper.Map<EquipmentTask, DisplayEquipmentTaskViewModel>(equipmentEquipmentTask);
            model.Popup = input.Popup;
            model._EmployeeNames = equipmentEquipmentTask.Employees.Select(x => x.FullName);
            model._PartsNames = equipmentEquipmentTask.Parts.Select(x => x.Name);
            model._AddUpdateUrl = UrlContext.GetUrlForAction<EquipmentTaskController>(x => x.AddUpdate(null));
            model._Title = WebLocalizationKeys.TASK_INFORMATION.ToString();
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(ViewModel input)
        {
            var equipmentEquipmentTask = _repository.Find<EquipmentTask>(input.EntityId);
            if (!equipmentEquipmentTask.Complete)
            {
                _repository.HardDelete(equipmentEquipmentTask);
                _repository.UnitOfWork.Commit();
            }
            return null;
        }

        public ActionResult DeleteMultiple(BulkActionViewModel input)
        {
            input.EntityIds.Each(x =>
            {
                var item = _repository.Find<EquipmentTask>(x);
                if(!item.Complete) _repository.HardDelete(item);
            });
            _repository.Commit();
            return Json(new Notification { Success = true }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Save(EquipmentTaskViewModel input)
        {
            var equipmentEquipmentTask = input.EntityId > 0 ? _repository.Find<EquipmentTask>(input.EntityId) : new EquipmentTask();

            mapItem(equipmentEquipmentTask, input);
            mapChildren(equipmentEquipmentTask, input);
            
            var validationManager = _saveEntityService.ProcessSave(equipmentEquipmentTask);
            var notification = validationManager.Finish();
            return Json(notification);
        }

        private void mapItem(EquipmentTask item, EquipmentTaskViewModel input)
        {
            item.ScheduledDate = DateTime.Parse(input.ScheduledDate);
            item.ActualTimeSpent = input.ActualTimeSpent;
            item.Notes = input.Notes;
            item.Complete = input.Complete;
            
        }

        private void mapChildren(EquipmentTask equipmentTask,EquipmentTaskViewModel model)
        {
            _updateCollectionService.Update(equipmentTask.Employees, model.Employees, equipmentTask.AddEmployee, equipmentTask.RemoveEmployee);
            _updateCollectionService.Update(equipmentTask.Parts, model.Parts, equipmentTask.AddPart, equipmentTask.RemovePart);

            equipmentTask.TaskType = _repository.Find<EquipmentTaskType>(model.TaskTypeEntityId);
            equipmentTask.Equipment = _repository.Find<Equipment>(model.EquipmentEntityId);
        }
    }

    
}