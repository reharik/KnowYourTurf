namespace DBFluentMigration.Iteration_2
{
    using FluentMigrator;

    [Migration(20060)]
    public class Add_AnOtherfield_to_Task : Migration
    {
        public override void Up()
        {
            Create.Column("ApplicationRequestedBy").OnTable("Task").AsString().Nullable();
        }

        public override void Down()
        {
            Delete.Column("ApplicationRequestedBy").FromTable("Task");
        }
    }
}