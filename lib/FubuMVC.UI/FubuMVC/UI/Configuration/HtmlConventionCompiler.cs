// Type: FubuMVC.UI.Configuration.HtmlConventionCompiler
// Assembly: FubuMVC.UI, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

using FubuMVC.Core;
using FubuMVC.Core.Registration;
using FubuMVC.UI.Forms;
using FubuMVC.UI.Tags;

namespace FubuMVC.UI.Configuration
{
    public class HtmlConventionCompiler : IConfigurationAction
    {
        #region IConfigurationAction Members

        public void Configure(BehaviorGraph graph)
        {
            var tagProfileLibrary = new TagProfileLibrary();
            graph.Services.FindAllValues<HtmlConventionRegistry>().Each(tagProfileLibrary.ImportRegistry);
            tagProfileLibrary.ImportRegistry(new DefaultHtmlConventions());
            tagProfileLibrary.Seal();
            graph.Services.ClearAll<HtmlConventionRegistry>();
            graph.Services.ReplaceService(tagProfileLibrary);
            graph.Services.SetServiceIfNone<IElementNamingConvention, DefaultElementNamingConvention>();
            graph.Services.SetServiceIfNone<ILabelAndFieldLayout, DefinitionListLabelAndField>();
        }

        #endregion
    }
}