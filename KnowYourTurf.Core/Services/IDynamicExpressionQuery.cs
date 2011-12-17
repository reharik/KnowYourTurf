using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Script.Serialization;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Html.Grid;

namespace KnowYourTurf.Core.Services
{
    public interface IDynamicExpressionQuery
    {
        IQueryable<ENTITY> PerformQuery<ENTITY>(string json = null,
                                                Expression<Func<ENTITY, bool>> extraFilters = null,
                                                bool isNullCheck = false) where ENTITY : Entity;

        IQueryable<ENTITY> PerformQueryWithItems<ENTITY>(IEnumerable<ENTITY> items,string json = null,
                                               Expression<Func<ENTITY, bool>> extraFilters = null,
                                               bool isNullCheck = false) where ENTITY : Entity;

        IQueryable<ENTITY> PerformQuery<ENTITY>(Grid<ENTITY> _grid, string json = null,
                                                                Expression<Func<ENTITY, bool>> extraFilters = null,
                                                                bool isNullCheck = false) where ENTITY : Entity;
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

        public IQueryable<ENTITY> PerformQuery<ENTITY>(Grid<ENTITY> _grid, string json = null,
                                                        Expression<Func<ENTITY, bool>> extraFilters = null,
                                                        bool isNullCheck = false) where ENTITY : Entity
        {
            var expression = prepareExpression(json, extraFilters);
            var entities = expression == null ? _repository.Query<ENTITY>() : _repository.Query(expression);
            var defaultSortColumnName = _grid.GetDefaultSortColumnName();
            if (defaultSortColumnName.IsNotEmpty())
            {
                entities.OrderBy(_grid.GetDefaultSortColumnName());
            }
            return entities;
        }

        public IQueryable<ENTITY> PerformQuery<ENTITY>(string json = null, 
                                                        Expression<Func<ENTITY, bool>> extraFilters = null,
                                                        bool isNullCheck = false) where ENTITY : Entity
        {
            var expression = prepareExpression(json, extraFilters);
            return expression == null ? _repository.Query<ENTITY>() : _repository.Query(expression);
        }

        public IQueryable<ENTITY> PerformQueryWithItems<ENTITY>(IEnumerable<ENTITY> items, string json = null,
                                                        Expression<Func<ENTITY, bool>> extraFilters = null,
                                                        bool isNullCheck = false) where ENTITY : Entity
        {
            var expression = prepareExpression(json, extraFilters);
            return expression == null ? items.AsQueryable() : items.Where(expression.Compile()).AsQueryable();
        }

        private Expression<Func<ENTITY, bool>> prepareExpression<ENTITY>(string json, Expression<Func<ENTITY, bool>> extraFilters) where ENTITY : Entity
        {
            if (json.IsEmpty()) 
                return extraFilters;
            var jqGridFilter = DeserializeJson(json);
            
            if (!jqGridFilter.rules.Any() ||  
                jqGridFilter.rules.Any(x => x.op == "ListContains" && x.listOfIds.Count() <= 0)) 
                return extraFilters;
            
            var expression = _dynamicExpressionBuilder.Build<ENTITY>(jqGridFilter);
            Expression binaryExpression = extraFilters != null
                                                    ? Expression.AndAlso(expression.Body, extraFilters.Body)
                                                    : expression.Body;

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