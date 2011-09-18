using FluentNHibernate.Mapping;

namespace KnowYourTurf.Core.Domain.Persistence
{
    public class BaseProductMap : DomainEntityMap<BaseProduct>
    {
        public BaseProductMap()
        {
            Map(x => x.Name);
            Map(x => x.Description);
            Map(x => x.InstantiatingType);
            Map(x => x.Notes);
        }

        public class FertilizerMap : SubclassMap<Fertilizer>
        {
            public FertilizerMap()
            {
                Map(x => x.N);
                Map(x => x.P);
                Map(x => x.K);
            }
        }

        public class MaterialMap : SubclassMap<Material>
        {
            public MaterialMap()
            {
            }
        }

        public class ChemicalMap : SubclassMap<Chemical>
        {
            public ChemicalMap()
            {
                Map(x => x.ActiveIngredient);
                Map(x => x.ActiveIngredientPercent);
                Map(x => x.EPAEstNumber);
                Map(x => x.EPARegNumber);
            }
        }

       
    }
}