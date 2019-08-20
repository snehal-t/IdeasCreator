using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ideas.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Web;
using System.Security.Claims;
using Ideas.Services.Services;
using Ideas.Common.ErrorHandler;
using Microsoft.Graph.Extensions;
using Microsoft.Graph;
using System.Net.Http.Headers;
using Microsoft.IdentityModel.Clients;
using Microsoft.Graph.Extensions;
using System.Net.Http;
using Microsoft.IdentityModel.Clients.ActiveDirectory;

namespace Ideas.API.Controllers
{
    [Authorize]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class IdeasController : ControllerBase
    {
        private readonly IIdeas _iIdeas;
        private readonly string ErrorMessage = "Something went wrong";
        
        public IdeasController(IIdeas iIdeas)
        {
            _iIdeas = iIdeas;
        }

        [Route("api/SignIn")]
        [HttpPost]
        public ActionResult<SignInResponse> SignIn()
        {
            try
            {
                var user = GetCurrentUser();
                return _iIdeas.SignIn(user.Name, user.Email);
            }
            catch (Exception ex)
            {
                LogError(ex);
                return new SignInResponse(false, ex.ToString(), null, null);
            }
        }

        [Route("api/CreateIdea")]
        [HttpPost]
        public ActionResult<Response> CreateIdea([FromBody]Request request)
        {
            try
            {
                var user = GetCurrentUser();
                return _iIdeas.CreateIdea(request.Idea, user.Email, request.Author);
            }
            catch (Exception ex)
            {
                LogError(ex);
                return new Response(false, ex.ToString());
            }
        }

        [Route("api/UpdateIdea")]
        [HttpPost]
        public ActionResult<Response> UpdateIdea(Request request)
        {
            try
            {
                var user = GetCurrentUser();
                return _iIdeas.UpdateIdea(request.Idea, user.Email, request.Author);
            }
            catch (Exception ex)
            {
                LogError(ex);
                return new Response(false, ex.ToString());
            }
        }

        [Route("api/WithdrawIdea")]
        [HttpPost]
        public ActionResult<Response> WithdrawIdea(Request request)
        {
            try
            {
                var user = GetCurrentUser();
                return _iIdeas.WithdrawIdea(request.IdeaId, user.Email, request.Author, request.Comments);
            }
            catch (Exception ex)
            {
                LogError(ex);
                return new Response(false, ex.ToString());
            }
        }

        [Route("api/ApproveIdea")]
        [HttpPost]
        public ActionResult<Response> ApproveIdea(Request request)
        {
            try
            {
                var user = GetCurrentUser();
                return _iIdeas.ApproveIdea(request.IdeaId, user.Email, request.Author, request.Comments);
            }
            catch (Exception ex)
            {
                LogError(ex);
                return new Response(false, ex.ToString());
            }
        }

        [Route("api/RejectIdea")]
        [HttpPost]
        public ActionResult<Response> RejectIdea(Request request)
        {
            try
            {
                var user = GetCurrentUser();
                return _iIdeas.RejectIdea(request.IdeaId, user.Email, request.Author, request.Comments);
            }
            catch (Exception ex)
            {
                LogError(ex);
                return new Response(false, ex.ToString());
            }
        }

        [Route("api/DeligateIdea")]
        [HttpPost]
        public ActionResult<Response> DeligateIdea(Request request)
        {
            try
            {
                var user = GetCurrentUser();
                return _iIdeas.DeligateIdea(request.IdeaId, request.Assignee, request.Comments, user.Email, request.Author);
            }
            catch (Exception ex)
            {
                LogError(ex);
                return new Response(false, ex.ToString());
            }
        }

        [Route("api/PickIdea")]
        [HttpPost]
        public ActionResult<Response> PickIdea(Request request)
        {
            try
            {
                var user = GetCurrentUser();
                return _iIdeas.PickIdea(request.IdeaId, user.Email, request.Author, request.Comments);
            }
            catch (Exception ex)
            {
                LogError(ex);
                return new Response(false, ex.ToString());
            }
        }

        [Route("api/PickIdeaDone")]
        [HttpPost]
        public ActionResult<Response> PickIdeaDone(Request request)
        {
            try
            {
                var user = GetCurrentUser();
                return _iIdeas.PickIdeaDone(request.IdeaId, user.Email, request.Author, request.Comments);
            }
            catch (Exception ex)
            {
                LogError(ex);
                return new Response(false, ex.ToString());
            }
        }

        [Route("api/PickIdeaGiveUp")]
        [HttpPost]
        public ActionResult<Response> PickIdeaGiveUp(Request request)
        {
            try
            {
                var user = GetCurrentUser();
                return _iIdeas.PickIdeaGiveUp(request.IdeaId, user.Email, request.Author, request.Comments);
            }
            catch (Exception ex)
            {
                LogError(ex);
                return new Response(false, ex.ToString());
            }
        }

        [Route("api/PickIdeaRework")]
        [HttpPost]
        public ActionResult<Response> PickIdeaRework(Request request)
        {
            try
            {
                var user = GetCurrentUser();
                return _iIdeas.PickIdeaRework(request.IdeaId, user.Email, request.Author, request.Comments);
            }
            catch (Exception ex)
            {
                LogError(ex);
                return new Response(false, ex.ToString());
            }
        }

        [Route("api/PickIdeaAccept")]
        [HttpPost]
        public ActionResult<Response> PickIdeaAccept(Request request)
        {
            try
            {
                var user = GetCurrentUser();
                return _iIdeas.PickIdeaAccept(request.IdeaId, user.Email, request.Author, request.Comments);
            }
            catch (Exception ex)
            {
                LogError(ex);
                return new Response(false, ex.ToString());
            }
        }

        [Route("api/PickIdeaReopen")]
        [HttpPost]
        public ActionResult<Response> PickIdeaReopen(Request request)
        {
            try
            {
                var user = GetCurrentUser();
                return _iIdeas.PickIdeaReopen(request.IdeaId, user.Email, request.Author, request.Comments);
            }
            catch (Exception ex)
            {
                LogError(ex);
                return new Response(false, ex.ToString());
            }
        }

        [Route("api/WatchIdea")]
        [HttpPost]
        public ActionResult<Response> WatchIdea(Request request)
        {
            try
            {
                var user = GetCurrentUser();
                return _iIdeas.WatchIdea(request.IdeaId, user.Email, request.Author, request.IsWatching);
            }
            catch (Exception ex)
            {
                LogError(ex);
                return new Response(false, ex.ToString());
            }
        }

        [Route("api/CommentIdea")]
        [HttpPost]
        public ActionResult<Response> CommentIdea(Request request)
        {
            try
            {
                var user = GetCurrentUser();
                return _iIdeas.CommentIdea(request.IdeaId, user.Email, request.Author, request.CommentType, request.CommentParentId, request.Comments);
            }
            catch (Exception ex)
            {
                LogError(ex);
                return new Response(false, ex.ToString());
            }
        }

        [Route("api/EditComment")]
        [HttpPost]
        public ActionResult<Response> EditComment(Request request)
        {
            try
            {
                var user = GetCurrentUser();
                return _iIdeas.EditComment(request.IdeaId, request.commentId, user.Email, request.Author, request.CommentType, request.CommentParentId, request.Comments);
            }
            catch (Exception ex)
            {
                LogError(ex);
                return new Response(false, ex.ToString());
            }
        }

        [Route("api/DeleteComment")]
        [HttpPost]
        public ActionResult<Response> DeleteComment(Request request)
        {
            try
            {
                var user = GetCurrentUser();
                return _iIdeas.DeleteComment(request.IdeaId, request.commentId, user.Email, request.Author);
            }
            catch (Exception ex)
            {
                LogError(ex);
                return new Response(false, ex.ToString());
            }
        }

        [Route("api/GetIdeas")]
        [HttpPost]
        public ActionResult<IdeasResponse> GetIdeas(Request request)
        {
            try
            {
                var user = GetCurrentUser();
                return _iIdeas.GetIdeas(user.Email, request.Author,request.IdeaPage, request.PageSize, request.CurrentPage, request.OrderBy, request.order);
            }
            catch (Exception ex)
            {
                LogError(ex);
                return new IdeasResponse(false, ex.ToString(), null);
            }
        }

        [Route("api/GetIdeaWatchers")]
        [HttpPost]
        public ActionResult<WatchersResponse> GetIdeaWatchers(Request request)
        {
            try
            {
                var user = GetCurrentUser();
                return _iIdeas.GetIdeaWatchers(user.Email, request.Author, request.IdeaId);
            }
            catch (Exception ex)
            {
                LogError(ex);
                return new WatchersResponse(false, ex.ToString(), null);
            }
        }

        [Route("api/GetIdeaComments")]
        [HttpPost]
        public ActionResult<CommentsResponse> GetIdeaComments(Request request)
        {
            try
            {
                var user = GetCurrentUser();
                return _iIdeas.GetIdeaComments(request.IdeaId, user.Email, request.Author, request.PageSize, request.CurrentPage);
            }
            catch (Exception ex)
            {
                LogError(ex);
                return new CommentsResponse(false, ex.ToString(), null);
            }
        }

        [Route("api/GetAlerts")]
        [HttpPost]
        public ActionResult<AlertsResponse> GetAlerts(Request request)
        {
            try
            {
                var user = GetCurrentUser();
                return _iIdeas.GetAlerts(user.Email, request.Author, request.PageSize, request.CurrentPage);
            }
            catch (Exception ex)
            {
                LogError(ex);
                return new AlertsResponse(false, ex.ToString(), null);
            }
        }

        [Route("api/GetIdeaDetails")]
        [HttpPost]
        public ActionResult<IdeaResponse> GetIdeaDetails(Request request)
        {
            try
            {
                var user = GetCurrentUser();
                return _iIdeas.GetIdeaDetails(user.Email, request.Author, request.IdeaId);
            }
            catch (Exception ex)
            {
                LogError(ex);
                return new IdeaResponse(false, ex.ToString(), null);
            }
        }

        [Route("api/GetAssignee")]
        [HttpPost]
        public async Task<List<Microsoft.Graph.User>> GetUserList()
        {
            List<Microsoft.Graph.User> userResult = new List<Microsoft.Graph.User>();
            GraphServiceClient graphClient = new GraphServiceClient(new AzureAuthenticationProvider());
            IGraphServiceUsersCollectionPage users = await graphClient.Users.Request().Top(500).GetAsync(); // The hard coded Top(500) is what allows me to pull all the users, the blog post did this on a param passed in
            userResult.AddRange(users);

            while (users.NextPageRequest != null)
            {
                users = await users.NextPageRequest.GetAsync();
                userResult.AddRange(users);
            }
            return userResult;
        }
        
        private Ideas.Models.User GetCurrentUser()
        {
            return new Ideas.Models.User
            {
                Name = HttpContext.User.FindFirst(ClaimTypes.GivenName).Value + " " + HttpContext.User.FindFirst(ClaimTypes.Surname).Value,
                Email = HttpContext.User.Identity.Name
            };
        }


        private void LogError(Exception ex)
        {

        }
    }

    class AzureAuthenticationProvider : IAuthenticationProvider
    {
        private string clientId = "520e43d8-b87b-457d-8bd6-e5fa53494354";
        private string appKey = "E?kX0@Ukh-+ZYjUR19cDPWpr6ig7c[HK";
        private string aadInstance = "https://login.microsoftonline.com/";

        public async Task AuthenticateRequestAsync(HttpRequestMessage request)
        {
            string signedInUserID = "o7nNpKHZ1sU4jao_uXzw2nKlifY7bAY6prUQWetD-kE";
            string tenantID = "21212548-dd86-4f27-a1fa-faf16eedb7c3";

            // get a token for the Graph without triggering any user interaction (from the cache, via multi-resource refresh token, etc)
            ClientCredential creds = new ClientCredential(clientId, appKey);
            // initialize AuthenticationContext with the token cache of the currently signed in user, as kept in the app's database
            AuthenticationContext authenticationContext = new AuthenticationContext(aadInstance + tenantID, false);
            AuthenticationResult authResult = await authenticationContext.AcquireTokenAsync("https://graph.microsoft.com/", creds);

            request.Headers.Add("Authorization", "Bearer " + authResult.AccessToken);
        }
    }
}
