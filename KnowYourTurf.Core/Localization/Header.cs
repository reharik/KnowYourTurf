namespace KnowYourTurf.Core.Localization
{
    public class Header
    {
        public string HeaderText { get; set; }
        public string Tooltip { get; set; }

        public bool Equals(Header obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return Equals(obj.HeaderText, HeaderText) && Equals(obj.Tooltip, Tooltip);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(Header)) return false;
            return Equals((Header)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((HeaderText != null ? HeaderText.GetHashCode() : 0) * 397) ^
                       (Tooltip != null ? Tooltip.GetHashCode() : 0);
            }
        }
    }
}