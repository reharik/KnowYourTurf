// Type: FubuMVC.UI.Configuration.ElementBuilder
// Assembly: FubuMVC.UI, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

using HtmlTags;

namespace FubuMVC.UI.Configuration
{
    public abstract class ElementBuilder : IElementBuilder
    {
        #region IElementBuilder Members

        public TagBuilder CreateInitial(AccessorDef accessorDef)
        {
            if (matches(accessorDef))
                return Build;
            else
                return null;
        }

        #endregion

        protected abstract bool matches(AccessorDef def);

        public abstract HtmlTag Build(ElementRequest request);
    }
}