using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microsite.Models
{
    public class NewTweetModel
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; } 
    }
}
