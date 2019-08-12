using System;
using System.Collections.Generic;
using System.Text;

namespace Ideas.Models
{
    public class Alert : User
    {
        public string AlertId { get; set; }
        public string IdeaId { get; set; }
        public string IdeaName { get; set; }
        public string AlertType { get; set; }
        public string AlertDescription { get; set; }
        public DateTime? CreatedDate { get; set; }
        public bool AlertFlag { get; set; }
    }
}
