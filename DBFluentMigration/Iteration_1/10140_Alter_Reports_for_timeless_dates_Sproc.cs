using FluentMigrator;

namespace DBFluentMigration.Iteration_1
{
    [Migration(10140)]
    public class Alter_Reports_for_timeless_dates_Sproc_10140 : Migration
    {
        public override void Up()
        {
            string sql1 = System.IO.File.ReadAllText(@"dbfluentmigration\alter_EmployeeDailyTasks_Sproc_10140.sql");
            string sql2 = System.IO.File.ReadAllText(@"dbfluentmigration\alter_EquipmentTaskReport_Sproc_10140.sql");
            string sql3 = System.IO.File.ReadAllText(@"dbfluentmigration\Alter_TaskReport_Sproc_10140.sql");
            Execute.Sql(sql1);
            Execute.Sql(sql2);
            Execute.Sql(sql3);
        }

        public override void Down()
        {
        }
    }
}