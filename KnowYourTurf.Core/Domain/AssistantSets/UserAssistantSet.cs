using System;
using System.Collections.Generic;

namespace DecisionCritical.Core.Domain
{
    public class UserAssistantSet:DomainEntity
    {
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual int PercentComplete { get; set; }
        public virtual User User { get; set; }
        public virtual AssistantSet AssistantSet { get; set; }
        public virtual DateTime? CompleteByDate { get; set; }

        #region Collections
        private readonly IList<UserAssistantItem> _userAssistantItems = new List<UserAssistantItem>();
        public virtual IEnumerable<UserAssistantItem> GetUserAssistantItems() { return _userAssistantItems; }
        public virtual void RemoveUserAssistantItem(UserAssistantItem userAssistantItem) { _userAssistantItems.Remove(userAssistantItem); }
        public virtual void AddUserAssistantItem(UserAssistantItem userAssistantItem)
        {
            if (!userAssistantItem.IsNew() && _userAssistantItems.Contains(userAssistantItem)) return;
            _userAssistantItems.Add(userAssistantItem);
        }
        #endregion
    }
}