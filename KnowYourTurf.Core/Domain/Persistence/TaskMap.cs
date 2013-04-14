using FluentNHibernate.Mapping;

namespace KnowYourTurf.Core.Domain.Persistence
{
    public class TaskMap : DomainEntityMap<Task>
    {
        public TaskMap()
        {
            References(x => x.TaskType);
            Map(x => x.ScheduledDate);
            Map(x => x.StartTime);
            Map(x => x.EndTime);
            Map(x => x.ActualTimeSpent);
            Map(x => x.Notes);
            Map(x => x.Deleted);
            Map(x => x.Complete);
            Map(x => x.QuantityNeeded);
            Map(x => x.QuantityUsed);
            Map(x => x.UnitType);
            Map(x => x.InventoryDecremented);
            Map(x => x.TargetPest);
            Map(x => x.RatePerUnit);
            Map(x => x.SprayPermitNumber);
            Map(x => x.ApplicationRequestedBy);
            References(x => x.Field);
            References(x => x.InventoryProduct);
            HasManyToMany(x => x.Equipment).Table("EquipmentToTask").Access.CamelCaseField(Prefix.Underscore).LazyLoad().Cascade.SaveUpdate();
            HasManyToMany(x => x.Employees).Table("EmployeeToTask").Access.CamelCaseField(Prefix.Underscore).LazyLoad().Cascade.SaveUpdate();

        }
    }

}