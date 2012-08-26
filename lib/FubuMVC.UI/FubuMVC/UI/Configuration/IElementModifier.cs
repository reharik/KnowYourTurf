// Type: FubuMVC.UI.Configuration.IElementModifier
// Assembly: FubuMVC.UI, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

namespace FubuMVC.UI.Configuration
{
    public interface IElementModifier
    {
        TagModifier CreateModifier(AccessorDef accessorDef);
    }
}