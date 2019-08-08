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
        public int IdeasTrending { get; set; }
        public Idea MostCommented { get; set; }
    }
}