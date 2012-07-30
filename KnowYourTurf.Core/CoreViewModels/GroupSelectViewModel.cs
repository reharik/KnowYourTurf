using System.Collections.Generic;
using System.Web.Mvc;

namespace KnowYourTurf.Core.CoreViewModels
{
   public class GroupSelectViewModel
    {
       public GroupSelectViewModel()
       {
           Groups = new List<SelectGroup>();
       }

       public List<SelectGroup> Groups { get; set; }
    }   
    public class SelectGroup
    {
        public string Label { get; set; }
        public IEnumerable<SelectListItem> Children { get; set; }
    }
}