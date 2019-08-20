using System;
using System.Collections.Generic;
using System.Text;
using Ideas.Models;

namespace Ideas.Services.Services
{
    public interface IIdeas
    {
        SignInResponse SignIn(string name, string email);
        Response CreateIdea(Idea idea, string email, string author);
        Response UpdateIdea(Idea idea, string email, string author);
        Response WithdrawIdea(string ideaId, string email, string author, string userComments);
        Response ApproveIdea(string ideaId, string email, string author, string userComments);
        Response RejectIdea(String ideaId, string email, string author, string userComments);
        Response DeligateIdea(String ideaId, string assignee, string userComments, string email, string author);
        Response PickIdea(string ideaId, string email, string author, string userComments);
        Response PickIdeaDone(string ideaId, string email, string author, string userComments);
        Response PickIdeaGiveUp(string ideaId, string email, string author, string userComments);
        Response PickIdeaRework(string ideaId, string email, string author, string userComments);
        Response PickIdeaAccept(string ideaId, string email, string author, string userComments);
        Response PickIdeaReopen(string ideaId, string email, string author, string userComments);
        Response WatchIdea(string ideaId, string email, string author, bool isActive);
        Response CommentIdea(string ideaId, string email, string author, bool commentType, long commentParentId, string userComments);
        Response EditComment(string ideaId, string commentId, string email, string author, bool commentType, long commentParentId, string userComments);
        Response DeleteComment(string ideaId, string commentId, string email, string author);
        IdeasResponse GetIdeas(string email, string author, string ideaPage, int pageSize, int currentPage, string orderBy, string order);
        WatchersResponse GetIdeaWatchers(string email, string author, string ideaId);
        CommentsResponse GetIdeaComments(string ideaId, string email, string author, int pageSize, int currentPage);
        AlertsResponse GetAlerts(string email, string author, int pageSize, int currentPage);
        IdeaResponse GetIdeaDetails(string email, string author, string ideaId);
    }
}
