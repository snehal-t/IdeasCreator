using System;
using System.Collections.Generic;
using System.Text;
using Ideas.Models;

namespace Ideas.Services.Services.Imp
{
    public class Ideas : IIdeas
    {
        public bool ApproveIdea(string ideaId, string email, string userComments)
        {
            throw new NotImplementedException();
        }

        public bool CreateIdea(Idea idea)
        {
            throw new NotImplementedException();
        }

        public bool DeligateIdea(string ideaId, User assignee, string userComments)
        {
            throw new NotImplementedException();
        }

        public List<Alert> GetAlerts(string email)
        {
            throw new NotImplementedException();
        }

        public List<Idea> GetIdeas(string email, string IdeaStatus)
        {
            throw new NotImplementedException();
        }

        public List<Comment> IdeaComments(string ideaId, string email)
        {
            throw new NotImplementedException();
        }

        public bool PickIdea(string ideaId, string email, string userComments)
        {
            throw new NotImplementedException();
        }

        public bool PickIdeaAccept(string ideaId, string email, string userComments)
        {
            throw new NotImplementedException();
        }

        public bool PickIdeaDone(string ideaId, string email, string userComments)
        {
            throw new NotImplementedException();
        }

        public bool PickIdeaGiveUp(string ideaId, string email, string userComments)
        {
            throw new NotImplementedException();
        }

        public bool PickIdeaReopen(string ideaId, string email, string userComments)
        {
            throw new NotImplementedException();
        }

        public bool PickIdeaRework(string ideaId, string email, string userComments)
        {
            throw new NotImplementedException();
        }

        public bool RejectIdea(string ideaId, string email, string userComments)
        {
            throw new NotImplementedException();
        }

        public bool UpdateIdea(Idea idea)
        {
            throw new NotImplementedException();
        }

        public bool WatchIdea(string ideaId, string email)
        {
            throw new NotImplementedException();
        }

        public bool WithDrawIdea(string ideaId, string email, string userComments)
        {
            throw new NotImplementedException();
        }
    }
}
