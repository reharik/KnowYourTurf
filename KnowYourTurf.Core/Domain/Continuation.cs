using System.Collections.Generic;
using CC.Core;
using CC.Core.DomainTools;
using xVal.ServerSide;
using System.Linq;

namespace KnowYourTurf.Core.Domain
{
    public class Continuation
    {
        public List<ErrorInfo> Errors { get; set; }
        public string Message { get; set; }
        public bool Success { get; set; }
        public int ErrorCount { get; set; }
        public object Target { get; set; }
        public string ListErrors()
        {
            var message = Message.IsNotEmpty()
                              ? Message + ", "
                              : string.Empty;
            var errors = string.Empty;
            Errors.ForEachItem(x => errors += x.ErrorMessage +", ");
            errors.Remove(errors.LastIndexOf(","));
            return message + errors;
        }
        // success is true untill proven other wise, at which point
        // it will remain fall even when new successfull continuations are added
        public Continuation()
        {
            Success = true;
            Errors = new List<ErrorInfo>();
        }

        public Continuation(Continuation continuation)
        {
            Success = true;
            AddContinuation(continuation);
        }

        public void AddContinuation(Continuation continuation)
        {
            if(!continuation.Success)
            {
                Success = continuation.Success;
                if(continuation.Errors.Any())
                {
                    Errors = Errors.Any() ? Errors = continuation.Errors.ToList() : Errors.Concat(continuation.Errors).ToList();
                }
            }
            if(continuation.Message!=Message)
            {
                Message = Message.IsNotEmpty() ? Message + ", " + continuation.Message : continuation.Message;
            }
        }

        public Notification ReturnNotification()
        {
            return new Notification
                {
                    Errors = Errors,
                    Message = Message,
                    Success = Success
                };
        }
    }


}