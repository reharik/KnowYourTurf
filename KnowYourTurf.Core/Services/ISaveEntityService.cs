using KnowYourTurf.Core.Domain;
using StructureMap;

namespace KnowYourTurf.Core.Services
{
    public interface ISaveEntityService
    {
        ICrudManager ProcessSave<DOMAINMODEL>(DOMAINMODEL model, ICrudManager crudManager) where DOMAINMODEL : Entity;
        ICrudManager ProcessSave<DOMAINMODEL>(DOMAINMODEL model) where DOMAINMODEL : Entity;
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

        public ICrudManager ProcessSave<DOMAINMODEL>(DOMAINMODEL model) where DOMAINMODEL : Entity
        {
            var crudManager = new CrudManager(_repository);
            return ProcessSave(model, crudManager);
        }

        public ICrudManager ProcessSave<DOMAINMODEL>(DOMAINMODEL model, ICrudManager crudManager)
            where DOMAINMODEL : Entity
        {
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

    // you can't turn off the interceptor you have to have a session object with never recieved one.
    // repo is request scoped so once you get one your stuck with it.  the orgional ses has repo 
    // constructor injected so you screwed must use one that doesn't have it in the constructor
    public interface ISaveEntityServiceWithoutPrincipal
    {
        ICrudManager ProcessSave<DOMAINMODEL>(DOMAINMODEL model) where DOMAINMODEL : Entity;
    }

    public class NullSaveEntityServiceWithoutPrincipal  : ISaveEntityServiceWithoutPrincipal
    {
        private readonly ICastleValidationRunner _castleValidationRunner;

        public NullSaveEntityServiceWithoutPrincipal(ICastleValidationRunner castleValidationRunner)
        {
            _castleValidationRunner = castleValidationRunner;
        }

        public ICrudManager ProcessSave<DOMAINMODEL>(DOMAINMODEL model) where DOMAINMODEL : Entity
        {
            throw new System.NotImplementedException();
        }
    }

    public class SaveEntityServiceWithoutPrincipal : ISaveEntityServiceWithoutPrincipal
    {
        private readonly ICastleValidationRunner _castleValidationRunner;
        private IRepository _repository;

        //public SaveEntityServiceWithoutPrincipal(IContainer container, ICastleValidationRunner castleValidationRunner)
        //{
        //    _castleValidationRunner = castleValidationRunner;
        //    _repository = container.GetInstance<Repository>("NoFiltersOrInterceptor");
        //}

        public ICrudManager ProcessSave<DOMAINMODEL>(DOMAINMODEL model) where DOMAINMODEL : Entity
        {
            //var report = _castleValidationRunner.Validate(model);
            //if (report.Success)
            //{
            //    _repository.Save(model);
            //    //report.Target = model;
            //}
            //var crudManager = new CrudManager(_repository);
            //crudManager.AddCrudReport(report);
            //return crudManager;
            return null;
        }
    }
}