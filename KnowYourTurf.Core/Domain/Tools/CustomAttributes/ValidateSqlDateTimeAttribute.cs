using System;
using System.Data.SqlTypes;
using Castle.Components.Validator;

namespace KnowYourTurf.Core.Domain.Tools.CustomAttributes
{
    [CLSCompliant(false)]
    [Serializable]
    public class ValidateSqlDateTimeAttribute : AbstractValidationAttribute
    {
        public ValidateSqlDateTimeAttribute()
        {
        }

        public ValidateSqlDateTimeAttribute(string errorMessage)
            : base(errorMessage)
        {
        }

        public override IValidator Build()
        {
            IValidator validator = (IValidator)new SqlDateTimeValidator();
            this.ConfigureValidatorMessage(validator);
            return validator;
        }
    }

    public class SqlDateTimeValidator : AbstractValidator
    {
        public override bool IsValid(object instance, object fieldValue)
        {
            if (fieldValue == null || fieldValue.ToString() == "")
            {
                return true;
            }
            DateTime result;
            DateTime.TryParse(fieldValue.ToString(), out result);
            if (result > (DateTime)SqlDateTime.MinValue && result < (DateTime)SqlDateTime.MaxValue)
            {
                return true;
            }
            return false;
        }

        protected override string MessageKey
        {
            get
            {
                return "date_invalid";
            }
        }
        
        public override bool SupportsBrowserValidation
        {
            get { return true; }
        }
    }
}
