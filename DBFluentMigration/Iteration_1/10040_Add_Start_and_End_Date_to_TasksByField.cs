using FluentMigrator;

namespace DBFluentMigration.Iteration_1
{
    [Migration(10040)]
    public class Add_Start_and_End_Date_to_TasksByField : Migration
    {
        public override void Up()
        {
            string sql = System.IO.File.ReadAllText(@"dbfluentmigration\AlterTasksByField.sql");
            Execute.Sql(sql);
        }

        public override void Down()
        {
        }
    }
}