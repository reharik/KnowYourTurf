using System;
using Castle.Components.Validator;
using KnowYourTurf.Core.Domain.Tools.CustomAttributes;
using KnowYourTurf.Core.Enumerations;
using FubuMVC.UI.Configuration;
using FubuMVC.Core.Util;

namespace KnowYourTurf.Core.Html.FubuUI.Builders
{
    public class RequiredModifier : IElementModifier
    {
        public TagModifier CreateModifier(AccessorDef accessorDef)
        {
            if (!accessorDef.Accessor.HasAttribute<ValidateNonEmptyAttribute>()) return null;
            TagModifier modifier = (request, tag) =>
                                   tag.AddValidationHelper(ValidationRule.Required + ":true",
                                                           ValidationRule.Required + ": '" +
                                                           CoreLocalizationKeys.FIELD_REQUIRED.ToFormat(request.Accessor.FieldName.ToSeperateWordsFromPascalCase()) +"'");
            return modifier;
        }
    }

   

    public class PasswordConfirmModifier : IElementModifier
    {
        public TagModifier CreateModifier(AccessorDef accessorDef)
        {
            if (!accessorDef.Accessor.HasAttribute<ValidateSameAsAttribute>()) return null;
            TagModifier modifier = (request, tag) =>
                                   tag.AddValidationHelper("equalTo:'[name$=\"Password\"]'",
                                                           "equalTo: '" +
                                                           CoreLocalizationKeys.CONFIRMATION_PASSWORD_MUST_MATCH.ToString() + "'");
            return modifier;
        }
    }

    public class UrlModifier : IElementModifier
    {
        public TagModifier CreateModifier(AccessorDef accessorDef)
        {
            if (!accessorDef.Accessor.HasAttribute<ValidateUrlAttribute>()) return null;
            TagModifier modifier = (request, tag) =>
                                   tag.AddValidationHelper(ValidationRule.Url + ":true",
                                                           ValidationRule.Url + ": '" +
                                                           CoreLocalizationKeys.VALID_URL_FORMAT.ToFormat(request.Accessor.FieldName.ToSeperateWordsFromPascalCase()) + "'");
            return modifier;
        }
    }

    public class EmailModifier : IElementModifier
    {
        public TagModifier CreateModifier(AccessorDef accessorDef)
        {
            if (!accessorDef.Accessor.HasAttribute<ValidateEmailAttribute>()) return null;
            TagModifier modifier = (request, tag) =>
                                   tag.AddValidationHelper(ValidationRule.Email + ":true",
                                                           ValidationRule.Email + ": '" +
                                                           CoreLocalizationKeys.VALID_EMAIL_FORMAT.ToFormat(request.Accessor.FieldName.ToSeperateWordsFromPascalCase()) + "'");
            return modifier;
        }
    }

    public class DateModifier : IElementModifier
    {
        public TagModifier CreateModifier(AccessorDef accessorDef)
        {
            if (!accessorDef.Accessor.HasAttribute<ValidateSqlDateTimeAttribute>()) return null;
            TagModifier modifier = (request, tag) =>
                                   tag.AddValidationHelper(ValidationRule.Date + ":true",
                                                           ValidationRule.Date + ": '" +
                                                           CoreLocalizationKeys.VALID_DATE_FORMAT.ToFormat(request.Accessor.FieldName.ToSeperateWordsFromPascalCase()) + "'");
            return modifier;
        }
    }

    public class RangeModifier : IElementModifier
    {
        public TagModifier CreateModifier(AccessorDef accessorDef)
        {
            if (!accessorDef.Accessor.HasAttribute<ValidateKYTRangeAttribute>()) return null;
            var maxInt = accessorDef.Accessor.GetAttribute<ValidateKYTRangeAttribute>().MaxInt;
            var minInt = accessorDef.Accessor.GetAttribute<ValidateKYTRangeAttribute>().MinInt;
            TagModifier modifier = (request, tag) =>
                                   tag.AddValidationHelper(ValidationRule.Range + ":["+minInt+","+maxInt+"]",
                                                           ValidationRule.Range + ": '" +
                                                           CoreLocalizationKeys.VALID_RANGE.ToFormat(request.Accessor.FieldName.ToSeperateWordsFromPascalCase(),minInt,maxInt) + "'");
            return modifier;
        }
    }

    public class NumberModifier : IElementModifier
    {
        public TagModifier CreateModifier(AccessorDef accessorDef)
        {
            if (accessorDef.Accessor.PropertyType == typeof(Int16)
                || accessorDef.Accessor.PropertyType == typeof(Int32)
                || accessorDef.Accessor.PropertyType == typeof(Int64)
                || accessorDef.Accessor.PropertyType == typeof(decimal)
                || accessorDef.Accessor.PropertyType == typeof(float)
                || accessorDef.Accessor.PropertyType == typeof(double)
                || accessorDef.Accessor.PropertyType.IsNullableOf(typeof(Int16)) 
                || accessorDef.Accessor.PropertyType.IsNullableOf(typeof(Int32))
                || accessorDef.Accessor.PropertyType.IsNullableOf(typeof(Int64))
                || accessorDef.Accessor.PropertyType.IsNullableOf(typeof(decimal))
                || accessorDef.Accessor.PropertyType.IsNullableOf(typeof(float))
                || accessorDef.Accessor.PropertyType.IsNullableOf(typeof(double)))
            {
                TagModifier modifier = (request, tag) =>
                                       tag.AddValidationHelper(ValidationRule.Number + ":true",
                                                               ValidationRule.Number + ": '" +
                                                               CoreLocalizationKeys.FIELD_MUST_BE_NUMBER.ToFormat(
                                                                   request.Accessor.FieldName.
                                                                       ToSeperateWordsFromPascalCase()) + "'");
                return modifier;
            }
            return null;
        }
    }
}