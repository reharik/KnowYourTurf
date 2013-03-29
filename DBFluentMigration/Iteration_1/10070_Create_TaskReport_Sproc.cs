using FluentMigrator;

namespace DBFluentMigration.Iteration_1
{
    [Migration(10070)]
    public class Create_TaskReportSproc : Migration
    {
        public override void Up()
        {
            string sql = System.IO.File.ReadAllText(@"dbfluentmigration\Create_TaskReport_Sproc.sql");
            Execute.Sql(sql);
        }

        public override void Down()
        {
        }
    }
}