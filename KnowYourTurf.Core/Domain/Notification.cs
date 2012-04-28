using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using FubuMVC.Core;
using xVal.ServerSide;

namespace KnowYourTurf.Core.Domain
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
            if(!Success && rulesResult.RuleResults.Count>1) Errors = new List<ErrorInfo>();
            rulesResult.RuleResults.Each(x => Errors.Add(new ErrorInfo(CoreLocalizationKeys.BUISNESS_RULE.ToString(), x.Message)));
        }

        public Notification(Continuation continuation)
        {
            Message = continuation.Message;
            Success = continuation.Success;
            Errors = continuation.Errors!=null?continuation.Errors.ToList():null;
            Target = continuation.Target;
        }

        public List<ErrorInfo> Errors;
        public string ShowDialog { get; set; }
        public bool Refresh { get; set; }
        public string Message { get; set; }
        public string Caption { get; set; }
        public bool Success { get; set; }
        public string RedirectUrl { get; set; }
        public bool Redirect { get; set; }
        public long EntityId { get; set; }
        public string Variable { get; set; }
        public bool SessionLoggedOut { get; set; }

        [ScriptIgnore]
        public object Target { get; set; }

        //public static Notification ForItemNotFoundMessage(StringToken key)
        //{
        //    return new Notification { message = LocalizationManager.GetTextForKey(key) };
        //}

        public static Notification ForItemNotFoundMessage(string message)
        {
            return new Notification{ Message = message };
        }

        //public static Notification ForMessage(StringToken key, object target)
        //{
        //    var continuation = ForItemNotFoundMessage(key);
        //    continuation.Target = target;

        //    return continuation;
        //}

        public static Notification ForMessage(string messgae, object target)
        {
            var continuation = ForItemNotFoundMessage(messgae);
            continuation.Target = target;

            return continuation;
        }

        public static Notification ForDialog(string url, object target)
        {
            return new Notification { ShowDialog = url, Target = target };
        }

        public static Notification ForRefresh(object target)
        {
            return new Notification{ Target = target, Success = true, Refresh = true };
        }
    }
}