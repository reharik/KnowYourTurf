using NHibernate;
using StructureMap;
using KnowYourTurf.Core.Domain;


namespace Generator.Commands
{

    public class DropReaddDatabaseCommand : IGeneratorCommand
    {
        private readonly ISessionFactory _sessionFactory;

        public DropReaddDatabaseCommand(ISessionFactory sessionFactory)
        {
            _sessionFactory = sessionFactory;
        }

        public string Description { get { return "Rebuilds the db and data"; } }

        public void Execute(string[] args)
        {
            SqlServerHelper.DeleteReaddDb(_sessionFactory);
            var sessionFactoryConfiguration =
                    ObjectFactory.Container.With("connectionStr")
                                 .EqualTo(args[0])
                                 .GetInstance<ISessionFactoryConfiguration>();
            ObjectFactory.Container.Inject(sessionFactoryConfiguration);
        }
    }
}