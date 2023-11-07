using System;
using System.Collections.Generic;
using System.Text;

namespace Microsite.Shared
{
    public class FollowModelDTO
    {
        public int ID { get; set; }
        public int FollowerId { get; set; }
        public int UserToFollowId { get; set; }
    }
}
