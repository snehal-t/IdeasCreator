using System;
using System.Collections.Generic;
using System.Text;

namespace Ideas.Models
{
    public class InviteeList : Response
    {
        public List<User> IdeaModerators { get; set; }
        public User IdeaCreator { get; set; }
        public User IdeaPicker { get; set; }
        public List<User> IdeaWatchers { get; set; }
        public List<User> IdeaCommenters { get; set; }
        public bool IsSent { get; set; }
    }
}
