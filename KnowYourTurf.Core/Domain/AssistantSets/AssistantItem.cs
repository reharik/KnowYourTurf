using System;
using System.Collections.Generic;
using System.Linq;
using Castle.Components.Validator;
using FluentNHibernate.Utils.Reflection;

namespace DecisionCritical.Core.Domain
{
    public class AssistantItem:DomainEntity
    {
        public virtual string FriendlyName { get; set; }
        public virtual string Description { get; set; }
        public virtual int InstancesRequired { get; set; }
        public virtual PortfolioItem PortfolioItem { get; set; }
        public virtual int ItemIndex { get; set; }

        #region Collections
        private readonly IList<AssistantItemRequiredField> _assistantItemRequiredFields = new List<AssistantItemRequiredField>();
        public virtual IEnumerable<AssistantItemRequiredField> GetAssistantItemRequiredFields() { return _assistantItemRequiredFields; }
        public virtual void RemoveAssistantItemRequiredField(AssistantItemRequiredField assistantItemRequiredField)
        {
            _assistantItemRequiredFields.Remove(assistantItemRequiredField);
        }
        public virtual void AddAssistantItemRequiredField(AssistantItemRequiredField assistantItemRequiredField)
        {
            if (!assistantItemRequiredField.IsNew() && _assistantItemRequiredFields.Contains(assistantItemRequiredField)) return;
            _assistantItemRequiredFields.Add(assistantItemRequiredField);
        }
        #endregion

        public virtual IEnumerable<RequiredItemsDto> getRequiredItemsDtos()
        {
            var items = new List<RequiredItemsDto>();
            var type = Type.GetType("DecisionCritical.Core.Domain." + PortfolioItem.TypeName);
            var allProperties = type.GetProperties();
            allProperties.Each(x =>
                                   {

                                       var requiredByDefault = x.GetCustomAttributes(typeof(ValidateNonEmptyAttribute), true).Any();
                                       var AssistantItemRequiredFields = GetAssistantItemRequiredFields().Where(f => f.PropertyName == x.Name).Any();
                                       items.Add(new RequiredItemsDto()
                                                     {
                                                         PropertyName = x.Name,
                                                         Required = requiredByDefault || AssistantItemRequiredFields,
                                                         RequiredByDefault = requiredByDefault
                                                     });
                                   });
            return items;
        }
    }

    public class RequiredItemsDto
    {
        public string PropertyName { get; set; }
        public bool Required { get; set; }
        public bool RequiredByDefault { get; set; }
    }
}