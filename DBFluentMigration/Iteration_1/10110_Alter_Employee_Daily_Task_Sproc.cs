<<<<<<< HEAD
﻿using FluentMigrator;

namespace DBFluentMigration.Iteration_1
{
    [Migration(10110)]
    public class Alter_Employee_Daily_Task_Sproc : Migration
    {
        public override void Up()
        {
            string sql = System.IO.File.ReadAllText(@"dbfluentmigration\Alter_EmployeeDailyTasks_Sproc.sql");
            Execute.Sql(sql);
        }

        public override void Down()
        {
        }
    }
=======
﻿using FluentMigrator;

namespace DBFluentMigration.Iteration_1
{
    [Migration(100110)]
    public class Alter_Employee_Daily_Task_Sproc : Migration
    {
        public override void Up()
        {
            string sql = System.IO.File.ReadAllText(@"dbfluentmigration\Alter_EmployeeDailyTasks_Sproc.sql");
            Execute.Sql(sql);
        }

        public override void Down()
        {
        }
    }
>>>>>>> e6c768aa5fcde7225dff8a7677f36df214c0183f
}