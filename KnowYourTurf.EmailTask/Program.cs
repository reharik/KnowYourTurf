using System;
using CC.Core;
using CC.Core.DomainTools;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Enums;
using KnowYourTurf.Core.Services;
using KnowYourTurf.Web;
using KnowYourTurf.Web.Services.EmailHandlers;
using StructureMap;

namespace KnowYourTurf.EmailTask
{
    public class Program
    {
        private static IRepository _repository;
        private static IEmailTemplateService _emailService;

        static void Main(string[] args)
        {
            Initialize();

            _repository = ObjectFactory.Container.GetInstance<IRepository>("SpecialInterceptorNoFilters");
            _emailService = ObjectFactory.Container.GetInstance<IEmailTemplateService>();
            ProcessEmail();
        }

        private static void Initialize()
        {
            ObjectFactory.Initialize(x =>
            {
                x.AddRegistry(new KYTWebRegistry());
            });
        }

        // this is obviously shite. plese rewrite the whole thing if people start using it.
        public static void ProcessEmail()
        {
            var emailJobs = _repository.FindAll<EmailJob>();
            emailJobs.ForEachItem(x =>
            {
                if (x.Status == Status.Active.ToString() && (
                    x.Frequency == EmailFrequency.Daily.ToString()
                    || x.Frequency == EmailFrequency.Once.ToString()
                    || (x.Frequency == EmailFrequency.Weekly.ToString() && DateTime.Now.Day == 1)))
                {
                    var emailTemplateHandler =
                        ObjectFactory.Container.GetInstance<IEmailTemplateHandler>(
                            x.EmailJobType.Name + "Handler");
                    x.Subscribers.ForEachItem(s =>
                    {
                        var model = emailTemplateHandler.CreateModel(x, s);
                        _emailService.SendSingleEmail(model);
                    });
                    if (x.Frequency == EmailFrequency.Once.ToString())
                    {
                        x.Status = Status.InActive.ToString();
                        _repository.Save(x);
                        _repository.Commit();
                    }

                }
            });
        }

    }
}
