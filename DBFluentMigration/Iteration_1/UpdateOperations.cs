using KnowYourTurf.Web.Security;


namespace DBFluentMigration.Iteration_1
{
    public class UpdateOperations
    {
        private readonly IOperations _operations;

        public UpdateOperations(IOperations operations)
        {
            _operations = operations;
        }

        public void Update()
        {
            CreateControllerOptions();
            CreateMenuItemOptions();
        }

        public void CreateControllerOptions()
        {
            _operations.CreateOperationForControllerType("TasksByFieldController");
        }

        public void CreateMenuItemOptions()
        {
        }
    }
}