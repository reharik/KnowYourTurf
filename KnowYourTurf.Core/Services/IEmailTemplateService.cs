using System;
using System.Collections.Generic;
using System.Net.Mail;
using Alpinely.TownCrier;
using StructureMap;

namespace KnowYourTurf.Core.Services
{
    public interface IEmailTemplateService
    {
        string SendEmail(EmailTemplateDTO input);
        string SendEmail(EmailTemplateDTO input, IEnumerable<MailAddress> addresses);
    }

    public class EmailTemplateService : IEmailTemplateService
    {
        private readonly IContainer _container;

        public EmailTemplateService(IContainer container)
        {
            _container = container;
        }

        public string SendEmail(EmailTemplateDTO input)
        {
           return SendEmail(input, input.Addresses ?? new[] { input.To });
        }
        public string SendEmail(EmailTemplateDTO input, IEnumerable<MailAddress> addresses )
        {
            try
            {
                var mergedEmailFactory = _container.GetInstance<IMergedEmailFactory>();
                MailMessage message = mergedEmailFactory
                    .WithTokenValues(input.TokenValues)
                    .WithSubject(input.Subject)
                    .WithHtmlBody(input.Body)
                    .Create();

                message.From = input.From;
                message.To.AddRange(addresses);

                var smtpClient = new SmtpClient("mail.methodfitness.com", 25);
                smtpClient.Send(message);
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
            return string.Empty;
        }
    }

    public class EmailTemplateDTO
    {
        public IDictionary<string, string> TokenValues { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public MailAddress From { get; set; }
        public MailAddress To { get; set; }
        public IEnumerable<MailAddress> Addresses { get; set; }
    }
}