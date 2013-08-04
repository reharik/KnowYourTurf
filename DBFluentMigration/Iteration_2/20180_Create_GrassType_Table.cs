namespace DBFluentMigration.Iteration_2
{
    using FluentMigrator;

    [Migration(20180)]
    public class Create_GrassType_Table : Migration
    {
        public override void Up()
        {
            Create.Table("GrassType")
                  .WithColumn("EntityId").AsInt32().NotNullable().PrimaryKey().Identity()
                  .WithColumn("ChangedDate").AsDateTime()
                  .WithColumn("ChangedDate").AsDateTime()
                  .WithColumn("IsDeleted").AsBoolean()
                  .WithColumn("ClientId").AsInt32().ForeignKey("FK_GrassTypeToClient", "Client", "EntityId")
                  .WithColumn("Name").AsString()
                  .WithColumn("Description").AsString()
                  .WithColumn("Status").AsString()
                  .WithColumn("CreatedBy_Id").AsInt32().ForeignKey("FK_GrassTypeToUser_CreatedBy", "User", "EntityId")
                  .WithColumn("ChangedBy_Id").AsInt32().ForeignKey("FK_GrassTypeToUser_ChangedBy", "User", "EntityId");
        }

        public override void Down()
        {
        }
    }
}