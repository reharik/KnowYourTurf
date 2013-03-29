using FluentMigrator;

namespace DBFluentMigration.Iteration_1
{
    [Migration(10060)]
    public class Add_WebSite_to_Equipment : Migration
    {
        public override void Up()
        {
            Create.Column("WebSite").OnTable("Equipment").AsString().Nullable();
        }

        public override void Down()
        {
            Delete.Column("WebSite").FromTable("Equipmnet");
        }
    }
}