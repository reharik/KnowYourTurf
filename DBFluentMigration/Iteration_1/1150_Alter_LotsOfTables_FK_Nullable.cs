using FluentMigrator;

namespace DBFluentMigration.Iteration_1
{
    [Migration(1154)]
    public class Alter_InventoryProduct_FK_Nullable : Migration
    {
        public override void Up()
        {
            Alter.Table("InventoryProduct").AlterColumn("CreatedBy_id").AsInt32().Nullable();
            Alter.Table("InventoryProduct").AlterColumn("ChangedBy_id").AsInt32().Nullable();
            Alter.Table("InventoryProduct").AlterColumn("LastVendor_id").AsInt32().Nullable();
            Alter.Table("Calculator").AlterColumn("CreatedBy_id").AsInt32().Nullable();
            Alter.Table("Calculator").AlterColumn("ChangedBy_id").AsInt32().Nullable();
            Alter.Table("PhotoCategory").AlterColumn("CreatedBy_id").AsInt32().Nullable();
            Alter.Table("PhotoCategory").AlterColumn("ChangedBy_id").AsInt32().Nullable();
            Alter.Table("security_Operations").AlterColumn("ParentId").AsInt32().Nullable();
            Alter.Table("security_Permissions").AlterColumn("UserId").AsInt32().Nullable();
            Alter.Table("security_Permissions").AlterColumn("UsersGroupId").AsInt32().Nullable();
            Alter.Table("security_UsersGroups").AlterColumn("ParentId").AsInt32().Nullable();
        }

        public override void Down()
        {
        }
    }
}