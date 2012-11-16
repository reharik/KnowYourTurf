using FluentMigrator;

namespace DBFluentMigration.Iteration_1
{
    [Migration(1009)]
    public class PartToEquipmentTask : Migration
    {
        public override void Up()
        {
            Create.Table("PartToEquipmentTask").InSchema("dbo")
                  .WithColumn("Part_id").AsInt32().PrimaryKey().NotNullable()
                  .WithColumn("EquipmentTask_id").AsInt32().PrimaryKey().NotNullable();

            Create.ForeignKey("FK_Part_manyToMany_EquipmentTask")
                  .FromTable("PartToEquipmentTask")
                  .InSchema("dbo")
                  .ForeignColumns("Part_id")
                  .ToTable("Part")
                  .InSchema("dbo")
                  .PrimaryColumns("EntityId");
            Create.ForeignKey("FK_EquipmentTask_manyToMany_Part")
                  .FromTable("PartToEquipmentTask")
                  .InSchema("dbo")
                  .ForeignColumns("EquipmentTask_id")
                  .ToTable("EquipmentTask")
                  .InSchema("dbo")
                  .PrimaryColumns("EntityId");
        }

        public override void Down()
        {
            Delete.Table("PartToEquipmentTask");
        }
    }
}