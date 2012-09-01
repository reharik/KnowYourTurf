// Type: FubuMVC.UI.Configuration.DefaultHtmlConventions
// Assembly: FubuMVC.UI, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

using FubuMVC.UI.Tags;
using HtmlTags;

namespace FubuMVC.UI.Configuration
{
    public class DefaultHtmlConventions : HtmlConventionRegistry
    {
        public DefaultHtmlConventions()
        {
            Editors.IfPropertyIs<bool>().BuildBy(TagActionExpression.BuildCheckbox);
            Editors.Always.BuildBy(TagActionExpression.BuildTextbox);
            Editors.Always.Modify(AddElementName);
            Displays.Always.BuildBy((req => new HtmlTag("span").Text(req.StringValue())));
            Labels.Always.BuildBy((req => new HtmlTag("span").Text(req.Accessor.Name)));
        }

        public static void AddElementName(ElementRequest request, HtmlTag tag)
        {
            if (!tag.IsInputElement())
                return;
            tag.Attr("name", request.ElementId);
        }
    }
}