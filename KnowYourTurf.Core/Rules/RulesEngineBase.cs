using System.Collections.Generic;
using FubuMVC.Core;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Localization;

namespace KnowYourTurf.Core
{
    public abstract class RulesEngineBase
    {
        public List<IRule> Rules { get; set; }
        public RulesResult ExecuteRules<ENTITY>(ENTITY entity) where ENTITY : DomainEntity
        {
            var rulesResult = new RulesResult {Success = true};
            Rules.ForEachItem(x =>
                           {
                               var result = x.Execute(entity);
                               if (!result.Success)
                               {
                                   rulesResult.Success = false;
                                   rulesResult.RuleResults.Add(result);
                               }
                           });
            return rulesResult;
        }
    }

    public class RulesResult
    {
        public RulesResult()
        {
            RuleResults = new List<RuleResult>();
        }

        public bool Success { get; set; }
        public List<RuleResult> RuleResults { get; set; }
    }

    public interface IRule
    {
        RuleResult Execute<ENTITY>(ENTITY entity) where ENTITY : DomainEntity;
    }

    public class RuleResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}