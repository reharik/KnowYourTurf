using FluentMigrator;

namespace DBFluentMigration.Iteration_1
{
    [Migration(1040)]
    public class Remove_Column_From_Photo_And_Doc : Migration
    {
        public override void Up()
        {
            Delete.Column("Field_id").FromTable("Photo");
            Delete.Column("Field_id").FromTable("Document");
        }

        public override void Down()
        {
        }
    }
}