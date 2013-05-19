using System;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using CC.Core;
using CC.Core.CoreViewModelAndDTOs;
using CC.Core.DomainTools;
using CC.Core.Html;
using CC.Core.Services;
using CC.Security.Interfaces;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Enums;
using KnowYourTurf.Core.Services;
using KnowYourTurf.Web.Models;
using KnowYourTurf.Web.Services;

namespace KnowYourTurf.Web.Controllers
{
    using Config;

    public class TaskController : KYTController
    {
        private readonly IRepository _repository;
        private readonly ISaveEntityService _saveEntityService;
        private readonly ISelectListItemService _selectListItemService;
        private readonly IInventoryService _inventoryService;
        private readonly IUpdateCollectionService _updateCollectionService;
        private readonly IAuthorizationService _authorizationService;

        public TaskController(IRepository repository,
            ISaveEntityService saveEntityService,
            ISelectListItemService selectListItemService,
            IInventoryService inventoryService,
            IUpdateCollectionService updateCollectionService,
            IAuthorizationService authorizationService)
        {
            _repository = repository;
            _saveEntityService = saveEntityService; 
            _selectListItemService = selectListItemService;
            _inventoryService = inventoryService;
            _updateCollectionService = updateCollectionService;
            _authorizationService = authorizationService;
        }
        public ActionResult AddUpdate_Template(AddUpdateTaskViewModel input)
        {
            return View("TaskAddUpdate", new TaskViewModel{Popup = input.Popup});
        }   

        public ActionResult AddUpdate(AddUpdateTaskViewModel input)
        {
            var task = input.EntityId > 0 ? _repository.Find<Task>(input.EntityId) : new Task();
            task.ScheduledDate = input.ScheduledDate.IsNotEmpty() ? DateTime.Parse(input.ScheduledDate) : task.ScheduledDate.HasValue?task.ScheduledDate.Value.Date:DateTime.Now.Date;
            task.StartTime= input.ScheduledStartTime.IsNotEmpty() ? DateTime.Parse(input.ScheduledStartTime) : task.StartTime;
            var taskTypes = _selectListItemService.CreateList<TaskType>(x => x.Name, x => x.EntityId, true);
            var fields = ((KYTSelectListItemService)_selectListItemService).CreateFieldsSelectListItems(input.RootId, input.ParentId);
            var products = ((KYTSelectListItemService)_selectListItemService).CreateProductSelectListItems();
            var availableEmployees = _repository.Query<User>(x => x.UserRoles.Any(y=>y.Name==UserType.Employee.ToString()))
                .Select(x => new TokenInputDto { id = x.EntityId.ToString(), name = x.FirstName + " " + x.LastName }).OrderBy(x=>x.name).ToList();
            var selectedEmployees = task.Employees.Select(x => new TokenInputDto { id = x.EntityId.ToString(), name = x.FullName }).OrderBy(x => x.name);
            var availableEquipment = _repository.FindAll<Equipment>().Select(x => new TokenInputDto { id = x.EntityId.ToString(), name = x.Name }).OrderBy(x => x.name);
            var selectedEquipment = task.Equipment.Select(x => new TokenInputDto { id = x.EntityId.ToString(), name = x.Name }).OrderBy(x => x.name);
            var model = Mapper.Map<Task, TaskViewModel>(task);
            model.Employees = new TokenInputViewModel { _availableItems = availableEmployees, selectedItems = selectedEmployees };
            model.Equipment = new TokenInputViewModel { _availableItems = availableEquipment, selectedItems = selectedEquipment};
            model._FieldEntityIdList = fields;
            model._InventoryProductEntityIdList = products;
            model._TaskTypeEntityIdList = taskTypes;
            model.StartTime = task.StartTime.HasValue?task.StartTime.Value.ToShortTimeString():"";
            model.EndTime = task.EndTime.HasValue ? task.EndTime.Value.ToShortTimeString() : "";
            model.ScheduledDate = task.ScheduledDate.Value.ToShortDateString();
            model._Title = WebLocalizationKeys.TASK_INFORMATION.ToString();
            model.Popup = input.Popup;
            model.RootId = input.RootId;
            model._saveUrl = UrlContext.GetUrlForAction<TaskController>(x => x.Save(null));

            if (input.Copy)
            {
                // entityid is changed on view cuz the entity is in the NH graph here on server
                model.Complete = false;
            }
                
            return new CustomJsonResult(model);
        }



