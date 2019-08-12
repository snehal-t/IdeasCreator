using System;
using System.Collections.Generic;
using System.Text;
using Ideas.Models;

namespace Ideas.Services.Services
{
    public interface IIdeas
    {
        bool CreateIdea(Idea idea);
        bool UpdateIdea(Idea idea);
        bool WithDrawIdea(string ideaId, string email, string userComments);
        bool ApproveIdea(string ideaId, string email, string userComments);
        bool RejectIdea(String ideaId, string email, string userComments);
        bool DeligateIdea(String ideaId, User assignee, string userComments);
        bool PickIdea(string ideaId, string email, string userComments);
        bool PickIdeaDone(string ideaId, string email, string userComments);
        bool PickIdeaGiveUp(string ideaId, string email, string userComments);
        bool PickIdeaRework(string ideaId, string email, string userComments);
        bool PickIdeaAccept(string ideaId, string email, string userComments);
        bool WatchIdea(string ideaId, string email);
        List<Idea> GetIdeas(string email, string IdeaStatus);
        List<Comment> IdeaComments(string ideaId, string email);
        List<Alert> GetAlerts(string email);
        bool PickIdeaReopen(string ideaId, string email, string userComments);
    }
}
