using System;
using System.Collections.Generic;
using System.Linq;
using FubuMVC.Core;
using KnowYourTurf.Core;
using FubuMVC.Core.Util;
using KnowYourTurf.Core.Enums;

namespace KnowYourTurf.Core.Localization
{
    public class LocalizedEnumRegistry
    {
        static LocalizedEnumRegistry()
        {
            ResetAll();
        }

        private static readonly Cache<Enumeration, List<Enumeration>> _cache = new Cache<Enumeration, List<Enumeration>>(t => new List<Enumeration>());

        public static void Store<T>(T enumeration) where T : Enumeration, new()
        {
            if (enumeration.IsDefault && FindDefault<T>() != null)
            {
                throw new InvalidOperationException("Cannot have more than one default value for {0}".ToFormat(typeof(T).Name));
            }
            var currentList = _cache.Retrieve(enumeration);
            Enumeration.GetAll<T>().Each(x =>
            {
                if (!currentList.Contains(x)) currentList.Add(x);
            });
        }

        public static T FindDefault<T>() where T : Enumeration, new()
        {
            return (T)_cache.Retrieve(new T()).Find(v => v.IsDefault);
        }

        public static void ForEachValue(Action<Enumeration> action)
        {
            _cache.Each(list => list.Each(action));
        }

        public static T Find<T>(string key) where T : Enumeration, new()
        {
            return (T)_cache.Retrieve(new T()).Find(v => v.Key == key);
        }

        public static T FindByValue<T>(string value) where T : Enumeration, new()
        {
            return (T)_cache.Retrieve(new T()).Find(v => v.Value == value);
        }

        public static object Find(Enumeration valueType, string key)
        {
            return _cache.Retrieve(valueType).Find(v => v.Key == key);
        }

        public static IList<T> GetAllActive<T>() where T : Enumeration, new()
        {
            return _cache.Retrieve(new T()).Where(v => v.IsActive).Cast<T>().ToList();
        }

        public static void ResetAll()
        {
            _cache.ClearAll();
            Store(new ValidationRule());
            Store(new ExtraViewModelData());
         
        }
    }
}