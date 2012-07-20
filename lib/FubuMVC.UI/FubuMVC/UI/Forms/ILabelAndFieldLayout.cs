// Type: FubuMVC.UI.Forms.ILabelAndFieldLayout
// Assembly: FubuMVC.UI, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

using HtmlTags;

namespace FubuMVC.UI.Forms
{
    public interface ILabelAndFieldLayout : ITagSource
    {
        HtmlTag LabelTag { get; set; }

        HtmlTag BodyTag { get; set; }

        void WrapBody(HtmlTag tag);

        HtmlTag WrapBody(string tagName);

        void SetLabelText(string text);
    }
}