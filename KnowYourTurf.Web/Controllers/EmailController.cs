using System;
using System.Web.Mvc;
using CC.Core;
using CC.Core.CoreViewModelAndDTOs;
using CC.Core.DomainTools;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Services;
using KnowYourTurf.Web.Services.EmailHandlers;
using StructureMap;

namespace KnowYourTurf.Web.Controllers
{
    public class EmailController:KYTController
    {
        private readonly IRepository _repository;
        private readonly IEmailTemplateService _emailService;

        public EmailController(IRepository repository, IEmailTemplateService emailService)
        {
            _repository = repository;
            _emailService = emailService;
        }

        public ActionResult ProcessEmail(ViewModel input)
        {
            var notification = new Notification{Success = true};
            var emailJob = _repository.Find<EmailJob>(input.EntityId);
            var emailTemplateHandler = ObjectFactory.Container.GetInstance<IEmailTemplateHandler>(emailJob.Name+"Handler");
            try{ 
                emailJob.Subscribers.ForEachItem(x=>
                                                    {
                                                        var model = emailTemplateHandler.CreateModel(emailJob, x);
                                                        _emailService.SendSingleEmail(model);
                                                    });
            }
            catch(Exception ex)
            {
                notification.Success = false;
                notification.Message = ex.Message;
            }
            return Json(notification, JsonRequestBehavior.AllowGet);
        }
    }
}