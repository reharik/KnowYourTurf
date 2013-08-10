using FluentMigrator;

namespace DBFluentMigration.Iteration_1
{
    [Migration(20100)]
    public class Alter_TaskReport_Sproc_20100 : Migration
    {
        public override void Up()
        {
            string sql = System.IO.File.ReadAllText(@"dbfluentmigration\Alter_TaskReport_Sproc_20100.sql");
            Execute.Sql(sql);
        }

        public override void Down()
        {
        }
    }
}