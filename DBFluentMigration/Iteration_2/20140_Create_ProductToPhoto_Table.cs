namespace DBFluentMigration.Iteration_2
{
    using FluentMigrator;

    [Migration(20140)]
    public class Create_ProductToPhoto_Table : Migration
    {
        public override void Up()
        {
            Create.Table("PhotoToBaseProduct")
                  .WithColumn("BaseProduct_id").AsInt32().NotNullable().ForeignKey("FK_ProductToPhotoProduct","BaseProduct","EntityId")
                  .WithColumn("Photo_id").AsInt32().NotNullable().ForeignKey("FK_ProductToPhotoPhoto","Photo","EntityId");
        }

        public override void Down()
        {
            Delete.Table("PhotoToBaseProduct");
        }
    }
}