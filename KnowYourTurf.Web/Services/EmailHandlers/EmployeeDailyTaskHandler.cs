using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using HtmlTags;
using KnowYourTurf.Core;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Services;
using CC.Core;

namespace KnowYourTurf.Web.Services.EmailHandlers
{
    public class EmployeeDailyTaskHandler : IEmailTemplateHandler
    {
        public EmailTemplateDTO CreateModel(EmailJob emailJob, User subscriber)
        {
            var employee = subscriber;
            var tasks = employee.Tasks.Where(x=>x.ScheduledDate.Value.Date == DateTime.Now.Date);
            var tasksHtml = buildHtmlForTasks(tasks);
            var tokenValues = new Dictionary<string, string>
                      {
                          {"name", subscriber.FullName},
                          {"data", DateTime.Now.ToLongDateString()},
                          {"tasks",tasksHtml.ToString()}
                      };
            var emailTemplateDTO = new EmailTemplateDTO
            {
                Subject = CoreLocalizationKeys.DAILY_TASK_SUMMARY.ToString(),
                Body = emailJob.EmailTemplate.Template,
                From = new MailAddress("DailyTasks@KnowYourTurf.Com", CoreLocalizationKeys.KNOWYOURTURF_DAILY_TASKS.ToString()),
                To = new MailAddress(subscriber.Email, subscriber.FullName),
                TokenValues = tokenValues
            };
            return emailTemplateDTO;
        }

        private HtmlTag buildHtmlForTasks(IEnumerable<Task> tasks)
        {
            var rootTag = new DivTag("tasks");
            var ul = new HtmlTag("ul");
            tasks.OrderBy(x=>x.ScheduledDate).ForEachItem(x=>
                           {
                               var li = new HtmlTag("li");
                               var liDiv = new DivTag("liDiv");
                               var task = new HtmlTag("span");
                               var taskText = x.ScheduledDate.Value.ToLongDateString()+" "+x.ScheduledStartTime.Value.ToShortTimeString();
                               taskText += x.ScheduledEndTime.HasValue ? " To " + x.ScheduledEndTime.Value.ToShortTimeString() : "";
                               taskText += ": " + x.TaskType.Name + " at " + x.Field.Name;
                               task.Text(taskText);
                               var br = new HtmlTag("br");
                               var notesSpan = new HtmlTag("span");
                               notesSpan.Text(x.Notes);
                               liDiv.Children.Add(task);
                               liDiv.Children.Add(br);
                               liDiv.Children.Add(notesSpan);

                               li.Children.Add(liDiv);
                               ul.Children.Add(li);
                           });
            rootTag.Children.Add(ul);
            return rootTag;
        }   
    }
}