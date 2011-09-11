using KnowYourTurf.Core;

namespace KnowYourTurf.Web.Models
{
    public class FieldViewModel:ViewModel
    {
        public bool DeleteImage { get; set; }
        public Core.Domain.Field Field { get; set; }
    }
}