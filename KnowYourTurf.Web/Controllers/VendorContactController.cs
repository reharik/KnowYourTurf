using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using CC.Core.CoreViewModelAndDTOs;
using CC.Core.DomainTools;
using CC.Core.Enumerations;
using CC.Core.Html;
using CC.Core.Services;
using KnowYourTurf.Core;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Web.Models.VendorContact;
using System.Linq;
using Status = KnowYourTurf.Core.Enums.Status;

namespace KnowYourTurf.Web.Controllers
{
    public class VendorContactController:KYTController
    {
        private readonly IRepository _repository;
        private readonly ISaveEntityService _saveEntityService;
        private readonly ISelectListItemService _selectListItemService;

        public VendorContactController(IRepository repository,
            ISaveEntityService saveEntityService,
            ISelectListItemService selectListItemService)
        {
            _repository = repository;
            _saveEntityService = saveEntityService;
            _selectListItemService = selectListItemService;
        }

        public ActionResult AddUpdate_Template(ViewModel input)
        {
            return View("VendorContactAddUpdate", new VendorContactViewModel());
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
            var status = _selectListItemService.CreateList<Status>(true);
            var states = _selectListItemService.CreateList<State>(true);
            var model = Mapper.Map<VendorContact,VendorContactViewModel>(vendorContact);
            model.ParentId = input.ParentId > 0 ? input.ParentId : vendorContact.Vendor.EntityId;
            model._Title = WebLocalizationKeys.VENDOR_CONTACT_INFORMATION.ToString();
            model._saveUrl = UrlContext.GetUrlForAction<VendorContactController>(x => x.Save(null));
            model._StatusList = status;
            model._StateList = states;
            return Json(model, JsonRequestBehavior.AllowGet);
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
            var deleteContacts = new List<VendorContact>();
            vendor.Contacts.Where(x => input.EntityIds.Contains(x.EntityId)).ForEachItem(deleteContacts.Add);
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
            vendorContact.Address1 = input.Address1;
            vendorContact.Address2 = input.Address2;
            vendorContact.City = input.City;
            vendorContact.Email = input.Email;
            vendorContact.Fax = input.Fax;
            vendorContact.FirstName = input.FirstName;
            vendorContact.LastName = input.LastName;
            vendorContact.Notes = input.Notes;
            vendorContact.Phone = input.Phone;
            vendorContact.State = input.State;
            vendorContact.Status = input.Status;
            vendorContact.ZipCode = input.ZipCode;
        }
    }
}