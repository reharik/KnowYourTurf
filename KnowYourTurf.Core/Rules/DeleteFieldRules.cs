using System.Collections.Generic;
using CC.Core.Services;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Services;
using KnowYourTurf.UnitTests.Rules;

namespace KnowYourTurf.Core.Rules
{
    public class DeleteFieldRules :RulesEngineBase
    {
        private readonly ISystemClock _systemClock;

        public DeleteFieldRules(ISystemClock systemClock)
        {
            _systemClock = systemClock;
            Rules = new List<IRule>();
            Rules.Add(new FieldHasNoOutstandingTasks());
            Rules.Add(new FieldHasNoOutstandingEvents(_systemClock));
        }
    }
}