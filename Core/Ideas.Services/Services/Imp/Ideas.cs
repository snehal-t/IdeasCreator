using System;
using System.Collections.Generic;
using System.Text;
using Ideas.Models;
using Ideas.Data.Repositories.Ideas;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;

namespace Ideas.Services.Services.Imp
{
    public class Ideas : IIdeas
    {
        private readonly IIdeasRepository _iIdeaRepository;
        private string _email = string.Empty;
        private string _author = string.Empty;

        public Ideas(IIdeasRepository iIdeaRepository)
        {
            _iIdeaRepository = iIdeaRepository;
        }

        public SignInResponse SignIn(string name, string email)
        {
            User user = _iIdeaRepository.SignIn(name, email);
            if (user.Id != null)
            {
                return new SignInResponse(true, "", user.Id, user.Role);
            }
            else
            {
                return new SignInResponse(true, "User not found", null, null);
            }
        }

        public DashboardResponse GetDashboard(string name, string author, string email)
        {
            Dashboard dashboard = _iIdeaRepository.GetDashboard(author, email);
            if (dashboard.IsSuccess)
            {
                return new DashboardResponse(dashboard.IsSuccess, dashboard.Message, dashboard);
            }
            else
            {
                return new DashboardResponse(dashboard.IsSuccess, dashboard.Message, null);
            }
        }

        public Response CreateIdea(string name, Idea idea, string email, string author)
        {
            InviteeList inviteeList = _iIdeaRepository.CreateIdea(idea, email, author);
            if (inviteeList.IsSuccess)
            {
                _email = email; _author=author;
                Task t = new Task(() => SendNotifications(inviteeList, NotificationType.CreateIdea, name, "", idea.Title));
                t.Start();
            }
            return new Response(inviteeList.IsSuccess, inviteeList.Message);
        }

