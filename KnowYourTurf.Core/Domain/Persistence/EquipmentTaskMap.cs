using FluentNHibernate.Mapping;

namespace KnowYourTurf.Core.Domain.Persistence
{
    public class EquipmentTaskMap : DomainEntityMap<EquipmentTask>
    {
        public EquipmentTaskMap()
        {
            Map(x => x.ScheduledDate);
            Map(x => x.StartTime);
            Map(x => x.EndTime);
            Map(x => x.ActualTimeSpent);
            Map(x => x.Notes);
            Map(x => x.Deleted);
            Map(x => x.Complete);
            References(x => x.TaskType);
            References(x => x.Equipment);
            HasManyToMany(x => x.Parts).Table("PartToEquipmentTask").Access.CamelCaseField(Prefix.Underscore).LazyLoad().Cascade.SaveUpdate();
            HasManyToMany(x => x.Employees).Table("EmployeeToEquipmentTask").Access.CamelCaseField(Prefix.Underscore).LazyLoad().Cascade.SaveUpdate();

        }
    }

}