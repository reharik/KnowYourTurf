namespace DBFluentMigration.Iteration_2
{
    using KnowYourTurf.Web.Security;

    public class UpdateOperations_2001 : IUpdateOperations
    {
        private readonly IOperations _operations;

        public UpdateOperations_2001(IOperations operations)
        {
            this._operations = operations;
        }

        public void Update()
        {
            this.CreateMenuItemOptions();
            this.CreateControllerOptions();
        }

        private void CreateControllerOptions()
        {
            this._operations.CreateOperationForControllerType("TDAController");
        }

        public void CreateMenuItemOptions()
        {
            this._operations.CreateOperationForMenuItem("TDA");
        }
    }
}