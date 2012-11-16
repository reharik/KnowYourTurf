using FluentMigrator;

namespace a
{
    [Migration(1007)]
    public class Add_DocumentToEquipment : Migration
    {
        public override void Up()
        {
            Create.Table("DocumentToEquipment").InSchema("dbo")
                  .WithColumn("Document_id").AsInt32().PrimaryKey().NotNullable()
                  .WithColumn("Equipment_id").AsInt32().PrimaryKey().NotNullable();

            Create.ForeignKey("FK_Document_manyToMany_Equipment")
                  .FromTable("DocumentToEquipment")
                  .InSchema("dbo")
                  .ForeignColumns("Document_id")
                  .ToTable("Document")
                  .InSchema("dbo")
                  .PrimaryColumns("EntityId");
            Create.ForeignKey("FK_Equipment_manyToMany_Document")
                  .FromTable("DocumentToEquipment")
                  .InSchema("dbo")
                  .ForeignColumns("Equipment_id")
                  .ToTable("Equipment")
                  .InSchema("dbo")
                  .PrimaryColumns("EntityId");
        }

        public override void Down()
        {
            Delete.Table("DocumentToEquipment");
        }
    }
}