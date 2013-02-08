using System.Linq;
using CC.Core;
using CC.Core.CoreViewModelAndDTOs;
using CC.Core.DomainTools;
using CC.Core.Services;
using KnowYourTurf.Core.Domain;
using NHibernate.Linq;

namespace KnowYourTurf.Web.Services
{
    using System.Collections.Generic;

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
                categoryId = field.Site.EntityId;
            }
            if (categoryId > 0)
            {
                var category = _repository.Find<Site>(categoryId);
                var list = CreateList(category.Fields, x => x.Name, x => x.EntityId, false);
                groups.groups.Add(new SelectGroup { label = category.Name, children = list });
            }
            else
            {
                var categories = _repository.FindAll<Site>().AsQueryable().Fetch(x => x.Fields);
                categories.ForEachItem(x =>
                {
                    var list = CreateList(x.Fields, f => f.Name, f => f.EntityId, false);
                    groups.groups.Add(new SelectGroup { label = x.Name, children = list });

                });
            }
            return groups;
        }

        public GroupedSelectViewModel CreateProductSelectListItems()
        {
            IEnumerable<InventoryProduct> inventory = _repository.FindAll<InventoryProduct>();
            var chemicals =
                CreateListWithConcatinatedText(
                    inventory.Where(i => i.Product.InstantiatingType == "Chemical"),
                    x => x.Product.Name,
                    x => x.UnitType,
                    "-->",
                    y => y.EntityId,
                    false);
            var fertilizer =
                CreateListWithConcatinatedText(
                    inventory.Where(i => i.Product.InstantiatingType == "Fertilizer"),
                    x => x.Product.Name,
                    x => x.UnitType,
                    "-->",
                    x => x.EntityId,
                    false);
            var materials =
                CreateListWithConcatinatedText(
                    inventory.Where(i => i.Product.InstantiatingType == "Material"),
                    x => x.Product.Name,
                    x => x.UnitType,
                    "-->",
                    x => x.EntityId,
                    false);
            var groups = new GroupedSelectViewModel();
            groups.groups.Add(new SelectGroup { label = "Chemicals", children = chemicals });
            groups.groups.Add(new SelectGroup { label = "Ferilizers", children = fertilizer });
            groups.groups.Add(new SelectGroup { label = "Materials", children = materials });

            return groups;
        }

    }
}