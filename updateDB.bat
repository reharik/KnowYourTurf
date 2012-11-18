@ECHO OFF
DBFluentMigration\MigrateApp\migrate -a DBFluentMigration\bin\debug\DBFluentMigration.dll -db sqlserver2008 -conn "Data Source=CannibalCoder.cloudapp.net;Initial Catalog=KnowYourTurf_QA;User ID=KnowYourTurf;Password=KYTadmin6;"
echo "Done!"
