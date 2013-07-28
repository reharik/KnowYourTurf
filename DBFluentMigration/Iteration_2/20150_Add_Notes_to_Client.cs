namespace DBFluentMigration.Iteration_2
{
    using FluentMigrator;

    [Migration(20150)]
    public class Add_Notes_to_Client : Migration
    {
        public override void Up()
        {
            Create.Column("Notes").OnTable("Client").AsString().Nullable();
        }

        public override void Down()
        {
            Delete.Column("Notes").FromTable("Client");
        }
    }
}