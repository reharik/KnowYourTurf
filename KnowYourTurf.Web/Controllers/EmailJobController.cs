using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using CC.Core.CoreViewModelAndDTOs;
using CC.Core.DomainTools;
using CC.Core.Html;
using CC.Core.Services;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Enums;
using KnowYourTurf.Web.Models;

namespace KnowYourTurf.Web.Controllers
{
    public class EmailJobController : KYTController
    {
        private readonly IRepository _repository;
        private readonly ISaveEntityService _saveEntityService;
        private readonly ISelectListItemService _selectListItemService;
        private readonly IUpdateCollectionService _updateCollectionService;

        public EmailJobController(IRepository repository,
            ISaveEntityService saveEntityService,
            ISelectListItemService selectListItemService,
            IUpdateCollectionService updateCollectionService)
        {
            _repository = repository;
            _saveEntityService = saveEntityService;
            _selectListItemService = selectListItemService;
            _updateCollectionService = updateCollectionService;
        }

        public ActionResult AddUpdate_Template(ViewModel input)
        {
            return View("EmailJobAddUpdate", new EmailJobViewModel());
        }

        public ActionResult AddUpdate(ViewModel input)
        {
            var emailJob = input.EntityId > 0 ? _repository.Find<EmailJob>(input.EntityId) : new EmailJob();
            emailJob.Status = input.EntityId > 0 ? emailJob.Status : Status.InActive.ToString();
            
            var availableSubscribers = _repository.Query<User>(x => x.UserLoginInfo.Status == Status.Active.ToString()).Select(x => new TokenInputDto { id = x.EntityId.ToString(), name = x.FirstName + " " + x.LastName }).ToList();
            var selectedSubscribers = emailJob.Subscribers.Select(x => new TokenInputDto { id = x.EntityId.ToString(), name = x.FullName });

            var model = Mapper.Map<EmailJob, EmailJobViewModel>(emailJob);
            model.Subscribers = new TokenInputViewModel
                                  {
                                      _availableItems = availableSubscribers,
                                      selectedItems = selectedSubscribers
                                  };
            model._EmailTemplateEntityIdList = _selectListItemService.CreateList<EmailJobType>(x => x.Name, x => x.EntityId, true);
            model._EmailJobTypeEntityIdList = _selectListItemService.CreateList<EmailTemplate>(x => x.Name, x => x.EntityId, true);
            model._StatusList = _selectListItemService.CreateList<Status>(true);
            model._FrequencyList = _selectListItemService.CreateList<EmailFrequency>(true);
            model._Title = WebLocalizationKeys.EMAIL_JOB_INFORMATION.ToString();
            model._saveUrl = UrlContext.GetUrlForAction<EmailJobController>(x => x.Save(null));
            return Json(model,JsonRequestBehavior.AllowGet);
        }

//        public ActionResult Display(ViewModel input)
//        {
//            var emailTemplate = _repository.Find<EmailJob>(input.EntityId);
//            var model = new EmailJobViewModel
//            {
//                Item = emailTemplate,
//                AddUpdateUrl = UrlContext.GetUrlForAction<EmailJobController>(x => x.AddUpdate(null)) + "/" + emailTemplate.EntityId,
//                _Title = WebLocalizationKeys.EMAIL_JOB_INFORMATION.ToString()
//            };
//            return PartialView("EmailJobView", model);
//        }

        public ActionResult Delete(ViewModel input)
        {
            var emailTemplate = _repository.Find<EmailJob>(input.EntityId);
            _repository.HardDelete(emailTemplate);
            _repository.UnitOfWork.Commit();
            return null;
        }

        public ActionResult Save(EmailJobViewModel input)
        {
            var job = input.EntityId > 0 ? _repository.Find<EmailJob>(input.EntityId) : new EmailJob();
            mapItem(job,input);
            var crudManager = _saveEntityService.ProcessSave(job);
            var notification = crudManager.Finish();
            return Json(notification, JsonRequestBehavior.AllowGet);
        }

        private void mapItem(EmailJob job, EmailJobViewModel input)
        {
            job.Description = input.Description;
            job.Frequency = input.Frequency;
            job.Name = input.Name;
            job.Status = input.Status;
            job.Subject = input.Subject;
            var emailTemplate = _repository.Find<EmailTemplate>(input.EmailTemplateEntityId);
            job.EmailTemplate = emailTemplate;
            job.EmailJobType = _repository.Find<EmailJobType>(input.EmailJobTypeEntityId);
            
            _updateCollectionService.Update(job.Subscribers, input.Subscribers, job.AddSubscriber, job.RemoveSubscriber);
            
        }
    }
}