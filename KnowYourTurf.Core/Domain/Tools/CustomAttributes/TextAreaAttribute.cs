namespace KnowYourTurf.Core.Domain.Tools.CustomAttributes
{
    [System.AttributeUsage(System.AttributeTargets.Property)]
    public class TextAreaAttribute : System.Attribute
    {
        public TextAreaAttribute()
        {
            // used just as a flag for ui gen        
        }
    }
}