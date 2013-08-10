namespace DBFluentMigration.Iteration_2
{
    using FluentMigrator;

    [Migration(20170)]
    public class Add_GrassType_to_Field : Migration
    {
        public override void Up()
        {
            Create.Column("GrassType").OnTable("Field").AsInt32().NotNullable().WithDefaultValue("");
        }

        public override void Down()
        {
            Delete.Column("GrassType").FromTable("Field");
        }
    }
}