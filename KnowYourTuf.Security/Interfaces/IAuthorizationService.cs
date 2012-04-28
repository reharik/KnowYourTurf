using KnowYourTurf.Security.Model;
using NHibernate;
using NHibernate.Criterion;

namespace KnowYourTurf.Security.Interfaces
{
	///<summary>
	/// Implementors of this interface are able to answer
	/// on authorization questions as well as enhance Criteria
	/// queries
	///</summary>
	public interface IAuthorizationService
	{
		/// <summary>
		/// Determines whether the specified user is allowed to perform the
		/// specified operation on the entity.
		/// </summary>
		/// <param name="user">The user.</param>
		/// <param name="operation">The operation.</param>
		/// <returns>
		/// 	<c>true</c> if the specified user is allowed; otherwise, <c>false</c>.
		/// </returns>
		bool IsAllowed(IUser user, string operation);

		/// <summary>
		/// Gets the authorization information for the specified user and operation,
		/// allows to easily understand why a given operation was granted / denied.
		/// </summary>
		/// <param name="user">The user.</param>
		/// <param name="operation">The operation.</param>
		/// <returns></returns>
		AuthorizationInformation GetAuthorizationInformation(IUser user, string operation);
	}
}