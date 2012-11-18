using FluentMigrator;

namespace DBFluentMigration.Iteration_1
{
    [Migration(1080)]
    public class Add_EquipmentTask : Migration
    {
        public override void Up()
        {
            Create.Table("EquipmentTask").InSchema("dbo")
                  .WithColumn("EntityId").AsInt32().PrimaryKey().Identity().NotNullable()
                .WithColumn("ScheduledDate").AsDateTime().Nullable()
                .WithColumn("ActualTimeSpent").AsString().Nullable()
                .WithColumn("Notes").AsString().Nullable()
                .WithColumn("Deleted").AsBoolean().Nullable()
                .WithColumn("Complete").AsBoolean().Nullable()
                .WithColumn("TaskType_id").AsInt32().Nullable()
                .WithColumn("Equipment_id").AsInt32().Nullable()
                .WithColumn("IsDeleted").AsBoolean().Nullable()
                .WithColumn("CompanyId").AsInt32().Nullable()
                .WithColumn("CreatedBy_Id").AsInt32().Nullable()
                .WithColumn("ChangedBy_Id").AsInt32().Nullable()
                .WithColumn("ChangedDate").AsDateTime().Nullable()
                .WithColumn("CreatedDate").AsDateTime().Nullable();

            Create.ForeignKey("FK_EquipmentTask_oneToMany_EquipmentTaskType")
                  .FromTable("EquipmentTask")
                  .InSchema("dbo")
                  .ForeignColumns("TaskType_id")
                  .ToTable("EquipmentTaskType")
                  .InSchema("dbo")
                  .PrimaryColumns("EntityId");
            Create.ForeignKey("FK_EquipmentTask_oneToMany_Equipment")
                  .FromTable("EquipmentTask")
                  .InSchema("dbo")
                  .ForeignColumns("Equipment_Id")
                  .ToTable("Equipment")
                  .InSchema("dbo")
                  .PrimaryColumns("EntityId");
            Create.ForeignKey("FK_EquipmentTask_oneToMany_User_Created")
                  .FromTable("EquipmentTask")
                  .InSchema("dbo")
                  .ForeignColumns("CreatedBy_Id")
                  .ToTable("User")
                  .InSchema("dbo")
                  .PrimaryColumns("EntityId");
            Create.ForeignKey("FK_EquipmentTask_oneToMany_User_Changed")
                  .FromTable("EquipmentTask")
                  .InSchema("dbo")
                  .ForeignColumns("ChangedBy_Id")
                  .ToTable("User")
                  .InSchema("dbo")
                  .PrimaryColumns("EntityId");
        }

        public override void Down()
        {
            Delete.Table("EquipmentTask");
        }
    }
}