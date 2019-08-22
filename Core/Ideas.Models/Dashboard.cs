using System;
using System.Collections.Generic;
using System.Text;

namespace Ideas.Models
{
    public class Dashboard
    {
        public int TotalIdeas { get; set; }
        public int IdeasInAction { get; set; }
        public int IdeasPendingAction { get; set; }
        public int IdeasRejected { get; set; }
        public int IdeasAccepted { get; set; }
        public int IdeasTrending { get; set; }
        public Idea MostCommented { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}