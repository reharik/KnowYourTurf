using System.Collections.Generic;
using System.Net.Mail;
using Alpinely.TownCrier;
using CC.Core;
using StructureMap;
using System.Linq;

namespace KnowYourTurf.Core.Services
{
    public interface IEmailTemplateService
    {
        void SendSingleEmail(EmailTemplateDTO input);
        void SendMultipleEmails(EmailTemplateDTO input, IEnumerable<MailAddress> addresses);
    }

    public class EmailTemplateService : IEmailTemplateService
    {
        private readonly IContainer _container;
        private readonly ILogger _logger;

        public EmailTemplateService(IContainer container, ILogger logger)
        {
            _container = container;
            _logger = logger;
        }

        public void SendSingleEmail(EmailTemplateDTO input)
        {
            var mergedEmailFactory = new MergedEmailFactory(new TemplateParser());
            MailMessage message = mergedEmailFactory
                .WithTokenValues(input.TokenValues)
                .WithSubject(input.Subject)
                .WithHtmlBody(input.Body)
                .Create();

            message.From = input.From;
            message.To.Add(input.To);
            
            var smtpClient = getSmtpClient();
            smtpClient.Send(message);
            _logger.LogInfo(message.To.FirstOrDefault().Address);
        }
      
        public void SendMultipleEmails(EmailTemplateDTO input, IEnumerable<MailAddress> addresses)
        {
            var mergedEmailFactory = new MergedEmailFactory(new TemplateParser());
            MailMessage message = mergedEmailFactory
                .WithTokenValues(input.TokenValues)
                .WithSubject(input.Subject)
                .WithHtmlBody(input.Body)
                .Create();

            message.From = input.From;
            message.To.AddRange(addresses);

            var smtpClient = getSmtpClient();
            smtpClient.Send(message);
        }
    
        private SmtpClient getSmtpClient()
        {
//            var smtpClient = new SmtpClient("smtp.gmail.com",587);
//            smtpClient.Credentials = new System.Net.NetworkCredential("reharik@gmail.com", "mishm124");
//            smtpClient.EnableSsl = true;

            //TODO get this from site settins

            var smtpClient = new SmtpClient("knowyourturf-qa.com");
            smtpClient.Credentials = new System.Net.NetworkCredential("mail@knowyourturf.com", "KYTadmin6");
            return smtpClient;
        }
    }

    public class EmailTemplateDTO
    {
        public IDictionary<string, string> TokenValues { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public MailAddress From { get; set; }
        public MailAddress To { get; set; }
    }
}