using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;

namespace KnowYourTurf.Core.Config
{
    public class CustomPrincipal : IPrincipal
    {
        private readonly IIdentity _identity;
        private string _userId;
        private string _companyId;
        private string _userRoles;

        public CustomPrincipal(IIdentity identity, string userData)
        {
            _identity = identity;
            var data = userData.Split('|');
            var userIdProp = data.FirstOrDefault(x=>x.Contains("UserId="));
            _userId = userIdProp.Replace("UserId=","");
            var companyIdProp = data.FirstOrDefault(x => x.Contains("CompanyId="));
            _companyId = companyIdProp.Replace("CompanyId=", "");
            var userRoles = data.FirstOrDefault(x => x.Contains("UserRoles="));
            _userRoles = userRoles.Replace("UserRoles=", "");
        }

        public bool IsInRole(string role)
        {
           return _userRoles.Split(',').Any(x => x == role);
        }

        public IIdentity Identity{ get { return _identity; } }

        public long UserId { get{return _userId.IsNotEmpty()? Int64.Parse(_userId):0;} } 
        public long CompanyId { get { return _companyId.IsNotEmpty()? Int64.Parse(_companyId):0; } }
        public string UserRoles { get { return _userRoles; } }
    }
}