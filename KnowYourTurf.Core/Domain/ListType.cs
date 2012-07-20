using KnowYourTurf.Core.Enums;
using KnowYourTurf.Core.Localization;

namespace KnowYourTurf.Core.Domain
{
    public interface IListType { }
    
    public class ListType:DomainEntity, IPersistableObject, IListType
    {
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        [ValueOfEnumeration(typeof(Status))]
        public virtual string Status { get; set; }
    }

    public class DocumentCategory : ListType
    {
    }
    public class TaskType : ListType
    {
        public virtual string TaskColor { get; set; }
    }
    public class EventType : ListType
    {
        public virtual string EventColor { get; set; }
    }
    public class PhotoCategory : ListType
    {
    }


}