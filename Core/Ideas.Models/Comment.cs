using System;
using System.Collections.Generic;
using System.Text;

namespace Ideas.Models
{
    public class Comment : User
    {
        public string CommentId { get; set; }
        public string IdeaId { get; set; }
        public string ParentCommentId { get; set; }
        public string CommentDescription { get; set; }
        public bool IsPublic { get; set; }
        public DateTimeOffset? CreatedDate { get; set; }
        public DateTimeOffset? UpdatedDate { get; set; }
    }
}
