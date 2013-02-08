using FluentMigrator;

namespace DBFluentMigration.Iteration_1
{
    [Migration(10080)]
    public class Create_EquipmentTaskReport_Sproc : Migration
    {
        public override void Up()
        {
            string sql = System.IO.File.ReadAllText(@"dbfluentmigration\Create_EquipmentTaskReport_Sproc.sql");
            Execute.Sql(sql);
        }

        public override void Down()
        {
        }
    }
}