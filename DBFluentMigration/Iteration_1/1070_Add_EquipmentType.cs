using FluentMigrator;

namespace DBFluentMigration.Iteration_1
{
    [Migration(1070)]
    public class Add_EquipmentType : Migration
    {
        public override void Up()
        {
            Create.Table("EquipmentType").InSchema("dbo")
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


            Create.ForeignKey("FK_EquipmentType_oneToMany_User_Created")
                  .FromTable("EquipmentType")
                  .InSchema("dbo")
                  .ForeignColumns("CreatedBy_Id")
                  .ToTable("User")
                  .InSchema("dbo")
                  .PrimaryColumns("EntityId");
            Create.ForeignKey("FK_EquipmentType_oneToMany_User_Changed")
                  .FromTable("EquipmentType")
                  .InSchema("dbo")
                  .ForeignColumns("ChangedBy_Id")
                  .ToTable("User")
                  .InSchema("dbo")
                  .PrimaryColumns("EntityId");
        }

        public override void Down()
        {
            Delete.Table("EquipmentType");
        }
    }
}