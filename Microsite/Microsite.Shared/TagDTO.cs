using System;
using System.Collections.Generic;
using System.Text;

namespace Microsite.Shared
{
    public class TagDTO
    {
        public int Id { get; set; }
        public int TweetId { get; set; }
        public string TagName { get; set; }
        public int SearchCount { get; set; }
    }
}
