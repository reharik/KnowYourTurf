using System;
using System.Web.Mvc;
using FubuMVC.Core;
using KnowYourTurf.Core;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Html;
using KnowYourTurf.Core.Services;
using KnowYourTurf.Web.Models.VendorContact;
using System.Linq;

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
            VendorContact vendorContact;
            if (input.EntityId > 0)
            {
                var vendor = _repository.Find<Vendor>(input.ParentId);
                vendorContact = vendor.Contacts.FirstOrDefault(x => x.EntityId == input.EntityId);
            }
            else{ vendorContact = new VendorContact();}
            var model = new VendorContactViewModel
                            {
                                ParentId = input.ParentId > 0 ? input.ParentId : vendorContact.Vendor.EntityId,
                                Item = vendorContact,
                                _Title = WebLocalizationKeys.VENDOR_CONTACT_INFORMATION.ToString()
                            };
            return PartialView("VendorContactAddUpdate", model);
        }
      
        public ActionResult Display(ViewModel input)
        {
            var vendor = _repository.Find<Vendor>(input.ParentId);
            var vendorContact = vendor.Contacts.FirstOrDefault(x => x.EntityId == input.EntityId);
            var model = new VendorContactViewModel
                            {
                                Item = vendorContact,
                                AddUpdateUrl = UrlContext.GetUrlForAction<VendorContactController>(x => x.AddUpdate(null)) + "/" + vendorContact.EntityId,
                                _Title = WebLocalizationKeys.VENDOR_CONTACT_INFORMATION.ToString()
                            };
            return PartialView("VendorContactView", model);
        }

        public ActionResult Delete(ViewModel input)
        {
            var vendor = _repository.Find<Vendor>(input.ParentId);
            var vendorContact = vendor.Contacts.FirstOrDefault(x => x.EntityId == input.EntityId);
            vendor.RemoveContact(vendorContact);
            var crudManager = _saveEntityService.ProcessSave(vendor);
            var notification = crudManager.Finish();
            return Json(notification, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteMultiple(BulkActionViewModel input)
        {
            var vendor = _repository.Find<Vendor>(input.ParentId);
            var deleteContacts = vendor.Contacts.Where(x => input.EntityIds.Contains(x.EntityId));
            deleteContacts.ForEachItem(vendor.RemoveContact);
            var crudManager = _saveEntityService.ProcessSave(vendor);
            var notification = crudManager.Finish();
            return Json(notification, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Save(VendorContactViewModel input)
        {
            var vendor = _repository.Find<Vendor>(input.ParentId);
            VendorContact vendorContact;
            if (input.EntityId > 0)
            {
                vendorContact = vendor.Contacts.FirstOrDefault(x => x.EntityId == input.EntityId);
            }
            else
            {
                vendorContact = new VendorContact();
                vendor.AddContact(vendorContact);
            }
            mapItem(vendorContact, input);
            var crudManager = _saveEntityService.ProcessSave(vendor);
            var notification = crudManager.Finish();
            return Json(notification, JsonRequestBehavior.AllowGet);
        }

        private void mapItem(VendorContact vendorContact, VendorContactViewModel input)
        {
            vendorContact.Address1 = input.Item.Address1;
            vendorContact.Address2 = input.Item.Address2;
            vendorContact.City = input.Item.City;
            vendorContact.Country = input.Item.Country;
            vendorContact.Email = input.Item.Email;
            vendorContact.Fax = input.Item.Fax;
            vendorContact.FirstName = input.Item.FirstName;
            vendorContact.LastName = input.Item.LastName;
            vendorContact.Notes = input.Item.Notes;
            vendorContact.Phone = input.Item.Phone;
            vendorContact.State = input.Item.State;
            vendorContact.Status = input.Item.State;
            vendorContact.ZipCode = input.Item.ZipCode;
        }
    }
}