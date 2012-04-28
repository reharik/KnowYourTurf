using System;
using KnowYourTurf.Security.Impl;
using KnowYourTurf.Security.Interfaces;
using KnowYourTurf.Security.Model;
using NHibernate;

namespace KnowYourTurf.Security.Services
{
	/// <summary>
	/// Allow to define permissions using a fluent interface
	/// </summary>
	public class PermissionsBuilderService : IPermissionsBuilderService
	{
	    private readonly ISession session;
	    private readonly IAuthorizationRepository authorizationRepository;
		/// <summary>
		/// Initializes a new instance of the <see cref="PermissionsBuilderService"/> class.
		/// </summary>
		/// <param name="session">The nhibernate session</param>
		/// <param name="authorizationRepository">The authorization editing service.</param>
		public PermissionsBuilderService(ISession session, IAuthorizationRepository authorizationRepository)
		{
		    this.session = session;
		    this.authorizationRepository = authorizationRepository;
		}

		/// <summary>
		/// Builds a permission
		/// </summary>
		public class FluentPermissionBuilder : IPermissionBuilder, IForPermissionBuilder, IOnPermissionBuilder,
		                                       ILevelPermissionBuilder
		{
			private readonly Permission permission = new Permission();
			private readonly PermissionsBuilderService permissionBuilderService;

			/// <summary>
			/// Initializes a new instance of the <see cref="FluentPermissionBuilder"/> class.
			/// </summary>
			/// <param name="permissionBuilderService">The permission service.</param>
			/// <param name="allow">if set to <c>true</c> create an allow permission.</param>
			/// <param name="operation">The operation.</param>
			public FluentPermissionBuilder(PermissionsBuilderService permissionBuilderService, bool allow, Operation operation)
			{
				this.permissionBuilderService = permissionBuilderService;
				permission.Allow = allow;
				permission.Operation = operation;
			}

            /// <summary>
            /// Define the description fir this permission
            /// </summary>
            /// <param name="description">The description.</param>
            /// <returns></returns>
            public IPermissionBuilder Description(string description)
            {
                permission.Description = description;
                return this;
            }

		    /// <summary>
			/// Save the created permission
			/// </summary>
			public Permission Save()
			{
				permissionBuilderService.Save(permission);
				return permission;
			}

		    /// <summary>
            /// Builds a permission without saving
		    /// </summary>
		    public Permission Build()
		    {
                return permission;
		    }

		    /// <summary>
			/// Set the user that this permission is built for
			/// </summary>
			/// <param name="user">The user.</param>
			/// <returns></returns>
			public IOnPermissionBuilder For(IUser user)
			{
				permission.User = user;
				return this;
			}


			/// <summary>
			/// Set the users group that this permission is built for
			/// </summary>
			/// <param name="usersGroupName">Name of the users group.</param>
			/// <returns></returns>
			public IOnPermissionBuilder For(string usersGroupName)
			{
				UsersGroup usersGroup = permissionBuilderService
					.authorizationRepository
					.GetUsersGroupByName(usersGroupName);

				Guard.Against<ArgumentException>(usersGroup == null, "There is not users group named: " + usersGroup);

				return For(usersGroup);
			}

			/// <summary>
			/// Set the users group that this permission is built for
			/// </summary>
			/// <param name="usersGroup">The users group.</param>
			/// <returns></returns>
			public IOnPermissionBuilder For(UsersGroup usersGroup)
			{
				permission.UsersGroup = usersGroup;

				return this;
			}

			/// <summary>
			/// Set this permission to be application to everything
			/// </summary>
			/// <returns></returns>
			public ILevelPermissionBuilder OnEverything()
			{
				return this;
			}

			/// <summary>
			/// Define the level of this permission
			/// </summary>
			/// <param name="level">The level.</param>
			/// <returns></returns>
			public IPermissionBuilder Level(int level)
			{
				permission.Level = level;
				return this;
			}


			/// <summary>
			/// Define the default level;
			/// </summary>
			/// <returns></returns>
			public IPermissionBuilder DefaultLevel()
			{
				return Level(1);
			}
		}

		/// <summary>
		/// Saves the specified permission
		/// </summary>
		/// <param name="permission">The permission.</param>
		public void Save(Permission permission)
		{
			session.Save(permission);
		}

        /// <summary>
        /// Allow permission for the specified operation.
        /// </summary>
        /// <param name="operationId">EntityId of the operation.</param>
        /// <returns></returns>
        public IForPermissionBuilder Allow(int operationId)
        {
            Operation operation = authorizationRepository.GetOperationById(operationId);
            Guard.Against<ArgumentException>(operation == null, "There is no operation with EntityId: " + operationId);
            return Allow(operation);
        }

        /// <summary>
        /// Deny permission for the specified operation 
        /// </summary>
        /// <param name="operationId">EntityId of the operation.</param>
        /// <returns></returns>
        public IForPermissionBuilder Deny(int operationId)
        {
            Operation operation = authorizationRepository.GetOperationById(operationId);
            Guard.Against<ArgumentException>(operation == null, "There is no operation with EntityId: " + operationId);
            return Deny(operation);
        }

		/// <summary>
		/// Allow permission for the specified operation.
		/// </summary>
		/// <param name="operationName">Name of the operation.</param>
		/// <returns></returns>
		public IForPermissionBuilder Allow(string operationName)
		{
			Operation operation = authorizationRepository.GetOperationByName(operationName);
			Guard.Against<ArgumentException>(operation == null, "There is no operation named: " + operationName);
			return Allow(operation);
		}

		/// <summary>
		/// Deny permission for the specified operation 
		/// </summary>
		/// <param name="operationName">Name of the operation.</param>
		/// <returns></returns>
		public IForPermissionBuilder Deny(string operationName)
		{
			Operation operation = authorizationRepository.GetOperationByName(operationName);
			Guard.Against<ArgumentException>(operation == null, "There is no operation named: " + operationName);
			return Deny(operation);
		}


		/// <summary>
		/// Allow permission for the specified operation.
		/// </summary>
		/// <param name="operation">The operation.</param>
		/// <returns></returns>
		public IForPermissionBuilder Allow(Operation operation)
		{
			return new FluentPermissionBuilder(this, true, operation);
		}

		/// <summary>
		/// Deny permission for the specified operation
		/// </summary>
		/// <param name="operation">The operation.</param>
		/// <returns></returns>
		public IForPermissionBuilder Deny(Operation operation)
		{
			return new FluentPermissionBuilder(this, false, operation);
		}
	}
}