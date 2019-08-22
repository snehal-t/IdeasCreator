using System;
using System.Collections.Generic;
using System.Text;

namespace Ideas.Models
{
    public class NotificationTemplate
    {
        public string IdeaSender { get; set; }
        public User receiver { get; set; }
        public string IdeaTitle { get; set; }
        public string UserComment { get; set; }
        public ReceiverType ReceiverType { get; set; }
        public NotificationType NotificationType { get; set; }
    }
}
