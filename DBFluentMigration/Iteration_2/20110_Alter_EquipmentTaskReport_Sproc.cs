using FluentMigrator;

namespace DBFluentMigration.Iteration_1
{
    [Migration(20110)]
    public class Alter_EquipmentTaskReport_Sproc_20110 : Migration
    {
        public override void Up()
        {
            string sql = System.IO.File.ReadAllText(@"dbfluentmigration\Alter_EquipmentTaskReport_Sproc_20110.sql");
            Execute.Sql(sql);
        }

        public override void Down()
        {
        }
    }
}