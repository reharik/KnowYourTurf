using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Castle.Components.Validator;
using KnowYourTurf.Core;
using KnowYourTurf.Core;
using KnowYourTurf.Core.CoreViewModelAndDTOs;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Domain.Tools;
using KnowYourTurf.Core.Enumerations;
using KnowYourTurf.Core.Html;
using KnowYourTurf.Core.Localization;
using KnowYourTurf.Core.Rules;
using KnowYourTurf.Core.Services;
using Rhino.Security.Interfaces;
using StructureMap;

namespace KnowYourTurf.Web.Controllers
{
    public class ClientController : AdminController
    {
        private readonly IRepository _repository;
        private readonly ISaveEntityService _saveEntityService;

        public ClientController(IRepository repository,
            ISaveEntityService saveEntityService)
        {
            _repository = repository;
            _saveEntityService = saveEntityService;
        }

        public ActionResult AddUpdate(ViewModel input)
        {
            var client = input.EntityId > 0 ? _repository.Find<Client>(input.EntityId) : new Client();
            var model = new ClientViewModel
            {
                Item = client,
                Title = WebLocalizationKeys.CLIENT_INFORMATION.ToString()
            };
            return PartialView(model);
        }

        public ActionResult Delete(ViewModel input)
        {
            var client = _repository.Find<User>(input.EntityId);
            var rulesEngineBase = ObjectFactory.Container.GetInstance<RulesEngineBase>("DeleteClientRules");
            var rulesResult = rulesEngineBase.ExecuteRules(client);
            if (!rulesResult.Success)
            {
                Notification notification = new Notification(rulesResult);
                return Json(notification);
            }
            _repository.Delete(client);
            _repository.UnitOfWork.Commit();
            return null;
        }

        public ActionResult Save(ClientViewModel input)
        {
            Client client;
            client = input.EntityId > 0 ? _repository.Find<Client>(input.EntityId) : new Client();
            client = mapToDomain(input, client);
//            if (input.DeleteImage)
//            {
////                _uploadedFileHandlerService.DeleteFile(client.ImageUrl);
//                client.ImageUrl = string.Empty;
//            }
//            
//            var file = _uploadedFileHandlerService.RetrieveUploadedFile();
////            var serverDirectory = "/CustomerPhotos/" + _sessionContext.GetCompanyId() + "/Clients";
//            client.ImageUrl = _uploadedFileHandlerService.GetUrlForFile(file, client.FirstName + "_" + client.LastName);
            var crudManager = _saveEntityService.ProcessSave(client);

//            _uploadedFileHandlerService.SaveUploadedFile(file, client.FirstName + "_" + client.LastName);
            var notification = crudManager.Finish();
            return Json(notification, "text/plain");
        }

        private Client mapToDomain(ClientViewModel model, Client client)
        {
            var clientModel = model.Item;
            client.Address1 = clientModel.Address1;
            client.Address2 = clientModel.Address2;
            client.FirstName = clientModel.FirstName;
            client.LastName = clientModel.LastName;
            client.Email = clientModel.Email;
            client.MobilePhone = clientModel.MobilePhone;
            client.City = clientModel.City;
            client.State = clientModel.State;
            client.ZipCode = clientModel.ZipCode;
            client.Notes = clientModel.Notes;
            client.Status = clientModel.Status;
            client.BirthDate = clientModel.BirthDate;
            return client;
        }
    }

    public class ClientViewModel:ViewModel
    {
        public Client Item { get; set; }
    }
}