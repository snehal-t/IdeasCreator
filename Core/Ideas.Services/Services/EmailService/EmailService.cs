using Ideas.Models;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ideas.Services.Services.EmailService
{
    public class EmailService
    {
        public bool SendNotifications(NotificationTemplate template)
        {
            var client = new SendGridClient(Constants.SendGridAPIKey);
            var msg = new SendGridMessage();
            //msg.SetTemplateId("de68e67f-490a-4e5b-9d89-91f41d2e631d");
            msg.SetFrom(new EmailAddress("support@qidea.com", Constants.Name));
            msg.AddTo(new EmailAddress(template.receiver.Email, template.receiver.Name));
            msg.Subject = GetSubject(template.NotificationType);

            string emailBody = "Dear " + template.receiver.Name +
                "<br><br>" + msg.Subject +
                "<br><br><b>Idea Title:</b>" + template.IdeaTitle;
            if (!String.IsNullOrEmpty(template.UserComment))
            {
                emailBody+=  "<br><br>" + template.IdeaSender + " > Comments: " + template.UserComment;
            }
            emailBody += "<br><br>Please visit QIdeas now to see the latest updates<br>" + Constants.Site;

            emailBody += "<br><br>Regards,<br>" + Constants.Name;

            msg.AddContent(MimeType.Html, emailBody);
            var response = client.SendEmailAsync(msg);
            return true;
            //return response.StatusCode == System.Net.HttpStatusCode.Unauthorized ? false : true;
        }

        string GetSubject(NotificationType type)
        {
            string subject = string.Empty;
            switch (type)
            {
                case NotificationType.CreateIdea:
                    subject = Constants.CreateIdeaDesc;
                    break;
                case NotificationType.UpdateIdea:
                    subject = Constants.EditIdeaDesc;
                    break;
                case NotificationType.WithdrawIdea:
                    subject = Constants.WithdrawIdeaDesc;
                    break;
                case NotificationType.ApproveIdea:
                    subject = Constants.ApproveIdeaDesc;
                    break;
                case NotificationType.RejectIdea:
                    subject = Constants.RejectIdeaDesc;
                    break;
                case NotificationType.DeligateIdea:
                    subject = Constants.DeligateIdeaDesc;
                    break;
                case NotificationType.PickIdea:
                    subject = Constants.PickIdeaDesc;
                    break;
                case NotificationType.PickIdeaDone:
                    subject = Constants.PickIdeaDoneDesc;
                    break;
                case NotificationType.PickIdeaGiveUp:
                    subject = Constants.PickIdeaGiveUpDesc;
                    break;
                case NotificationType.PickIdeaRework:
                    subject = Constants.PickIdeaReworkDesc;
                    break;
                case NotificationType.PickIdeaAccept:
                    subject = Constants.PickIdeaAcceptDesc;
                    break;
                case NotificationType.PickIdeaReopen:
                    subject = Constants.PickIdeaReopenDesc;
                    break;
                case NotificationType.CommentIdea:
                    subject = Constants.CommentIdeaDesc;
                    break;
            }
            return subject;
        }
    }
}