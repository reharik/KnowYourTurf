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
        /// <summary>
        /// Aggregate Root that should not be modified through Event
        /// </summary>
        private Field _readOnlyField;
        [ValidateNonEmpty]
        public virtual Field ReadOnlyField { get { return _readOnlyField; } }
        public virtual void SetField(Field field)
        {
            _readOnlyField = field;
        }
        ////
        [ValidateNonEmpty]
        public virtual DateTime? ScheduledDate { get; set; }
        [ValidateNonEmpty]
        public virtual DateTime? StartTime { get; set; }
        public virtual DateTime? EndTime { get; set; }
        [TextArea]
        public virtual string Notes { get; set; }
    }

    
}