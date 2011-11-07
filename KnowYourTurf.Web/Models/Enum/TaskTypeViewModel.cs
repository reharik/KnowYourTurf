using KnowYourTurf.Core;
using KnowYourTurf.Web.Controllers;

namespace KnowYourTurf.Web.Models
{
    public class ListTypeViewModel : ViewModel 
    {
        public ListType ListType { get; set; }
    }

    public class EventTypeViewModel : ViewModel
    {
        public EventType ListType { get; set; }
    }

    public class TaskTypeViewModel : ViewModel
    {
        public TaskType ListType { get; set; }
    }
}