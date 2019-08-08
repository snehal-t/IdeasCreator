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
        bool WithDrawIdea(string ideaId, User user, string userComments);
        bool ApproveIdea(string ideaId, User user, string userComments);
        bool RejectIdea(String ideaId, User user, string userComments);
        bool DeligateIdea(String ideaId, User user, User assignee, string userComments);
        bool PickIdea(string ideaId, User user, string userComments);
        bool PickIdeaDone(string ideaId, User user, string userComments);
        bool PickIdeaGiveUp(string ideaId, User user, string userComments);
        bool PickIdeaRework(string ideaId, User user, string userComments);
        bool PickIdeaAccept(string ideaId, User user, string userComments);
        bool WatchIdea(string ideaId, User user);
        List<Idea> GetIdeas(User user, string IdeaStatus);
        List<Comment> IdeaComments(string ideaId, User user);
        List<Alert> GetAlerts(User user);
    }
}
