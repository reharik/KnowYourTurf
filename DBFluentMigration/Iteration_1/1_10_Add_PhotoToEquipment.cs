using FluentMigrator;

namespace DBFluentMigration.Iteration_1
{
    [Migration(1010)]
    public class PhotoToEquipment : Migration
    {
        public override void Up()
        {
            Create.Table("PhotoToEquipment").InSchema("dbo")
                  .WithColumn("Photo_id").AsInt32().PrimaryKey().NotNullable()
                  .WithColumn("Equipment_id").AsInt32().PrimaryKey().NotNullable();

            Create.ForeignKey("FK_Photo_manyToMany_Equipment")
                  .FromTable("PhotoToEquipment")
                  .InSchema("dbo")
                  .ForeignColumns("Photo_id")
                  .ToTable("Photo")
                  .InSchema("dbo")
                  .PrimaryColumns("EntityId");
            Create.ForeignKey("FK_Equipment_manyToMany_Photo")
                  .FromTable("PhotoToEquipment")
                  .InSchema("dbo")
                  .ForeignColumns("Equipment_id")
                  .ToTable("Equipment")
                  .InSchema("dbo")
                  .PrimaryColumns("EntityId");
        }

        public override void Down()
        {
            Delete.Table("PhotoToEquipment");
        }
    }
}