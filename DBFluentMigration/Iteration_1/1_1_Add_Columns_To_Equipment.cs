using FluentMigrator;

namespace DBFluentMigration.Iteration_1
{
    [Migration(201211161721)]
    public class Add_Columns_To_Equipment : Migration
    {
        public override void Up()
        {
            Alter.Table("Equipment").AddColumn("Make").AsString(255).Nullable();
            Alter.Table("Equipment").AddColumn("Model").AsString(255).Nullable();
            Alter.Table("Equipment").AddColumn("SerialNumber").AsString(255).Nullable();
            Alter.Table("Equipment").AddColumn("WarrentyInfo").AsString(255).Nullable();
            Alter.Table("Equipment").AddColumn("EquipmentTask_id").AsInt32().Nullable();
            Alter.Table("Equipment").AddColumn("EquipmentVendor_id").AsInt32().Nullable();

            Create.ForeignKey("FK_Equipment_oneToMany_EquipmentTask")
                  .FromTable("Equipment")
                  .InSchema("dbo")
                  .ForeignColumns("EquipmentTask_id")
                  .ToTable("EquipmentTask_id")
                  .InSchema("dbo")
                  .PrimaryColumns("EntityId");

            Create.ForeignKey("FK_Equipment_oneToMany_EquipmentVendor")
                  .FromTable("Equipment")
                  .InSchema("dbo")
                  .ForeignColumns("EquipmentVendor_id")
                  .ToTable("VendorBase")
                  .InSchema("dbo")
                  .PrimaryColumns("EntityId");

        }

        public override void Down()
        {
        }
    }
}