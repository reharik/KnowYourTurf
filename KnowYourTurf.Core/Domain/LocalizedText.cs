using System;

namespace KnowYourTurf.Core.Domain
{
    public interface ILocalizedItem 
    {
        string Name { get; }
        string Culture { get; }
        string Text { get; set; }
    }

    [Serializable]
    public class LocalizedText : DomainEntity, ILocalizedItem
    {
        public virtual string Name { get; set; }
        public virtual string Culture { get; set; }
        public virtual string Text { get; set; }

        public LocalizedText()
        {
        }

        public LocalizedText(string name, string culture, string text)
        {
            Name = name;
            Culture = culture;
            Text = text;
        }
    }
}