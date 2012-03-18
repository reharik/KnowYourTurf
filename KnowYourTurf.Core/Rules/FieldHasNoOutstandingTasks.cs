using System.Linq;
using KnowYourTurf.Core;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Services;

namespace KnowYourTurf.UnitTests.Rules
{
    public class FieldHasNoOutstandingTasks:IRule
    {
        public FieldHasNoOutstandingTasks()
        {
        }

        public RuleResult Execute<ENTITY>(ENTITY field) where ENTITY : DomainEntity
        {
            var result = new RuleResult {Success = true};
            var _field = field as Field;
            var pendingTasks = _field.GetPendingTasks();
            if(pendingTasks.Count()>0)
            {
                result.Success = false;
                result.Message = CoreLocalizationKeys.FIELD_HAS_TASKS_IN_FUTURE.ToFormat(pendingTasks.Count());
            }
            return result;
        }
    }
}