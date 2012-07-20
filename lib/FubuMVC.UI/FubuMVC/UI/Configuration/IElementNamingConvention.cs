// Type: FubuMVC.UI.Configuration.IElementNamingConvention
// Assembly: FubuMVC.UI, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

using System;
using FubuMVC.Core.Util;

namespace FubuMVC.UI.Configuration
{
    public interface IElementNamingConvention
    {
        string GetName(Type modelType, Accessor accessor);
    }
}