namespace DBFluentMigration.Iteration_2
{
    using FluentMigrator;

    [Migration(20190)]
    public class Add_GrassType_To_Field : Migration
    {
        public override void Up()
        {
            Create.Column("GrassType_Id").OnTable("Field").AsInt32().Nullable().ForeignKey("FK_FieldToGrassType","GrassType","EntityId");
        }

        public override void Down()
        {
        }
    }
}