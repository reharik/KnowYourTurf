using System;
using System.Linq;
using System.Web.Mvc;
using KnowYourTurf.Core;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Enums;
using KnowYourTurf.Core.Html;
using KnowYourTurf.Core.Services;
using KnowYourTurf.Web.Models;

namespace KnowYourTurf.Web.Controllers
{
    public class EmailJobController : KYTController
    {
        private readonly IRepository _repository;
        private readonly ISaveEntityService _saveEntityService;
        private readonly ISelectBoxPickerService _selectBoxPickerService;
        private readonly ISelectListItemService _selectListItemService;

        public EmailJobController(IRepository repository,
            ISaveEntityService saveEntityService,
            ISelectBoxPickerService selectBoxPickerService,
            ISelectListItemService selectListItemService)
        {
            _repository = repository;
            _saveEntityService = saveEntityService;
            _selectBoxPickerService = selectBoxPickerService;
            _selectListItemService = selectListItemService;
        }

        public ActionResult EmailJob(ViewModel input)
        {
            var emailJob = input.EntityId > 0 ? _repository.Find<EmailJob>(input.EntityId) : new EmailJob();
            emailJob.Status = input.EntityId > 0 ? emailJob.Status : Status.InActive.ToString();
            var emailTemplates = _selectListItemService.CreateList<EmailTemplate>(x => x.Name, x => x.EntityId, true);
            var selectorDto = _selectBoxPickerService.GetPickerDto(emailJob.GetSubscribers().OrderBy(x=>x.FullName), x => x.FullName, x => x.EntityId);
            var model = new EmailJobViewModel
            {
                EmailJob = emailJob,
                UserSelectBoxPickerDto = selectorDto,
                EmailTemplateList = emailTemplates
            };
            return PartialView("EmailJobAddUpdate", model);
        }

        public ActionResult Display(ViewModel input)
        {
            var emailTemplate = _repository.Find<EmailJob>(input.EntityId);
            var model = new EmailJobViewModel
            {
                EmailJob = emailTemplate,
                AddEditUrl = UrlContext.GetUrlForAction<EmailJobController>(x => x.EmailJob(null)) + "/" + emailTemplate.EntityId
            };
            return PartialView("EmailJobView", model);
        }

        public ActionResult Delete(ViewModel input)
        {
            var emailTemplate = _repository.Find<EmailJob>(input.EntityId);
            _repository.HardDelete(emailTemplate);
            _repository.UnitOfWork.Commit();
            return null;
        }

        public ActionResult Save(EmailJobViewModel input)
        {
            var job = input.EmailJob.EntityId > 0 ? _repository.Find<EmailJob>(input.EmailJob.EntityId) : new EmailJob();
            mapItem(job,input);
            var crudManager = _saveEntityService.ProcessSave(job);
            var notification = crudManager.Finish();
            return Json(notification, JsonRequestBehavior.AllowGet);
        }

        private void mapItem(EmailJob job, EmailJobViewModel input)
        {
            job.Description = input.EmailJob.Description;
            job.Frequency = input.EmailJob.Frequency;
            job.Name = input.EmailJob.Name;
            job.Sender = input.EmailJob.Sender;
            job.Status = input.EmailJob.Status;
            job.Subject = input.EmailJob.Subject;
            job.EmailTemplate= _repository.Find<EmailTemplate>(input.EmailJob.EmailTemplate.EntityId);

            var listOfSelectedEntities = _selectBoxPickerService.GetListOfSelectedEntities<User>(input.UserSelectBoxPickerDto);
            listOfSelectedEntities.Each(job.AddSubscriber);
        }
    }
}