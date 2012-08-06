using System;
using System.Linq;
using System.Web.Mvc;
using FluentNHibernate.Utils;
using KnowYourTurf.Core;
using KnowYourTurf.Core.CoreViewModels;
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
        private readonly ISelectListItemService _selectListItemService;

        public EmailJobController(IRepository repository,
            ISaveEntityService saveEntityService,
            ISelectListItemService selectListItemService)
        {
            _repository = repository;
            _saveEntityService = saveEntityService;
            _selectListItemService = selectListItemService;
        }

        public ActionResult AddUpdate(ViewModel input)
        {
            var emailJob = input.EntityId > 0 ? _repository.Find<EmailJob>(input.EntityId) : new EmailJob();
            emailJob.Status = input.EntityId > 0 ? emailJob.Status : Status.InActive.ToString();
            var emailTemplates = _selectListItemService.CreateList<EmailTemplate>(x => x.Name, x => x.EntityId, true);
            var emailTypes = _selectListItemService.CreateList<EmailJobType>(x => x.Name, x => x.EntityId, true);
            var availableEmployees = _repository.Query<User>(x => x.UserLoginInfo.Status == Status.Active.ToString()).Select(x => new TokenInputDto { id = x.EntityId.ToString(), name = x.FirstName + " " + x.LastName }).ToList();
            var selectedEmployees = emailJob.Subscribers.Select(x => new TokenInputDto { id = x.EntityId.ToString(), name = x.FullName });
            
            
            var model = new EmailJobViewModel
            {
                Item = emailJob,
                EmailTemplateList = emailTemplates,
                EmailJobTypeList = emailTypes,
                _Title = WebLocalizationKeys.EMAIL_JOB_INFORMATION.ToString(),
                AvailableEmployees = availableEmployees.ToList(),
                SelectedEmployees = selectedEmployees.ToList()
            };
            return PartialView("EmailJobAddUpdate", model);
        }

        public ActionResult Display(ViewModel input)
        {
            var emailTemplate = _repository.Find<EmailJob>(input.EntityId);
            var model = new EmailJobViewModel
            {
                Item = emailTemplate,
                AddUpdateUrl = UrlContext.GetUrlForAction<EmailJobController>(x => x.AddUpdate(null)) + "/" + emailTemplate.EntityId,
                _Title = WebLocalizationKeys.EMAIL_JOB_INFORMATION.ToString()
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
            var job = input.Item.EntityId > 0 ? _repository.Find<EmailJob>(input.Item.EntityId) : new EmailJob();
            mapItem(job,input);
            var crudManager = _saveEntityService.ProcessSave(job);
            var notification = crudManager.Finish();
            return Json(notification, JsonRequestBehavior.AllowGet);
        }

        private void mapItem(EmailJob job, EmailJobViewModel input)
        {
            job.Description = input.Item.Description;
            job.Frequency = input.Item.Frequency;
            job.Name = input.Item.Name;
            job.Sender = input.Item.Sender;
            job.Status = input.Item.Status;
            job.Subject = input.Item.Subject;
            var emailTemplate = _repository.Find<EmailTemplate>(input.Item.EmailTemplate.EntityId);
            job.EmailTemplate = emailTemplate;
            job.EmailJobType = _repository.Find<EmailJobType>(input.Item.EmailJobType.EntityId);

            job.ClearSubscriber();
            if (input.EmployeeInput.IsNotEmpty())
                input.EmployeeInput.Split(',').Each(x => job.AddSubscriber(_repository.Find<User>(Int32.Parse(x))));
        }
    }
}