using System;
using CC.Core.CustomAttributes;
using Castle.Components.Validator;
using KnowYourTurf.Web.Controllers;

namespace KnowYourTurf.Core.Domain
{
    public class Event : DomainEntity
    {
        [ValidateNonEmpty]
        public virtual EventType EventType { get; set; }

        [ValidateNonEmpty]
        public virtual Field Field { get;  set; }
        [ValidateNonEmpty]
        public virtual DateTime? ScheduledDate { get; set; }
        [ValidateNonEmpty]
        public virtual DateTime? StartTime { get; set; }
        public virtual DateTime? EndTime { get; set; }
        [TextArea]
        public virtual string Notes { get; set; }
    }

    
}