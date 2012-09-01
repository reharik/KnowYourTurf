// Type: FubuMVC.UI.Tags.TagFactoryExpression
// Assembly: FubuMVC.UI, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

using System;
using FubuMVC.Core.Util;
using FubuMVC.UI.Configuration;
using HtmlTags;

namespace FubuMVC.UI.Tags
{
    public class TagFactoryExpression
    {
        private readonly TagFactory _factory;

        public TagFactoryExpression(TagFactory factory)
        {
            _factory = factory;
        }

        public TagActionExpression Always
        {
            get { return If((def => true)); }
        }

        public void Builder<T>() where T : IElementBuilder, new()
        {
            Builder(new T());
        }

        public void Builder(IElementBuilder builder)
        {
            _factory.AddBuilder(builder);
        }

        public void Modifier<T>() where T : IElementModifier, new()
        {
            Modifier(new T());
        }

        public void Modifier(IElementModifier modifier)
        {
            _factory.AddModifier(modifier);
        }

        public TagActionExpression If(Func<AccessorDef, bool> matches)
        {
            return new TagActionExpression(_factory, matches);
        }

        public TagActionExpression IfPropertyTypeIs(Func<Type, bool> matches)
        {
            return If((def => matches(def.Accessor.PropertyType)));
        }

        public TagActionExpression IfPropertyIs<T>()
        {
            return If((def => def.Accessor.PropertyType == typeof (T)));
        }

        public TagActionExpression IfPropertyHasAttribute<T>() where T : Attribute
        {
            return If((def => def.Accessor.HasAttribute<T>()));
        }

        public void AddClassForAttribute<T>(string className) where T : Attribute
        {
            If((def => def.Accessor.HasAttribute<T>())).AddClass(className);
        }

        public void ModifyForAttribute<T>(Action<HtmlTag, T> modification) where T : Attribute
        {
            If((def => true)).Modify(
                ((request, tag) => request.Accessor.ForAttribute((Action<T>) (att => modification(tag, att)))));
        }

        public void ModifyForAttribute<T>(Action<HtmlTag> modification) where T : Attribute
        {
            ModifyForAttribute((Action<HtmlTag, T>) ((tag, att) => modification(tag)));
        }
    }
}