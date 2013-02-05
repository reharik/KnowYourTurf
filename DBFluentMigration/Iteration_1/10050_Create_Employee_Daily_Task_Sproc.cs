using FluentMigrator;

namespace DBFluentMigration.Iteration_1
{
    [Migration(10050)]
    public class Create_Employee_Daily_Task_Sproc : Migration
    {
        public override void Up()
        {
            string sql = System.IO.File.ReadAllText(@"dbfluentmigration\Create_EmployeeDailyTasks_Sproc.sql");
            Execute.Sql(sql);
        }

        public override void Down()
        {
        }
    }
}