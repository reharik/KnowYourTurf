using System.Collections.Generic;
using KnowYourTurf.Core.Services;

namespace KnowYourTurf.Core.Rules
{
    public class DeleteEmployeeRules :RulesEngineBase
    {
        private readonly ISystemClock _systemClock;

        public DeleteEmployeeRules(ISystemClock systemClock)
        {
            _systemClock = systemClock;
            Rules = new List<IRule>();
            Rules.Add(new EmployeeHasNoOutstandingTasks(_systemClock));
        }
    }
}