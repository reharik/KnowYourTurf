using FluentMigrator;

namespace DBFluentMigration.Iteration_1
{
    [Migration(10030)]
    public class Add_Color_to_EquipmentType : Migration
    {
        public override void Up()
        {
            Create.Column("TaskColor").OnTable("EquipmentType").AsString().Nullable();
        }

        public override void Down()
        {
            Delete.Column("Task").FromTable("EquipmentType");
        }
    }
}