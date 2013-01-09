using FluentMigrator;

namespace DBFluentMigration.Iteration_1
{
    [Migration(10010)]
    public class Create_TasksByFields_Sproc : Migration
    {
        public override void Up()
        {
            string sql = System.IO.File.ReadAllText(@"dbfluentmigration\Create_TasksByField_Sproc.sql");
            Execute.Sql(sql);
        }

        public override void Down()
        {
        }
    }
}