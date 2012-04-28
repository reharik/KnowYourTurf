using System;
using KnowYourTurf.Security.Interfaces;

namespace KnowYourTurf.Security.Model
{
   
	/// <summary>
	/// Represent a permission on the system, allow (or denying) 
	/// [operation] for [someone] on [something]
	/// </summary>
	public class Permission : EqualityAndHashCodeProvider<Permission>
	{
	    /// <summary>
	    /// Gets or sets the operation this permission applies to
	    /// </summary>
	    /// <value>The operation.</value>
	    public virtual Operation Operation { get; set; }

	    /// <summary>
	    /// Gets or sets a value indicating whether this <see cref="Permission"/> is allowing 
	    /// or denying the operation.
	    /// </summary>
	    /// <value><c>true</c> if allow; otherwise, <c>false</c>.</value>
	    public virtual bool Allow { get; set; }

	    /// <summary>
	    /// Gets or sets the user this permission belongs to.
	    /// </summary>
	    /// <value>The user.</value>
	    public virtual IUser User { get; set; }

	    /// <summary>
	    /// Gets or sets the users group this permission belongs to
	    /// </summary>
	    /// <value>The users group.</value>
	    public virtual UsersGroup UsersGroup { get; set; }

	    /// <summary>
	    /// Gets or sets the level of this permission
	    /// </summary>
	    /// <value>The level.</value>
	    public virtual int Level { get; set; }

        /// <summary>
        /// Gets or sets the Description of this permission
        /// </summary>
        /// <value>The Description.</value>
        [TextArea]
        public virtual string Description { get; set; }
        
	}
}