using System;
using System.Collections.Generic;
using System.Text;
using Ideas.Models;

namespace Ideas.Services.Services.Imp
{
    public class Ideas : IIdeas
    {
        public bool ApproveIdea(string ideaId, User user, string userComments)
        {
            throw new NotImplementedException();
        }

        public bool CreateIdea(Idea idea)
        {
            throw new NotImplementedException();
        }

        public bool DeligateIdea(string ideaId, User user, User assignee, string userComments)
        {
            throw new NotImplementedException();
        }

        public List<Alert> GetAlerts(User user)
        {
            throw new NotImplementedException();
        }

        public List<Idea> GetIdeas(User user, string IdeaStatus)
        {
            throw new NotImplementedException();
        }

        public List<Comment> IdeaComments(string ideaId, User user)
        {
            throw new NotImplementedException();
        }

        public bool PickIdea(string ideaId, User user, string userComments)
        {
            throw new NotImplementedException();
        }

        public bool PickIdeaAccept(string ideaId, User user, string userComments)
        {
            throw new NotImplementedException();
        }

        public bool PickIdeaDone(string ideaId, User user, string userComments)
        {
            throw new NotImplementedException();
        }

        public bool PickIdeaGiveUp(string ideaId, User user, string userComments)
        {
            throw new NotImplementedException();
        }

        public bool PickIdeaRework(string ideaId, User user, string userComments)
        {
            throw new NotImplementedException();
        }

        public bool RejectIdea(string ideaId, User user, string userComments)
        {
            throw new NotImplementedException();
        }

        public bool UpdateIdea(Idea idea)
        {
            throw new NotImplementedException();
        }

        public bool WatchIdea(string ideaId, User user)
        {
            throw new NotImplementedException();
        }

        public bool WithDrawIdea(string ideaId, User user, string userComments)
        {
            throw new NotImplementedException();
        }
    }
}
