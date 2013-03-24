using FluentMigrator;

namespace DBFluentMigration.Iteration_1
{
    [Migration(10090)]
    public class Add_ID_To_Equipment : Migration
    {
        public override void Up()
        {
            Create.Column("ID").OnTable("Equipment").AsString().Nullable();
        }

        public override void Down()
        {
            Delete.Column("ID").FromTable("Equipment");
        }
    }
}