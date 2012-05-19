using System;
using KnowYourTurf.Security.Model;

namespace KnowYourTurf.Security.Interfaces
{
	/// <summary>
	/// Allow to retrieve and remove permissions
	/// on users, user groups, entities groups and entities.
	/// </summary>
	public interface IPermissionsService
	{
		/// <summary>
		/// Gets the permissions for the specified user
		/// </summary>
		/// <param name="user">The user.</param>
		/// <returns></returns>
		Permission[] GetPermissionsFor(IUser user);

		/// <summary>
		/// Gets the permissions for the specified entity
		/// </summary>
		/// <param name="user">The user.</param>
		/// <param name="operationName">Name of the operation.</param>
		/// <returns></returns>
		Permission[] GetGlobalPermissionsFor(IUser user, string operationName) ;

		/// <summary>
		/// Gets all permissions for the specified operation
		/// </summary>
		/// <param name="operationName">Name of the operation.</param>
		/// <returns></returns>
		Permission[] GetPermissionsFor(string operationName) ;

	    /// <summary>
	    /// Gets the permissions for the specified usergroup
	    /// </summary>
	    /// <param name="userGroup">The usersGroup.</param>
	    /// <returns></returns>
	    // RH 4/5/2012
	    Permission[] GetPermissionsFor(UsersGroup userGroup);

	    /// <summary>
	    /// RH 4/9/12
	    /// Gets permissions By Permission id
	    /// </summary>
	    /// <param name="permissionId">Guid EntityId for Permission </param>
	    /// <returns></returns>
	    Permission GetPermission(int permissionId);
	}
}
