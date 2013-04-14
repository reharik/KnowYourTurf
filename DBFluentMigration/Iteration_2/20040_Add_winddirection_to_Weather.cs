namespace DBFluentMigration.Iteration_2
{
    using FluentMigrator;

    [Migration(20040)]
    public class Add_winddirection_to_Weather : Migration
    {
        public override void Up()
        {
            Create.Column("WindDirection").OnTable("Weather").AsString().Nullable();
        }

        public override void Down()
        {
            Delete.Column("WindDirection").FromTable("Weather");
        }
    }
}