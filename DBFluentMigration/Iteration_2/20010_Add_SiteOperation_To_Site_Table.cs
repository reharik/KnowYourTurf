using FluentMigrator;

namespace DBFluentMigration.Iteration_2
{
    [Migration(20010)]
    public class Add_SiteOperation_To_Site_Table : Migration
    {
        public override void Up()
        {
            Create.Column("SiteOperation").OnTable("Site").AsString().Nullable();
        }

        public override void Down()
        {
            Delete.Column("SiteOperation").FromTable("Site");
        }
    }
}