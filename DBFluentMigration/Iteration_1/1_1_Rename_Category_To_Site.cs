using FluentMigrator;

namespace DBFluentMigration.Iteration_1
{
    [Migration(1001)]
    public class Rename_Category_To_Site : Migration
    {
        public override void Up()
        {
            Rename.Table("Category").To("Site");
            Rename.Column("Category_Id").OnTable("Field").To("Site_Id");
        }

        public override void Down()
        {
            Rename.Table("Site").To("Category");
            Rename.Column("Site_Id").OnTable("Field").To("Category_Id");
        }
    }
}