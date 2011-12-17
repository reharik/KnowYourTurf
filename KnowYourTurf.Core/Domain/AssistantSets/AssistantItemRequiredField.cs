using System;

namespace DecisionCritical.Core.Domain
{
    public class AssistantItemRequiredField : DomainEntity, IEquatable<AssistantItemRequiredField>
    {
        public virtual string PropertyName{ get; set; }

        #region IEquatable
        public virtual bool Equals(AssistantItemRequiredField obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (GetType() != obj.GetType()) return false;
            return obj.PropertyName == PropertyName;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (GetType() != obj.GetType()) return false;
            return Equals((AssistantItemRequiredField)obj);
        }

        public override int GetHashCode()
        {
            return EntityId.GetHashCode();
        }

        public static bool operator ==(AssistantItemRequiredField left, AssistantItemRequiredField right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(AssistantItemRequiredField left, AssistantItemRequiredField right)
        {
            return !Equals(left, right);
        }
        #endregion
    
    }
}