using Microsite.Data;
using Microsite.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsite.BusinessLogic
{
    public class AnalyticsBusinessContext
    {
        readonly TweetDbContext tweetDbContext = new();
        readonly UserDbContext userDbContext = new();
        public AnalyticsDTO Analytic()
        {
            AnalyticsDTO bonus = new()
            {
                MostTrending = tweetDbContext.MostTrending(),
                MostLiked = tweetDbContext.MostLiked(),
                MostTweetsBy = userDbContext.MostTweetsBy(),
                TotalTweetsToday = tweetDbContext.TotalTweetsToday()
            };
            return bonus;
        }
    }
}
