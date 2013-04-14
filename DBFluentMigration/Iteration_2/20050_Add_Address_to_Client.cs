namespace DBFluentMigration.Iteration_2
{
    using FluentMigrator;

    [Migration(20050)]
    public class Add_Address_To_Client : Migration
    {
        public override void Up()
        {
            Create.Column("Address").OnTable("Client").AsString().Nullable();
            Create.Column("Address2").OnTable("Client").AsString().Nullable();
            Create.Column("City").OnTable("Client").AsString().Nullable();
            Create.Column("State").OnTable("Client").AsString().Nullable();
        }

        public override void Down()
        {
            Delete.Column("Address").FromTable("Client");
            Delete.Column("Address2").FromTable("Client");
            Delete.Column("City").FromTable("Client");
            Delete.Column("State").FromTable("Client");
        }
    }
}