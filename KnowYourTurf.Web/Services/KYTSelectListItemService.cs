using System.Linq;
using CC.Core;
using CC.Core.CoreViewModelAndDTOs;
using CC.Core.DomainTools;
using CC.Core.Services;
using KnowYourTurf.Core.Domain;
using NHibernate.Linq;

namespace KnowYourTurf.Web.Services
{
    public class KYTSelectListItemService:SelectListItemService
    {
        private readonly IRepository _repository;

        public KYTSelectListItemService(IRepository repository) : base(repository)
        {
            _repository = repository;
        }

        public GroupedSelectViewModel CreateFieldsSelectListItems(int categoryId = 0, int fieldId = 0)
        {
            var groups = new GroupedSelectViewModel();
            if (fieldId > 0 && categoryId == 0)
            {
                var field = _repository.Find<Field>(fieldId);
                categoryId = field.Category.EntityId;
            }
            if (categoryId > 0)
            {
                var category = _repository.Find<Category>(categoryId);
                var list = CreateList(category.Fields, x => x.Name, x => x.EntityId, false);
                groups.groups.Add(new SelectGroup { label = category.Name, children = list });
            }
            else
            {
                var categories = _repository.FindAll<Category>().AsQueryable().Fetch(x => x.Fields);
                categories.ForEachItem(x =>
                {
                    var list = CreateList(x.Fields, f => f.Name, f => f.EntityId, false);
                    groups.groups.Add(new SelectGroup { label = x.Name, children = list });

                });
            }
            return groups;
        }
    }
}