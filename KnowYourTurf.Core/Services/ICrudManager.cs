using System.Collections.Generic;
using System.Linq;
using KnowYourTurf.Core.Domain;
using xVal.ServerSide;

namespace KnowYourTurf.Core.Services
{
    public interface ICrudManager
    {
        IEnumerable<CrudReport> GetCrudReports();
        void RemoveCrudReport(CrudReport crudReport);
        void AddCrudReport(CrudReport crudReport);
        bool HasFailingReport();
        Notification Finish();
    }

    public class CrudManager : ICrudManager
    {
        private readonly IRepository _repository;

        public CrudManager(IRepository repository)
        {
            _repository = repository;
        }

        #region Collections
        private readonly IList<CrudReport> _crudReports = new List<CrudReport>();
        public IEnumerable<CrudReport> GetCrudReports() { return _crudReports; }
        public void RemoveCrudReport(CrudReport crudReport)
        {
            _crudReports.Remove(crudReport);
        }
        public void AddCrudReport(CrudReport crudReport)
        {
            _crudReports.Add(crudReport);
        }
        #endregion

        public bool HasFailingReport()
        {
            return _crudReports.Any(x => !x.Success);
        }
        
        public Notification Finish()
        {
            var notification = new Notification { Success = true };
            GetCrudReports().Each(x =>
            {
                if (!x.Success)
                {
                    notification.Success = false;
                    if (notification.Errors == null)
                        notification.Errors = x.GetErrorInfos().ToList();
                    else
                        x.GetErrorInfos().Each(notification.Errors.Add).ToList();
                }
            });
            if (notification.Success)
            {
                _repository.Commit();
                notification.Message = CoreLocalizationKeys.SUCCESSFUL_SAVE.ToString();
            }
            else
                _repository.Rollback();
            return notification;
        }
    }

    public class CrudReport
    {
        public bool Success { get; set; }
        #region Collections
        private readonly IList<ErrorInfo> _errorInfos = new List<ErrorInfo>();
        public IEnumerable<ErrorInfo> GetErrorInfos() { return _errorInfos; }
        public void RemoveErrorInfo(ErrorInfo errorInfo)
        {
            _errorInfos.Remove(errorInfo);
        }
        public void AddErrorInfo(ErrorInfo errorInfo)
        {
            _errorInfos.Add(errorInfo);
        }
        public void AddErrorInfos(IEnumerable<ErrorInfo> errors)
        {
            _errorInfos.AddMany(errors.ToArray());
        }
        #endregion
    }

}