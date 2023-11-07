using System;
using System.Collections.Generic;
using System.Text;

namespace Microsite.Shared
{
    public class GetAllTweetsDTO
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Image { get; set; }
        public bool IsLiked { get; set; }
    }
}
