using System;
using KnowYourTurf.Core.Html.Grid;
using KnowYourTurf.Core.Services;

namespace KnowYourTurf.Core.Domain
{
    public abstract class Entity: IEquatable<Entity>
    {
        public virtual long EntityId { get; set; }
        public virtual DateTime? DateCreated { get; set; }
        public virtual DateTime? LastModified { get; set; }
        public virtual long CreatedBy { get; set; }
        public virtual long ModifiedBy { get; set; }
        public virtual bool IsDeleted { get; set; }
        public virtual bool IsNew()
        {
            return EntityId == 0;
        }

        public virtual void UpdateSelf(Entity entity)
        {
            throw new NotImplementedException();
        }

        #region IEquatable
        public virtual bool Equals(Entity obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (GetType() != obj.GetType()) return false;
            return obj.EntityId == EntityId;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (GetType() != obj.GetType()) return false;
            return Equals((Entity)obj);
        }
        public override int GetHashCode()
        {
            return EntityId.GetHashCode();
        }

        public static bool operator ==(Entity left, Entity right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Entity left, Entity right)
        {
            return !Equals(left, right);
        }
        #endregion
    
    }

    public abstract class DomainEntity : Entity, IGridEnabledClass
    {
        public virtual long CompanyId { get; set; }
    }

}