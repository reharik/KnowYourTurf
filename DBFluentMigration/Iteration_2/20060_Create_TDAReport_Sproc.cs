using FluentMigrator;

namespace DBFluentMigration.Iteration_1
{
    [Migration(20060)]
    public class Create_TDAReport_Sproc : Migration
    {
        public override void Up()
        {
            string sql = System.IO.File.ReadAllText(@"dbfluentmigration\Create_TDAReport_sproc.sql");
            Execute.Sql(sql);
        }

        public override void Down()
        {
        }
    }
}