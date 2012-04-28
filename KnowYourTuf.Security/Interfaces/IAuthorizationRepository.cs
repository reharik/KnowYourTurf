using System;
using System.Collections.Generic;
using KnowYourTurf.Security.Model;

namespace KnowYourTurf.Security.Interfaces
{
	/// <summary>
	/// Allows to edit the security information of the 
	/// system
	/// </summary>
	public interface IAuthorizationRepository
	{
		/// <summary>
		/// Creates a new users group.
		/// </summary>
		/// <param name="name">The name of the new group.</param>
		UsersGroup CreateUsersGroup(string name);

		/// <summary>
		/// Gets the associated users group for the specified user.
		/// </summary>
		/// <param name="user">The user.</param>
		UsersGroup[] GetAssociatedUsersGroupFor(IUser user);

		/// <summary>
		/// Gets the users group by its name
		/// </summary>
		/// <param name="groupName">Name of the group.</param>
		UsersGroup GetUsersGroupByName(string groupName);

        /// <summary>
        /// Gets the users group by its id
        /// </summary>
        /// <param name="id">id of the group.</param>
        UsersGroup GetUsersGroupById(int id);

		/// <summary>
		/// Associates the user with a group with the specified name
		/// </summary>
		/// <param name="user">The user.</param>
		/// <param name="groupName">Name of the group.</param>
		void AssociateUserWith(IUser user, string groupName);

		/// <summary>
		/// Associates the user with a group with the specified name
		/// </summary>
		/// <param name="user">The user.</param>
		/// <param name="group">The group.</param>
		void AssociateUserWith(IUser user, UsersGroup group);


		/// <summary>
		/// Creates the operation with the given name
		/// </summary>
		/// <param name="operationName">Name of the operation.</param>
		/// <returns></returns>
		Operation CreateOperation(string operationName);

		/// <summary>
		/// Gets the operation by the specified name
		/// </summary>
		/// <param name="operationName">Name of the operation.</param>
		/// <returns></returns>
		Operation GetOperationByName(string operationName);

		/// <summary>
		/// Removes the user from the specified group
		/// </summary>
		/// <param name="user">The user.</param>
		/// <param name="usersGroupName">Name of the users group.</param>
		void DetachUserFromGroup(IUser user, string usersGroupName);

		/// <summary>
		/// Creates the users group as a child of <paramref name="parentGroupName"/>.
		/// </summary>
		/// <param name="parentGroupName">Name of the parent group.</param>
		/// <param name="usersGroupName">Name of the users group.</param>
		/// <returns></returns>
		UsersGroup CreateChildUserGroupOf(string parentGroupName, string usersGroupName);

		/// <summary>
		/// Gets the ancestry association of a user with the named users group.
		/// This allows to track how a user is associated to a group through 
		/// their ancestry.
		/// </summary>
		/// <param name="user">The user.</param>
		/// <param name="usersGroupName">Name of the users group.</param>
		/// <returns></returns>
		UsersGroup[] GetAncestryAssociation(IUser user, string usersGroupName);

		/// <summary>
		/// Removes the specified users group.
		/// Cannot remove parent users groups, you must remove them first.
		/// Will also delete all permissions that are related to this group.
		/// </summary>
		/// <param name="usersGroupName">Name of the users group.</param>
		void RemoveUsersGroup(string usersGroupName);

		/// <summary>
		/// Removes the specified operation.
		/// Will also delete all permissions for this operation
		/// </summary>
		/// <param name="operationName">The operation N ame.</param>
		void RemoveOperation(string operationName);

		/// <summary>
		/// Removes the user from rhino security.
		/// This does NOT delete the user itself, merely reset all the
		/// information that rhino security knows about it.
		/// It also allows it to be removed by external API without violating
		/// FK constraints
		/// </summary>
		/// <param name="user">The user.</param>
		void RemoveUser(IUser user);


		/// <summary>
		/// Removes the specified permission.
		/// </summary>
		/// <param name="permission">The permission.</param>
		void RemovePermission(Permission permission);

        ///<summary>
        /// Renames an existing users group
        ///</summary>
        ///<param name="usersGroupName">The name of the usersgroup to rename</param>
        ///<param name="newName">The new name of the usersgroup</param>
        ///<returns>The renamed group</returns> 
        UsersGroup RenameUsersGroup(string usersGroupName, string newName);

	    /// <summary>
	    /// Gets all user groups ordered by name
	    /// </summary>
	    UsersGroup[] GetAllUsersGroups();

	    /// <summary>
	    /// Gets all the operations
	    /// </summary>
	    /// <returns></returns>
        IList<Operation> GetAllOperations();

	    /// <summary>
	    /// Gets the operation by the specified EntityId
	    /// </summary>
	    /// <param name="operationId">EntityId of the operation.</param>
	    /// <returns></returns>
	    Operation GetOperationById(int operationId);

	    /// <summary>
	    /// 
	    /// </summary>
	    /// <param name="usersGroup"></param>
	    /// <returns></returns>
	    UsersGroup UpdateUsersGroup(UsersGroup usersGroup);
	}
}