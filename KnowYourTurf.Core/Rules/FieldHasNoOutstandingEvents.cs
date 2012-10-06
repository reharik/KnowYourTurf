using CC.Core;
using KnowYourTurf.Core;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Services;

namespace KnowYourTurf.UnitTests.Rules
{
    public class FieldHasNoOutstandingEvents:IRule
    {
        private readonly ISystemClock _systemClock;

        public FieldHasNoOutstandingEvents(ISystemClock systemClock)
        {
            _systemClock = systemClock;
        }

        public RuleResult Execute<ENTITY>(ENTITY field) where ENTITY : DomainEntity
        {
            var result = new RuleResult {Success = true};
            var count = 0;
            var _field = field as Field;
            _field.Events.ForEachItem(x => { if (x .StartTime > _systemClock.Now) count++; });
            if(count>0)
            {
                result.Success = false;
                result.Message = CoreLocalizationKeys.FIELD_HAS_EVENTS_IN_FUTURE.ToFormat(count);
            }
            return result;
        }
    }
}