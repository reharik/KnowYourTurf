using KnowYourTurf.Core.Domain;
using KnowYourTurf.Web.Services;
using NHibernate;
using StructureMap;

namespace Generator.Commands
{
    public class DefaultSecuritySetupCommand : IGeneratorCommand
    {
        private readonly IRepository _repository;
        private readonly ISecuritySetupService _securitySetupService;

        public DefaultSecuritySetupCommand(IRepository repository, ISecuritySetupService securitySetupService)
        {
            _repository = repository;
            _securitySetupService = securitySetupService;
        }

        public string Description { get { return "Loads security options defaults"; } }

        public void Execute(string[] args)
        {
            ObjectFactory.Configure(x => x.For<ISessionFactory>().Singleton().Use(ctx => ctx.GetInstance<ISessionFactoryConfiguration>().CreateSessionFactory()));
            var sessionFactory = ObjectFactory.GetInstance<ISessionFactory>();
            SqlServerHelper.killRhinoSecurity(sessionFactory);
            _securitySetupService.ExecuteAll();
        }
    }
}