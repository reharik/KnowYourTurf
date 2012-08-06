using System.Collections.Generic;
using System.Web.Mvc;

namespace KnowYourTurf.Core.CoreViewModels
{
   public class GroupedSelectViewModel
    {
       public GroupedSelectViewModel()
       {
           groups = new List<SelectGroup>();
       }

       public List<SelectGroup> groups { get; set; }
    }   
    public class SelectGroup
    {
        public string label { get; set; }
        public IEnumerable<SelectListItem> children { get; set; }
    }
}