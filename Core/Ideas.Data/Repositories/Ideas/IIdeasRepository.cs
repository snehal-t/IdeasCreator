using System;
using System.Collections.Generic;
using System.Text;
using Ideas.Models;
namespace Ideas.Data.Repositories.Ideas
{
    public interface IIdeasRepository
    {
        InviteeList CreateIdea(Idea idea, string email);
        InviteeList UpdateIdea(Idea idea, string email);
        InviteeList WithDrawIdea(string ideaId, string email, string userComments);
        InviteeList ApproveIdea(string ideaId, string email, string userComments, bool commentType, long commentParentId);
        InviteeList RejectIdea(String ideaId, string email, string userComments);
        InviteeList DeligateIdea(String ideaId, User assignee, string userComments, string email, bool commentType, long commentParentId);
        InviteeList PickIdea(string ideaId, string email, string userComments);
        InviteeList PickIdeaDone(string ideaId, string email, string userComments);
        InviteeList PickIdeaGiveUp(string ideaId, string email, string userComments);
        InviteeList PickIdeaRework(string ideaId, string email, string userComments);
        InviteeList PickIdeaAccept(string ideaId, string email, string userComments);
        InviteeList PickIdeaReopen(string ideaId, string email, string userComments);
        bool WatchIdea(string ideaId, string email, bool isActive);
        InviteeList CommentIdea(string ideaId, string email, bool commentType, long commentParentId, string userComments);
        List<Idea> GetIdeas(string email, string ideaPage, int pageSize, int currentPage, string orderBy, string order);
        List<Comment> GetIdeaComments(string ideaId, string email, int pageSize, int currentPage);
        List<Alert> GetAlerts(string email);
    }
}
