using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Castle.Components.Validator;
using KnowYourTurf.Core;

namespace KnowYourTurf.Web.Models
{
    public class EventViewModel:ViewModel
    {
        public Core.Domain.Event Event { get; set; }
        [ValidateNonEmpty]
        public long Field { get; set; }
        public IEnumerable<SelectListItem> FieldList { get; set; }
        [ValidateNonEmpty]
        public long EventType { get; set; }
        public IEnumerable<SelectListItem> EventTypeList { get; set; }
        public bool Copy { get; set; }
    }
    
    public class AddEditEventViewModel : ViewModel
    {
        public DateTime? StartTime { get; set; }
        public bool Copy { get; set; }

        public DateTime? ScheduledDate { get; set; }
    }
}