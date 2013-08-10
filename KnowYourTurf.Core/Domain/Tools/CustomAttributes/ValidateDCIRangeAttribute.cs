using System;
using Castle.Components.Validator;

namespace KnowYourTurf.Core.Domain.Tools.CustomAttributes
{
    public class ValidateKYTRangeAttribute : ValidateRangeAttribute
    {
        public string propType { get; set; }
        public decimal MinDec { get; set; }
        public decimal MaxDec { get; set; }
        public int MinInt { get; set; }
        public int MaxInt { get; set; }
        protected string MaxStr { get; set; }
        protected string MinStr { get; set; }
        protected DateTime MaxDt { get; set; }
        protected DateTime MinDt { get; set; }
        protected object MaxObj { get; set; }
        protected object MinObj { get; set; }


        public ValidateKYTRangeAttribute(int min, int max)
            : base(min, max)
        {
            MinInt = min;
            MaxInt = max;
            propType = "int";
        }

        public ValidateKYTRangeAttribute(int min, int max, string errorMessage)
            : base(min, max, errorMessage)
        {
            MinInt = min;
            MaxInt = max;
            propType = "int";
        }

        public ValidateKYTRangeAttribute(decimal min, decimal max)
            : base(min, max)
        {
            MinDec = min;
            MaxDec = max;
            propType = "dec";
        }

        public ValidateKYTRangeAttribute(decimal min, decimal max, string errorMessage) : base(min, max, errorMessage)
        {
            MinDec = min;
            MaxDec = max;
            propType = "dec";
        }

        public ValidateKYTRangeAttribute(string min, string max) : base(min, max)
        {
            MinStr = min;
            MaxStr = max;
            propType = "string";
        }
        
        public ValidateKYTRangeAttribute(string min, string max, string errorMessage) : base(min, max, errorMessage)
        {
            MinStr = min;
            MaxStr = max;
            propType = "string";
        }

        public ValidateKYTRangeAttribute(DateTime min, DateTime max) : base(min, max)
        {
            MinDt = min;
            MaxDt = max;
            propType = "DateTime";
        }

        public ValidateKYTRangeAttribute(DateTime min, DateTime max, string errorMessage) : base(min, max, errorMessage)
        {
            MinDt = min;
            MaxDt = max;
            propType = "DateTime";
        }

        public ValidateKYTRangeAttribute(RangeValidationType type, object min, object max) : base(type, min, max)
        {
            MinObj = min;
            MaxObj = max;
            propType = "object";
        }
        
        public ValidateKYTRangeAttribute(RangeValidationType type, object min, object max, string errorMessage) : base(type, min, max, errorMessage)
        {
            MinObj = min;
            MaxObj = max;
            propType = "object";
        }
    }
}