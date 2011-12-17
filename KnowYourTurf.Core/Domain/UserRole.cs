namespace KnowYourTurf.Core.Domain
{
    public class UserRole:Entity
    {
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
    }
}