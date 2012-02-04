using System;
using System.Collections.Generic;
using Castle.Components.Validator;

namespace DecisionCritical.Core.Domain
{
    public class UserAssistantItem : DomainEntity
    {
        public virtual string FriendlyName { get; set; }
        public virtual string Description { get; set; }
        public virtual string Instructions { get; set; }
        public virtual string TypeName { get; set; }
        public virtual string Discriminator { get; set; }
        public virtual bool Finished { get; set; }
        public virtual UserAssistantSet UserAssistantSet { get; set; }
        public virtual AssistantItem AssistantItem { get; set; }
        #region Collections
        private readonly IList<UserAssistantItemInstance> _userAssistantItemInstances = new List<UserAssistantItemInstance>();
        public virtual void EmptyUserAssistantItemInstances()
        {
            _userAssistantItemInstances.Each(x => x.UserAssistantItem = null);
            _userAssistantItemInstances.Clear();
        }
        public virtual IEnumerable<UserAssistantItemInstance> GetUserAssistantItemInstances() { return _userAssistantItemInstances; }
        public virtual void RemoveUserAssistantItemInstance(UserAssistantItemInstance userAssistantItemInstance) { _userAssistantItemInstances.Remove(userAssistantItemInstance); }
        public virtual void AddUserAssistantItemInstance(UserAssistantItemInstance userAssistantItemInstance)
        {
            // this has iequatable overridden and checking for portitemid which we want not to duplicate
            if ( _userAssistantItemInstances.Contains(userAssistantItemInstance)) return;
            _userAssistantItemInstances.Add(userAssistantItemInstance);
        }
        #endregion
    }

    public class UserAssistantItemInstance : DomainEntity, IEquatable<UserAssistantItemInstance>
    {
        public virtual int Id { get; set; }
        [ValidateNonEmpty]
        public virtual UserAssistantItem UserAssistantItem { get; set; }

        #region IEquatable
        public virtual bool Equals(UserAssistantItemInstance obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (GetType() != obj.GetType()) return false;
            return obj.Id == Id;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (GetType() != obj.GetType()) return false;
            return Equals((UserAssistantItemInstance)obj);
        }

        public override int GetHashCode()
        {
            return EntityId.GetHashCode();
        }

        public static bool operator ==(UserAssistantItemInstance left, UserAssistantItemInstance right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(UserAssistantItemInstance left, UserAssistantItemInstance right)
        {
            return !Equals(left, right);
        }
        #endregion
    }
}
