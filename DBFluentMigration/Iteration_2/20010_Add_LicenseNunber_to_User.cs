namespace DBFluentMigration.Iteration_2
{
    using FluentMigrator;

    [Migration(20010)]
    public class Add_LicenseNunber_to_User : Migration
    {
        public override void Up()
        {
            Create.Column("LicenseNumber").OnTable("User").AsString().Nullable();
        }

        public override void Down()
        {
            Delete.Column("LicenseNumber").FromTable("User");
        }
    }
}