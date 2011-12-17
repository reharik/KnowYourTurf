using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Services;

namespace KnowYourTurf.Core.Rules
{
    public class EmployeeHasNoOutstandingTasks:IRule
    {
        private readonly ISystemClock _systemClock;

        public EmployeeHasNoOutstandingTasks(ISystemClock systemClock)
        {
            _systemClock = systemClock;
        }

        public RuleResult Execute<ENTITY>(ENTITY employee) where ENTITY : DomainEntity
        {
            var result = new RuleResult {Success = true};
//            var count = 0;
//            var _employee = employee as User;
//            _employee.GetTasks().Each(x => { if (x.ScheduledStartTime > _systemClock.Now) count++; });
//            if(count>0)
//            {
//                result.Success = false;
//                result.Message = CoreLocalizationKeys.EMPLOYEE_HAS_TASKS_IN_FUTURE.ToFormat(count);
//            }
            return result;
        }
    }
}