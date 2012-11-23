@ECHO OFF
DBFluentMigration\MigrateApp\migrate -a DBFluentMigration\bin\debug\DBFluentMigration.dll -db sqlserver2008 -conn "Data Source=(local);Initial Catalog=KnowYourTurf_DEV;Trusted_Connection=Yes;"
echo "Done!"
