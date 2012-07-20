using System;
using System.Linq;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Html.FubuUI.Builders;
using KnowYourTurf.Core.Html.FubuUI.Tags;
using FubuMVC.UI;
using FubuMVC.UI.Configuration;
using FubuMVC.UI.Tags;
using HtmlTags;
using KnowYourTurf.Core;

namespace KnowYourTurf.Core.Html.FubuUI.HtmlConventionRegistries
{
    public class KnowYourTurfHtmlConventions : HtmlConventionRegistry
    {
        public KnowYourTurfHtmlConventions()
        {
            numbers();
            Editors.Builder<SelectFromEnumerationBuilder>();
            Editors.Builder<SelectFromIEnumerableBuilder>();
            Editors.Builder<GroupSelectedBuilder>();
            Editors.Builder<TextAreaBuilder>();
            Editors.Builder<DatePickerBuilder>();
            Editors.Builder<TimePickerBuilder>();
            Editors.IfPropertyIs<bool>().BuildBy(TagActionExpression.BuildCheckbox);
            Editors.If(x => x.Accessor.Name.ToLowerInvariant().Contains("password")).BuildBy(r => new PasswordTag().Attr("value", r.RawValue));
            Editors.Builder<TextboxBuilder>();
            
//            Editors.Always.BuildBy(TagActionExpression.BuildTextbox);
            Editors.Always.Modify(AddElementName);
            Displays.Builder<ImageBuilder>();
            Displays.Builder<EmailDisplayBuilder>();
            Displays.Builder<ListDisplayBuilder>();
            Displays.Builder<DateTimeDisplayBuilder>();
            Displays.Builder<ImageFileDisplayBuilder>();
            Displays.If(x => x.Accessor.PropertyType == typeof(DateTime) || x.Accessor.PropertyType == typeof(DateTime?))
                .BuildBy(req => req.RawValue != null ? new HtmlTag("span").Text(DateTime.Parse(req.RawValue.ToString()).ToLongDateString()) : new HtmlTag("span"));
            Displays.Always.BuildBy(req => new HtmlTag("span").Text(req.StringValue()));
            Labels.Always.BuildBy(req =>
                                      {
                                          var htmlTag = new HtmlTag("label").Attr("for", req.Accessor.Name);
                                          var display = req.Accessor.FieldName.Contains("ReadOnly")
                                                            ? req.Accessor.FieldName.Replace("ReadOnly", "")
                                                            : req.Accessor.FieldName;
                                          htmlTag.Text(display.ToSeperateWordsFromPascalCase());
                                          return htmlTag;
                                      });
            validationAttributes();
        }

        public static void AddElementName(ElementRequest request, HtmlTag tag)
        {
            if (tag.IsInputElement())
            {
                tag.Attr("name", DeriveElementName(request));
            }
        }

        public static string DeriveElementName(ElementRequest request)
        {
            var name = request.Accessor.Name;
            if (request.Accessor is FubuMVC.Core.Util.PropertyChain)
            {
                name = ((FubuMVC.Core.Util.PropertyChain)(request.Accessor)).Names.Aggregate((current, next) => current + "." + next);
                var isDomainEntity = false;
                var de = request.Accessor.PropertyType.BaseType;
                while (de.Name != "Object")
                {
                    if (de.Name == "DomainEntity") isDomainEntity = true;
                    de = de.BaseType;
                }
                if (isDomainEntity) name += ".EntityId";

            }
            return name;
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
            Editors.Modifier<NumberModifier>();
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