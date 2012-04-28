using System;
using KnowYourTurf.Security.Impl.Util;
using KnowYourTurf.Security.Interfaces;
using KnowYourTurf.Security.Model;
using KnowYourTurf.Security.Properties;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.SqlCommand;

namespace KnowYourTurf.Security.Services
{
	/// <summary>
	/// Answers authorization questions as well as enhance Criteria
	/// queries
	/// </summary>
	public class AuthorizationService : IAuthorizationService
	{
		private readonly IAuthorizationRepository authorizationRepository;

		private readonly IPermissionsService permissionsService;

		/// <summary>
		/// Initializes a new instance of the <see cref="AuthorizationService"/> class.
		/// </summary>
		/// <param name="permissionsService">The permissions service.</param>
		/// <param name="authorizationRepository">The authorization editing service.</param>
		public AuthorizationService(IPermissionsService permissionsService,
		                            IAuthorizationRepository authorizationRepository)
		{
			this.permissionsService = permissionsService;
			this.authorizationRepository = authorizationRepository;
		}

		#region IAuthorizationService 

		/// <summary>
		/// Determines whether the specified user is allowed to perform the
		/// specified operation on the entity.
		/// </summary>
		/// <param name="user">The user.</param>
		/// <param name="operation">The operation.</param>
		/// <returns>
		/// 	<c>true</c> if the specified user is allowed; otherwise, <c>false</c>.
		/// </returns>
		public bool IsAllowed(IUser user, string operation)
		{
			Permission[] permissions = permissionsService.GetGlobalPermissionsFor(user, operation);
			if (permissions.Length == 0)
				return false;
			return permissions[0].Allow;
		}

		/// <summary>
		/// Gets the authorization information for the specified user and operation,
		/// allows to easily understand why a given operation was granted / denied.
		/// </summary>
		/// <param name="user">The user.</param>
		/// <param name="operation">The operation.</param>
		/// <returns></returns>
		public AuthorizationInformation GetAuthorizationInformation(IUser user, string operation)
		{
			AuthorizationInformation info;
			if (InitializeAuthorizationInfo(operation, out info))
				return info;
			Permission[] permissions = permissionsService.GetGlobalPermissionsFor(user, operation);
			AddPermissionDescriptionToAuthorizationInformation(operation, info, user, permissions);
			return info;
		}

		#endregion

		private void AddPermissionDescriptionToAuthorizationInformation(string operation,
		                                                                         AuthorizationInformation info,
		                                                                         IUser user, Permission[] permissions)
		{
			if (permissions.Length == 0)
			{
				UsersGroup[] usersGroups = authorizationRepository.GetAssociatedUsersGroupFor(user);
					info.AddDeny(Resources.PermissionForOperationNotGrantedToUser,
					             operation,
					             user.SecurityInfo.Name,
					             Strings.Join(usersGroups)
						);
				return;
			}
			foreach (Permission permission in permissions)
			{
				AddUserLevelPermissionMessage(operation, info, user, permission);
				AddUserGroupLevelPermissionMessage(operation, info, user, permission);
			}
		}

		private bool InitializeAuthorizationInfo(string operation, out AuthorizationInformation info)
		{
			info = new AuthorizationInformation();
			Operation op = authorizationRepository.GetOperationByName(operation);
			if (op == null)
			{
				info.AddDeny(Resources.OperationNotDefined, operation);
				return true;
			}
			return false;
		}

		private void AddUserGroupLevelPermissionMessage(string operation, AuthorizationInformation info,
		                                                IUser user, Permission permission)
		{
			if (permission.UsersGroup != null)
			{
				UsersGroup[] ancestryAssociation =
					authorizationRepository.GetAncestryAssociation(user, permission.UsersGroup.Name);
				string groupAncestry = Strings.Join(ancestryAssociation, " -> ");
				if (permission.Allow)
				{
					info.AddAllow(Resources.PermissionGrantedForUsersGroup,
					              operation,
					              permission.UsersGroup.Name,
					              GetPermissionTarget(permission),
					              user.SecurityInfo.Name,
					              permission.Level,
					              groupAncestry);
				}
				else
				{
					info.AddDeny(Resources.PermissionDeniedForUsersGroup,
					             operation,
					             permission.UsersGroup.Name,
					             GetPermissionTarget(permission),
					             user.SecurityInfo.Name,
					             permission.Level,
					             groupAncestry);
				}
			}
		}

		private static void AddUserLevelPermissionMessage(
			string operation,
			AuthorizationInformation info,
			IUser user,
			Permission permission)
		{
			if (permission.User != null)
			{
				string target = GetPermissionTarget(permission);
				if (permission.Allow)
				{
					info.AddAllow(Resources.PermissionGrantedForUser,
					              operation,
					              user.SecurityInfo.Name,
					              target,
					              permission.Level);
				}
				else
				{
					info.AddDeny(Resources.PermissionDeniedForUser,
					             operation,
					             user.SecurityInfo.Name,
					             target,
					             permission.Level);
				}
			}
		}

		private static string GetPermissionTarget(Permission permission)
		{
			return Resources.Everything;
		}
	}
}
