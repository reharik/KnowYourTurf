using System.Collections.Generic;
using KnowYourTurf.Core.Html.Menu;
using KnowYourTurf.Web.Controllers;

namespace KnowYourTurf.Web.Config
{
    public class SetupMenuConfig : IMenuConfig
    {
        private readonly IMenuBuilder _builder;

        public SetupMenuConfig(IMenuBuilder builder)
        {
            _builder = builder;
        }

        public IList<MenuItem> Build()
        {
            return DefaultMenubuilder();
        }

        private IList<MenuItem> DefaultMenubuilder()
        {
            return _builder
                .CreateNode<EmployeeListController>(c => c.EmployeeList(), WebLocalizationKeys.EMPLOYEES)
                .CreateNode<ListTypeListController>(c => c.ListType(), WebLocalizationKeys.ENUMERATIONS)
                .CreateNode(WebLocalizationKeys.PRODUCTS)
                    .HasChildren()
                    .CreateNode<MaterialListController>(c => c.MaterialList(), WebLocalizationKeys.MATERIALS)
                    .CreateNode<FertilizerListController>(c => c.FertilizerList(), WebLocalizationKeys.FERTILIZERS)
                    .CreateNode<ChemicalListController>(c => c.ChemicalList(), WebLocalizationKeys.CHEMICALS)
                    .CreateNode<SeedListController>(c => c.SeedList(), WebLocalizationKeys.SEEDS)
                    .EndChildren()
                .CreateNode<DocumentListController>(c => c.DocumentList(null), WebLocalizationKeys.DOCUMENTS)
                .CreateNode<PhotoListController>(c => c.PhotoList(null), WebLocalizationKeys.PHOTOS)
                //.CreateNode<EmailJobListController>(c => c.EmailJobList(), WebLocalizationKeys.EMAIL_JOBS)
                //.CreateNode<EmailTemplateListController>(c => c.EmailTemplateList(null), WebLocalizationKeys.EMAIL_TEMPLATES)
                .MenuTree();
        }
    }
}