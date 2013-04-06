using FluentMigrator;

namespace DBFluentMigration.Iteration_1
{
    [Migration(20080)]
    public class Alter_EquipmentTaskReport_Sproc_20020 : Migration
    {
        public override void Up()
        {
            string sql = System.IO.File.ReadAllText(@"dbfluentmigration\Alter_EquipmentTaskReport_Sproc_20080.sql");
            Execute.Sql(sql);
        }

        public override void Down()
        {
        }
    }
}