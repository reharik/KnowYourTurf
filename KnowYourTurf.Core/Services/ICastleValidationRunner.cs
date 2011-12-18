using System;
using System.Collections.Generic;
using System.Linq;
using Castle.Components.Validator;
using KnowYourTurf.Core.Domain;
using xVal.ServerSide;

namespace KnowYourTurf.Core.Services
{
    public interface ICastleValidationRunner
    {
        IEnumerable<ErrorInfo> GetErrors<ENTITY>(ENTITY entity) where ENTITY : DomainEntity;
        CrudReport Validate<ENTITY>(ENTITY entity) where ENTITY : DomainEntity;
    }

    public class CastleValidationRunner : ICastleValidationRunner
    {
        private static readonly CachedValidationRegistry registry = new CachedValidationRegistry();

        public IEnumerable<ErrorInfo> GetErrors<ENTITY>(ENTITY entity) where ENTITY : DomainEntity
        {
            var result = new List<ErrorInfo>();
            var runner = new ValidatorRunner(registry);

            if (entity != null && !runner.IsValid(entity))
            {
                var errorSummary = runner.GetErrorSummary(entity);
                var errorInfos = errorSummary.InvalidProperties.SelectMany(
                    prop => errorSummary.GetErrorsForProperty(prop),
                    (prop, err) => new ErrorInfo(prop, err));
                result.AddRange(errorInfos);
            }
            return result;
        }

        public CrudReport Validate<ENTITY>(ENTITY entity) where ENTITY : DomainEntity
        {
            var crudReport = new CrudReport();
            var runner = new ValidatorRunner(registry);
            if (runner.IsValid(entity))
            {
                crudReport.Success = true;
            }
            else
            {
                crudReport.AddErrorInfos(GetErrors(entity));
            }
            return crudReport;
        }
    }
}