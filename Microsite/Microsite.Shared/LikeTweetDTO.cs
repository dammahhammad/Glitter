using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsite.Shared
{
    public class LikeTweetDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int TweetId { get; set; }
    }
}
