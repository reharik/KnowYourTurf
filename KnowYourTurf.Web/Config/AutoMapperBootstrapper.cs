using AutoMapper;
using KnowYourTurf.Core.CoreViewModels;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Web.Controllers;
using KnowYourTurf.Web.Models;
using KnowYourTurf.Web.Models.Fertilizer;
using KnowYourTurf.Web.Models.Material;
using KnowYourTurf.Web.Models.Vendor;
using KnowYourTurf.Web.Models.VendorContact;

namespace KnowYourTurf.Web.Config
{
    public class AutoMapperBootstrapper
    {
        public virtual void BootstrapAutoMapper()
        {
            Mapper.CreateMap<User, UserViewModel>().ForMember(d => d.UserRoles, o => o.Ignore());
            Mapper.CreateMap<Field, FieldViewModel>();
            Mapper.CreateMap<Photo, PhotoDto>();
            Mapper.CreateMap<Task, TaskViewModel>().ForMember(d => d.Equipment, o => o.Ignore()).ForMember(d => d.Employees, o => o.Ignore());
            Mapper.CreateMap<Task, DisplayTaskViewModel>();
            Mapper.CreateMap<Equipment, EquipmentViewModel>();
            Mapper.CreateMap<EmailJob, EmailJobViewModel>().ForMember(d => d.Subscribers, o => o.Ignore());
            Mapper.CreateMap<EventType, EventTypeViewModel>();
            Mapper.CreateMap<TaskType, ListTypeViewModel>();
            Mapper.CreateMap<PhotoCategory, ListTypeViewModel>();
            Mapper.CreateMap<DocumentCategory, ListTypeViewModel>();
            Mapper.CreateMap<Material, MaterialViewModel>();
            Mapper.CreateMap<Fertilizer, FertilizerViewModel>();
            Mapper.CreateMap<Chemical, ChemicalViewModel>();
            Mapper.CreateMap<Document, DocumentViewModel>();
            Mapper.CreateMap<Photo, PhotoViewModel>();
            Mapper.CreateMap<Vendor, VendorViewModel>();
            Mapper.CreateMap<VendorContact, VendorContactViewModel>();
            Mapper.CreateMap<InventoryProduct, InventoryFertilizerViewModel>();
            Mapper.CreateMap<InventoryProduct, InventoryChemicalViewModel>();
            Mapper.CreateMap<InventoryProduct, InventoryMaterialViewModel>();
            Mapper.CreateMap<PurchaseOrder, POListViewModel>();
            Mapper.CreateMap<Event, EventViewModel>();
            Mapper.CreateMap<PurchaseOrderLineItem, PurchaseOrderLineItemViewModel>();

        
        }

        public static void Bootstrap()
        {
            new AutoMapperBootstrapper().BootstrapAutoMapper();
        }
    }
}