using NHibernate;

namespace Generator.Commands
{
    public class KillAllFKsCommand : IGeneratorCommand
    {
        private readonly ISessionFactory _sessionFactory;

        public KillAllFKsCommand(ISessionFactory source)
        {
            _sessionFactory = source;
        }

        public string Description { get { return "Removes all the FK's from the db"; } }

        public void Execute(string[] args)
        {
            SqlServerHelper.KillAllFKs(_sessionFactory);
        }
    }
}