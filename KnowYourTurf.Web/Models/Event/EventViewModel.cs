using System;
using System.Collections.Generic;
using System.Web.Mvc;
using CC.Core.CoreViewModelAndDTOs;
using CC.Core.CustomAttributes;
using Castle.Components.Validator;
using KnowYourTurf.Core;

namespace KnowYourTurf.Web.Models
{
    public class EventViewModel:ViewModel
    {
        public IEnumerable<SelectListItem> _FieldEntityIdList { get; set; }
        public IEnumerable<SelectListItem> _EventTypeEntityIdList { get; set; }
        public bool Copy { get; set; }

        [ValidateNonEmpty]
        public int FieldEntityId { get; set; }
        [ValidateNonEmpty]
        public int EventTypeEntityId { get; set; }
        [ValidateNonEmpty]
        public DateTime? ScheduledDate { get; set; }
        [ValidateNonEmpty]
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        [TextArea]
        public string Notes { get; set; }

        public string FieldName { get; set; }
        public string EventTypeName { get; set; }
        public string _saveUrl { get; set; }
    }
    
    
}