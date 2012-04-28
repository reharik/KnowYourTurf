using System;
using KnowYourTurf.Security.Model;

namespace KnowYourTurf.Security.Interfaces
{
	/// <summary>
	/// Allow to define permissions using a fluent interface
	/// </summary>
	public interface IPermissionsBuilderService
	{
		/// <summary>
		/// Allow permission for the specified operation.
		/// </summary>
		/// <param name="operationName">Name of the operation.</param>
		/// <returns></returns>
		IForPermissionBuilder Allow(string operationName);

		/// <summary>
		/// Deny permission for the specified operation 
		/// </summary>
		/// <param name="operationName">Name of the operation.</param>
		/// <returns></returns>
		IForPermissionBuilder Deny(string operationName);

        /// <summary>
        /// Allow permission for the specified operation.
        /// </summary>
        /// <param name="operationId">ID of the operation.</param>
        /// <returns></returns>
        IForPermissionBuilder Allow(int operationId);

        /// <summary>
        /// Deny permission for the specified operation 
        /// </summary>
        /// <param name="operationId">EntityId of the operation.</param>
        /// <returns></returns>
        IForPermissionBuilder Deny(int operationId);

		/// <summary>
		/// Allow permission for the specified operation.
		/// </summary>
		/// <param name="operation">The operation.</param>
		IForPermissionBuilder Allow(Operation operation);

		/// <summary>
		/// Deny permission for the specified operation 
		/// </summary>
		/// <param name="operation">The operation.</param>
		IForPermissionBuilder Deny(Operation operation);

        /// <summary>
        /// Save Permission with out using fluent interface
        /// </summary>
        /// <param name="permission">The permission.</param>
        void Save(Permission permission);
	}
}