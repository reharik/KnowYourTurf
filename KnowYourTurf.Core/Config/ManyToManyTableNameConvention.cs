using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Inspections;

namespace KnowYourTurf.Core.Config
{
    public class CustomManyToManyTableNameConvention: ManyToManyTableNameConvention
        {
            protected override string GetBiDirectionalTableName(IManyToManyCollectionInspector collection, IManyToManyCollectionInspector otherSide)
            {
                return collection.EntityType.Name + "_" + otherSide.EntityType.Name;
            }

            protected override string GetUniDirectionalTableName(IManyToManyCollectionInspector collection)
            {
                return collection.EntityType.Name + "_" + collection.ChildType.Name;
            }
        }
}