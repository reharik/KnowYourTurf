using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Enums;
using KnowYourTurf.Core.Localization;

namespace KnowYourTurf.Web.Controllers
{
    public class ListType:DomainEntity
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