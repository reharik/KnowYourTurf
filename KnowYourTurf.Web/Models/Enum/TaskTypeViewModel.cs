using KnowYourTurf.Core;
using KnowYourTurf.Web.Controllers;

namespace KnowYourTurf.Web.Models
{
    public class ListTypeViewModel : ViewModel 
    {
        public ListType Item { get; set; }
    }

    public class EventTypeViewModel : ViewModel
    {
        public EventType Item { get; set; }
    }

    public class TaskTypeViewModel : ViewModel
    {
        public TaskType Item { get; set; }
    }
}