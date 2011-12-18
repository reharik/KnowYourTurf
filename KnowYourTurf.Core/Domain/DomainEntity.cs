using System;
using KnowYourTurf.Core.Html.Grid;
using KnowYourTurf.Core.Services;

namespace KnowYourTurf.Core.Domain
{
    public abstract class DomainEntity : IEquatable<DomainEntity>, IGridEnabledClass
    {
        public virtual long EntityId { get; set; }
        public virtual DateTime? DateCreated { get; set; }
        public virtual DateTime? LastModified { get; set; }
        public virtual User CreatedBy { get; set; }
        public virtual User ModifiedBy { get; set; }
        public virtual long CompanyId { get; set; }
        public virtual bool IsDeleted { get; set; }
        public virtual bool IsNew()
        {
            return EntityId == 0;
        }


        #region IEquatable
        public virtual bool Equals(DomainEntity obj)
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
            return Equals((DomainEntity)obj);
        }

        public override int GetHashCode()
        {
            return EntityId.GetHashCode();
        }

        public static bool operator ==(DomainEntity left, DomainEntity right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(DomainEntity left, DomainEntity right)
        {
            return !Equals(left, right);
        } 
        #endregion
    }

    public static class DomainEntityExtensions
    {
        public static void MarkModified(this object entity, ISystemClock clock, IGetCompanyIdService getCompanyIdService)
        {
            var domainEntity = entity as DomainEntity;
            if (domainEntity == null) return;
            domainEntity.LastModified = clock.Now;

            if (!domainEntity.DateCreated.HasValue)
            {
                domainEntity.DateCreated = clock.Now;
            }

            if(domainEntity.CompanyId<=0)
            {
                domainEntity.CompanyId = getCompanyIdService.Execute();
            }
        }
    }
}