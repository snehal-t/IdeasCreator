namespace Ideas.Models
{
    public class Request
    {
        public string Author { get; set; }
        public string IdeaId { get; set; }
        public User Assignee { get; set; }
        public string Comments { get; set; }
        public bool CommentType { get; set; }
        public int CommentParentId { get; set; }
        public string IdeaPage { get; set; }
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public string OrderBy { get; set; }
        public string order { get; set; }
        public bool IsWatching { get; set; }
        public Idea Idea { get; set; }
    }
}
