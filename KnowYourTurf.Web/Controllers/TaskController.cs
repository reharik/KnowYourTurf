using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using FluentNHibernate.Utils;
using KnowYourTurf.Core;
using KnowYourTurf.Core.CoreViewModels;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Enums;
using KnowYourTurf.Core.Html;
using KnowYourTurf.Core.Services;
using KnowYourTurf.Web.Models;
using NHibernate.Linq;

namespace KnowYourTurf.Web.Controllers
{
    public class TaskController : KYTController
    {
        private readonly IRepository _repository;
        private readonly ISaveEntityService _saveEntityService;
        private readonly ISelectListItemService _selectListItemService;
        private readonly IInventoryService _inventoryService;

        public TaskController(IRepository repository,
            ISaveEntityService saveEntityService,
            ISelectListItemService selectListItemService,
            IInventoryService inventoryService)
        {
            _repository = repository;
            _saveEntityService = saveEntityService;
            _selectListItemService = selectListItemService;
            _inventoryService = inventoryService;
        }

        public ActionResult AddUpdate(AddUpdateTaskViewModel input)
        {
            var task = input.EntityId > 0 ? _repository.Find<Task>(input.EntityId) : new Task();
            task.ScheduledDate = input.ScheduledDate.HasValue ? input.ScheduledDate.Value : task.ScheduledDate;
            task.ScheduledStartTime= input.ScheduledStartTime.HasValue ? input.ScheduledStartTime.Value: task.ScheduledStartTime;
            var taskTypes = _selectListItemService.CreateList<TaskType>(x => x.Name, x => x.EntityId, true);
            var fields = _selectListItemService.CreateFieldsSelectListItems(input.RootId,input.ParentId);
            var products = createProductSelectListItems();
            var availableEmployees = _repository.Query<User>(x => x.UserLoginInfo.Status == Status.Active.ToString() && x.UserRoles.Any(y=>y.Name==UserType.Employee.ToString()))
                .Select(x => new TokenInputDto { id = x.EntityId.ToString(), name = x.FirstName + " " + x.LastName }).OrderBy(x=>x.name).ToList();
            var selectedEmployees = task.Employees.Select(x => new TokenInputDto { id = x.EntityId.ToString(), name = x.FullName }).OrderBy(x => x.name);
            var availableEquipment = _repository.FindAll<Equipment>().Select(x => new TokenInputDto { id = x.EntityId.ToString(), name = x.Name }).OrderBy(x => x.name);
            var selectedEquipment = task.Equipment.Select(x => new TokenInputDto { id = x.EntityId.ToString(), name = x.Name }).OrderBy(x => x.name);
            var model = new TaskViewModel
                            {//strangly I have to itterate this or NH freaks out
                                AvailableEmployees = availableEmployees.ToList(),
                                SelectedEmployees = selectedEmployees.ToList(),
                                AvailableEquipment = availableEquipment.ToList(),
                                SelectedEquipment = selectedEquipment,
                                FieldList = fields,
                                ProductList = products,
                                TaskTypeList = taskTypes,
                                Item = task,
                                Title = WebLocalizationKeys.TASK_INFORMATION.ToString(),
                                Popup = input.Popup,
                                RootId = input.RootId,

            };

            if (input.Copy)
            {
                // entityid is changed on view cuz the entity is in the NH graph here on server
                model.Item.Complete = false;
            }
                
            return PartialView("TaskAddUpdate", model);
        }

        

