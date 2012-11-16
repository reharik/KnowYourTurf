using FluentMigrator;

namespace DBFluentMigration.Iteration_1
{
    [Migration(1002)]
    public class Update_Rename_Vendor : Migration
    {
        public override void Up()
        {
            Rename.Table("Vendor").To("VendorBase");
            Alter.Table("VendorBase").AddColumn("VendorType").AsString(255).WithDefaultValue("FieldVendor");

            Create.ForeignKey("FK_Equipment_oneToMany_VendorBase")
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