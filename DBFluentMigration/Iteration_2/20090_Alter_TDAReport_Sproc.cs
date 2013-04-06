using FluentMigrator;

namespace DBFluentMigration.Iteration_1
{
    [Migration(20090)]
    public class Alter_TDAReport_Sproc : Migration
    {
        public override void Up()
        {
            string sql = System.IO.File.ReadAllText(@"dbfluentmigration\Alter_TDAReport_Sproc_20090.sql");
            Execute.Sql(sql);
        }

        public override void Down()
        {
        }
    }
}