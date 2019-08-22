using System;
using System.Collections.Generic;
using System.Text;

namespace Ideas.Models
{
    public class Response
    {
        public Response()
        {

        }
        public Response(bool isSuccess, string message)
        {
            IsSuccess = isSuccess;
            Message = message;
        }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }

    public class IdeasResponse : Response
    {
        public IdeasResponse(bool isSuccess, string message, List<Idea> ideas) : base(isSuccess, message)
        {
            base.IsSuccess = isSuccess;
            base.Message = message;
            Ideas = ideas;
        }
        public List<Idea> Ideas { get; set; }
    }

    public class WatchersResponse : Response
    {
        public WatchersResponse(bool isSuccess, string message, List<Watcher> watchers) : base(isSuccess, message)
        {
            base.IsSuccess = isSuccess;
            base.Message = message;
            Watchers = watchers;
        }
        public List<Watcher> Watchers { get; set; }
    }

    public class CommentsResponse : Response
    {
        public CommentsResponse(bool isSuccess, string message, List<Comment> comments) : base(isSuccess, message)
        {
            base.IsSuccess = isSuccess;
            base.Message = message;
            Comments = comments;
        }
        public List<Comment> Comments { get; set; }
    }

    public class AlertsResponse : Response
    {
        public AlertsResponse(bool isSuccess, string message, List<Alert> alerts) : base(isSuccess, message)
        {
            base.IsSuccess = isSuccess;
            base.Message = message;
            Alerts = alerts;
        }
        public List<Alert> Alerts { get; set; }
    }

    public class IdeaResponse : Response
    {
        public IdeaResponse(bool isSuccess, string message, Idea idea) : base(isSuccess, message)
        {
            base.IsSuccess = isSuccess;
            base.Message = message;
            Idea = idea;
        }
        public Idea Idea { get; set; }
    }

    public class SignInResponse : Response
    {
        public SignInResponse(bool isSuccess, string message, string author, string role) : base(isSuccess, message)
        {
            base.IsSuccess = isSuccess;
            base.Message = message;
            Author = author;
            Role = role;
        }
        public string Author { get; set; }
        public string Role { get; set; }
    }

    public class DashboardResponse : Response
    {
        public DashboardResponse(bool isSuccess, string message, Dashboard dashboard) : base(isSuccess, message)
        {
            base.IsSuccess = isSuccess;
            base.Message = message;
            Dashboard = dashboard;
        }
        public Dashboard Dashboard { get; set; }
    }
}
