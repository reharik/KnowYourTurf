using System;
using KnowYourTurf.Core;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Services;

namespace KnowYourTurf.Web.Services.EmailHandlers
{
    public interface IEmailTemplateHandler
    {
        EmailTemplateDTO CreateModel(EmailJob emailJob, User subscriber);
    }
}