        private Dictionary<string, IEnumerable<SelectListItem>> createProductSelectListItems()
        {
            var dictionary = new Dictionary<string, IEnumerable<SelectListItem>>();
            IEnumerable<InventoryProduct> inventory = _repository.FindAll<InventoryProduct>();
            var chemicals =
                _selectListItemService.CreateListWithConcatinatedText(
                    inventory.Where(i => i.ReadOnlyProduct.InstantiatingType == "Chemical"),
                    x => x.ReadOnlyProduct.Name,
                    x => x.UnitType,
                    "-->",
                    y => y.EntityId,
                    false);
            var fertilizer =
                _selectListItemService.CreateListWithConcatinatedText(
                    inventory.Where(i => i.ReadOnlyProduct.InstantiatingType == "Fertilizer"),
                    x => x.ReadOnlyProduct.Name,
                    x => x.UnitType,
                    "-->",
                    x => x.EntityId,
                    false);
            var materials =
                _selectListItemService.CreateListWithConcatinatedText(
                    inventory.Where(i => i.ReadOnlyProduct.InstantiatingType == "Material"),
                    x => x.ReadOnlyProduct.Name,
                    x => x.UnitType,
                    "-->",
                    x => x.EntityId,
                    false);

            dictionary.Add(WebLocalizationKeys.CHEMICALS.ToString(), chemicals);
            dictionary.Add(WebLocalizationKeys.MATERIALS.ToString(), materials);
            dictionary.Add(WebLocalizationKeys.FERTILIZERS.ToString(), fertilizer);
            return dictionary;
        }

        public ActionResult Display(ViewModel input)
        {
            var task = _repository.Find<Task>(input.EntityId);
//            var productName = task.GetProductName();
            var model = new TaskViewModel
                            {
                                Popup = input.Popup,
                                Item = task,
//                                Product = productName,
                                EmployeeNames = task.Employees.Select(x =>  x.FullName ),
                                EquipmentNames = task.Equipment.Select(x => x.Name ),
                                AddUpdateUrl = UrlContext.GetUrlForAction<TaskController>(x=>x.AddUpdate(null))+"/"+task.EntityId,
                                Title = WebLocalizationKeys.TASK_INFORMATION.ToString()

            };
            return PartialView( model);
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
            input.EntityIds.Each(x =>
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

            mapItem(task, input.Item);
            mapChildren(task, input);
            ICrudManager crudManager = null;
            if (task.Complete && !task.InventoryDecremented)
            {
                crudManager = _inventoryService.DecrementTaskProduct(task);
                task.InventoryDecremented = true;
            }
            crudManager = _saveEntityService.ProcessSave(task, crudManager);
            var notification = crudManager.Finish();
            return Json(notification);
        }

        private void mapItem(Task item, Task input)
        {
            item.ScheduledDate = input.ScheduledDate;
            item.ScheduledStartTime = null;
            if (input.ScheduledStartTime.HasValue)
                item.ScheduledStartTime =
                    DateTime.Parse(input.ScheduledDate.Value.ToShortDateString() + " " +
                                   input.ScheduledStartTime.Value.ToShortTimeString());
            item.ScheduledEndTime = null;
            if (input.ScheduledEndTime.HasValue)
            {
                item.ScheduledEndTime = DateTime.Parse(input.ScheduledDate.Value.ToShortDateString() + " " + input.ScheduledEndTime.Value.ToShortTimeString());
            }
            item.ActualTimeSpent = input.ActualTimeSpent;
            item.QuantityNeeded = input.QuantityNeeded;
            item.QuantityUsed = input.QuantityUsed;
            item.Notes = input.Notes;
            item.Complete = input.Complete;
            
        }

        private void mapChildren(Task task,TaskViewModel model)
        {
            task.ClearEmployees();
            task.ClearEquipment(); 
            if(model.EmployeeInput.IsNotEmpty())
                model.EmployeeInput.Split(',').Each(x => task.AddEmployee(_repository.Find<User>(Int32.Parse(x))));
            if(model.EquipmentInput.IsNotEmpty())
                model.EquipmentInput.Split(',').Each(x => task.AddEquipment(_repository.Find<Equipment>(Int32.Parse(x))));

            task.TaskType = _repository.Find<TaskType>(model.Item.TaskType.EntityId);
            task.SetField(_repository.Find<Field>(model.Item.ReadOnlyField.EntityId));
//            if (model.Product!=null && model.Product.Contains("_"))
//            {
//                var product = model.Product.Split('_');
//            task.InventoryProduct = _repository.Find<InventoryProduct>(Int64.Parse(product[0]));
            task.SetInventoryProduct(_repository.Find<InventoryProduct>(model.Item.ReadOnlyInventoryProduct.ReadOnlyProduct.EntityId));
//            }else
//            {
//                task.InventoryProduct = null;
//            }
        }
    }
}