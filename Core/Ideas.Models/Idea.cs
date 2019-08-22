using System;
using System.Collections.Generic;
using System.Text;

namespace Ideas.Models
{
    public static class Constants
    {
        public static string SendGridAPIKey = "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";
        public static string Site = "https://ideawebapp.azurewebsites.net";
        public static string Email = "support@QIdea.com";
        public static string Name = "QIdea Support";
        public static string CreateIdeaDesc = "New idea has been created by Creator";
        public static string EditIdeaDesc = "Idea has been edited by Creator";
        public static string WithdrawIdeaDesc = "Idea has been withdrawn by the Creator";
        public static string ApproveIdeaDesc = "Idea has been approved by the Moderator";
        public static string RejectIdeaDesc = "Idea has been rejected by the Moderator";
        public static string DeligateIdeaDesc = "Idea has been delegated to you for moderation";
        public static string PickIdeaDesc = "Idea has been picked by Picker";
        public static string PickIdeaDoneDesc = "Idea has been implemented by Picker";
        public static string PickIdeaGiveUpDesc = "Idea has been give up by Picker";
        public static string PickIdeaReworkDesc = "Idea has been asked to rework by Creator";
        public static string PickIdeaAcceptDesc = "Idea has been accepted by Creator";
        public static string PickIdeaReopenDesc = "Idea has been reopened by Moderator";
        public static string CommentIdeaDesc = "New comment has been added for Idea";
    }
    public enum NotificationType
    {
        CreateIdea = 1,
        UpdateIdea = 2,
        WithdrawIdea = 3,
        ApproveIdea = 4,
        RejectIdea = 5,
        DeligateIdea = 6,
        PickIdea = 7,
        PickIdeaDone = 8,
        PickIdeaGiveUp = 9,
        PickIdeaRework = 10,
        PickIdeaAccept = 11,
        PickIdeaReopen = 12,
        CommentIdea = 13,
        EditComment = 14,
        DeleteComment = 15
    }

    public enum ReceiverType
    {
        Approver = 1,
        Creator = 2,
        Picker = 3,
        Watcher = 4,
        Commenter = 5,
    }
    public class Idea : User
    {
        public string IdeaId { get; set; }
        public string Type { get; set; }
        public string IdeaName { get; set; }
        public string Title { get; set; }
        public string Source { get; set; }
        public string Description { get; set; }
        public string BusinessCase { get; set; }
        public string IdealTime { get; set; }
        public string BusinessJustification { get; set; }
        public string ContactName { get; set; }
        public string ContactEmail { get; set; }
        public string ContactMobileNo { get; set; }
        public string IdeaStatus { get; set; }
        public DateTimeOffset? CreatedDate { get; set; }
        public DateTimeOffset? UpdatedDate { get; set; }
        public int WatchCount { get; set; }
        public int CommentCount { get; set; }
        public string ModeratorId { get; set; }
        public string Moderator { get; set; }
        public string ModeratorEmail { get; set; }
        public string DelegatorId { get; set; }
        public string Delegator { get; set; }
        public string DelegatorEmail { get; set; }
        public string PickerId { get; set; }
        public string Picker { get; set; }
        public string PickerEmail { get; set; }
        public bool IsWatching { get; set; }
    }
}
