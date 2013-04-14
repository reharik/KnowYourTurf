using FluentMigrator;

namespace DBFluentMigration.Iteration_1
{
    [Migration(20030)]
    public class Alter_TaskReport_Sproc_subselect : Migration
    {
        public override void Up()
        {
            string sql = System.IO.File.ReadAllText(@"dbfluentmigration\Alter_TaskReport_Sproc_20030.sql");
            Execute.Sql(sql);
        }

        public override void Down()
        {
        }
    }
}