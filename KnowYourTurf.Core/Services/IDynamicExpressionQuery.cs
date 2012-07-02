using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Script.Serialization;
using KnowYourTurf.Core.Domain;

namespace KnowYourTurf.Core.Services
{
    public interface IDynamicExpressionQuery
    {
        IQueryable<ENTITY> PerformQuery<ENTITY>(string json = null,
                                                Expression<Func<ENTITY, bool>> extraFilters = null,
                                                bool isNullCheck = false) where ENTITY : IPersistableObject;

        Expression<Func<ENTITY, bool>> PrepareExpression<ENTITY>(string json,
                                                                 Expression<Func<ENTITY, bool>> extraFilters = null);

        IQueryable<ENTITY> PerformQuery<ENTITY>(IEnumerable<ENTITY> items, string json = null,
                                                Expression<Func<ENTITY, bool>> extraFilters = null,
                                                bool isNullCheck = false);
    }

    public class DynamicExpressionQuery : IDynamicExpressionQuery
    {
        private readonly IRepository _repository;
        private readonly IDynamicExpressionBuilder _dynamicExpressionBuilder;

        public DynamicExpressionQuery(IRepository repository, IDynamicExpressionBuilder dynamicExpressionBuilder)
        {
            _repository = repository;
            _dynamicExpressionBuilder = dynamicExpressionBuilder;
        }

        public IQueryable<ENTITY> PerformQuery<ENTITY>(string json = null,
                                                        Expression<Func<ENTITY, bool>> extraFilters = null,
                                                        bool isNullCheck = false) where ENTITY : IPersistableObject
        {
            var expression = PrepareExpression(json, extraFilters);
            return expression == null ? _repository.Query<ENTITY>() : _repository.Query(expression);
        }
        public IQueryable<ENTITY> PerformQuery<ENTITY>(IEnumerable<ENTITY> items, string json = null,
                                                        Expression<Func<ENTITY, bool>> extraFilters = null,
                                                        bool isNullCheck = false) 
        {
            var expression = PrepareExpression(json, extraFilters);
            return expression == null ? items.AsQueryable() : items.Where(expression.Compile()).AsQueryable();
        }

        public Expression<Func<ENTITY, bool>> PrepareExpression<ENTITY>(string json, Expression<Func<ENTITY, bool>> extraFilters = null) 
        {
            if (json.IsEmpty()) return extraFilters;
            var jqGridFilter = DeserializeJson(json);
            if (jqGridFilter.rules.Any(x => x.op == "ListContains" && !x.listOfIds.Any())) return extraFilters;
            
            var expression = _dynamicExpressionBuilder.Build<ENTITY>(jqGridFilter);
            if(extraFilters == null)
            {
                return expression;
            }
            BinaryExpression binaryExpression = Expression.AndAlso(expression.Body, extraFilters.Body);
            Expression<Func<ENTITY, bool>> finalExpression = Expression.Lambda<Func<ENTITY, bool>>(binaryExpression, expression.Parameters);
            return finalExpression;
        }

        private JqGridFilter DeserializeJson(string json)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            JqGridFilter filter = jss.Deserialize<JqGridFilter>(json);
            return filter;
        }
    }
}