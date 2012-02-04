using System;
using System.Text.RegularExpressions;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Html.FubuUI.Builders;
using KnowYourTurf.Core.Html.FubuUI.Tags;
using FubuMVC.UI;
using FubuMVC.UI.Configuration;
using FubuMVC.UI.Tags;
using HtmlTags;
using System.Linq;

namespace KnowYourTurf.Core.Html.FubuUI.HtmlConventionRegistries
{
    public class KnowYourTurfHtmlConventions : HtmlConventionRegistry
    {
        public KnowYourTurfHtmlConventions()
        {
            numbers();
            Editors.Builder<SelectFromEnumerationBuilder>();
            Editors.Builder<SelectFromIEnumerableBuilder>();
            Editors.Builder<SelectFromILookupTypeBuilder>();
            Editors.Builder<GroupSelectedBuilder>();
            Editors.Builder<RadioButtonListBuilder>();
            Editors.Builder<TextAreaBuilder>();
            Editors.Builder<DatePickerBuilder>();
            Editors.Builder<TimePickerBuilder>();
            Editors.Builder<CheckboxBuilder>();
            Editors.If(x => x.Accessor.Name.ToLowerInvariant().Contains("password")).BuildBy(r => new  PasswordTag().Attr("value", r.RawValue));
            
            Editors.Always.BuildBy(TagActionExpression.BuildTextbox);
            Editors.Always.Modify(AddElementName);
            Displays.Builder<ImageBuilder>();
            Displays.Builder<EmailDisplayBuilder>();
            Displays.Builder<ListDisplayBuilder>();
            Displays.Builder<LinkDisplayBuilder>();
            Displays.Builder<ImageFileDisplayBuilder>();
            Displays.Builder<DateFormatter>();
            Displays.If(x => x.Accessor.PropertyType == typeof(DateTime) || x.Accessor.PropertyType == typeof(DateTime?))
                .BuildBy(req => req.RawValue != null ? new HtmlTag("span").Text(DateTime.Parse(req.RawValue.ToString()).ToLongDateString()) : new HtmlTag("span"));
            Displays.Always.BuildBy(req => new HtmlTag("span").Text(req.StringValue()));
            Labels.Always.BuildBy(req => new HtmlTag("label").Attr("for", req.Accessor.Name).Text(req.Accessor.FieldName.ToSeperateWordsFromPascalCase()));
            //Labels.Always.Modify(ModifyLabelForName);
            validationAttributes();
        }

        public static void AddElementName(ElementRequest request, HtmlTag tag)
        {
            if (tag.IsInputElement())
            {
                var name = request.Accessor.Name;
                if (request.Accessor is FubuMVC.Core.Util.PropertyChain)
                {
                    name = ((FubuMVC.Core.Util.PropertyChain)(request.Accessor)).Names.Aggregate((current, next) => current + "." + next);
                    if (new InheritsFromDomainEntity().execute(request.Accessor.PropertyType))
                        name += ".EntityId";
                }
                //var name = request.Accessor.Name.Substring(0, request.Accessor.Name.IndexOf(request.Accessor.FieldName)) + "." + request.Accessor.FieldName; 
                //tag.Attr("name", name);
                tag.Attr("name", name);
            }
        }
        // I understand this is a retarded way to do it but I can't figure it rigt now
        private class InheritsFromDomainEntity
        {
            private bool check(Type type)
            {
                if (type == typeof(Entity) || type.BaseType == typeof(Entity))
                    return true;
                return type.BaseType != null && check(type.BaseType);
            }

            public bool execute(Type type)
            {
                var result = false;
                if (type.BaseType != null)
                    result = check(type.BaseType);
                return result;
            }
        }

        private void numbers()
        {
            Editors.IfPropertyIs<Int32>().Attr("max", Int32.MaxValue);
            Editors.IfPropertyIs<Int16>().Attr("max", Int16.MaxValue);
            //Editors.IfPropertyIs<Int64>().Attr("max", Int64.MaxValue);
            Editors.IfPropertyTypeIs(IsIntegerBased).AddClass("integer");
            Editors.IfPropertyTypeIs(IsFloatingPoint).AddClass("number");
            Editors.IfPropertyTypeIs(IsIntegerBased).Attr("mask", "wholeNumber");
        }

        private void validationAttributes()
        {
            Editors.Modifier<RequiredModifier>();
            Editors.Modifier<PasswordConfirmModifier>();
            Editors.Modifier<EmailModifier>();
            Editors.Modifier<NumberModifier>();
            Editors.Modifier<UrlModifier>();
            Editors.Modifier<DateModifier>();
            Editors.Modifier<RangeModifier>();
        }

        public static bool IsIntegerBased(Type type)
        {
            return type == typeof(int) || type == typeof(long) || type == typeof(short);
        }

        public static bool IsFloatingPoint(Type type)
        {
            return type == typeof(decimal) || type == typeof(float) || type == typeof(double);
        }
    }
}