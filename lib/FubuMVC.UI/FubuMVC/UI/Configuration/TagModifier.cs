// Type: FubuMVC.UI.Configuration.TagModifier
// Assembly: FubuMVC.UI, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

using HtmlTags;

namespace FubuMVC.UI.Configuration
{
    public delegate void TagModifier(ElementRequest request, HtmlTag tag);
}