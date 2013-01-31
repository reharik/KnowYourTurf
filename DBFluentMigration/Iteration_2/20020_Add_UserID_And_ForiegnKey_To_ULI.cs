namespace DBFluentMigration.Iteration_2
{
    using FluentMigrator;

    [Migration(20020)]
    public class Add_UserID_And_ForeignKey_To_ULI : Migration
    {
        public override void Up()
        {
            Create.Column("User_Id").OnTable("UserLoginInfo").AsInt32().Nullable();
            Create.ForeignKey("FK_User_OneToOne_UserLoginInfo").FromTable("UserLoginInfo").ForeignColumn("User_id").ToTable("User").PrimaryColumn("EntityId");
        }

        public override void Down()
        {
            Delete.Column("User_Id").FromTable("UserLoginInfo");
            Delete.ForeignKey("FK_User_OneToOne_UserLoginInfo").OnTable("UserLoginInfo");
        }
    }
}