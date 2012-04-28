using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web.Script.Serialization;
using FubuMVC.Core;
using FubuMVC.Core.Util;
using KnowYourTurf.Core.Domain;

namespace KnowYourTurf.Core.Services
{
    public interface IDynamicExpressionBuilder
    {
        Expression<Func<ENTITY, bool>> Build<ENTITY>(string json, bool isNullCheck = false) where ENTITY : DomainEntity;
        Expression<Func<ENTITY, bool>> Build<ENTITY>(JqGridFilter filter, bool isNullCheck = false) where ENTITY : DomainEntity;
    }

    public class DynamicExpressionBuilder : IDynamicExpressionBuilder
    {
        public Expression<Func<ENTITY, bool>> Build<ENTITY>(string json, bool isNullCheck = false) where ENTITY : DomainEntity
        {
            if (json.IsEmpty()) return null;
            var jqGridFilter = DeserializeJson(json);
            return jqGridFilter.rules.Any(x => x.op == "ListContains" && x.listOfIds.Count() <= 0) ?
                null :
                Build<ENTITY>(jqGridFilter, isNullCheck);
        }
        private JqGridFilter DeserializeJson(string json)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            JqGridFilter filter = jss.Deserialize<JqGridFilter>(json);
            return filter;
        }
        public Expression<Func<ENTITY, bool>> Build<ENTITY>(JqGridFilter filter, bool isNullCheck = false) where ENTITY : DomainEntity
        {
            ParameterExpression pe = Expression.Parameter(typeof(ENTITY), "x");
            if (filter.groupOp == "NONE") return null;
            Expression predicateBody = createBinaryExpressionsForRules(filter.rules, pe, null, isNullCheck);
            if (predicateBody == null) return null;
            Expression<Func<ENTITY, bool>> expression = Expression.Lambda<Func<ENTITY, bool>>(predicateBody, new[] { pe });
            return expression;
        }

        private MemberExpression createMemberExpressionForProperty(ParameterExpression parameterExpression, string fullName)
        {
            var names = fullName.Split('.');
            if (names[0] == parameterExpression.Type.Name)
            {
                names = names.Skip(1).ToArray();
            }
            MemberExpression left = Expression.Property(parameterExpression, names[0]);
            for (int i = 1; i < names.Length; i++)
            {
                left = Expression.Property(left, names[i]);
            }
            return left;
        }

        private Expression createBinaryExpressionsForRules(IEnumerable<FilterItem> filterItems, ParameterExpression pe, Expression pb = null, bool isNullCheck = false)
        {
            Expression predicateBody = pb;
            filterItems.Each(item =>
            {
                if (item.data.IsNotEmpty() || item.listOfIds != null)
                {
                    MemberExpression left = createMemberExpressionForProperty(pe, item.field);
                    var expression = ExpressionChooser(left, item, isNullCheck);
                    predicateBody = (predicateBody == null
                                                            ? expression
                                                            : Expression.AndAlso(predicateBody, expression));
                }
            });
            return predicateBody;
        }

        public Expression ExpressionChooser(MemberExpression left, FilterItem item, bool isNullCheck = false)
        {
            var accessor = ReflectionHelper.GetAccessor(left);
            var pi = accessor.InnerProperty;
            if (item.op == "StartsWith")
            {
                return isNullCheck ? getStartsWithExpressionForStringWithNullCheck(left, item.data) : getStartsWithExpressionForString(left, item.data);
            }
            if (item.op != "Exact" && pi.PropertyType.Name == "String")
                return isNullCheck ? getLikeExpressionForStringWithNullCheck(left, item.data) : getLikeExpressionForString(left, item.data);
            // if this is an empty set then should filter all items so we can't check for count>0 because it's valid
            if (item.op == "ListContains")
            {
                return getItemsWithIdsInEnumerableExpression(left, item.listOfIds);
            }
            if (item.op == "ListDoesNotContain" && item.listOfIds != null && item.listOfIds.Count() > 0)
            {
                return getItemsWithIdsNOTInEnumerableExpression(left, item.listOfIds);
            }
            return item.data.IsNotEmpty() ? getBinaryExpression(left, pi, item.data) : null;
        }

        private Expression getItemsWithIdsInEnumerableExpression(MemberExpression left, IEnumerable<int> data)
        {
            MethodInfo containseMethod = typeof(List<int>).GetMethod("Contains", new[] { typeof(int) });
            ConstantExpression value = Expression.Constant(data);
            MethodCallExpression containsMethodExp = Expression.Call(value, containseMethod, left);
            return containsMethodExp;

        }

        private Expression getItemsWithIdsNOTInEnumerableExpression(MemberExpression left, IEnumerable<int> data)
        {
            MethodInfo containseMethod = typeof(List<int>).GetMethod("Contains", new[] { typeof(int) });
            ConstantExpression value = Expression.Constant(data.ToList());
            MethodCallExpression containsMethodExp = Expression.Call(value, containseMethod, left);
            UnaryExpression unaryExpression = Expression.Not(containsMethodExp);
            return unaryExpression;
        }

