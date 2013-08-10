namespace KnowYourTurf.Core.Domain.Tools.CustomAttributes
{
    [System.AttributeUsage(System.AttributeTargets.Property)]
    public class LinkDisplayAttribute : System.Attribute
    {
        public LinkDisplayAttribute()
        {
            // used just as a flag for ui gen
        }
    }
}