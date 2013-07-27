namespace DBFluentMigration.Iteration_2
{
    using FluentMigrator;

    [Migration(20160)]
    public class Add_UserRoles_to_Client : Migration
    {
        public override void Up()
        {
            Create.Column("Client_id").OnTable("UserRole").AsInt32().NotNullable().ForeignKey("FK_ClientToUserRole", "UserRole", "EntityId");
        }

        public override void Down()
        {
            Delete.Column("Client_id").FromTable("UserRole");
        }
    }
}