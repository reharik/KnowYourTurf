using FluentNHibernate.Mapping;

namespace KnowYourTurf.Core.Domain.Persistence
{
    public class EquipmentMap : DomainEntityMap<Equipment>
    {
        public EquipmentMap()
        {
            Map(x => x.Name);
            Map(x => x.TotalHours);
            Map(x => x.Description);
            Map(x => x.Make);
            Map(x => x.Model);
            Map(x => x.SerialNumber);
            Map(x => x.WarrentyInfo);
            References(x => x.Vendor);
        }
    }
}