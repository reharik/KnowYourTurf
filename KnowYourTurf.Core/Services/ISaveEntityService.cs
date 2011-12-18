using KnowYourTurf.Core.Domain;

namespace KnowYourTurf.Core.Services
{
    public interface ISaveEntityService
    {
        ICrudManager ProcessSave<DOMAINMODEL>(DOMAINMODEL model, ICrudManager crudManager=null) where DOMAINMODEL : DomainEntity;
    }

    public class SaveEntityService : ISaveEntityService
    {
        private readonly IRepository _repository;
        private readonly ICastleValidationRunner _castleValidationRunner;

        public SaveEntityService(IRepository repository, ICastleValidationRunner castleValidationRunner)
        {
            _repository = repository;
            _castleValidationRunner = castleValidationRunner;
        }

        public ICrudManager ProcessSave<DOMAINMODEL>(DOMAINMODEL model, ICrudManager crudManager = null)
            where DOMAINMODEL : DomainEntity
        {
            if(crudManager==null) crudManager = new CrudManager(_repository);
            var report = _castleValidationRunner.Validate(model);
            if (report.Success)
            {
                _repository.Save(model);
                //report.Target = model;
            }
            crudManager.AddCrudReport(report);
            return crudManager;
        }
    }

    
}