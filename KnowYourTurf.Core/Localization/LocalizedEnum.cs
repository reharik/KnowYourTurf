using System;
using System.Linq.Expressions;
using System.Reflection;
using FubuMVC.Core.Util;

namespace KnowYourTurf.Core.Localization
{
    public static class EnumerationExtensions
    {
        public static Enumeration GetEnumeration(this PropertyInfo property, string key)
        {
            if (key == null) return null;
            var valueOfAttribute = property.GetAttribute<ValueOfAttribute>();
            return valueOfAttribute != null ? Enumeration.CreateInstance(valueOfAttribute.LocalizedEnumType, key) : null;
        }

        public static Enumeration GetAltEnumeration(this PropertyInfo property, string key)
        {
            if (key == null) return null;
            var valueOfAttribute = property.GetAttribute<AltListValueOfAttribute>();
            return valueOfAttribute != null ? Enumeration.CreateInstance(valueOfAttribute.LocalizedEnumType, key) : null;
        }

        public static Enumeration GetLocalizedEnum(this Accessor accessor, string key)
        {
            return accessor.InnerProperty.GetEnumeration(key);
        }

        public static Enumeration GetEnumerationByValue(this Accessor accessor, string value)
        {
            if (value == null) return null; 
            var enumeration = accessor.InnerProperty.GetEnumeration("");
            return enumeration != null ? Enumeration.FromValue(enumeration, value) : null;
        }

        public static string LocalizedValue<T>(this T entity, Expression<Func<T, string>> expression)
        {
            var accessor = ReflectionHelper.GetAccessor(expression);
            var key = accessor.GetValue(entity) as string;
            var valueObject = accessor.GetLocalizedEnum(key);
            return LocalizationManager.GetText(valueObject);
        }
    }

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class ValueOfAttribute : Attribute
    {
        public Type LocalizedEnumType { get; private set; }

        public ValueOfAttribute(Type valueObjectType)
        {
            
            LocalizedEnumType = valueObjectType;
        }
    }

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class AltListValueOfAttribute : Attribute
    {
        public Type LocalizedEnumType { get; private set; }

        public AltListValueOfAttribute(Type valueObjectType)
        {

            LocalizedEnumType = valueObjectType;
        }
    }
}