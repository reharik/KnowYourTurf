namespace DBFluentMigration.Iteration_2
{
    using FluentMigrator;

    [Migration(20040)]
    public class Create_LoginStatistics_Table : Migration
    {
        public override void Up()
        {
            Create.Table("LoginStatistics")
                  .WithColumn("EntityId").AsInt32().Identity().PrimaryKey().NotNullable()
                  .WithColumn("ChangedDate").AsDateTime()
                  .WithColumn("CreatedDate").AsDateTime()
                  .WithColumn("IsDeleted").AsBoolean()
                  .WithColumn("ClientId").AsInt32()
                  .WithColumn("CreatedBy_Id").AsInt32().ForeignKey("User", "EntityId")
                  .WithColumn("ChangedBy_Id").AsInt32().ForeignKey("User", "EntityId")
                  .WithColumn("User_Id").AsInt32().ForeignKey("User", "EntityId")
                  .WithColumn("BrowserType").AsString()
                  .WithColumn("BrowserVersion").AsString()
                  .WithColumn("UserAgent").AsString()
                  .WithColumn("UserHostName").AsString()
                  .WithColumn("UserHostAddress").AsString();

        }

        public override void Down()
        {
            Delete.Table("LoginStatistics");
        }
    }
}