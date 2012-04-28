using System;
using System.Collections.Generic;
using System.Reflection;
using KnowYourTurf.Security.Impl.MappingRewriting;
using log4net;
using NHibernate.Cfg;

namespace KnowYourTurf.Security
{
	/// <summary>
	/// This class allows to configure the security system
	/// </summary>
	public class Security
	{
		private static readonly MethodInfo getSecurityKeyMethod = typeof (Security).GetMethod(
			"GetSecurityKeyPropertyInternal", BindingFlags.NonPublic | BindingFlags.Static);

		private static readonly Dictionary<Type, Func<string>> GetSecurityKeyPropertyCache =
			new Dictionary<Type, Func<string>>();

	    private ILog logger = LogManager.GetLogger(typeof (Security));
		/// <summary>
		/// Gets the security key property for the specified entity type
		/// </summary>
		/// <param name="entityType">Type of the entity.</param>
		/// <returns></returns>
		public static string GetSecurityKeyProperty(Type entityType)
		{
			lock (GetSecurityKeyPropertyCache)
			{
				Func<string> func;
				if (GetSecurityKeyPropertyCache.TryGetValue(entityType, out func))
					return func();
				func = (Func<string>)
				       Delegate.CreateDelegate(typeof (Func<string>),getSecurityKeyMethod.MakeGenericMethod(entityType));
				GetSecurityKeyPropertyCache[entityType] = func;
				return func();
			}
		}

        ///<summary>
        /// Setup NHibernate to include Rhino Security configuration
        ///</summary>
        public static void Configure<TUserType>(Configuration cfg, SecurityTableStructure securityTableStructure)
             where TUserType : IUser
        {
            cfg.AddAssembly(typeof (IUser).Assembly);
            new SchemaChanger(cfg, securityTableStructure).Change();
            new UserMapper(cfg, typeof(TUserType)).Map();
        }
	}
}