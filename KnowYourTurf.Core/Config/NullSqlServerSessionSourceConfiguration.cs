using NHibernate;

namespace KnowYourTurf.Core.Domain
{
    public class NullSqlServerSessionSourceConfiguration : ISessionFactoryConfiguration
    {
        private readonly INHSetupConfig _config;
        private readonly string _connectionStr;

        public NullSqlServerSessionSourceConfiguration(INHSetupConfig config)
        {
            _config = config;
        }

        public ISessionFactory CreateSessionFactoryAndGenerateSchema()
        {
            return null;
        }

        public ISessionFactory CreateSessionFactory()
        {
            return null;
        }

    }
}