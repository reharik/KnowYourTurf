// Type: FubuMVC.UI.Stringifier
// Assembly: FubuMVC.UI, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FubuMVC.Core;
using FubuMVC.Core.Util;

namespace FubuMVC.UI
{
    public class Stringifier
    {
        private readonly Cache<Type, Func<object, string>> _converters = new Cache<Type, Func<object, string>>();
        private readonly List<PropertyOverrideStrategy> _overrides = new List<PropertyOverrideStrategy>();
        private readonly List<StringifierStrategy> _strategies = new List<StringifierStrategy>();

        public Stringifier()
        {
            _converters.OnMissing = (type =>
                                         {
                                             if (type.IsNullable())
                                                 return
                                                     (Func<object, string>)
                                                     (instance =>
                                                      instance == null
                                                          ? string.Empty
                                                          : _converters[type.GetInnerTypeFromNullable()](instance));
                                             StringifierStrategy local =
                                                 (_strategies).FirstOrDefault((x => x.Matches(type)));
                                             return local == null ? toString : local.StringFunction;
                                         });
        }

        private static string toString(object value)
        {
            return value == null ? string.Empty : value.ToString();
        }

        public string GetString(PropertyInfo property, object rawValue)
        {
            if (rawValue == null || rawValue as string == string.Empty)
                return string.Empty;
            PropertyOverrideStrategy overrideStrategy = (_overrides).FirstOrDefault((o => o.Matches(property)));
            return overrideStrategy != null ? overrideStrategy.StringFunction(rawValue) : GetString(rawValue);
        }

        public string GetString(object rawValue)
        {
            if (rawValue == null || rawValue as string == string.Empty)
                return string.Empty;
            else
                return _converters[rawValue.GetType()](rawValue);
        }

        public void IfIsType<T>(Func<T, string> display)
        {
            _strategies.Add(new StringifierStrategy
                                {
                                    Matches = (type => type == typeof (T)),
                                    StringFunction = (o => display((T) o))
                                });
        }

        public void IfCanBeCastToType<T>(Func<T, string> display)
        {
            _strategies.Add(new StringifierStrategy
                                {
                                    Matches = (t => t.CanBeCastTo<T>()),
                                    StringFunction = (o => display((T) o))
                                });
        }

        public void IfPropertyMatches(Func<PropertyInfo, bool> matches, Func<object, string> display)
        {
            _overrides.Add(new PropertyOverrideStrategy
                               {
                                   Matches = matches,
                                   StringFunction = display
                               });
        }

        public void IfPropertyMatches<T>(Func<PropertyInfo, bool> matches, Func<T, string> display)
        {
            IfPropertyMatches((p => p.PropertyType.CanBeCastTo<T>() && matches(p)), (o => display((T) o)));
        }

        #region Nested type: PropertyOverrideStrategy

        public class PropertyOverrideStrategy
        {
            public Func<PropertyInfo, bool> Matches;
            public Func<object, string> StringFunction;
        }

        #endregion

        #region Nested type: StringifierStrategy

        public class StringifierStrategy
        {
            public Func<Type, bool> Matches;
            public Func<object, string> StringFunction;
        }

        #endregion
    }
}