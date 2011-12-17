using System;
using KnowYourTurf.Core.Domain.Tools.CustomAttributes;
using KnowYourTurf.Core.Html.Grid;


namespace KnowYourTurf.Core.Domain
{
    public class DomainEntity : Entity
    {
        public virtual int CompanyId { get; set; }
    }

    public class Entity :  IGridEnabledClass, IEquatable<Entity>
    {
        
        public virtual int EntityId { get; set; }
        
        
        [ValidateSqlDateTime]
        public virtual DateTime? CreateDate { get; set; }

        [ValidateSqlDateTime]
        public virtual DateTime? ChangeDate { get; set; }

        
        //private DateTime? _createDate;
        //[ValidateSqlDateTime]
        //public virtual DateTime? CreateDate
        //{
        //    get
        //    {   if (_createDate == null)
        //            { return System.DateTime.Now; }
        //        else
        //            { return _createDate; }
        //    }
        //    set { _createDate = value; }
        //}



        ////private DateTime _changeDate = System.DateTime.Now;
        ////pzt
        //private DateTime? _changeDate;
        //[ValidateSqlDateTime]
        //public virtual DateTime? ChangeDate
        //{
        //    get
        //    {
        //        if (_changeDate == null)
        //        { return System.DateTime.Now; }
        //        else
        //        { return _changeDate; }
        //    }
        //    set { _changeDate = value; }
        //}



        public virtual int ChangedBy { get; set; }

        [System.ComponentModel.DefaultValue(0)] //pzt
        public virtual bool Archived { get; set; }

        public Entity()
        {
            
        }

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
            return Equals((Entity) obj);
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

    public interface ILookupType
    {
        int EntityId { get; set; }
        string Name { get; set; }
    }
}