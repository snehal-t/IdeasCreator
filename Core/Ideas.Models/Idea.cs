using System;
using System.Collections.Generic;
using System.Text;

namespace Ideas.Models
{
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
