using System;
using System.Collections.Generic;
using System.Text;

namespace Ideas.Models
{
    public class Idea : User
    {
        public Guid IdeaId { get; set; }
        public string Type { get; set; }
        public string Title { get; set; }
        public string Source { get; set; }
        public string Description { get; set; }
        public string BusinessCase { get; set; }
        public DateTime IdealTime { get; set; }
        public string BusinessJustification { get; set; }
        public string ContactName { get; set; }
        public string ContactEmail { get; set; }
        public string ContactMobileNo { get; set; }
        public string IdeaStatus { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public List<User> Watchers { get; set; }
        public User Moderator { get; set; }
        public User Picker { get; set; }
    }
}
