using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;

namespace KnowYourTurf.Core.Config
{
    /// <summary>
    /// Naming convention for the Foreign Keys in the database
    /// </summary>
    public class ForeignKeyConstraintNameConvention : IHasManyConvention, IReferenceConvention, IHasOneConvention , IHasManyToManyConvention// , IManyToManyCollectionInstance
    {
        public void Apply(IOneToManyCollectionInstance instance)
        {
            var sKey = "FK_{0}_oneToMany_{1}".ToFormat(instance.Member.Name, instance.EntityType.Name);
            instance.Key.ForeignKey(sKey);
        }

        public void Apply(IManyToOneInstance instance)
        {
            var sKey = "FK_" + instance.EntityType.Name + "_manyToOne_" + instance.StringIdentifierForModel;
            instance.ForeignKey(sKey);
        }

        public void Apply(IOneToOneInstance instance)
        {
            var sKey = "FK_" + instance.EntityType.Name + "_onToOne_" + instance.StringIdentifierForModel;
            instance.ForeignKey(sKey);
        }

        public void Apply(IManyToManyCollectionInstance instance)   //   IManyToOneInstance  instance)
        {
            var constraint = "FK_{0}_manyToMany_{1}".ToFormat(instance.StringIdentifierForModel, instance.EntityType.Name);
            instance.Key.ForeignKey(constraint);
            instance.Relationship.ForeignKey(constraint + "_otherFK");
        }
    }
}