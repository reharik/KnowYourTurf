using System;

namespace KnowYourTurf.Security.Interfaces
{
	/// <summary>
	/// Mark an entity with an id
	/// </summary>
	public interface IIDentifiable
	{
		/// <summary>
		/// Gets or sets the id.
		/// </summary>
		/// <value>The id.</value>
        long EntityId { get; set; }
	}
}