namespace KnowYourTurf.Web.Config
{
    using System;
    using System.Web;
    using System.Web.Mvc;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    using CC.Core;

    public class CustomJsonResult : JsonResult
    {
        public CustomJsonResult(object input, string contentType = "")
        {
            this.Data = input;
            this.ContentType = contentType.IsNotEmpty()?contentType:ContentType;
        }

        private const string _dateFormat = "yyyy-MM-ddTHH:mm:ss";

        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            HttpResponseBase response = context.HttpContext.Response;

            if (!String.IsNullOrEmpty(this.ContentType))
            {
                response.ContentType = this.ContentType;
            }
            else
            {
                response.ContentType = "application/json";
            }
            if (this.ContentEncoding != null)
            {
                response.ContentEncoding = this.ContentEncoding;
            }
            if (this.Data != null)
            {
                this.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                // Using Json.NET serializer
                var isoConvert = new IsoDateTimeConverter();
                isoConvert.DateTimeFormat = _dateFormat;
                response.Write(JsonConvert.SerializeObject(this.Data, isoConvert));
            }
        }
    }
}