        public ActionResult Display_Template(ViewModel input)
        {
            return View("Display", new DisplayTaskViewModel{Popup = input.Popup});
        }

        public ActionResult Display(ViewModel input)
        {
            var task = _repository.Find<Task>(input.EntityId);
            var model = Mapper.Map<Task, DisplayTaskViewModel>(task);
            model.StartTime = task.StartTime.Value.ToShortTimeString();
            model.EndTime = task.EndTime.HasValue ? task.EndTime.Value.ToShortTimeString() : "";
            model.ScheduledDate = task.ScheduledDate.HasValue ? task.ScheduledDate.Value.ToShortDateString() : "";
            model.Popup = input.Popup;
            model._EmployeeNames = task.Employees.Select(x => x.FullName);
            model._EquipmentNames = task.Equipment.Select(x => x.Name);
            model._AddUpdateUrl = UrlContext.GetUrlForAction<TaskController>(x => x.AddUpdate(null));
            model._Title = WebLocalizationKeys.TASK_INFORMATION.ToString();    
            model._IsChemical = task.InventoryProduct != null && task.InventoryProduct.Product.InstantiatingType == "Chemical";
            model.AllowEdit = task.ScheduledDate.Value.SetTime(task.StartTime.Value.ToShortTimeString()) > DateTime.Now
                              || _authorizationService.IsAllowed(input.User, "/EditPastTask");
            return new CustomJsonResult(model);
        }

        public ActionResult Delete(ViewModel input)
        {
            var task = _repository.Find<Task>(input.EntityId);
            if (!task.Complete)
            {
                _repository.HardDelete(task);
                _repository.UnitOfWork.Commit();
            }
            return null;
        }

        public ActionResult DeleteMultiple(BulkActionViewModel input)
        {
            input.EntityIds.ForEachItem(x =>
            {
                var item = _repository.Find<Task>(x);
                if(!item.Complete) _repository.HardDelete(item);
            });
            _repository.Commit();
            return Json(new Notification { Success = true }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Save(TaskViewModel input)
        {
            var task = input.EntityId > 0 ? _repository.Find<Task>(input.EntityId) : new Task();

            mapItem(task, input);
            mapChildren(task, input);
            IValidationManager validationManager = null;
            if (task.Complete && !task.InventoryDecremented)
            {
                validationManager = _inventoryService.DecrementTaskProduct(task);
                task.InventoryDecremented = true;
            }
            validationManager = _saveEntityService.ProcessSave(task, validationManager??new ValidationManager(_repository));
            var notification = validationManager.Finish();
            return new CustomJsonResult(notification);
        }

        private void mapItem(Task item, TaskViewModel input)
        {
            item.ScheduledDate = DateTime.Parse(input.ScheduledDate);
            item.StartTime = null;
            item.StartTime = DateTime.Parse(input.ScheduledDate + " " + input.StartTime);
            item.EndTime = null;
            if(!string.IsNullOrEmpty(input.EndTime))
            {
                item.EndTime = DateTime.Parse(input.ScheduledDate + " " + input.EndTime);
            }
            item.ActualTimeSpent = input.ActualTimeSpent;
            item.QuantityNeeded = input.QuantityNeeded;
            item.QuantityUsed = input.QuantityUsed;
            item.Notes = input.Notes;
            item.Complete = input.Complete;
            item.TargetPest = input.TargetPest;
            item.RatePerUnit = input.RatePerUnit;
            item.SprayPermitNumber = input.SprayPermitNumber;
            item.ApplicationRequestedBy = input.ApplicationRequestedBy;
            
        }

        private void mapChildren(Task task,TaskViewModel model)
        {
            _updateCollectionService.Update(task.Employees, model.Employees, task.AddEmployee, task.RemoveEmployee);
            _updateCollectionService.Update(task.Equipment, model.Equipment, task.AddEquipment, task.RemoveEquipment);

            task.TaskType = model.TaskTypeEntityId>0? _repository.Find<TaskType>(model.TaskTypeEntityId):null;
            task.Field = model.FieldEntityId>0?_repository.Find<Field>(model.FieldEntityId):null;
            task.InventoryProduct = model.InventoryProductEntityId>0?_repository.Find<InventoryProduct>(model.InventoryProductEntityId):null;
        }
    }

    
}