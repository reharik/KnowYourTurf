using FluentMigrator;

namespace DBFluentMigration.Iteration_1
{
    [Migration(20120)]
    public class Alter_TDAReport_Sproc_20120 : Migration
    {
        public override void Up()
        {
            string sql = System.IO.File.ReadAllText(@"dbfluentmigration\Alter_TDAReport_Sproc_20120.sql");
            Execute.Sql(sql);
        }

        public override void Down()
        {
        }
    }
}