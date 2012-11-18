using FluentMigrator;

namespace DBFluentMigration.Iteration_1
{
    [Migration(1100)]
    public class Add_EmployeeToEquipmentTask : Migration
    {
       public override void Up()
        {
            Create.Table("EmployeeToEquipmentTask").InSchema("dbo")
                  .WithColumn("User_id").AsInt32().PrimaryKey().NotNullable()
                  .WithColumn("EquipmentTask_id").AsInt32().PrimaryKey().NotNullable();
            
            Create.ForeignKey("FK_User_manyToMany_EquipmentTask")
                 .FromTable("EmployeeToEquipmentTask")
                 .InSchema("dbo")
                 .ForeignColumns("User_id")
                 .ToTable("User")
                 .InSchema("dbo")
                 .PrimaryColumns("EntityId");
            Create.ForeignKey("FK_EquipmentTask_manyToMany_User")
                  .FromTable("EmployeeToEquipmentTask")
                  .InSchema("dbo")
                  .ForeignColumns("EquipmentTask_id")
                  .ToTable("EquipmentTask")
                  .InSchema("dbo")
                  .PrimaryColumns("EntityId");
        }

        public override void Down()
        {         
			Delete.Table("EmployeeToEquipmentTask");
        }
    }
}