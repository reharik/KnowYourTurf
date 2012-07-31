using System.ComponentModel.DataAnnotations;
using Castle.Components.Validator;
using HtmlTags;
using KnowYourTurf.Core;
using FubuMVC.UI.Configuration;
using FubuMVC.Core.Util;
using KnowYourTurf.Core.Enums;

namespace KnowYourTurf.Core.Html.FubuUI.Builders
{
    public class RequiredModifier : IElementModifier
    {
        public TagModifier CreateModifier(AccessorDef accessorDef)
        {
            if (!accessorDef.Accessor.HasAttribute<ValidateNonEmptyAttribute>()) return null;
            TagModifier modifier = (request, tag) => tag.AddClass("required");
//                                   tag.AddValidationHelper(ValidationRule.Required + ":true",
//                                                           ValidationRule.Required + ": '" +
//                                                           CoreLocalizationKeys.FIELD_REQUIRED.ToFormat(request.Accessor.FieldName.ToSeperateWordsFromPascalCase()) +"'");
            return modifier;
        }
    }

    public class NumberModifier : IElementModifier
    {
        public TagModifier CreateModifier(AccessorDef accessorDef)
        {
            if (accessorDef.Accessor.HasAttribute<ValidateDoubleAttribute>()
                || accessorDef.Accessor.HasAttribute<ValidateDecimalAttribute>()
                || accessorDef.Accessor.HasAttribute<ValidateIntegerAttribute>())
            {
                TagModifier modifier = (request, tag) => { if (tag.TagName() == new TextboxTag().TagName())tag.AddClass("number"); };
//                                       tag.AddValidationHelper(ValidationRule.Number + ":true",
//                                                               ValidationRule.Number + ": '" +
//                                                               CoreLocalizationKeys.FIELD_MUST_BE_NUMBER.ToFormat(
//                                                                   request.Accessor.FieldName.
//                                                                       ToSeperateWordsFromPascalCase()) + "'");
                return modifier;
            }
            return null;
        }
    }
}