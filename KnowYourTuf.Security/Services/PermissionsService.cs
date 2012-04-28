using System;
using System.Collections.Generic;
using KnowYourTurf.Security.Impl.Util;
using KnowYourTurf.Security.Interfaces;
using KnowYourTurf.Security.Model;
using NHibernate;
using NHibernate.Criterion;
using System.Linq;

namespace KnowYourTurf.Security.Services
{
    /// <summary>
	/// Allow to retrieve and remove permissions
	/// on users, user groups, entities groups and entities.
	/// </summary>
	public class PermissionsService : IPermissionsService
	{
		private readonly IAuthorizationRepository authorizationRepository;
        private readonly ISession session;

        /// <summary>
		/// Initializes a new instance of the <see cref="PermissionsService"/> class.
		/// </summary>
		/// <param name="authorizationRepository">The authorization editing service.</param>
		/// <param name="session">The NHibernate session</param>
		public PermissionsService(IAuthorizationRepository authorizationRepository,
		                          ISession session)
		{
			this.authorizationRepository = authorizationRepository;
		    this.session = session;
		}

		#region IPermissionsService Members

		/// <summary>
		/// Gets the permissions for the specified user
		/// </summary>
		/// <param name="user">The user.</param>
		/// <returns></returns>
		public Permission[] GetPermissionsFor(IUser user)
		{
			DetachedCriteria criteria = DetachedCriteria.For<Permission>()
				.Add(Expression.Eq("User", user)
				     || Subqueries.PropertyIn("UsersGroup.EntityId",
				                              SecurityCriterions.AllGroups(user).SetProjection(Projections.Id())));

			return FindResults(criteria);
		}

        /// <summary>
        /// Gets the permissions for the specified usergroup
        /// </summary>
        /// <param name="userGroup">The usersGroup.</param>
        /// <returns></returns>
        // RH 4/5/2012
        public Permission[] GetPermissionsFor(UsersGroup userGroup)
        {
            DetachedCriteria criteria = DetachedCriteria.For<Permission>()
                .Add(Expression.Eq("UsersGroup", userGroup));

            return FindResults(criteria);
        }


		/// <summary>
		/// Gets the permissions for the specified entity
		/// </summary>
		/// <param name="user">The user.</param>
		/// <param name="operationName">Name of the operation.</param>
		/// <returns></returns>
		public Permission[] GetGlobalPermissionsFor(IUser user, string operationName)
		{
			string[] operationNames = Strings.GetHierarchicalOperationNames(operationName);
			DetachedCriteria criteria = DetachedCriteria.For<Permission>()
				.Add(Expression.Eq("User", user)
				     || Subqueries.PropertyIn("UsersGroup.EntityId",
				                              SecurityCriterions.AllGroups(user).SetProjection(Projections.Id())))
                .CreateAlias("Operation", "op")
				.Add(Expression.In("op.Name", operationNames));

			return FindResults(criteria);
		}

        /// <summary>
        /// RH 4/9/12
        /// Gets permissions By Permission id
        /// </summary>
        /// <param name="permissionId">Guid EntityId for Permission </param>
        /// <returns></returns>
        public Permission GetPermission(int permissionId)
        {
            DetachedCriteria criteria = DetachedCriteria.For<Permission>()
                .Add(Expression.Eq("EntityId",permissionId));

            return FindResult(criteria);
        }

        /// <summary>
        /// Gets all permissions for the specified operation
        /// </summary>
        /// <param name="operationName">Name of the operation.</param>
        /// <returns></returns>
        public Permission[] GetPermissionsFor(string operationName)
        {
            string[] operationNames = Strings.GetHierarchicalOperationNames(operationName);
            DetachedCriteria criteria = DetachedCriteria.For<Permission>()
                .CreateAlias("Operation", "op")
                .Add(Restrictions.In("op.Name", operationNames));

            return this.FindResults(criteria);
        }

		#endregion

		private Permission[] FindResults(DetachedCriteria criteria)
		{
		    ICollection<Permission> permissions = criteria.GetExecutableCriteria(session)
		        .AddOrder(Order.Desc("Level"))
		        .AddOrder(Order.Asc("Allow"))
                .SetCacheable(true)
                .List<Permission>();
		    return permissions.ToArray();
		}

        private Permission FindResult(DetachedCriteria criteria)
        {
            Permission permission = criteria.GetExecutableCriteria(session)
                .SetCacheable(true).
                UniqueResult<Permission>();
            if (permission == null)
			{
                permission = new Permission();
			    session.Save(permission);
			}
            return permission;
        }
	}

}
