// Type: FubuMVC.UI.FubuRegistryUIExtensions
// Assembly: FubuMVC.UI, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

using System;
using FubuMVC.Core;
using FubuMVC.UI.Configuration;

namespace FubuMVC.UI
{
    public static class FubuRegistryUIExtensions
    {
        public static void UseDefaultHtmlConventions(this FubuRegistry registry)
        {
            registry.Policies.Add<HtmlConventionCompiler>();
        }

        public static void HtmlConvention<T>(this FubuRegistry registry) where T : HtmlConventionRegistry, new()
        {
            registry.HtmlConvention(Activator.CreateInstance<T>());
        }

        public static void HtmlConvention(this FubuRegistry registry, HtmlConventionRegistry conventions)
        {
            registry.Services((x => x.AddService(conventions)));
            registry.Policies.Add<HtmlConventionCompiler>();
        }

        public static void HtmlConvention(this FubuRegistry registry, Action<HtmlConventionRegistry> configure)
        {
            var conventions = new HtmlConventionRegistry();
            configure(conventions);
            registry.HtmlConvention(conventions);
        }

        public static void StringConversions(this FubuRegistry registry, Action<Stringifier> configure)
        {
            registry.Policies.Add(new StringifierConfiguration(configure));
            registry.Policies.Add<HtmlConventionCompiler>();
        }
    }
}