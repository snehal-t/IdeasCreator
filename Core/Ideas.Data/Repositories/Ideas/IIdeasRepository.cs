using System;
using System.Collections.Generic;
using System.Text;
using Ideas.Models;
namespace Ideas.Data.Repositories.Ideas
{
    public interface IIdeasRepository
    {
        InviteeList CreateIdea(Idea idea);
        InviteeList UpdateIdea(Idea idea);
        InviteeList WithDrawIdea(string ideaId, string email, string userComments);
        InviteeList ApproveIdea(string ideaId, string email, string userComments);
        InviteeList RejectIdea(String ideaId, string email, string userComments);
        InviteeList DeligateIdea(String ideaId, User assignee, string userComments);
        InviteeList PickIdea(string ideaId, string email, string userComments);
        InviteeList PickIdeaDone(string ideaId, string email, string userComments);
        InviteeList PickIdeaGiveUp(string ideaId, string email, string userComments);
        InviteeList PickIdeaRework(string ideaId, string email, string userComments);
        InviteeList PickIdeaAccept(string ideaId, string email, string userComments);
        InviteeList WatchIdea(string ideaId, string email);
        List<Idea> GetIdeas(string email, string IdeaStatus);
        List<Comment> IdeaComments(string ideaId, string email);
        List<Alert> GetAlerts(string email);
    }
}