        private Expression getLikeExpressionForString(MemberExpression left, string data)
        {
            ConstantExpression value = Expression.Constant(data.ToLowerInvariant());
            MethodInfo containseMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });
            MethodInfo toLowerMethod = typeof(string).GetMethod("ToLowerInvariant", Type.EmptyTypes);
            MethodCallExpression toLowerMethodExp = Expression.Call(left, toLowerMethod);
            return Expression.Call(toLowerMethodExp, containseMethod, value);
        }

        // these with null methods are don't work and are not needed for nhibernate but
        // are necessary for linq to objects
        private Expression getLikeExpressionForStringWithNullCheck(MemberExpression left, string data)
        {
            UnaryExpression notNullMethodExp = IsNotNullCheck(left);
            ConstantExpression value = Expression.Constant(data.ToLowerInvariant());
            MethodInfo containseMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });
            MethodInfo toLowerMethod = typeof(string).GetMethod("ToLowerInvariant", Type.EmptyTypes);
            MethodCallExpression toLowerMethodExp = Expression.Call(left, toLowerMethod);
            MethodCallExpression containsMethodExp = Expression.Call(toLowerMethodExp, containseMethod, value);
            return Expression.AndAlso(notNullMethodExp, containsMethodExp);
        }

        private Expression getStartsWithExpressionForString(MemberExpression left, string data)
        {
            ConstantExpression value = Expression.Constant(data.ToLowerInvariant());
            MethodInfo startsWithMethod = typeof(string).GetMethod("StartsWith", new[] { typeof(string) });
            MethodInfo toLowerMethod = typeof(string).GetMethod("ToLowerInvariant", Type.EmptyTypes);
            MethodCallExpression toLowerMethodExp = Expression.Call(left, toLowerMethod);
            return Expression.Call(toLowerMethodExp, startsWithMethod, value);
        }

        // these with null methods are don't work and are not needed for nhibernate but
        // are necessary for linq to objects
        private Expression getStartsWithExpressionForStringWithNullCheck(MemberExpression left, string data)
        {
            UnaryExpression notNullMethodExp = IsNotNullCheck(left);
            ConstantExpression value = Expression.Constant(data.ToLowerInvariant());
            MethodInfo startsWithMethod = typeof(string).GetMethod("StartsWith", new[] { typeof(string) });
            MethodInfo toLowerMethod = typeof(string).GetMethod("ToLowerInvariant", Type.EmptyTypes);
            MethodCallExpression toLowerMethodExp = Expression.Call(left, toLowerMethod);
            MethodCallExpression containsMethodExp = Expression.Call(toLowerMethodExp, startsWithMethod, value);
            return Expression.AndAlso(notNullMethodExp, containsMethodExp);
        }

        private UnaryExpression IsNotNullCheck(MemberExpression left)
        {
            MethodInfo isNullMethod = typeof(string).GetMethod("IsNullOrEmpty", new[] { typeof(string) });
            MethodCallExpression isNullMethodExp = Expression.Call(null, isNullMethod, left);
            UnaryExpression notNullMethodExp = Expression.Not(isNullMethodExp);
            return notNullMethodExp;
        }

        private Expression getBinaryExpression(MemberExpression left, PropertyInfo pi, string data)
        {
            Expression right = createExpressionConstantForData(pi, data);
            BinaryExpression binaryExpression = Expression.Equal(left, right);
            return binaryExpression;
        }

        public T? GetNullable<T>(string s) where T : struct
        {
            var result = new T?();
            if (!string.IsNullOrEmpty(s) && s.Trim().Length > 0)
            {
                var conv = TypeDescriptor.GetConverter(typeof(T));
                result = (T)conv.ConvertFrom(s);
            }
            return result;
        }

        private Expression createExpressionConstantForData(PropertyInfo propertyInfo, string data)
        {
            if (propertyInfo.PropertyType.IsNullable())
            {
                if (propertyInfo.PropertyType.UnderlyingSystemType.IsNullableOf(typeof(Int32)))
                {
                    return Expression.Constant(GetNullable<Int32>(data), typeof(Int32?));
                }
                if (propertyInfo.PropertyType.UnderlyingSystemType.IsNullableOf(typeof(Int64)))
                {
                    return Expression.Constant(GetNullable<Int64>(data), typeof(Int64?));
                }
                if (propertyInfo.PropertyType.UnderlyingSystemType.IsNullableOf(typeof(Decimal)))
                {
                    return Expression.Constant(GetNullable<Decimal>(data), typeof(Decimal?));
                }
                if (propertyInfo.PropertyType.UnderlyingSystemType.IsNullableOf(typeof(DateTime)))
                {
                    return Expression.Constant(GetNullable<DateTime>(data), typeof(DateTime?));
                }
                if (propertyInfo.PropertyType.UnderlyingSystemType.IsNullableOf(typeof(Boolean)))
                {
                    return Expression.Constant(GetNullable<Boolean>(data), typeof(Boolean?));
                }
            }
            switch (propertyInfo.PropertyType.Name)
            {
                case "Int32":
                    return Expression.Constant(Int32.Parse(data));
                case "Int64":
                    return Expression.Constant(Int64.Parse(data));
                case "Decimal":
                    return Expression.Constant(Decimal.Parse(data));
                case "DateTime":
                    return Expression.Constant(DateTime.Parse(data));
                case "Boolean":
                    return Expression.Constant(Boolean.Parse(data));
                default:
                    return Expression.Constant(data);
            }
        }

    }

    public class JqGridFilter
    {
        public string groupOp { get; set; }
        public IEnumerable<FilterItem> rules { get; set; }

    }

    public class FilterItem
    {
        public string field { get; set; }
        public string op { get; set; }
        public string data { get; set; }
        public IEnumerable<int> listOfIds { get; set; }
    }
}