        public Response UpdateIdea(string name, Idea idea, string email, string author)
        {
            InviteeList inviteeList = _iIdeaRepository.UpdateIdea(idea, email, author);
            if (inviteeList.IsSuccess)
            {
                _email = email; _author=author;
                Task t = new Task(() => SendNotifications(inviteeList, NotificationType.UpdateIdea, name, "", idea.Title));
                t.Start();
            }
            return new Response(inviteeList.IsSuccess, inviteeList.Message);
        }
        public Response WithdrawIdea(string name, string ideaId, string email, string author, string userComments)
        {
            InviteeList inviteeList = _iIdeaRepository.WithdrawIdea(ideaId, email, author, userComments);
            if (inviteeList.IsSuccess)
            {
                _email = email; _author=author;
                Task t = new Task(() => SendNotifications(inviteeList, NotificationType.WithdrawIdea, name, userComments, ideaId));
                t.Start();
            }
            return new Response(inviteeList.IsSuccess, inviteeList.Message);
        }
        public Response ApproveIdea(string name, string ideaId, string email, string author, string userComments)
        {
            InviteeList inviteeList = _iIdeaRepository.ApproveIdea(ideaId, email, author, userComments);
            if (inviteeList.IsSuccess)
            {
                _email = email; _author=author;
                Task t = new Task(() => SendNotifications(inviteeList, NotificationType.ApproveIdea, name, userComments, ideaId));
                t.Start();
            }
            return new Response(inviteeList.IsSuccess, inviteeList.Message);
        }
        public Response RejectIdea(string name, String ideaId, string email, string author, string userComments)
        {
            InviteeList inviteeList = _iIdeaRepository.RejectIdea(ideaId, email, author, userComments);
            if (inviteeList.IsSuccess)
            {
                _email = email; _author=author;
                Task t = new Task(() => SendNotifications(inviteeList, NotificationType.RejectIdea, name, userComments, ideaId));
                t.Start();
            }
            return new Response(inviteeList.IsSuccess, inviteeList.Message);
        }
        public Response DeligateIdea(string name, String ideaId, string assignee, string userComments, string email, string author)
        {
            InviteeList inviteeList = _iIdeaRepository.DeligateIdea(ideaId, assignee, userComments, email, author);
            if (inviteeList.IsSuccess)
            {
                _email = email; _author=author;
                Task t = new Task(() => SendNotifications(inviteeList, NotificationType.DeligateIdea, name, userComments, ideaId));
                t.Start();
            }
            return new Response(inviteeList.IsSuccess, inviteeList.Message);
        }
        public Response PickIdea(string name, string ideaId, string email, string author, string userComments)
        {
            InviteeList inviteeList = _iIdeaRepository.PickIdea(ideaId, email, author, userComments);
            if (inviteeList.IsSuccess)
            {
                _email = email; _author=author;
                Task t = new Task(() => SendNotifications(inviteeList, NotificationType.PickIdea, name, userComments, ideaId));
                t.Start();
            }
            return new Response(inviteeList.IsSuccess, inviteeList.Message);
        }
        public Response PickIdeaDone(string name, string ideaId, string email, string author, string userComments)
        {
            InviteeList inviteeList = _iIdeaRepository.PickIdeaDone(ideaId, email, author, userComments);
            if (inviteeList.IsSuccess)
            {
                _email = email; _author=author;
                Task t = new Task(() => SendNotifications(inviteeList, NotificationType.PickIdeaDone, name, userComments, ideaId));
                t.Start();
            }
            return new Response(inviteeList.IsSuccess, inviteeList.Message);
        }
        public Response PickIdeaGiveUp(string name, string ideaId, string email, string author, string userComments)
        {
            InviteeList inviteeList = _iIdeaRepository.PickIdeaGiveUp(ideaId, email, author, userComments);
            if (inviteeList.IsSuccess)
            {
                _email = email; _author=author;
                Task t = new Task(() => SendNotifications(inviteeList, NotificationType.PickIdeaGiveUp, name, userComments, ideaId));
                t.Start();
            }
            return new Response(inviteeList.IsSuccess, inviteeList.Message);
        }
        public Response PickIdeaRework(string name, string ideaId, string email, string author, string userComments)
        {
            InviteeList inviteeList = _iIdeaRepository.PickIdeaRework(ideaId, email, author, userComments);
            if (inviteeList.IsSuccess)
            {
                _email = email; _author=author;
                Task t = new Task(() => SendNotifications(inviteeList, NotificationType.PickIdeaRework, name, userComments, ideaId));
                t.Start();
            }
            return new Response(inviteeList.IsSuccess, inviteeList.Message);
        }
        public Response PickIdeaAccept(string name, string ideaId, string email, string author, string userComments)
        {
            InviteeList inviteeList = _iIdeaRepository.PickIdeaAccept(ideaId, email, author, userComments);
            if (inviteeList.IsSuccess)
            {
                _email = email; _author=author;
                Task t = new Task(() => SendNotifications(inviteeList, NotificationType.PickIdeaAccept, name, userComments, ideaId));
                t.Start();
            }
            return new Response(inviteeList.IsSuccess, inviteeList.Message);
        }
        public Response PickIdeaReopen(string name, string ideaId, string email, string author, string userComments)
        {
            InviteeList inviteeList = _iIdeaRepository.PickIdeaReopen(ideaId, email, author, userComments);
            if (inviteeList.IsSuccess)
            {
                _email = email; _author=author;
                Task t = new Task(() => SendNotifications(inviteeList, NotificationType.PickIdeaReopen, name, userComments, ideaId));
                t.Start();
            }
            return new Response(inviteeList.IsSuccess, inviteeList.Message);
        }
        public Response WatchIdea(string name, string ideaId, string email, string author, bool isActive)
        {
            return _iIdeaRepository.WatchIdea(ideaId, email, author, isActive);
        }
        public Response CommentIdea(string name, string ideaId, string email, string author, bool commentType, long commentParentId, string userComments)
        {
            InviteeList inviteeList = _iIdeaRepository.CommentIdea(ideaId, email, author, commentType, commentParentId, userComments);
            if (inviteeList.IsSuccess)
            {
                _email = email; _author=author;
                Task t = new Task(() => SendNotifications(inviteeList, NotificationType.CommentIdea, name, userComments, ideaId));
                t.Start();
            }
            return new Response(inviteeList.IsSuccess, inviteeList.Message);
        }

        public Response EditComment(string name, string ideaId, string commentId, string email, string author, bool commentType, long commentParentId, string userComments)
        {
            bool result = _iIdeaRepository.EditComment(ideaId, commentId, email, author, true, commentParentId, userComments);
            if (result)
            {
                return new Response(result, "Comment has been edited successfully");
            }
            else
            {
                return new Response(result, "Edit comment failed");
            }
        }

        public Response DeleteComment(string name, string ideaId, string commentId, string email, string author)
        {
            bool result = _iIdeaRepository.DeleteComment(ideaId, commentId, email, author);
            if (result)
            {
                return new Response(result, "Comment has been deleted successfully");
            }
            else
            {
                return new Response(result, "Delete comment failed");
            }
        }

