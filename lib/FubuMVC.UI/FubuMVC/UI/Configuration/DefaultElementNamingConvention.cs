// Type: FubuMVC.UI.Configuration.DefaultElementNamingConvention
// Assembly: FubuMVC.UI, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

using System;
using FubuMVC.Core.Util;

namespace FubuMVC.UI.Configuration
{
    public class DefaultElementNamingConvention : IElementNamingConvention
    {
        #region IElementNamingConvention Members

        public string GetName(Type modelType, Accessor accessor)
        {
            return accessor.Name;
        }

        #endregion
    }
}