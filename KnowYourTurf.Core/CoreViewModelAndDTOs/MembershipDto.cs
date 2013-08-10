namespace KnowYourTurf.Core.CoreViewModelAndDTOs
{
    public class MembershipDto
    {
        public int EntityId { get; set; }
        public string Name { get; set; }
        public string HeldFrom { get; set; }
        public string HeldTo { get; set; }
    }

    public class PublicationAuthorDto
    {
        public int EntityId { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string MiddleInitial { get; set; }
        public virtual string LastName { get; set; }
        public virtual string Title { get; set; }
        public virtual string Organization { get; set; }
        public virtual int Rank { get; set; }
    }

    public class FundedActivityAuthorDto
    {
        public int EntityId { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string MiddleInitial { get; set; }
        public virtual string LastName { get; set; }
        public virtual string Credentials { get; set; }
        public virtual string Organization { get; set; }
        public virtual int Rank { get; set; }
    }
}