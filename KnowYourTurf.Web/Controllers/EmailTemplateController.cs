using System.Web.Mvc;
using KnowYourTurf.Core;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Html;
using KnowYourTurf.Core.Services;

namespace KnowYourTurf.Web.Controllers
{
    public class EmailTemplateController:KYTController
    {
        private readonly IRepository _repository;
        private readonly ISaveEntityService _saveEntityService;

        public EmailTemplateController(IRepository repository,
            ISaveEntityService saveEntityService)
        {
            _repository = repository;
            _saveEntityService = saveEntityService;
        }

        public ActionResult AddUpdate(ViewModel input)
        {
            var emailTemplate = input.EntityId > 0 ? _repository.Find<EmailTemplate>(input.EntityId) : new EmailTemplate();
            var model = new EmailTemplateViewModel
            {
                EmailTemplate = emailTemplate,
                _Title = WebLocalizationKeys.EMAIL_TEMPLATE_INFORMATION.ToString()
            };
            return PartialView("EmailTemplateAddUpdate", model);
        }
      
        public ActionResult Display(ViewModel input)
        {
            var emailTemplate = _repository.Find<EmailTemplate>(input.EntityId);
            var model = new EmailTemplateViewModel
                            {
                                EmailTemplate = emailTemplate,
                                _Title = WebLocalizationKeys.EMAIL_TEMPLATE_INFORMATION.ToString()
                            };
            return PartialView("EmailTemplateView", model);
        }

        public ActionResult Delete(ViewModel input)
        {
            var emailTemplate = _repository.Find<EmailTemplate>(input.EntityId);
            _repository.HardDelete(emailTemplate);
            _repository.UnitOfWork.Commit();
            return null;
        }
        [ValidateInput(false)]
        public ActionResult Save(EmailTemplateViewModel input)
        {
            var emailTemplate = input.EmailTemplate.EntityId > 0 ? _repository.Find<EmailTemplate>(input.EmailTemplate.EntityId) : new EmailTemplate();
            var newTask = mapToDomain(input, emailTemplate);

            var crudManager = _saveEntityService.ProcessSave(newTask);
            var notification = crudManager.Finish();
            return Json(notification, JsonRequestBehavior.AllowGet);
        }

        private EmailTemplate mapToDomain(EmailTemplateViewModel input, EmailTemplate emailTemplate)
        {
            var emailTemplateModel = input.EmailTemplate;
            emailTemplate.Name= emailTemplateModel.Name;
            emailTemplate.Description= emailTemplateModel.Description;
            emailTemplate.Template= emailTemplateModel.Template;
            return emailTemplate;
        }
    }

    public class EmailTemplateViewModel:ViewModel
    {
        public EmailTemplate EmailTemplate { get; set; }
    }
}