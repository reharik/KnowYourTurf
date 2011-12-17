using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using KnowYourTurf.Core.Rules;
using KnowYourTurf.Core.Services;
using xVal.ServerSide;

namespace KnowYourTurf.Core.Domain.Tools
{
    /// <summary>
    /// Communicates (notifies) the result of an action from the server to the client
    /// </summary>
    public class Notification : JsonResult
    {
        public Notification()
        {
        }

        public Notification(RulesResult rulesResult)
        {
            Success = rulesResult.Success;
            if (!Success && rulesResult.RuleResults.Count > 1) Errors = new List<ErrorInfo>();
            rulesResult.RuleResults.Each(x => Errors.Add(new ErrorInfo(CoreLocalizationKeys.BUISNESS_RULE.ToString(), x.Message)));
        }

        public Notification(Notification report)
        {
            Message = report.Message;
            Success = report.Success;
            Errors = report.Errors;
            //Target = continuation.Target;
        }

        public List<ErrorInfo> Errors;
        public string Message { get; set; }
        public bool Success { get; set; }
        public string RedirectUrl { get; set; }
        public bool Redirect { get; set; }
        public long EntityId { get; set; }
        public string Variable { get; set; }

    }
}