using System.Collections.Generic;
using xVal.ServerSide;
using System.Linq;

namespace KnowYourTurf.Core.Domain.Tools
{
    public class Continuation
    {
        public IEnumerable<ErrorInfo> Errors { get; set; }
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
            Errors.Each(x => errors += x.ErrorMessage + ", ");
            errors.Remove(errors.LastIndexOf(","));
            return message + errors;
        }
        // success is true untill proven other wise, at which point
        // it will remain fall even when new successfull continuations are added
        public Continuation()
        {
            Success = true;
        }

        public Continuation(Continuation continuation)
        {
            Success = true;
            AddContinuation(continuation);
        }

        public void AddContinuation(Continuation continuation)
        {
            if (!continuation.Success)
            {
                Success = continuation.Success;
                if (continuation.Errors != null)
                {
                    Errors = Errors == null ? Errors = continuation.Errors : Errors.Concat(continuation.Errors);
                }
            }
            if (continuation.Message != Message)
            {
                Message = Message.IsNotEmpty() ? Message + ", " + continuation.Message : continuation.Message;
            }
        }
    }


}