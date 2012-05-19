namespace KnowYourTurf.Security.Interfaces
{
	/// <summary>
	/// Define who this permission is on
	/// </summary>
	public interface IOnPermissionBuilder
	{
		/// <summary>
		/// Set this permission to be application to everything
		/// </summary>
		/// <returns></returns>
		ILevelPermissionBuilder OnEverything();
	}
}