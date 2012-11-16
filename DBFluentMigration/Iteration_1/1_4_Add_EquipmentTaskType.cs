using FluentMigrator;

namespace DBFluentMigration.Iteration_1
{
    [Migration(1004)]
    public class Add_EquipmentTaskType : Migration
    {
        public override void Up()
        {
            Create.Table("EquipmentTaskType").InSchema("dbo")
                  .WithColumn("EntityId").AsInt32().PrimaryKey().Identity().NotNullable()
                  .WithColumn("Name").AsString().Nullable()
                  .WithColumn("Description").AsString().Nullable()
                  .WithColumn("Status").AsString().Nullable()
                .WithColumn("IsDeleted").AsBoolean().Nullable()
                .WithColumn("CompanyId").AsInt32().Nullable()
                .WithColumn("CreatedBy_Id").AsInt32().Nullable()
                .WithColumn("ChangedBy_Id").AsInt32().Nullable()
                .WithColumn("ChangedDate").AsDateTime().Nullable()
                .WithColumn("CreatedDate").AsDateTime().Nullable();


            Create.ForeignKey("FK_EquipmentTaskType_oneToMany_User_Created")
                  .FromTable("EquipmentTaskType")
                  .InSchema("dbo")
                  .ForeignColumns("CreatedBy_Id")
                  .ToTable("User")
                  .InSchema("dbo")
                  .PrimaryColumns("EntityId");
            Create.ForeignKey("FK_EquipmentTaskType_oneToMany_User_Changed")
                  .FromTable("EquipmentTaskType")
                  .InSchema("dbo")
                  .ForeignColumns("ChangedBy_Id")
                  .ToTable("User")
                  .InSchema("dbo")
                  .PrimaryColumns("EntityId");
        }

        public override void Down()
        {
            Delete.Table("EquipmentTaskType");
        }
    }
}