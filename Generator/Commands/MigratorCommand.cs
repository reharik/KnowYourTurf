namespace Generator.Commands
{
    public class MigratorCommand : IGeneratorCommand
    {
        private readonly string _connectionString;

        public MigratorCommand()
        {
        }

        public string Description { get { return "Rebuilds Database"; } }

        public void Execute(string[] args)
        {
            new MigratorConsole(new[]
                {
                    "-a",
                    @"generator\bin\debug\DBFluentMigration.dll",
                    "-db",
                    "sqlserver2008",
                    "--conn",
                    args[0]
                });
        }
    }
}