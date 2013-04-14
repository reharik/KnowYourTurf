using FluentMigrator;

namespace DBFluentMigration.Iteration_1
{
    [Migration(10120)]
    public class Alter_TaskReport_Sproc : Migration
    {
        public override void Up()
        {
            string sql = System.IO.File.ReadAllText(@"dbfluentmigration\Alter_TaskReport_Sproc.sql");
            Execute.Sql(sql);
        }

        public override void Down()
        {
        }
    }
}