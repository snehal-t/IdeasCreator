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

        private User GetCurrentUser()
        {
            User user = new User();
            user.Name = HttpContext.User.FindFirst(ClaimTypes.GivenName).Value + " " + HttpContext.User.FindFirst(ClaimTypes.Surname).Value;
            user.Email = HttpContext.User.Identity.Name;
            return user;
        }

        private void LogError(Exception ex)
        {

        }
    }
}
