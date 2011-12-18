using System;
using System.Web.Mvc;
using KnowYourTurf.Core;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Html;
using KnowYourTurf.Core.Services;
using KnowYourTurf.Web.Models.VendorContact;

namespace KnowYourTurf.Web.Controllers
{
    public class VendorContactController:KYTController
    {
        private readonly IRepository _repository;
        private readonly ISaveEntityService _saveEntityService;

        public VendorContactController(IRepository repository,ISaveEntityService saveEntityService)
        {
            _repository = repository;
            _saveEntityService = saveEntityService;
        }

        public ActionResult AddUpdate(ViewModel input)
        {
            var vendorContact = input.EntityId > 0 ? _repository.Find<VendorContact>(input.EntityId) : new VendorContact();
            var model = new VendorContactViewModel
                            {
                                ParentId = input.ParentId > 0 ? input.ParentId : vendorContact.Vendor.EntityId,
                                VendorContact = vendorContact,
                                Title = WebLocalizationKeys.VENDOR_CONTACT_INFORMATION.ToString()
                            };
            return PartialView("VendorContactAddUpdate", model);
        }
      
        public ActionResult Display(ViewModel input)
        {
            var vendorContact = _repository.Find<VendorContact>(input.EntityId);
            var model = new VendorContactViewModel
                            {
                                VendorContact = vendorContact,
                                AddUpdateUrl = UrlContext.GetUrlForAction<VendorContactController>(x => x.AddUpdate(null)) + "/" + vendorContact.EntityId,
                                Title = WebLocalizationKeys.VENDOR_CONTACT_INFORMATION.ToString()
                            };
            return PartialView("VendorContactView", model);
        }

        public ActionResult Delete(ViewModel input)
        {
            var vendorContact = _repository.Find<VendorContact>(input.EntityId);
            _repository.HardDelete(vendorContact);
            _repository.UnitOfWork.Commit();
            return null;
        }

        public ActionResult Save(VendorContactViewModel input)
        {
            VendorContact vendorContact;
            if (input.VendorContact.EntityId > 0)
            {
                vendorContact = _repository.Find<VendorContact>(input.VendorContact.EntityId);
            }
            else
            {
                vendorContact = new VendorContact();
                var vendor = _repository.Find<Vendor>(input.ParentId);
                vendorContact.Vendor = vendor;
            }
            mapItem(vendorContact, input);
            var crudManager = _saveEntityService.ProcessSave(vendorContact);
            var notification = crudManager.Finish();
            return Json(notification, JsonRequestBehavior.AllowGet);
        }

        private void mapItem(VendorContact vendorContact, VendorContactViewModel input)
        {
            vendorContact.Address1 = input.VendorContact.Address1;
            vendorContact.Address2 = input.VendorContact.Address2;
            vendorContact.City = input.VendorContact.City;
            vendorContact.Country = input.VendorContact.Country;
            vendorContact.Email = input.VendorContact.Email;
            vendorContact.Fax = input.VendorContact.Fax;
            vendorContact.FirstName = input.VendorContact.FirstName;
            vendorContact.LastName = input.VendorContact.LastName;
            vendorContact.Notes = input.VendorContact.Notes;
            vendorContact.Phone = input.VendorContact.Phone;
            vendorContact.State = input.VendorContact.State;
            vendorContact.Status = input.VendorContact.State;
            vendorContact.ZipCode = input.VendorContact.ZipCode;
        }
    }
}