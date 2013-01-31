using DBFluentMigration.Iteration_2;
using KnowYourTurf.Web.Security;


namespace DBFluentMigration.Iteration_1
{
    public class UpdateOperations_100:IUpdateOperations
    {
        private readonly IOperations _operations;

        public UpdateOperations_100(IOperations operations)
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