using System;
using Castle.Components.Validator;
using KnowYourTurf.Core.Domain.Tools.CustomAttributes;
using KnowYourTurf.Web.Controllers;

namespace KnowYourTurf.Core.Domain
{
    public class Event : DomainEntity
    {
        [ValidateNonEmpty]
        public virtual EventType EventType { get; set; }
        public virtual Field Field { get; set; }
        [ValidateNonEmpty]
        public virtual DateTime? ScheduledDate { get; set; }
        [ValidateNonEmpty]
        public virtual DateTime? StartTime { get; set; }
        public virtual DateTime? EndTime { get; set; }
        [TextArea(2, 60)]
        public virtual string Notes { get; set; }
    }

    
}