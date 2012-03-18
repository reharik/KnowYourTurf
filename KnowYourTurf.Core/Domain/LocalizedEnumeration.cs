using System;
using Castle.Components.Validator;


namespace KnowYourTurf.Core.Domain
{
    [Serializable]
    public class LocalizedEnumeration : DomainEntity, ILocalizedItem
    {
        [ValidateLength(500)]
        public virtual string Name { get; set; }
        [ValidateLength(500)]
        public virtual string ValueType { get; set; }
        public virtual string Culture { get; set; }
        [ValidateLength(500)]
        public virtual string Text { get; set; }
        [ValidateLength(500)]
        public virtual string Tooltip { get; set; }
    }
}