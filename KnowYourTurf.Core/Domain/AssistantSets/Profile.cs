using System.Collections.Generic;

namespace DecisionCritical.Core.Domain
{
    public class Profile:DomainEntity
    {
        public virtual int OrgId { get; set; }
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual string AlphaGroup { get; set; }
        #region Collections
        private readonly IList<User> _users = new List<User>();
        public virtual IEnumerable<User> GetUsers() { return _users; }
        public virtual void RemoveUser(User user) { _users.Remove(user); }
        public virtual void AddUser(User user)
        {
            if (!user.IsNew() && _users.Contains(user)) return;
            _users.Add(user);
        }
        #endregion

    }
}