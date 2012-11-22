//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net.Mail;
//using System.Text;
//using Alpinely.TownCrier;
//using CC.Core;
//using CC.Core.DomainTools;
//using KnowYourTurf.Core.Domain;
//using KnowYourTurf.Core.Enums;
//using NHibernate.Linq;

//namespace KnowYourTurf.Core.Services.IEmailJob
//{
//    public class EquipmentEmailJob : IEmailJob
//    {
//        private readonly IRepository _repository;

//        public EquipmentEmailJob(IRepository repository)
//        {
//            _repository = repository;
//        }

//        public void Execute()
//        {
//            var factory = new MergedEmailFactory(new TemplateParser());
//            var equipment = _repository.Query<Equipment>(x => x.Threshold >= x.TotalHours);
//            var emailJob = _repository.Query<EmailTemplate>(x => x.Name == "Equipment Maintenance").FirstOrDefault();
//            equipment.ForEachItem(x =>
//                               {
////                                   var sb = new StringBuilder();
//                                   //                                   x.Tasks.Where(t=>t.ScheduledDate >= DateTime.Now && t.ScheduledDate <= DateTime.Now.AddDays(1)).ForEachItem(task =>
//                                   //                                                         {
//                                   //                                                             sb.Append("Task Type: ");
//                                   //                                                             sb.Append(task.TaskType.Name);
//                                   //                                                             sb.AppendLine();
//                                   //                                                             sb.Append("Start Time: ");
//                                   //                                                             sb.Append(task.ScheduledStartTime);
//                                   //                                                             sb.AppendLine();
//                                   //                                                             sb.Append("Field: ");
//                                   //                                                             sb.Append(task.Field.Name);
//                                   //                                                             sb.AppendLine();
//                                   //                                                             sb.AppendLine();
//                                   //                                                         });
//                                   var tokenValues = new Dictionary<string, string>
//                                                         {
//                                                             {"name", x.Name},
//                                                             {"date", DateTime.Now.ToLongDateString()},
////                                                             {"tasks", sb.ToString()}
//                                                         };

//                                   MailMessage message = factory
//                                       .WithTokenValues(tokenValues)
//                                       .WithSubject("Equipment Maintenance")
//                                       .WithHtmlBody(emailTemplate.Template)
//                                       .Create();


//                                   var from = new MailAddress("DailyTaskEmail@KnowYourTurf.com", "Automated Emailer");
//                                   //                                   var to = new MailAddress(x.Email, x.FullName);
//                                   message.From = from;
//                                   //                                   message.To.Add(to);

//                                   var smtpClient = new SmtpClient();
//                                   smtpClient.Send(message);
//                               });
//        }
//    }
//}
