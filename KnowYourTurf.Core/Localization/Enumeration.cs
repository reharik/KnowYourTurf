using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace KnowYourTurf.Core.Localization
{
    [Serializable]
    public class Enumeration : IEquatable<Enumeration>
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public bool IsActive { get; set; }
        public bool IsDefault { get; set; }

        #region IEquatable<LocalizedEnum> Members
        public bool Equals(Enumeration obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return Equals(obj.Key, Key) && Equals(obj.GetType(), GetType());
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Enumeration)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Key != null ? Key.GetHashCode() : 0) * 397) ^ GetType().GetHashCode();
            }
        }

        public static bool operator ==(Enumeration left, Enumeration right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Enumeration left, Enumeration right)
        {
            return !Equals(left, right);
        }
        #endregion

        public static IEnumerable<Enumeration> GetAll(Enumeration e)
        {
            var type = e.GetType();
            var fields = type.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly);

            foreach (var info in fields)
            {
                var instance = e;
                var locatedValue = info.GetValue(instance);

                if (locatedValue != null)
                {
                    yield return (Enumeration)locatedValue;
                }
            }
        }

        public static IEnumerable<T> GetAll<T>(bool justActive = false) where T : Enumeration, new()
        {
            var type = typeof(T);
            var fields = type.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly);

            foreach (var info in fields)
            {
                var instance = new T();
                var locatedValue = info.GetValue(instance) as T;
                if (justActive &&  locatedValue != null && locatedValue.IsActive) yield return locatedValue;
                else if (!justActive && locatedValue != null)yield return locatedValue;
                
            }
        }
        public static IEnumerable<Enumeration> GetAllActive<ENUM>() where ENUM : Enumeration,new()
        {
            return GetAll<ENUM>(true);
        }
        public static IEnumerable<Enumeration> GetAllActive(Enumeration e)
        {
            var type = e.GetType();
            var fields = type.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly);

            foreach (var info in fields)
            {
                var instance = e;
                var locatedValue = info.GetValue(instance);

                if (locatedValue != null && ((Enumeration)locatedValue).IsActive)
                {
                    yield return (Enumeration)locatedValue;
                }
            }
        }

        public static ENUM CreateInstance<ENUM>(string key) where ENUM:Enumeration
        {
            Type type = typeof(ENUM);
            var instance = Activator.CreateInstance(type) as ENUM;
            if (instance == null) throw new ArgumentException("Type " + type.Name + " must derive from Enumeration.");
            instance.Key = key;
            return instance;
        }

        public static Enumeration CreateInstance(Type type, string key)
        {
            var instance = Activator.CreateInstance(type) as Enumeration;
            if (instance == null) throw new ArgumentException("Type " + type.Name + " must derive from Enumeration.");
            instance.Key = key;
            return instance;
        }

        public static Enumeration CreateInstance(Type type, string key, string value)
        {
            var instance = Activator.CreateInstance(type) as Enumeration;
            if (instance == null) throw new ArgumentException("Type " + type.Name + " must derive from Enumeration.");
            instance.Key = key;
            instance.Value = value;
            return instance;
        }

        public static T FromKey<T>(string key) where T : Enumeration, new()
        {
            var matchingItem = parse<T, string>(key, "display name", item => item.Key == key);
            return matchingItem;
        }

        public static T FromValue<T>(string value) where T : Enumeration, new()
        {
            var matchingItem = parse<T, string>(value, "value", item => item.Value == value);
            return matchingItem;
        }

        public static Enumeration FromValue(Enumeration e, string value)
        {
            var matchingItem = parse(value, e, item => item.Value == value);
            return matchingItem;
        }

        private static Enumeration parse(string value, Enumeration enumeration, Func<Enumeration, bool> predicate)
        {
            var matchingItem = GetAll(enumeration).FirstOrDefault(predicate);

            if (matchingItem == null)
            {
                var message = string.Format("'{0}' is not a valid Value in {1}", value, enumeration.GetType().Name);
                throw new ApplicationException(message);
            }

            return matchingItem;

        }

        private static T parse<T, K>(K value, string key, Func<T, bool> predicate) where T : Enumeration, new()
        {
            var matchingItem = GetAll<T>().FirstOrDefault(predicate);

            if (matchingItem == null)
            {
                var message = string.Format("'{0}' is not a valid {1} in {2}", value, key, typeof(T));
                throw new ApplicationException(message);
            }

            return matchingItem;
        }

        public override string ToString()
        {
            return Key;
        }

    }
}