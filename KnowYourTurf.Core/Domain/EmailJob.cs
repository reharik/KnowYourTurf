using System;
using System.Collections.Generic;
using Castle.Components.Validator;
using KnowYourTurf.Core;
using KnowYourTurf.Core.Enums;
using KnowYourTurf.Core.Localization;

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
        /// <summary>
        /// Aggregate Root that should not be modified through Email Job
        /// must have set on readonly field right now for model binder.
        /// </summary>
        private EmailTemplate _readOnlyEmailTemplate;
        [ValidateNonEmpty]
        public virtual EmailTemplate ReadOnlyEmailTemplate { get { return _readOnlyEmailTemplate; } set { _readOnlyEmailTemplate = value; } }
        public virtual void SetEmailTemplate(EmailTemplate emailTemplate)
        {
            _readOnlyEmailTemplate = emailTemplate;
        }
        ////
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