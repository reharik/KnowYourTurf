using System;
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
                                                bool isNullCheck = false) where ENTITY : DomainEntity;

        Expression<Func<ENTITY, bool>> PrepareExpression<ENTITY>(string json, Expression<Func<ENTITY, bool>> extraFilters= null)where ENTITY : DomainEntity;
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
                                                        bool isNullCheck = false) where ENTITY : DomainEntity
        {
            var expression = PrepareExpression(json, extraFilters);
            return expression == null ? _repository.Query<ENTITY>() : _repository.Query(expression);
        }

        public Expression<Func<ENTITY, bool>> PrepareExpression<ENTITY>(string json, Expression<Func<ENTITY, bool>> extraFilters= null)where ENTITY : DomainEntity
        {
            if (json.IsEmpty()) return extraFilters;
            var jqGridFilter = DeserializeJson(json);
            if (jqGridFilter.rules.Any(x => x.op == "ListContains" && x.listOfIds.Count() <= 0)) return extraFilters;
            
            var expression = _dynamicExpressionBuilder.Build<ENTITY>(jqGridFilter);
            
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