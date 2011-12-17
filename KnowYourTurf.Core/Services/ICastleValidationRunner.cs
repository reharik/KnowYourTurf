using System;
using System.Collections.Generic;
using System.Linq;
using Castle.Components.Validator;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Domain.Tools;
using xVal.ServerSide;

namespace KnowYourTurf.Core.Services
{
    public interface ICastleValidationRunner
    {
        IEnumerable<ErrorInfo> GetErrors<ENTITY>(ENTITY entity) where ENTITY : Entity;
        CrudReport Validate<ENTITY>(ENTITY entity) where ENTITY : Entity;
    }

    public class DummyCastleValidationRunnerSuccess : ICastleValidationRunner
    {
        #region Implementation of ICastleValidationRunner

        public IEnumerable<ErrorInfo> GetErrors<ENTITY>(ENTITY entity) where ENTITY : Entity
        {
            throw new NotImplementedException();
        }

        public CrudReport Validate<ENTITY>(ENTITY entity) where ENTITY : Entity
        {
            var crudReport = new CrudReport { Success = true };
            return crudReport;
        }

        #endregion
    }

    public class DummyCastleValidationRunnerFail : ICastleValidationRunner
    {
        #region Implementation of ICastleValidationRunner

        public IEnumerable<ErrorInfo> GetErrors<ENTITY>(ENTITY entity) where ENTITY : Entity
        {
            throw new NotImplementedException();
        }

        public CrudReport Validate<ENTITY>(ENTITY entity) where ENTITY : Entity
        {
            var crudReport = new CrudReport { Success = false};
            crudReport.AddErrorInfo(new ErrorInfo("test", "test error"));
            return crudReport;
        }
        #endregion
    }

    public class CastleValidationRunner : ICastleValidationRunner
    {
        private static readonly CachedValidationRegistry registry = new CachedValidationRegistry();

        public IEnumerable<ErrorInfo> GetErrors<ENTITY>(ENTITY entity) where ENTITY : Entity
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

        public CrudReport Validate<ENTITY>(ENTITY entity) where ENTITY : Entity
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