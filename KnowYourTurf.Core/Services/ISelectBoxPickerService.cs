using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using FubuMVC.Core;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Enums;
using KnowYourTurf.Core.Localization;

namespace KnowYourTurf.Core.Services
{
    public interface ISelectBoxPickerService
    {
        SelectBoxPickerDto GetPickerDto<ENTITY>(IEnumerable<ENTITY> selectedItems, Expression<Func<ENTITY, object>> Text, Expression<Func<ENTITY, object>> Value, bool softDelete = false) where ENTITY : DomainEntity;
        SelectBoxPickerDto GetPickerDto<ENUM>(IEnumerable<string> selectedItems) where ENUM : Enumeration, new();
        IEnumerable<ENTITY> GetListOfSelectedEntities<ENTITY>(SelectBoxPickerDto dto)where ENTITY :DomainEntity;
        SelectBoxPickerDto PreSelectEntity<ENTITY>(SelectBoxPickerDto dto, string itemValue) where ENTITY : DomainEntity;
    }
    public class SelectBoxPickerService : ISelectBoxPickerService
    {
        private readonly ISelectListItemService _selectListItemService;
        private readonly IRepository _repository;

        public SelectBoxPickerService(ISelectListItemService selectListItemService, IRepository repository)
        {
            _selectListItemService = selectListItemService;
            _repository = repository;
        }

        public SelectBoxPickerDto GetPickerDto<ENTITY>(IEnumerable<ENTITY> selectedItems, Expression<Func<ENTITY, object>> text, Expression<Func<ENTITY, object>> value, bool softDelete = false) where ENTITY : DomainEntity
        {
            var allItems = _selectListItemService.CreateList(text, value, false,softDelete).ToList();
            var _selectedItems = _selectListItemService.CreateList(selectedItems, text, value, false);
            _selectedItems.ForEachItem(x =>
                                    {
                                        var availableListItem = allItems.FirstOrDefault(a=>a.Text==x.Text && a.Value==x.Value);
                                        if (availableListItem!=null) allItems.Remove(availableListItem);
                                    });
            return new SelectBoxPickerDto
            {
                SelectedListItems = _selectedItems.ToList(),
                AvailableListItems = allItems,
                EntityTypeName = typeof(ENTITY).Name
            };
        }

        public SelectBoxPickerDto GetPickerDto<ENUM>(IEnumerable<string> selectedItems) where ENUM : Enumeration, new()
        {
            var allItems = _selectListItemService.CreateList<ENUM>().ToList();
            var _selectedItems = selectedItems.Select(x => new SelectListItem {Text = x, Value = x});
            _selectedItems.ForEachItem(x =>
            {
                var availableListItem = allItems.FirstOrDefault(a => a.Text == x.Text && a.Value == x.Value);
                if (availableListItem != null) allItems.Remove(availableListItem);
            });
            return new SelectBoxPickerDto
            {
                SelectedListItems = _selectedItems.ToList(),
                AvailableListItems = allItems,
                EntityTypeName = typeof(ENUM).Name
            };
        }

        public IEnumerable<ENTITY> GetListOfSelectedEntities<ENTITY>(SelectBoxPickerDto dto) where ENTITY : DomainEntity
        {
            var result = new List<ENTITY>();
            if (dto ==null || dto.Selected == null) return result;
            dto.Selected.ForEachItem(x =>
            {
                var entity = _repository.Find<ENTITY>(Int64.Parse(x));
                result.Add(entity);
            });
            return result;
        }
        
        public SelectBoxPickerDto PreSelectEntity<ENTITY>(SelectBoxPickerDto dto, string itemValue) where ENTITY : DomainEntity
        {
            var newDto = dto;
            var item = newDto.AvailableListItems.FirstOrDefault(x => x.Value == itemValue);
            newDto.AvailableListItems.Remove(item);
            newDto.SelectedListItems.Add(item);
            return newDto;
        }
    }

    public class SelectBoxPickerDto
    {
        public IList<SelectListItem> SelectedListItems { get; set; }
        public IList<SelectListItem> AvailableListItems { get; set; }
        public IEnumerable<string> Selected { get; set; }

        public string EntityTypeName { get; set; }

        public IEnumerable<string> DisplayItems { get; set; }
    }
}