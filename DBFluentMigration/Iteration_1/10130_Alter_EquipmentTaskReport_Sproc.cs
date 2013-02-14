using FluentMigrator;

namespace DBFluentMigration.Iteration_1
{
    [Migration(100130)]
    public class Alter_EquipmentTaskReport_Sproc : Migration
    {
        public override void Up()
        {
            string sql = System.IO.File.ReadAllText(@"dbfluentmigration\Alter_EquipmentTaskReport_Sproc.sql");
            Execute.Sql(sql);
        }

        public override void Down()
        {
        }
    }
}