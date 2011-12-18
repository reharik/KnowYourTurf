using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using KnowYourTurf.Core;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Html;
using KnowYourTurf.Core.Services;
using KnowYourTurf.Web.Models;

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

        public ActionResult AddEdit(AddEditTaskViewModel input)
        {
            var task = input.EntityId > 0 ? _repository.Find<Task>(input.EntityId) : new Task();
            task.ScheduledDate = input.ScheduledDate.HasValue ? input.ScheduledDate.Value : task.ScheduledDate;
            task.ScheduledStartTime= input.ScheduledStartTime.HasValue ? input.ScheduledStartTime.Value: task.ScheduledStartTime;
            var fields = _selectListItemService.CreateList<Field>(x => x.Name, x => x.EntityId, true,true);
            var taskTypes = _selectListItemService.CreateList<TaskType>(x => x.Name, x => x.EntityId, true);

            var dictionary = new Dictionary<string, IEnumerable<SelectListItem>>();
            IEnumerable<InventoryProduct> inventory = _repository.FindAll<InventoryProduct>();
            var chemicals = _selectListItemService.CreateListWithConcatinatedText(inventory.Where(i=>i.Product.InstantiatingType=="Chemical"),
                x => x.Product.Name,
                x => x.UnitType,
                "-->",
                y => y.EntityId, 
                false);
            var fertilizer = _selectListItemService.CreateListWithConcatinatedText(inventory.Where(i => i.Product.InstantiatingType == "Fertilizer"),
                x => x.Product.Name,
                x => x.UnitType,
                "-->", 
                x => x.EntityId,
                false);
            var materials = _selectListItemService.CreateListWithConcatinatedText(inventory.Where(i => i.Product.InstantiatingType == "Material"),
                x => x.Product.Name,
                x => x.UnitType,
                "-->", 
                x => x.EntityId, 
                false);
            
            dictionary.Add(WebLocalizationKeys.CHEMICALS.ToString(), chemicals);
            dictionary.Add(WebLocalizationKeys.MATERIALS.ToString(), materials);
            dictionary.Add(WebLocalizationKeys.FERTILIZERS.ToString(), fertilizer);
            var availableEmployees = _repository.FindAll<User>().Select(x => new TokenInputDto { id = x.EntityId.ToString(), name = x.FullName });
            var selectedEmployees = task.Employees.Select(x => new TokenInputDto { id = x.EntityId.ToString(), name = x.FullName });
            var availableEquipment = _repository.FindAll<Equipment>().Select(x => new TokenInputDto { id = x.EntityId.ToString(), name = x.Name });
            var selectedEquipment = task.Equipment.Select(x => new TokenInputDto { id = x.EntityId.ToString(), name = x.Name });
            
            var model = new TaskViewModel
                            {//strangly I have to itterate this or NH freaks out
                                AvailableEmployees = availableEmployees.ToList(),
                                SelectedEmployees = selectedEmployees.ToList(),
                                AvailableEquipment = availableEquipment.ToList(),
                                SelectedEquipment = selectedEquipment,
                                FieldList = fields,
                                ProductList = dictionary,
                                TaskTypeList = taskTypes,
                                Task = task,
                                Title = WebLocalizationKeys.TASK_INFORMATION.ToString()
            };
            if (task.EntityId > 0)
            {
                model.Product = task.GetProductIdAndName();
            }
            decorateModel(input,model);
            if (input.Copy)
            {
                model.Task = model.Task.CloneTask();
                model.Task.Complete = false;
            }
                
            return PartialView("TaskAddUpdate", model);
        }

        private void decorateModel(AddEditTaskViewModel input, TaskViewModel model)
        { 
            if (input.From == "Field")
            {
                if (model.Task.EntityId <= 0)
                    model.Task.Field = _repository.Find<Field>(input.ParentId);
            }
            if (input.From == "Employee")
            {
                if (model.Task.EntityId <= 0)
                {
                    var employee = _repository.Find<User>(input.ParentId);
                    model.SelectedEmployees.Add(new TokenInputDto { id = employee.EntityId.ToString(), name = employee.FullName });
                }
            }
            if (input.From == "Calculator")
            {
                model.Task.Field = _repository.Find<Field>(input.Field);
                model.Product = input.Product;
                model.Task.QuantityNeeded = input.Quantity;
            }
        }

        public ActionResult Display(ViewModel input)
        {
            var task = _repository.Find<Task>(input.EntityId);
            var productName = task.GetProductName();
            var model = new TaskViewModel
                            {
                                Task = task,
                                Product = productName,
                                EmployeeNames = task.Employees.Select(x =>  x.FullName ),
                                EquipmentNames = task.Equipment.Select(x => x.Name ),
                                AddEditUrl = UrlContext.GetUrlForAction<TaskController>(x=>x.AddEdit(null))+"/"+task.EntityId,
                                Title = WebLocalizationKeys.TASK_INFORMATION.ToString()

            };
            return PartialView("TaskView", model);
        }

        public ActionResult Delete(ViewModel input)
        {
            var task = _repository.Find<Task>(input.EntityId);
            _repository.HardDelete(task);
            _repository.UnitOfWork.Commit();
            return null;
        }

        public ActionResult Save(TaskViewModel input)
        {
            var task = input.Task.EntityId > 0? _repository.Find<Task>(input.Task.EntityId):new Task();
            mapItem(task, input.Task);
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

            task.TaskType = _repository.Find<TaskType>(model.Task.TaskType.EntityId);
            task.Field = _repository.Find<Field>(model.Task.Field.EntityId);
            if (model.Product.IsNotEmpty())
            {
                var product = model.Product.Split('_');
                task.InventoryProduct= _repository.Find<InventoryProduct>(Int64.Parse(product[0]));
            }else
            {
                task.InventoryProduct = null;
            }
        }
    }
}