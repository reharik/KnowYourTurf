using FluentMigrator;

namespace DBFluentMigration.Iteration_1
{
    [Migration(1075)]
    public class Add_Columns_To_Equipment : Migration
    {
        public override void Up()
        {
            Alter.Table("Equipment").AddColumn("Make").AsString(255).Nullable();
            Alter.Table("Equipment").AddColumn("Model").AsString(255).Nullable();
            Alter.Table("Equipment").AddColumn("SerialNumber").AsString(255).Nullable();
            Alter.Table("Equipment").AddColumn("WarrentyInfo").AsString(255).Nullable();
            Alter.Table("Equipment").AddColumn("EquipmentType_id").AsInt32().Nullable();
             
            Create.ForeignKey("FK_Equipment_oneToMany_EquipmentType")
                     .FromTable("Equipment")
                     .InSchema("dbo")
                     .ForeignColumns("EquipmentType_id")
                     .ToTable("EquipmentType")
                     .InSchema("dbo")
                     .PrimaryColumns("EntityId");
        }

        public override void Down()
        {
        }
    }
}