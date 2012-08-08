using System.Collections.Generic;
using System.Web.Mvc;
using KnowYourTurf.Core;
using KnowYourTurf.Core.Enums;
using KnowYourTurf.Core.Localization;

namespace KnowYourTurf.Web.Models
{
    public class ListTypeViewModel : ViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        [ValueOfEnumeration(typeof(Status))]
        public string Status { get; set; }
        public string _saveUrl { get; set; }
        public IEnumerable<SelectListItem> _StatusList { get; set; }
    }

    public class EventTypeViewModel : ListTypeViewModel
    {
        public string EventColor { get; set; }

    }

    public class TaskTypeViewModel : ListTypeViewModel
    {
        public string TaskColor { get; set; }
    }
}