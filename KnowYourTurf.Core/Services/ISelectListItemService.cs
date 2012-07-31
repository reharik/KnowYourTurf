using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using FubuMVC.Core;
using FubuMVC.Core.Util;
using KnowYourTurf.Core.CoreViewModels;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Enums;
using KnowYourTurf.Core.Localization;
using NHibernate.Linq;

namespace KnowYourTurf.Core.Services
{
    public interface ISelectListItemService
    {
        IEnumerable<SelectListItem> CreateList<ENTITY>(IEnumerable<ENTITY> entityList,
                                                       Expression<Func<ENTITY, object>> text,
                                                       Expression<Func<ENTITY, object>> value, bool addSelectItem);

        IEnumerable<SelectListItem> CreateList<ENTITY>(Expression<Func<ENTITY, object>> text, Expression<Func<ENTITY, object>> value, bool addSelectItem, bool softDelete = false)
            where ENTITY : IPersistableObject;

        IEnumerable<SelectListItem> CreateList<ENUM>(bool addSelectItem = false) where ENUM : Enumeration, new();

        IEnumerable<SelectListItem> SetSelectedItemByValue(IEnumerable<SelectListItem> entityList,
                                                                   string value);

        IEnumerable<SelectListItem> CreateListWithConcatinatedText<ENTITY>(IEnumerable<ENTITY> entityList,
                                                                           Expression<Func<ENTITY, object>> text1,
                                                                           Expression<Func<ENTITY, object>> text2,
                                                                           string seperator,
                                                                           Expression<Func<ENTITY, object>> value,
                                                                           bool addSelectItem);

        IEnumerable<SelectListItem> CreateListWithConcatinatedText<ENTITY>(Expression<Func<ENTITY, object>> text1, Expression<Func<ENTITY, object>> text2, string seperator, Expression<Func<ENTITY, object>> value, bool addSelectItem) where ENTITY : IPersistableObject;
        GroupSelectViewModel CreateFieldsSelectListItems(long categoryId = 0, long fieldId = 0);
    }

    public class SelectListItemService : ISelectListItemService
    {
        private readonly IRepository _repository;

        public SelectListItemService(IRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<SelectListItem> CreateList<ENTITY>(IEnumerable<ENTITY> entityList, 
                                                              Expression<Func<ENTITY, object>> text,
                                                              Expression<Func<ENTITY, object>> value, bool addSelectItem) 
            
        {

            IList<SelectListItem> items = new List<SelectListItem>();
            Accessor textAccessor = text.ToAccessor();
            Accessor valueAccessor = value.ToAccessor();
            if (addSelectItem)
            {
                items.Add(new SelectListItem { Text = CoreLocalizationKeys.SELECT_ITEM.ToString(), Value = "" });
            }
            entityList.ForEachItem( x =>
                                 {
                                     if (textAccessor.GetValue(x) != null && valueAccessor.GetValue(x) != null)
                                     {
                                         items.Add(new SelectListItem
                                                       {
                                                           Text = textAccessor.GetValue(x).ToString(),
                                                           Value = valueAccessor.GetValue(x).ToString()
                                                       });
                                     }
                                 });
            return items.OrderBy(x => x.Text);
        }

        public IEnumerable<SelectListItem> CreateListWithConcatinatedText<ENTITY>(IEnumerable<ENTITY> entityList,
                                                              Expression<Func<ENTITY, object>> text1,
                                                              Expression<Func<ENTITY, object>> text2,
                                                              string seperator,
                                                              Expression<Func<ENTITY, object>> value, bool addSelectItem)
        {

            IList<SelectListItem> items = new List<SelectListItem>();
            Accessor text1Accessor = text1.ToAccessor();
            Accessor text2Accessor = text2.ToAccessor();
            Accessor valueAccessor = value.ToAccessor();
            if (addSelectItem)
            {
                items.Add(new SelectListItem { Text = CoreLocalizationKeys.SELECT_ITEM.ToString(), Value = "" });
            }
            entityList.ForEachItem(x =>
            {
                if (text1Accessor.GetValue(x) != null && text2Accessor.GetValue(x) != null && valueAccessor.GetValue(x) != null)
                {
                    items.Add(new SelectListItem
                    {
                        Text = text1Accessor.GetValue(x).ToString()+seperator+text2Accessor.GetValue(x),
                        Value = valueAccessor.GetValue(x).ToString()
                    });
                }
            });
            return items.OrderBy(x => x.Text);
        }

        public IEnumerable<SelectListItem> CreateListWithConcatinatedText<ENTITY>(Expression<Func<ENTITY, object>> text1, Expression<Func<ENTITY, object>> text2, string seperator, Expression<Func<ENTITY, object>> value, bool addSelectItem) where ENTITY : IPersistableObject
        {
            var enumerable = _repository.FindAll<ENTITY>();
            return CreateListWithConcatinatedText(enumerable, text1, text2, seperator, value, addSelectItem);
        }

        public IEnumerable<SelectListItem> CreateList<ENTITY>(Expression<Func<ENTITY, object>> text, Expression<Func<ENTITY, object>> value, bool addSelectItem, bool softDelete = false) where ENTITY : IPersistableObject
        {
            var enumerable = _repository.FindAll<ENTITY>();
            return CreateList(enumerable, text, value, addSelectItem);
        }

        public IEnumerable<SelectListItem> CreateList<ENUM>(bool addSelectItem = false) where ENUM:Enumeration, new()
        {
            IEnumerable<Enumeration> enumerations = Enumeration.GetAllActive<ENUM>();
            if (enumerations == null) return null;
            enumerations = enumerations.OrderBy(item => item.Key);
            var items = enumerations.Select(x=> new SelectListItem{Text = x.Key, Value = x.Value.IsEmpty() ? x.Key : x.Value}).ToList();
            if (addSelectItem)
            {
                items.Insert(0, new SelectListItem { Text = CoreLocalizationKeys.SELECT_ITEM.ToString(), Value = "" });
            }
            return items;
        }

        public IEnumerable<SelectListItem> SetSelectedItemByValue(IEnumerable<SelectListItem> entityList, string value)
        {
            SelectListItem selectListItem = entityList.FirstOrDefault(x => x.Value == value);
            if (selectListItem != null)
            {
                selectListItem.Selected = true;
            }
            return entityList;
        }

        public GroupSelectViewModel CreateFieldsSelectListItems(long categoryId = 0, long fieldId = 0)
        {
            var groups = new GroupSelectViewModel();
            if (fieldId > 0 && categoryId == 0)
            {
                var field = _repository.Find<Field>(fieldId);
                categoryId = field.ReadOnlyCategory.EntityId;
            }
            if (categoryId > 0)
            {
                var category = _repository.Find<Category>(categoryId);
                var list = CreateList(category.Fields, x => x.Name, x => x.EntityId, false);
                groups.groups.Add(new SelectGroup{label = category.Name, children = list});
            }
            else
            {
                var categories = _repository.FindAll<Category>().AsQueryable().Fetch(x => x.Fields);
                categories.ForEachItem(x =>
                {
                    var list = CreateList(x.Fields, f => f.Name, f => f.EntityId, false);
                    groups.groups.Add(new SelectGroup{label = x.Name, children = list});

                });
            }
            return groups;
        }
    }
}