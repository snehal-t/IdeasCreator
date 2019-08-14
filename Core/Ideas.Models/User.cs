using System;
using System.Collections.Generic;

namespace Ideas.Models
{
    public class User : Response
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
    }
}