using System;
using System.Linq.Expressions;
using System.Reflection;
using StructureMap;
using FubuMVC.Core.Util;

namespace KnowYourTurf.Core.Localization
{
    public static class LocalizationManager
    {
        public static string ToHeader(this Accessor property)
        {
            return GetHeader(property).HeaderText;
        }

        public static Header GetHeader(Accessor accessor)
        {
            ILocalizationDataProvider provider = ObjectFactory.Container.GetInstance<ILocalizationDataProvider>();
            return provider.GetHeader(accessor);
        }

        public static Header GetHeader<T>(Expression<Func<T, object>> expression)
        {
           
            return GetHeader(expression.ToAccessor());
        }

        public static string GetTextForKey(StringToken token)
        {
            return Localization.GetTextForKey(token);
        }

        public static string GetText(Enumeration localizedEnum)
        {
            return Localization.GetText(localizedEnum);
        }

        public static string GetLocalString<T>(Expression<Func<T, object>> expression)
        {
            PropertyInfo propertyInfo = FubuMVC.Core.Util.ReflectionHelper.GetProperty(expression);
            return  propertyInfo.Name ;
        }

        public static ILocalizationDataProvider Localization
        {
            get { return ObjectFactory.GetInstance<ILocalizationDataProvider>(); }
        }

        public static void Clear()
        {
        }
    }
}