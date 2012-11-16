using FluentMigrator;

namespace DBFluentMigration.Iteration_1
{
    [Migration(1011)]
    public class EquipmentTypeToEquipmentVendor : Migration
    {
        public override void Up()
        {
            Create.Table("EquipmentTypeToEquipmentVendor").InSchema("dbo")
                  .WithColumn("EquipmentType_id").AsInt32().PrimaryKey().NotNullable()
                  .WithColumn("EquipmentVendor_id").AsInt32().PrimaryKey().NotNullable();
            
            Create.ForeignKey("FK_EquipmentType_manyToMany_EquipmentVendor")
                 .FromTable("EquipmentTypeToEquipmentVendor")
                 .InSchema("dbo")
                 .ForeignColumns("EquipmentType_id")
                 .ToTable("EquipmentType")
                 .InSchema("dbo")
                 .PrimaryColumns("EntityId");
            Create.ForeignKey("FK_EquipmentVendor_manyToMany_EquipmentType")
                  .FromTable("EquipmentTypeToEquipmentVendor")
                  .InSchema("dbo")
                  .ForeignColumns("EquipmentVendor_id")
                  .ToTable("VendorBase")
                  .InSchema("dbo")
                  .PrimaryColumns("EntityId");
        }

        public override void Down()
        {
            Delete.Table("EquipmentTypeToEquipmentVendor");
        }
    }
}