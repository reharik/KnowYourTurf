using System.Collections.Generic;
using CC.Core.Domain;
using CC.Core.Localization;
using Castle.Components.Validator;
using KnowYourTurf.Core.Enums;

namespace KnowYourTurf.Core.Domain
{
    public class EmailJob : DomainEntity, IPersistableObject
    {
        [ValidateNonEmpty]
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual string Sender { get; set; }
        public virtual string Subject { get; set; }
        [ValidateNonEmpty]
        [ValueOf(typeof(EmailFrequency))]
        public virtual string Frequency { get; set; }
        [ValueOf(typeof(Status))]
        public virtual string Status { get; set; }
        [ValidateNonEmpty]
        public virtual EmailTemplate EmailTemplate { get; set; }
        [ValidateNonEmpty]
        public virtual EmailJobType EmailJobType { get; set; }

        #region Collections
        private IList<User> _subscribers = new List<User>();
        public virtual IEnumerable<User> Subscribers { get { return _subscribers; } }
        public virtual void ClearSubscriber() { _subscribers = new List<User>(); }
        public virtual void RemoveSubscriber(User subscriber) { _subscribers.Remove(subscriber); }
        public virtual void AddSubscriber(User subscriber)
        {
            if (!subscriber.IsNew() && _subscribers.Contains(subscriber)) return;
            _subscribers.Add(subscriber);
        }
        #endregion
    }
}