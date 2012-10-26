
using CC.Core.Domain;

namespace KnowYourTurf.Core.Domain
{
    public class UserRole:ListType
    {
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public override void UpdateSelf(Entity entity)
        {
            var item = (UserRole) entity;
            Name = item.Name;
            Description = item.Description;

        }
    }
}