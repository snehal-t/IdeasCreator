using System;
using System.Collections.Generic;
using System.Text;
using Ideas.Models;
namespace Ideas.Data.Repositories.Ideas
{
    public interface IIdeasRepository
    {
        User SignIn(string name, string email);
        InviteeList CreateIdea(Idea idea, string email, string author);
        InviteeList UpdateIdea(Idea idea, string email, string author);
        InviteeList WithdrawIdea(string ideaId, string email, string author, string userComments);
        InviteeList ApproveIdea(string ideaId, string email, string author, string userComments);
        InviteeList RejectIdea(String ideaId, string email, string author, string userComments);
        InviteeList DeligateIdea(String ideaId, User assignee, string userComments, string email, string author);
        InviteeList PickIdea(string ideaId, string email, string author, string userComments);
        InviteeList PickIdeaDone(string ideaId, string email, string author, string userComments);
        InviteeList PickIdeaGiveUp(string ideaId, string email, string author, string userComments);
        InviteeList PickIdeaRework(string ideaId, string email, string author, string userComments);
        InviteeList PickIdeaAccept(string ideaId, string email, string author, string userComments);
        InviteeList PickIdeaReopen(string ideaId, string email, string author, string userComments);
        Response WatchIdea(string ideaId, string email, string author, bool isActive);
        InviteeList CommentIdea(string ideaId, string email, string author, bool commentType, long commentParentId, string userComments);
        List<Idea> GetIdeas(string email, string author, string ideaPage, int pageSize, int currentPage, string orderBy, string order);
        List<Watcher> GetIdeaWatchers(string email, string author, string ideaId);
        List<Comment> GetIdeaComments(string ideaId, string email, string author, int pageSize, int currentPage);
        List<Alert> GetAlerts(string email, string author, int pageSize, int currentPage);
        Idea GetIdeaDetails(string email, string author, string ideaId);
    }
}
