using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Ideas.Models;

namespace Ideas.Services.Services
{
    public interface IIdeas
    {
        SignInResponse SignIn(string name, string email);
        DashboardResponse GetDashboard(string name, string author, string email);
        Response CreateIdea(string name, Idea idea, string email, string author);
        Response UpdateIdea(string name, Idea idea, string email, string author);
        Response WithdrawIdea(string name, string ideaId, string email, string author, string userComments);
        Response ApproveIdea(string name, string ideaId, string email, string author, string userComments);
        Response RejectIdea(string name, String ideaId, string email, string author, string userComments);
        Response DeligateIdea(string name, String ideaId, string assignee, string userComments, string email, string author);
        Response PickIdea(string name, string ideaId, string email, string author, string userComments);
        Response PickIdeaDone(string name, string ideaId, string email, string author, string userComments);
        Response PickIdeaGiveUp(string name, string ideaId, string email, string author, string userComments);
        Response PickIdeaRework(string name, string ideaId, string email, string author, string userComments);
        Response PickIdeaAccept(string name, string ideaId, string email, string author, string userComments);
        Response PickIdeaReopen(string name, string ideaId, string email, string author, string userComments);
        Response WatchIdea(string name, string ideaId, string email, string author, bool isActive);
        Response CommentIdea(string name, string ideaId, string email, string author, bool commentType, long commentParentId, string userComments);
        Response EditComment(string name, string ideaId, string commentId, string email, string author, bool commentType, long commentParentId, string userComments);
        Response DeleteComment(string name, string ideaId, string commentId, string email, string author);
        IdeasResponse GetIdeas(string name, string email, string author, string ideaPage, int pageSize, int currentPage, string orderBy, string order);
        WatchersResponse GetIdeaWatchers(string name, string email, string author, string ideaId);
        CommentsResponse GetIdeaComments(string name, string ideaId, string email, string author, int pageSize, int currentPage);
        AlertsResponse GetAlerts(string name, string email, string author, int pageSize, int currentPage);
        IdeaResponse GetIdeaDetails(string name, string email, string author, string ideaId);
    }
}
