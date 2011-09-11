using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using Alpinely.TownCrier;
using KnowYourTurf.Core.Domain;

namespace KnowYourTurf.Core.Services.IEmailJob
{
    public interface IEmailJob
    {
        void Execute();
    }

    public class DailyTaskEmailJob : IEmailJob
    {
        private readonly IRepository _repository;

        public DailyTaskEmailJob(IRepository repository)
        {
            _repository = repository;
        }

        public void Execute()
        {
            var factory = new MergedEmailFactory(new TemplateParser());
            var employees = _repository.FindAll<Employee>();
            var emailTemplate = _repository.Query<EmailTemplate>(x => x.Name == "Daily Tasks List").FirstOrDefault();
            employees.Each(x =>
                               {
                                   var sb = new StringBuilder();
                                   x.GetTasks().Where(t=>t.ScheduledDate >= DateTime.Now && t.ScheduledDate <= DateTime.Now.AddDays(1)).Each(task =>
                                                         {
                                                             sb.Append("Task Type: ");
                                                             sb.Append(task.TaskType.Name);
                                                             sb.AppendLine();
                                                             sb.Append("Start Time: ");
                                                             sb.Append(task.ScheduledStartTime);
                                                             sb.AppendLine();
                                                             sb.Append("Field: ");
                                                             sb.Append(task.Field.Name);
                                                             sb.AppendLine();
                                                             sb.AppendLine();
                                                         });
                                   var tokenValues = new Dictionary<string, string>
                                                         {
                                                             {"name", x.FullName},
                                                             {"date", DateTime.Now.ToLongDateString()},
                                                             {"tasks", sb.ToString()}
                                                         };

                                   MailMessage message = factory
                                       .WithTokenValues(tokenValues)
                                       .WithSubject("Daily Tasks Email")
                                       .WithHtmlBody(emailTemplate.Template)
                                       .Create();


                                   var from = new MailAddress("DailyTaskEmail@KnowYourTurf.com", "Automated Emailer");
                                   var to = new MailAddress(x.Email, x.FullName);
                                   message.From = from;
                                   message.To.Add(to);

                                   var smtpClient = new SmtpClient();
                                   smtpClient.Send(message);
                               });
        }
    }
}