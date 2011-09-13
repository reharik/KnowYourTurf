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

        public IList<MenuItem> Build(bool withoutPermissions = false)
        {
            return DefaultMenubuilder(withoutPermissions);
        }

        private IList<MenuItem> DefaultMenubuilder(bool withoutPermissions = false)
        {
            return _builder
                //.CreateNode<AdminListController>(c => c.AdminList(), WebLocalizationKeys.ADMINS)
                .CreateNode<EmployeeListController>(c => c.EmployeeList(), WebLocalizationKeys.EMPLOYEES)
                .CreateNode<FacilitiesListController>(c => c.FacilitiesList(), WebLocalizationKeys.FACILITIES)
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
                .CreateNode<VendorListController>(c => c.VendorList(null), WebLocalizationKeys.VENDORS)
                .CreateNode(WebLocalizationKeys.INVENTORY)
                    .HasChildren()
                    .CreateNode<InventoryListController>(c => c.InventoryProductList(null), WebLocalizationKeys.MATERIALS, "ProductType=Material")
                    .CreateNode<InventoryListController>(c => c.InventoryProductList(null), WebLocalizationKeys.FERTILIZERS, "ProductType=Fertilizer")
                    .CreateNode<InventoryListController>(c => c.InventoryProductList(null), WebLocalizationKeys.CHEMICALS, "ProductType=Chemical")
                    .CreateNode<InventoryListController>(c => c.InventoryProductList(null), WebLocalizationKeys.SEEDS, "ProductType=Seed")
                    .EndChildren()
                .CreateNode<PurchaseOrderListController>(c => c.PurchaseOrderList(), WebLocalizationKeys.PURCHASE_ORDERS)

                .MenuTree(withoutPermissions);
        }
    }
}