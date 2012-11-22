using System;
using System.IO;
using CC.Core;
using CC.Core.DomainTools;
using KnowYourTurf.Core;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Enums;
using KnowYourTurf.Core.Services;
using KnowYourTurf.Web;
using KnowYourTurf.Web.Services.EmailHandlers;
using StructureMap;
using log4net.Config;

namespace KnowYourTurf.EmailTask
{
    public class Program
    {
        private static IRepository _repository;
        private static IEmailTemplateService _emailService;
        private static ILogger _logger;

        static void Main(string[] args)
        {
            Initialize();

            _repository = ObjectFactory.Container.GetInstance<IRepository>("SpecialInterceptorNoFilters");
            _emailService = ObjectFactory.Container.GetInstance<IEmailTemplateService>();
            _logger = ObjectFactory.Container.GetInstance<ILogger>();
            ProcessEmail();
        }

        private static void Initialize()
        {
            ObjectFactory.Initialize(x =>
            {
                x.AddRegistry(new KYTWebRegistry());
            });
            XmlConfigurator.ConfigureAndWatch(new FileInfo(locateFileAsAbsolutePath("log4net.config")));

        }

        private static string locateFileAsAbsolutePath(string filename)
        {
            if (Path.IsPathRooted(filename))
                return filename;
            string applicationBase = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            string path = Path.Combine(applicationBase, filename);
            if (!File.Exists(path))
            {
                path = Path.Combine(Path.Combine(applicationBase, "bin"), filename);
                if (!File.Exists(path))
                    path = Path.Combine(Path.Combine(applicationBase, ".."), filename);
            }
            return path;
        }

        // this is obviously shite. plese rewrite the whole thing if people start using it.
        public static void ProcessEmail()
        {
            var emailJobs = _repository.FindAll<EmailJob>();
            emailJobs.ForEachItem(x =>
            {
                try
                {
                    if (x.Status == Status.Active.ToString() && (
                                                                    x.Frequency == EmailFrequency.Daily.ToString()
                                                                    || x.Frequency == EmailFrequency.Once.ToString()
                                                                    ||
                                                                    (x.Frequency == EmailFrequency.Weekly.ToString() &&
                                                                     DateTime.Now.Day == 1)))
                    {
                        var emailTemplateHandler = ObjectFactory.Container.GetInstance<IEmailTemplateHandler>(x.EmailJobType.Name + "Handler");
                        emailTemplateHandler.Execute(x);
                        if (x.Frequency == EmailFrequency.Once.ToString())
                        {
                            x.Status = Status.InActive.ToString();
                            _repository.Save(x);
                            _repository.Commit();
                        }

                    }
                }catch(Exception ex)
                {
                    _logger.LogError(ex.Message);
                }
            });
        }

    }
}
