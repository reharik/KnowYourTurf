using FluentMigrator;

namespace DBFluentMigration.Iteration_1
{
    [Migration(1030)]
    public class Update_Rename_Vendor : Migration
    {
        public override void Up()
        {
            Rename.Table("Vendor").To("VendorBase");
            Alter.Table("VendorBase").AddColumn("VendorType").AsString(255).WithDefaultValue("FieldVendor");
         }

        public override void Down()
        {
        }
    }
}