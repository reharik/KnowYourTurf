using System.Collections.Generic;
using KnowYourTurf.Web.Controllers;

namespace KnowYourTurf.Core.Domain.Persistence
{
    public class EquipmentVendor:VendorBase
    {
        private readonly IList<EquipmentType> _equipmentTypes = new List<EquipmentType>();
        public virtual IEnumerable<EquipmentType> EquipmentTypes { get { return _equipmentTypes; } }
        public virtual void RemoveEquipmentType(EquipmentType EquipmentType) { _equipmentTypes.Remove(EquipmentType); }
        public virtual void AddEquipmentType(EquipmentType EquipmentType)
        {
            if (_equipmentTypes.Contains(EquipmentType)) return;
            _equipmentTypes.Add(EquipmentType);
        }
    }
}