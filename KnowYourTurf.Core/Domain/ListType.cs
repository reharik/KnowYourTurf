using CC.Core.Domain;
using CC.Core.Localization;
using KnowYourTurf.Core.Enums;

namespace KnowYourTurf.Core.Domain
{
    public interface IListType { }
    
    public class ListType:DomainEntity, IPersistableObject, IListType
    {
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
    public class EventType : ListType
    {
        public virtual string EventColor { get; set; }
    }
    public class PhotoCategory : ListType
    {
    }


}