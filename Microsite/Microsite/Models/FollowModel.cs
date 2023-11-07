using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microsite.Models
{
    public class FollowModel
    {
        public int ID { get; set; }
        public int FollowerId { get; set; }
        public int UserToFollowId { get; set; }
    }
}
