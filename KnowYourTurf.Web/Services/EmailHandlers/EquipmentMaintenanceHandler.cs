
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using CC.Core.DomainTools;
using HtmlTags;
using KnowYourTurf.Core;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Services;
using CC.Core;
using StructureMap;

namespace KnowYourTurf.Web.Services.EmailHandlers
{
    public class EquipmentMaintenanceHandler : IEmailTemplateHandler
    {
        private readonly IEmailTemplateService _emailTemplateService;
        private readonly IRepository _repository;
        private readonly ILogger _logger;

        public EquipmentMaintenanceHandler(IEmailTemplateService emailTemplateService,
       ILogger logger)
        {
            _emailTemplateService = emailTemplateService;
            _repository = ObjectFactory.Container.GetInstance<IRepository>("SpecialInterceptorNoFilters");

            _logger = logger;
        }

        public void Execute(EmailJob emailJob)
        {
            var equipment = _repository.Query<Equipment>(x => x.Threshold > 0 && x.Threshold <= x.TotalHours);
            equipment.ForEachItem(eq =>
            {
                emailJob.Subscribers.ForEachItem(sub =>
                {
                    var tokenValues = new Dictionary<string, string>
                    {
                        {"name", sub.FullName},
                        {"equipmentName", eq.Name},
                    };
                    var emailTemplateDTO = new EmailTemplateDTO
                    {
                        Subject = emailJob.Subject,
                        Body = emailJob.EmailTemplate.Template,
                        From = new MailAddress("EquipmentNotification@KnowYourTurf.Com", CoreLocalizationKeys.EQUIPMENT_MAINTENANCE_NOTIFICATION.ToString()),
                        To = new MailAddress(sub.Email, sub.FullName),
                        TokenValues = tokenValues
                    };
                    _emailTemplateService.SendSingleEmail(emailTemplateDTO);
                });
            });
        }
    }
}