namespace DBFluentMigration.Iteration_2
{
    using FluentMigrator;

    [Migration(20130)]
    public class Create_ProductToDocuemnt_Table : Migration
    {
        public override void Up()
        {
            Create.Table("DocumentToBaseProduct")
                  .WithColumn("BaseProduct_id").AsInt32().NotNullable().ForeignKey("FK_ProductToDocumentProduct","BaseProduct","EntityId")
                  .WithColumn("Document_id").AsInt32().NotNullable().ForeignKey("FK_ProductToDocumentDocument","Document","EntityId");
        }

        public override void Down()
        {
            Delete.Table("DocuemntToBaseProduct");
        }
    }
}