        public IdeasResponse GetIdeas(string name, string email, string author, string ideaPage, int pageSize, int currentPage, string orderBy, string order)
        {
            List<Idea> ideas = _iIdeaRepository.GetIdeas(email, author, ideaPage, pageSize, currentPage, orderBy, order);
            if (ideas.Any())
            {
                if (ideas.First().IsSuccess)
                {
                    return new IdeasResponse(ideas.First().IsSuccess, ideas.First().Message, ideas);
                }
                else
                {
                    return new IdeasResponse(ideas.First().IsSuccess, ideas.First().Message, null);
                }
            }
            return new IdeasResponse(true, "", null);
        }
        public WatchersResponse GetIdeaWatchers(string name, string email, string author, string ideaId)
        {
            List<Watcher> watchers = _iIdeaRepository.GetIdeaWatchers(email, author, ideaId);
            if (watchers.Any())
            {
                if (watchers.First().IsSuccess)
                {
                    return new WatchersResponse(watchers.First().IsSuccess, watchers.First().Message, watchers);
                }
                else
                {
                    return new WatchersResponse(watchers.First().IsSuccess, watchers.First().Message, null);
                }
            }
            return new WatchersResponse(true, "", null);
        }
        public CommentsResponse GetIdeaComments(string name, string ideaId, string email, string author, int pageSize, int currentPage)
        {
            List<Comment> comments = _iIdeaRepository.GetIdeaComments(ideaId, email, author, pageSize, currentPage);
            if (comments.Any())
            {
                if (comments.First().IsSuccess)
                {
                    return new CommentsResponse(comments.First().IsSuccess, comments.First().Message, comments);
                }
                else
                {
                    return new CommentsResponse(comments.First().IsSuccess, comments.First().Message, null);
                }
            }
            return new CommentsResponse(true, "", null);
        }
        public AlertsResponse GetAlerts(string name, string email, string author, int pageSize, int currentPage)
        {
            List<Alert> alerts = _iIdeaRepository.GetAlerts(email, author, pageSize, currentPage);
            if (alerts.Any())
            {
                if (alerts.First().IsSuccess)
                {
                    return new AlertsResponse(alerts.First().IsSuccess, alerts.First().Message, alerts);
                }
                else
                {
                    return new AlertsResponse(alerts.First().IsSuccess, alerts.First().Message, null);
                }
            }
            return new AlertsResponse(true, "", null);
        }
        public IdeaResponse GetIdeaDetails(string name, string email, string author, string ideaId)
        {
            Idea idea = _iIdeaRepository.GetIdeaDetails(email, author, ideaId);
            if (idea != null)
            {
                if (idea.IsSuccess)
                {
                    return new IdeaResponse(idea.IsSuccess, idea.Message, idea);
                }
                else
                {
                    return new IdeaResponse(idea.IsSuccess, idea.Message, null);
                }
            }
            return new IdeaResponse(false, "", null);
        }

        bool SendNotifications(InviteeList inviteeList, NotificationType type, string sender, string comments, string ideaId)
        {
            try
            {
                string ideaName = string.Empty;
                if (type == NotificationType.CreateIdea || type == NotificationType.UpdateIdea)
                {
                    ideaName = ideaId;
                }
                else
                {
                    Idea idea = _iIdeaRepository.GetIdeaDetails(_email, _author, ideaId);
                    ideaName = idea.Title;
                }
                SendNotification(inviteeList.IdeaModerators, ReceiverType.Approver, type, sender, comments, ideaName);
                if (inviteeList.IdeaCreator != null)
                {
                    SendNotification(inviteeList.IdeaCreator, ReceiverType.Creator, type, sender, comments, ideaName);
                }
                if (inviteeList.IdeaPicker != null)
                {
                    SendNotification(inviteeList.IdeaPicker, ReceiverType.Picker, type, sender, comments, ideaName);
                }
                SendNotification(inviteeList.IdeaWatchers, ReceiverType.Watcher, type, sender, comments, ideaName);
                SendNotification(inviteeList.IdeaCommenters, ReceiverType.Commenter, type, sender, comments, ideaName);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        bool SendNotification(List<User> users, ReceiverType receiverType, NotificationType type, string sender, string comments, string ideaName)
        {
            foreach (var user in users)
            {
                SendNotification(user, receiverType, type, sender, comments, ideaName);
            }
            return true;
        }

        bool SendNotification(User receiver, ReceiverType receiverType, NotificationType type, string sender, string comments, string ideaName)
        {
            if (receiver.Name != null)
            {
                NotificationTemplate template = new NotificationTemplate();
                template.receiver = receiver;
                template.NotificationType = type;
                template.ReceiverType = receiverType;
                template.IdeaSender = sender;
                template.UserComment = comments;
                template.IdeaTitle = ideaName;
                Services.EmailService.EmailService emailService = new EmailService.EmailService();
                if (template.receiver.Name == "Snehal Thube")
                {
                    emailService.SendNotifications(template);
                }
            }
            return true;
        }
    }
}
