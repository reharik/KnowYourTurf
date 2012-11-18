using FluentMigrator;

namespace DBFluentMigration.Iteration_1
{
    [Migration(1020)]
    public class Rename_BaseProductToVendor : Migration
    {
        public override void Up()
        {
            Rename.Table("BaseProductToVendor").To("BaseProductToFieldVendor");
        }

        public override void Down()
        {
            Rename.Table("BaseProductToFieldVendor").To("BaseProductToVendor");
        }
    }
}