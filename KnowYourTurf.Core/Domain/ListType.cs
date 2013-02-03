using CC.Core.Domain;
using CC.Core.Localization;
using Castle.Components.Validator;
using KnowYourTurf.Core.Enums;

namespace KnowYourTurf.Core.Domain
{
    public interface IListType { }
    
    public class ListType:DomainEntity, IPersistableObject, IListType
    {
        [ValidateNonEmpty]
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        [ValueOf(typeof(Status))]
        public virtual string Status { get; set; }
    }

    public class DocumentCategory : ListType
    {
    }
    public class TaskType : ListType
    {
    }
    public class EquipmentTaskType : ListType
    {
    }
    public class EquipmentType : ListType
    {
    }
    public class EventType : ListType
    {
        public virtual string EventColor { get; set; }
    }
    public class PhotoCategory : ListType
    {
    }
    public class Part : ListType
    {
        public virtual string Vendor { get; set; }
        public virtual string FileUrl { get; set; }
    }

    public class UserRole : ListType
    {
        public override void UpdateSelf(Entity entity)
        {
            var item = (UserRole)entity;
            Name = item.Name;
            Description = item.Description;

        }
    }


}