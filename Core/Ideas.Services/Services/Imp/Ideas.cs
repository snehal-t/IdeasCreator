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
        
        public Ideas(IIdeasRepository iIdeaRepository)
        {
            _iIdeaRepository = iIdeaRepository;
        }

        public SignInResponse SignIn(string name, string email)
        {
            User user =  _iIdeaRepository.SignIn(name, email);
            if (user.Id != null)
            {
                return new SignInResponse(true, "", user.Id, user.Role);
            }
            else
            {
                return new SignInResponse(true, "User not found", null, null);
            }
        }

        public Response CreateIdea(Idea idea, string email, string author) {
            InviteeList inviteeList = _iIdeaRepository.CreateIdea(idea, email, author);
            if (inviteeList.IsSuccess)
            { 
                SendNotifications(inviteeList, NotificationType.CreateIdea);
            }
            return new Response(inviteeList.IsSuccess, inviteeList.Message);
        }

        public Response UpdateIdea(Idea idea, string email, string author)
        {
            InviteeList inviteeList = _iIdeaRepository.UpdateIdea(idea, email, author);
            if (inviteeList.IsSuccess)
            {
                SendNotifications(inviteeList, NotificationType.UpdateIdea);
            }
            return new Response(inviteeList.IsSuccess, inviteeList.Message);
        }
        public Response WithdrawIdea(string ideaId, string email, string author, string userComments)
        {
            InviteeList inviteeList = _iIdeaRepository.WithdrawIdea(ideaId, email, author, userComments);
            if (inviteeList.IsSuccess)
            {
                SendNotifications(inviteeList, NotificationType.WithdrawIdea);
            }
            return new Response(inviteeList.IsSuccess, inviteeList.Message);
        }
        public Response ApproveIdea(string ideaId, string email, string author, string userComments)
        {
            InviteeList inviteeList = _iIdeaRepository.ApproveIdea(ideaId, email, author, userComments);
            if (inviteeList.IsSuccess)
            {
                SendNotifications(inviteeList, NotificationType.ApproveIdea);
            }
            return new Response(inviteeList.IsSuccess, inviteeList.Message);
        }
        public Response RejectIdea(String ideaId, string email, string author, string userComments)
        {
            InviteeList inviteeList = _iIdeaRepository.RejectIdea(ideaId, email, author, userComments);
            if (inviteeList.IsSuccess)
            {
                SendNotifications(inviteeList, NotificationType.RejectIdea);
            }
            return new Response(inviteeList.IsSuccess, inviteeList.Message);
        }
        public Response DeligateIdea(String ideaId, User assignee, string userComments, string email, string author)
        {
            InviteeList inviteeList = _iIdeaRepository.DeligateIdea(ideaId, assignee, userComments, email, author);
            if (inviteeList.IsSuccess)
            {
                SendNotifications(inviteeList, NotificationType.DeligateIdea);
            }
            return new Response(inviteeList.IsSuccess, inviteeList.Message);
        }
        public Response PickIdea(string ideaId, string email, string author, string userComments)
        {
            InviteeList inviteeList = _iIdeaRepository.PickIdea(ideaId, email, author, userComments);
            if (inviteeList.IsSuccess)
            {
                SendNotifications(inviteeList, NotificationType.PickIdea);
            }
            return new Response(inviteeList.IsSuccess, inviteeList.Message);
        }
        public Response PickIdeaDone(string ideaId, string email, string author, string userComments)
        {
            InviteeList inviteeList = _iIdeaRepository.PickIdeaDone(ideaId, email, author, userComments);
            if (inviteeList.IsSuccess)
            {
                SendNotifications(inviteeList, NotificationType.PickIdeaDone);
            }
            return new Response(inviteeList.IsSuccess, inviteeList.Message);
        }
        public Response PickIdeaGiveUp(string ideaId, string email, string author, string userComments)
        {
            InviteeList inviteeList = _iIdeaRepository.PickIdeaGiveUp(ideaId, email, author, userComments);
            if (inviteeList.IsSuccess)
            {
                SendNotifications(inviteeList, NotificationType.PickIdeaGiveUp);
            }
            return new Response(inviteeList.IsSuccess, inviteeList.Message);
        }
        public Response PickIdeaRework(string ideaId, string email, string author, string userComments)
        {
            InviteeList inviteeList = _iIdeaRepository.PickIdeaRework(ideaId, email, author, userComments);
            if (inviteeList.IsSuccess)
            {
                SendNotifications(inviteeList, NotificationType.PickIdeaRework);
            }
            return new Response(inviteeList.IsSuccess, inviteeList.Message);
        }
        public Response PickIdeaAccept(string ideaId, string email, string author, string userComments)
        {
            InviteeList inviteeList = _iIdeaRepository.PickIdeaAccept(ideaId, email, author, userComments);
            if (inviteeList.IsSuccess)
            {
                SendNotifications(inviteeList, NotificationType.PickIdeaAccept);
            }
            return new Response(inviteeList.IsSuccess, inviteeList.Message);
        }
        public Response PickIdeaReopen(string ideaId, string email, string author, string userComments)
        {
            InviteeList inviteeList = _iIdeaRepository.PickIdeaReopen(ideaId, email, author, userComments);
            if (inviteeList.IsSuccess)
            {
                SendNotifications(inviteeList, NotificationType.PickIdeaReopen);
            }
            return new Response(inviteeList.IsSuccess, inviteeList.Message);
        }
        public Response WatchIdea(string ideaId, string email, string author, bool isActive)
        {
            return _iIdeaRepository.WatchIdea(ideaId, email, author, isActive);
        }
        public Response CommentIdea(string ideaId, string email, string author, bool commentType, long commentParentId, string userComments)
        {
            InviteeList inviteeList = _iIdeaRepository.CommentIdea(ideaId, email, author, commentType, commentParentId, userComments);
            if (inviteeList.IsSuccess)
            {
                SendNotifications(inviteeList, NotificationType.CommentIdea);
            }
            return new Response(inviteeList.IsSuccess, inviteeList.Message);
        }
        public IdeasResponse GetIdeas(string email, string author, string ideaPage, int pageSize, int currentPage, string orderBy, string order)
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
            return new IdeasResponse(false, "", null);
        }
        public WatchersResponse GetIdeaWatchers(string email, string author, string ideaId)
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
            return new WatchersResponse(false, "", null);
        }
        public CommentsResponse GetIdeaComments(string ideaId, string email, string author, int pageSize, int currentPage)
        {
            List<Comment> comments =  _iIdeaRepository.GetIdeaComments(ideaId, email, author, pageSize, currentPage);
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
            return new CommentsResponse(false, "", null);
        }
        public AlertsResponse GetAlerts(string email, string author, int pageSize, int currentPage)
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
            return new AlertsResponse(false, "", null);
        }
        public IdeaResponse GetIdeaDetails(string email, string author, string ideaId)
        {
            Idea idea =  _iIdeaRepository.GetIdeaDetails(email, author, ideaId);
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

        private void SendNotifications(InviteeList inviteeList, NotificationType type)
        {
            
        }
    }

    public enum NotificationType
    {
        CreateIdea = 1,
        UpdateIdea = 2,
        WithdrawIdea = 3,
        ApproveIdea = 4,
        RejectIdea = 5,
        DeligateIdea = 6,
        PickIdea = 7,
        PickIdeaDone = 8,
        PickIdeaGiveUp = 9,
        PickIdeaRework = 10,
        PickIdeaAccept = 11,
        PickIdeaReopen = 12,
        CommentIdea = 13
    }
}
