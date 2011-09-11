using KnowYourTurf.Core;

namespace KnowYourTurf.Web.Models.Equipment
{
    public class EquipmentViewModel:ViewModel
    {
        public Core.Domain.Equipment Equipment { get; set; }
        public bool DeleteImage { get; set; }
    }
}