// Type: FubuMVC.UI.StringifierConfiguration
// Assembly: FubuMVC.UI, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

using System;
using System.Linq;
using FubuMVC.Core.Registration;

namespace FubuMVC.UI
{
    public class StringifierConfiguration : IConfigurationAction
    {
        private readonly Action<Stringifier> _configure;

        public StringifierConfiguration(Action<Stringifier> configure)
        {
            _configure = configure;
        }

        #region IConfigurationAction Members

        public void Configure(BehaviorGraph graph)
        {
            graph.Services.SetServiceIfNone(new Stringifier());
            _configure(graph.Services.FindAllValues<Stringifier>().First());
        }

        #endregion
    }
}