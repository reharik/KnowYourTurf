namespace DBFluentMigration.Iteration_2
{
    using FluentMigrator;

    [Migration(20020)]
    public class Add_Fields_to_Task : Migration
    {
        public override void Up()
        {
            Create.Column("TargetPest").OnTable("Task").AsString().Nullable();
            Create.Column("RatePerUnit").OnTable("Task").AsString().Nullable();
            Create.Column("SprayPermitNumber").OnTable("Task").AsString().Nullable();
        }

        public override void Down()
        {
            Delete.Column("TargetPest").FromTable("Task");
            Delete.Column("RatePerUnit").FromTable("Task");
            Delete.Column("SprayPermitNumber").FromTable("Task");
        }
    }
}