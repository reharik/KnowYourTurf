namespace KnowYourTurf.Core.Domain.Tools.CustomAttributes
{
    [System.AttributeUsage(System.AttributeTargets.Property)]
    public class TextAreaAttribute : System.Attribute
    {
        public int Rows { get; private set; }
        public int Columns { get; private set; }
        public TextAreaAttribute(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;
        }
    }
}