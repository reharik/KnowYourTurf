using System;
using System.Collections;
using Castle.Components.Validator;

namespace KnowYourTurf.Core.Domain.Tools.CustomAttributes
{
    public class ValidateDateComesAfterAttribute : AbstractValidationAttribute
    {
        private readonly string propertyToCompare;

        public ValidateDateComesAfterAttribute(string propertyToCompare)
        {
            this.propertyToCompare = propertyToCompare;
        }

        public ValidateDateComesAfterAttribute(string propertyToCompare, string errorMessage)
            : base(errorMessage)
        {
            this.propertyToCompare = propertyToCompare;
        }

        public override IValidator Build()
        {
            IValidator validator = (IValidator) new DateComesAfterValidator(this.propertyToCompare);
            this.ConfigureValidatorMessage(validator);
            return validator;
        }
    }


    public class DateComesAfterValidator : AbstractValidator
    {
        private readonly string propertyToCompare;

        public string PropertyToCompare
        {
            get { return this.propertyToCompare; }
        }

        public override bool SupportsBrowserValidation
        {
            get { return true; }
        }

        protected override string MessageKey
        {
            get { return "not_same_as_invalid"; }
        }

        public DateComesAfterValidator(string propertyToCompare)
        {
            this.propertyToCompare = propertyToCompare;
        }

        public override bool IsValid(object instance, object fieldValue)
        {
            object obj = GetFieldOrPropertyValue(instance, propertyToCompare);
            if (fieldValue == null) return true;
            if(!(fieldValue is DateTime?) && !(fieldValue is DateTime))
            {
                throw new Exception("Attribute only valid for DateTime Fields");
            }
            if (obj == null) return false;
            return (DateTime)obj < (DateTime)fieldValue;
        }

        public override void ApplyBrowserValidation(BrowserValidationConfiguration config, InputElementType inputType,
                                                    IBrowserValidationGenerator generator, IDictionary attributes,
                                                    string target)
        {
            base.ApplyBrowserValidation(config, inputType, generator, attributes, target);
            generator.SetAsNotSameAs(target, this.propertyToCompare, this.BuildErrorMessage());
        }
    }
}