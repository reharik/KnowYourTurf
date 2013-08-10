using System.Collections.Generic;
using Castle.Components.Validator;

namespace DecisionCritical.Core.Domain
{
    public class AssistantSet:DomainEntity
    {
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        [ValidateNonEmpty]
        public virtual Profile Profile { get; set; }

        #region Collections
        private readonly IList<AssistantItem> _assistantItems = new List<AssistantItem>();
        public virtual IEnumerable<AssistantItem> GetAssistantItems() { return _assistantItems; }
        public virtual void RemoveAssistantItem(AssistantItem assistantItem)
        {
            _assistantItems.Remove(assistantItem);
        }
        public virtual void AddAssistantItem(AssistantItem assistantItem)
        {
            if (!assistantItem.IsNew() && _assistantItems.Contains(assistantItem)) return;
            _assistantItems.Add(assistantItem);
        }
        #endregion
    }
}